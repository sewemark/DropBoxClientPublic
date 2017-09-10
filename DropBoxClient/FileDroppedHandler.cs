using DropBoxClient.Infrastructure;
using System.Threading.Tasks;

namespace DropBoxClient
{
    public interface IFileDroppedHandler
    {
        Task Handle(string filePath);
    }

    public class FileDroppedHandler : IFileDroppedHandler
    {
        TcpCommunicator tcpCommunicator;
        IFileDroppedFilter filter;

        public FileDroppedHandler(TcpCommunicator _tcpCommunicator, IFileDroppedFilter _filter)
        {
            tcpCommunicator = _tcpCommunicator;
            filter = _filter;
        }

        public async Task Handle(string filePath)
        {
            if (filter.Filter(filePath))
            {
                await tcpCommunicator.SendFile(filePath);
            }
        }
    }
}
