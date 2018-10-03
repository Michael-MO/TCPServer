using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCPServer
{
    public class Server
    {
        private readonly int pORT;

        public Server(int pORT)
        {
            this.pORT = pORT;
        }

        public void Start()
        {
            TcpListener serverListener = new TcpListener(IPAddress.Loopback, pORT);
            serverListener.Start();

            while (true)
            {
                TcpClient socket = serverListener.AcceptTcpClient();

                Task.Run(() =>
                {
                    TcpClient tempSocket = socket;
                    DoClient(tempSocket);
                });
            }
        }

        public void DoClient(TcpClient socket)
        {
            using (StreamReader sr = new StreamReader(socket.GetStream()))
            using (StreamWriter sw = new StreamWriter(socket.GetStream()))
            {
                String str = sr.ReadLine();
                int words = CountWordNum(str);

                Thread.Sleep(5000);
                Console.WriteLine($"Input = {str}");
                Console.WriteLine($"Number of words = {words}");

                sw.WriteLine(str);
                sw.Flush();
            }
        }

        public int CountWordNum(string input)
        {
            if (input != "" || input != null)
            {
                int length = 0;
                int words = 1;

                while (length <= input.Length - 1)
                {
                    if (input[length] == ' ' || input[length] == '\n' || input[length] == '\t')
                    {
                        words++;
                    }

                    length++;
                }

                return words;
            }
            else
            {
                return 0;
            }
        }
    }
}
