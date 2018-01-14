using Server.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Instance
    {

        public bool Running { get; private set; }

        TcpListener clientLoginListener = new TcpListener(IPAddress.Any, 10300);

        static void Main(string[] args)
        {
            new Instance();
            short f;
            f.
        }

        public void Start()
        {
            clientLoginListener.Start();
            Socket s = clientLoginListener.AcceptSocket();
            //clientLoginListener.BeginAcceptSocket(new AsyncCallback(LoginListenerCallback), clientLoginListener);
            Run();
        }

        private void Run()
        {
            Running = true;
            while (Running)
            {
                string consoleLine = Console.ReadLine();
                switch (consoleLine.ToLower())
                {
                    case "exit" :
                        Running = false;
                        break;
                    case "reload":
                        break;
                }
            }
            clientLoginListener.Stop();
        }

        public void Stop()
        {

        }

        private void LoginListenerCallback(IAsyncResult ar)
        {


        }

    }
}
