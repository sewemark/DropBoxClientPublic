namespace DropBoxClient.Models
{
    public class InitRequestModel
    {
        public string[] Files { get; set; }
        public string UserName { get; set; }
        public int Port { get; set; }
    }
}
