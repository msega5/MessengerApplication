using System.Net;
using MessengerApplication.Models;

namespace MessengerApplication.Abstraction
{
    public interface IMessageSourceServer<T>
    {
        Task SendAsync(NetMessage message, T ep);
        NetMessage Receive(ref T ep);
        T CreateEndpoint();
        T CopyEndpoint(IPEndPoint ep);
    }
}