using System;

namespace TCPServer
{
    public class Program
    {
        private const int PORT = 7;
        public static void Main(string[] args)
        {
            Server server = new Server(PORT);
            server.Start();

            Console.ReadLine();
        }
    }
}
