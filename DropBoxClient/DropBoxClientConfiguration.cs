using System;

namespace DropBoxClient
{
    public class ClientConfiguration
    {
        private static ClientConfiguration config = null;
        private string homeDirectory = @"C:\obs";
        private int port;
        private string userName;

        public ClientConfiguration()
        {
                
        }

        public static ClientConfiguration GetCurrentConfiguration(string homeDir)
        {
            if (config == null)
                config =new ClientConfiguration(homeDir);
            return config;
        }

        public static ClientConfiguration GetCurrentConfiguration()
        {
            if (config == null)
                config = new ClientConfiguration();
            return config;
        }

        public static ClientConfiguration GetCurrentConfiguration(string homeDir, int port,string nick)
        {
            if (config == null)
                config = new ClientConfiguration(homeDir, port,nick);
            return config;
        }

        private ClientConfiguration(string homeDir)
        {
            homeDirectory = homeDir;
        }

        public ClientConfiguration(string homeDir, int port,string nick)
        {
            this.homeDirectory = homeDir;
            this.port = port;
            this.userName = nick;
        }

        public string  GetHomeDirectory()
        {
            return homeDirectory;
        }

        public string GetServerAddress()
        {
            return "127.0.0.1"; 
        }

        public string GetCurrentUserName()
        {
            return userName;
        }

        public int  GetInitialRequestPort()
        {
            return 3999;
        }

        public int GetListenPort()
        {
            return port;
        }

        public int GetFileSendPort()
        {
            return 4999;
        }

        public static void SetCurrentConfiguration(string folder, int port,string nick)
        {
            config = new ClientConfiguration(folder, port,nick);
        }
    }
}
