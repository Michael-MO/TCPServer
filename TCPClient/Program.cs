using System;

namespace TCPClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Client client = new Client();
            client.Start();
        }
    }
}
