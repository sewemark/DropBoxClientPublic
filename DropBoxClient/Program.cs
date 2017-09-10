using DropBoxClient.Infrastructure;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DropBoxClient
{
    class Program
    {
        static void Main(string[] args)
        {
          
            GetInput();
            var currentConfig = ClientConfiguration.GetCurrentConfiguration();
            FromServerListProvider provider = FromServerListProvider.Get();
            TcpCommunicator communicator = new TcpCommunicator(currentConfig, provider);
            IFileDroppedHandler handler = new FileDroppedHandler(communicator,new FileDroppedFilter(provider));
            IDropBoxServerMessageHandler fileListerner = new DropBoxServerMessageHandler(currentConfig, communicator);
            DroppedFileWatcher droppedFileWatcher = new DroppedFileWatcher(currentConfig.GetHomeDirectory(), "*");
            droppedFileWatcher.StartWatching();
            droppedFileWatcher.DirectoryDiff.Subscribe(async x =>await handler.Handle(x.FilePath));
            Task.Factory.StartNew(() => fileListerner.InitAsync());
            Task.Factory.StartNew(() => fileListerner.Start());
            Console.ReadKey();  
        } 

        public static void GetInput()
        { 
            Console.WriteLine("Podaj userName:");
            var nick = Console.ReadLine();
            Console.WriteLine("Podaj folder:");
            var folder = Console.ReadLine();
            if (!Directory.Exists(folder))
            {
                Console.WriteLine("Folder does not exist, creating folder");
                Directory.CreateDirectory(folder);
            }
            Console.WriteLine("Podaj port:");
            var port = int.Parse(Console.ReadLine());
            ClientConfiguration.SetCurrentConfiguration(folder, port, nick);
        }

    }
}