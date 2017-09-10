using System.Collections.Generic;
using System.Threading;

namespace DropBoxClient
{
    public class FromServerListProvider
    {
       private List<string> fromServer = new List<string>();
       private  static FromServerListProvider provider = null;
       Mutex m;

        private FromServerListProvider()
        {
            m = new Mutex(false, "Mutex");
        }

        public static FromServerListProvider Get()
        {
            if (provider == null)
                provider = new FromServerListProvider();
            return provider;
        }

        public void AddFile(string s)
        {
            m.WaitOne();
            fromServer.Add(s);
            m.ReleaseMutex();
        }

        public List<string> GetFiles()
        {
            return this.fromServer;
        }
    }
}
