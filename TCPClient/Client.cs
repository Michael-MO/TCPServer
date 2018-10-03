using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCPClient
{
    public class Client
    {
        private int _port = 7;

        public Client()
        {
        }

        public void Start()
        {
            using (TcpClient socket = new TcpClient(IPAddress.Loopback.ToString(), _port))
            {
                NetworkStream ns = socket.GetStream();

                using (StreamReader sr = new StreamReader(ns))
                using (StreamWriter sw = new StreamWriter(ns))
                {
                    string input = Console.ReadLine();
                    sw.WriteLine(input);
                    sw.Flush();

                    string line = sr.ReadLine();
                    Console.WriteLine(line);
                }
            }
        }
    }
}
