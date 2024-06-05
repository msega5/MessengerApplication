using System.Net.Sockets;
using System.Net;
using MessengerApplication.Models;
using MessengerApplication.Abstraction;

namespace MessengerApplication.ChatApp
{
    public class Client<T>
    {


        public readonly string _name;
        public IMessageSourceClient<T> _messageSource;
        public T remoteEndPoint;
        public Client(IMessageSourceClient<T> messageSourceClient, string name)
        {
            this._name = name;
            _messageSource = messageSourceClient;
            remoteEndPoint = _messageSource.CreateEndpoint();
        }

        public UdpClient udpClientClient = new UdpClient();
        public async Task ClientListener()
        {
            while (true)
            {
                try
                {
                    var messageReceived = _messageSource.Receive(ref remoteEndPoint);
                    Console.WriteLine($"Получено сообщение от {messageReceived.NickNameFrom}:");
                    Console.WriteLine(messageReceived.Text);

                    await Confirm(messageReceived, remoteEndPoint);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при получении сообщения: " + ex.Message);
                }
            }
        }

        public async Task Confirm(NetMessage message, T remoteEndPoint)
        {
            message.Command = Command.Confirmation;
            await _messageSource.SendAsync(message, remoteEndPoint);
        }

        public void Register(T remoteEndPoint)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
            var message = new NetMessage() { NickNameFrom = _name, NickNameTo = null, Text = null };
            _messageSource.SendAsync(message, remoteEndPoint);
            Console.WriteLine("Сообщение");
        }

        public async Task ClientSender()
        {
            Register(remoteEndPoint);

            while (true)
            {
                try
                {
                    Console.Write("Введите имя получателя: ");
                    var nameTo = Console.ReadLine();

                    Console.Write("Введите сообщение: ");
                    var messageText = Console.ReadLine();

                    var message = new NetMessage() { Command = Command.Message, NickNameFrom = _name };

                    await _messageSource.SendAsync(message, remoteEndPoint);

                    Console.Write("Сообщение отправлено.");
                }

                catch (Exception ex)
                {
                    Console.Write("Ошибка при обработке сообщения: " + ex.Message);
                }
            }
        }

        public async Task Start()
        {
            new Thread(async () => await ClientListener()).Start();
            await ClientSender();
        }
    }
}