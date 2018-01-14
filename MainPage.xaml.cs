using Sockets.Plugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Sockets;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        TcpSocketClient client = new TcpSocketClient();
        Random r = new Random();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            client.ConnectAsync(txtAddress.Text, int.Parse(txtPort.Text));
            StartReading();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //txtMes.Text += "-sending-";
            for (int i = 0; i < 5; i++)
            {
                ushort bufSize = (ushort)r.Next(5, 10);
                //txtMes.Text += "size-" + bufSize + "-mes-";
                var buf = new byte[bufSize];
                for (int bf = 0; bf < bufSize; bf++)
                {
                    buf[bf] = (byte)r.Next(0, 254);
                    //txtMes.Text += buf[bf].ToString() + ",";
                }
                await client.WriteStream.WriteAsync(BitConverter.GetBytes(bufSize), 0, 2);
                await client.WriteStream.WriteAsync(buf, 0, bufSize);
                // wait a little before sending the next bit of data
                await Task.Delay(TimeSpan.FromMilliseconds(50));
            }
        }
        
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            client.DisconnectAsync();
        }

        private async void StartReading()
        {
            txtMes.Text += "c";
            var bytesRead = -1;
            ushort messize = 0;
            var msbuf = new byte[2];
            byte[] mesbuf;

            while (bytesRead != 0)
            {
                bytesRead = await client.ReadStream.ReadAsync(msbuf, 0, 2);
                if (bytesRead > 0)
                {
                    //txtMes.Text += "-receiving-";
                    while (bytesRead < 2)
                    {
                        bytesRead += await client.ReadStream.ReadAsync(msbuf, bytesRead, 2 - bytesRead);
                    }
                    messize = BitConverter.ToUInt16(msbuf, 0);
                    mesbuf = new byte[messize];
                    bytesRead = await client.ReadStream.ReadAsync(mesbuf, 0, messize);
                    while (bytesRead < messize)
                    {
                        bytesRead += await client.ReadStream.ReadAsync(msbuf, bytesRead, messize - bytesRead);
                    }
                    //txtMes.Text += "-size-" + messize + "-msg-";
                    for(int x = 0; x < messize; x++)
                    {
                        //txtMes.Text += mesbuf[x] + ",";
                    }
                }
            }
        }
    }
    public class NetworkMessage
    {

    }
    public class NetworkMessageFactory
    {
        private ushort size;
        private ushort byteToRead;

        public NetworkMessageFactory()
        {
            size = 0;
            byteToRead = 2;
        }

    }
}
