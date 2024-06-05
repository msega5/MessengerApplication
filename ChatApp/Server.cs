using MessengerApplication.Models;
using MessengerApplication.Context;
using MessengerApplication.Abstraction;

namespace MessengerApplication.ChatApp
{
    public class Server<T>
    {
        Dictionary<string, T> clients = new Dictionary<string, T>();
        private readonly IMessageSourceServer<T> _messageSource;
        private T ep;
        public Server(IMessageSourceServer<T> messageSource)
        {
            _messageSource = messageSource;
            ep = _messageSource.CreateEndpoint();
        }

        bool work = true;
        public void Stop()
        {
            work = false;
        }

        private async Task Register(NetMessage message)
        {
            Console.WriteLine($"Message register name = {message.EmailFrom}");

            if (clients.TryAdd(message.EmailFrom, _messageSource.CopyEndpoint(message.EndPoint)))
            {
                using (MessengerContext context = new MessengerContext())
                {
                    context.Users.Add(new User() { Email = message.EmailFrom });
                    await context.SaveChangesAsync();
                }
            }
        }

        private async Task RelyMessage(NetMessage message)
        {
            if (clients.TryGetValue(message.EmailTo, out T ep))
            {
                int? id = 0;
                using (var ctx = new MessengerContext())
                {
                    var fromUser = ctx.Users.First(x => x.Email == message.EmailFrom);
                    var toUser = ctx.Users.First(x => x.Email == message.EmailTo);
                    var msg = new Message { UserFrom = fromUser, UserTo = toUser, IsSent = false, Text = null };
                    ctx.Messages.Add(msg);

                    ctx.SaveChanges();

                    id = msg.MessageId;
                }

                message.Id = id;

                await _messageSource.SendAsync(message, ep);

                Console.WriteLine($"Message relied from = {message.EmailFrom} to {message.EmailTo}");
            }
            else
            {
                Console.WriteLine("User not found");
            }
        }
        async Task ConfirmMessageReceived(int? id)
        {
            Console.WriteLine("Message confirmation id=" + id);

            using (var ctx = new MessengerContext())
            {
                var msg = ctx.Messages.FirstOrDefault(x => x.MessageId == id);

                if (msg != null)
                {
                    msg.IsSent = true;
                    await ctx.SaveChangesAsync();
                }
            }
        }

        private async Task ProcessMessage(NetMessage message)
        {
            switch (message.Command)
            {
                case Command.Register: await Register(message); break;
                case Command.Message: await RelyMessage(message); break;
                case Command.Confirmation: await ConfirmMessageReceived(message.Id); break;
            }
        }

        public async Task Start()
        {
            Console.WriteLine("Server is waiting for message");

            while (work)
            {
                try
                {
                    var message = _messageSource.Receive(ref ep);
                    Console.WriteLine(message.ToString());
                    await ProcessMessage(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}