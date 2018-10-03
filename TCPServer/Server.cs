using ConversionClass;
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
                string requestLine = sr.ReadLine();

                string[] request = requestLine.Split(' ');

                string command = request[0];
                double number = Convert.ToDouble(request[1]);

                string result = "";

                if (command == "TOGRAM")
                {
                    result = $"{Conversion.ConvertToGrams(number)} g";
                }
                else if (command == "TOOUNCES")
                {
                    result = $"{Conversion.ConvertToOunces(number)} oz";
                }

                Thread.Sleep(1000);
                Console.WriteLine($"Input = {command} {number}");
                Console.WriteLine($"Result = {result}");

                sw.WriteLine(result);
                sw.Flush();
            }
            socket?.Close();
        }
    }
}
