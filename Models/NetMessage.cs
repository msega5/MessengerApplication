using System.Net;
using System.Text.Json;

namespace MessengerApplication.Models
{
    public class NetMessage
    {
        public int? Id { get; set; }
        public string? Text { get; set; }
        public DateTime DateTime { get; set; }
        public string? EmailFrom { get; set; }
        public string? EmailTo { get; set; }
        public IPEndPoint? EndPoint { get; set; }
        public Command Command { get; set; }
        public string SerializeMessageToJSON() => JsonSerializer.Serialize(this);
        public static NetMessage? DeserializeMessageFromJSON(string message) => JsonSerializer.Deserialize<NetMessage>(message);
        public void PrintGetMessageFrom()
        {
            Console.WriteLine(ToString());
        }

        public override string ToString()
        {
            return $"{DateTime} получено сообщение {Text} от {EmailFrom}";
        }
    }
}
