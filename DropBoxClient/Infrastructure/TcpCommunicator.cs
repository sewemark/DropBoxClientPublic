using DropBoxClient.Models;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace DropBoxClient.Infrastructure
{
    public class TcpCommunicator
    {
        ClientConfiguration configuration;
        FromServerListProvider fromServerProvider;

        public TcpCommunicator(ClientConfiguration _configuration, FromServerListProvider _fromServerProvider)
        {
            configuration = _configuration;
            fromServerProvider = _fromServerProvider;
        }

        public async Task SendFile(string filePath)
        {
            TcpClient client = new TcpClient();
            await client.ConnectAsync(configuration.GetServerAddress(), configuration.GetFileSendPort());
            var data = Serializers.SerializeObject<FileModel>(new FileModel()
            {
                Content = File.ReadAllBytes(filePath),
                FileName = filePath.Substring(filePath.LastIndexOf("\\")),
                UserName = configuration.GetCurrentUserName()
            });
            client.Client.Send(data);
            client.Dispose();
        }

        public async Task SendInitiRequestAsync()
        {
            TcpClient client = new TcpClient();
            await client.ConnectAsync(IPAddress.Parse(configuration.GetServerAddress()), configuration.GetInitialRequestPort());
            var data = GetInitialReqData();
            client.Client.Send(data, SocketFlags.None);
            client.Dispose();
        }

        public static byte[] GetNetworkDate(TcpClient client)
        {
            using (NetworkStream stream = client.GetStream())
            {
                byte[] data = new byte[1024];

                using (MemoryStream ms = new MemoryStream())
                {
                    int numOfBytes = 0;
                    while ((numOfBytes = stream.Read(data, 0, data.Length)) > 0)
                    {
                        ms.Write(data, 0, numOfBytes);
                    }
                    return ms.ToArray();
                }
            }
        }
        public void SaveFile(TcpClient sender)
        {
            var data = GetNetworkDate(sender);
            var fileModel = Serializers.DeserializeObject<FileModel>(data);
            fromServerProvider.AddFile(fileModel.FileName);
            File.WriteAllBytes(configuration.GetHomeDirectory() + "\\" + fileModel.FileName, fileModel.Content);
        }

        private byte[] GetInitialReqData()
        {
            var files = Directory.GetFiles(configuration.GetHomeDirectory());
            InitRequestModel model = new InitRequestModel() { Port = configuration.GetListenPort(), Files = files, UserName = configuration.GetCurrentUserName() };
            byte[] data = Serializers.SerializeObject<InitRequestModel>(model);
            return data;
        }
    }
}
