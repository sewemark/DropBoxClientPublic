using DropBoxClient.Infrastructure;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace DropBoxClient
{
    public interface IDropBoxServerMessageHandler
    {
        void Start();
        Task InitAsync();
    }

    public class DropBoxServerMessageHandler : IDropBoxServerMessageHandler
    {
        ClientConfiguration config;
        TcpCommunicator communicator;
        public DropBoxServerMessageHandler(ClientConfiguration _config, TcpCommunicator _communicator)
        {
            config = _config;
            communicator = _communicator;
        }

        public async Task InitAsync()
        {
            await Task.Factory.StartNew(async () =>
            {
                await communicator.SendInitiRequestAsync();
            }).ContinueWith((taks)=>
             {
                 TcpListener tcpListenre = new TcpListener(IPAddress.Any, config.GetListenPort());
                 tcpListenre.Start();
                 while (true)
                 {
                     var sender = tcpListenre.AcceptTcpClientAsync().Result;
                     communicator.SaveFile(sender);
                 }
             });

        }
       
        public void Start()
        {
            TcpListener tcpListenre = new TcpListener(IPAddress.Any, config.GetListenPort() + 1);
            tcpListenre.Start();
            while (true)
            {
                var sender = tcpListenre.AcceptTcpClientAsync().Result;
                communicator.SaveFile(sender);
            }
        }
    }
}
