using System.Net;
using System.Net.Sockets;
using System.Text;
using MessengerApplication.Abstraction;
using MessengerApplication.Models;

namespace MessengerApplication.ChatApp
{
    public class UdpMessageSourceServer : IMessageSourceServer<IPEndPoint>
    {
        private readonly UdpClient _udpClient;

        public UdpMessageSourceServer()
        {
            _udpClient = new UdpClient(12345);
        }

        public IPEndPoint CopyEndpoint(IPEndPoint ep)
        {
            return new IPEndPoint(ep.Address, ep.Port);
        }

        public IPEndPoint CreateEndpoint()
        {
            return new IPEndPoint(IPAddress.Any, 0);
        }

        public NetMessage Receive(ref IPEndPoint ep)
        {
            byte[] data = _udpClient.Receive(ref ep);
            string str = Encoding.UTF8.GetString(data);
            return NetMessage.DeserializeMessageFromJSON(str) ?? new NetMessage();
        }

        public async Task SendAsync(NetMessage message, IPEndPoint ep)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message.SerializeMessageToJSON());
            await _udpClient.SendAsync(buffer, buffer.Length, ep);
        }
    }
}
