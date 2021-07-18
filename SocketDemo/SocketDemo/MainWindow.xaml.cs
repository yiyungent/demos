using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SocketDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Dictionary<string, Socket> SocketClientConList = new Dictionary<string, Socket>();

        private void BtnListen_Click(object sender, RoutedEventArgs e)
        {
            Socket socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socketWatch.Bind(new IPEndPoint(IPAddress.Any, 6780));
            socketWatch.Listen(10);
            MessageBox.Show("监听成功");

            Thread th = new Thread(CreateClientCon);
            th.IsBackground = true;
            th.Start(socketWatch);
        }

        private void CreateClientCon(object sw)
        {
            Socket socketWatch = sw as Socket;
            while (true)
            {
                Socket socketClientCon = socketWatch.Accept();
                string clientIPAndPort = socketClientCon.RemoteEndPoint.ToString();
                this.CBoxOfSocketClientCon.Dispatcher.Invoke(() =>
                {
                    this.CBoxOfSocketClientCon.Items.Add(clientIPAndPort);
                });
                this.SocketClientConList.Add(clientIPAndPort, socketClientCon);
                this.TextBlockOfTipMsg.Dispatcher.Invoke(delegate ()
                {
                    this.TextBlockOfTipMsg.Text += "\n\r" + clientIPAndPort + " : 连接成功";
                });

                Thread th = new Thread(ReceiveClientMsg);
                th.IsBackground = true;
                th.Start(socketClientCon);
            }
        }

        private void ReceiveClientMsg(object scc)
        {
            Socket socketClientCon = scc as Socket;
            while (true)
            {
                byte[] buffer = new byte[1024 * 1024 * 2];
                int receiveLength = socketClientCon.Receive(buffer);
                if (receiveLength == 0)
                {
                    this.CBoxOfSocketClientCon.Items.Remove(socketClientCon.RemoteEndPoint.ToString());
                    this.SocketClientConList.Remove(socketClientCon.RemoteEndPoint.ToString());
                    break;
                }
                this.TextBlockOfReceiveMsg.Dispatcher.Invoke(() =>
                {
                    this.TextBlockOfReceiveMsg.Text += "\n\r" + socketClientCon.RemoteEndPoint.ToString() + " : " + Encoding.UTF8.GetString(buffer, 0, receiveLength);
                });
            }
        }

        private void BtnSendMsg_Click(object sender, RoutedEventArgs e)
        {
            string sendMsg = this.TextBoxOfSendMsg.Text;
            byte[] sendBuffer = Encoding.UTF8.GetBytes(sendMsg);
            if (!SocketClientConList.ContainsKey(this.CBoxOfSocketClientCon.Text))
            {
                SocketClientConList[this.CBoxOfSocketClientCon.Text] = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            bool isConnected = SocketClientConList[this.CBoxOfSocketClientCon.Text].Connected;
            if (!isConnected)
            {
                string[] IPAndPort = this.CBoxOfSocketClientCon.Text.Split(':');
                SocketClientConList[this.CBoxOfSocketClientCon.Text].Connect(IPAddress.Parse(IPAndPort[0]), int.Parse(IPAndPort[1]));
            }
            SocketClientConList[this.CBoxOfSocketClientCon.Text].Send(sendBuffer);
        }
    }
}
