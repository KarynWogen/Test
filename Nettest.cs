using Sockets.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Nettest
    {
        int listenerPort;
        TcpSocketListener listener;
        static void Main(string[] args)
        {
            new Nettest().Run();
        }
        public Nettest()
        {
            listenerPort = 10300;
            listener = new TcpSocketListener();
        }
        public void Run()
        {
            var port = listenerPort;
            listener.ConnectionReceived += async (sender, args) =>
            {
                var client = args.SocketClient;

                var bytesRead = -1;
                ushort messize = 0;
                var msbuf = new byte[2];
                byte[] mesbuf;
                try
                {

                    while (bytesRead != 0)
                    {
                        bytesRead = await args.SocketClient.ReadStream.ReadAsync(msbuf, 0, 2);
                        if (bytesRead > 0)
                        {
                            while (bytesRead < 2)
                            {
                                bytesRead += await args.SocketClient.ReadStream.ReadAsync(msbuf, bytesRead, 2 - bytesRead);
                            }
                            messize = BitConverter.ToUInt16(msbuf, 0);
                            mesbuf = new byte[messize];
                            bytesRead = await args.SocketClient.ReadStream.ReadAsync(mesbuf, 0, messize);
                            while (bytesRead < messize)
                            {
                                bytesRead += await args.SocketClient.ReadStream.ReadAsync(msbuf, bytesRead, messize - bytesRead);
                            }
                            Console.Write("size-" + messize + "-mes-");
                            for (int x = 0; x < messize; x++)
                            {
                                Console.Write(mesbuf[x] + ",");
                            }
                            Console.WriteLine();
                            await client.WriteStream.WriteAsync(msbuf, 0, 2);
                            await client.WriteStream.WriteAsync(mesbuf, 0, messize);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Data.ToString());
                }
                args.SocketClient.Dispose();

            };
            listener.StartListeningAsync(port);
        }
    }
}
