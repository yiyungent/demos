using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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
using WebServer;

namespace wpfUseCefsharpDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CefSharp.Wpf.ChromiumWebBrowser Browser { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Browser = new CefSharp.Wpf.ChromiumWebBrowser();
            //browser.Address = @"http://147zxcv.xiaopangkj.space/player.html";

            //this.WebBrowser.WebBrowser.Do
            // 注意，第二个路径不要加 "/"
            // 错误：@"/Res/index.html"

            IPAddress iPAddress = IPAddress.Any;
            IPEndPoint iPEndPoint = new IPEndPoint(iPAddress, 5893);
            MyWebServer webServer = new MyWebServer(iPEndPoint);
            webServer.Start();
            this.Browser.Address = @"http://127.0.0.1:5893/player.html";

            this.Content = this.Browser;
        }
    }
}
