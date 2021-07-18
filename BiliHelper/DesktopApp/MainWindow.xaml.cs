using BiliLib.Live;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            InitRoomIds();
        }

        private static IList<int> all_roomIds = new List<int>();
        private static int receive_DanmakuCount;
        private static int try_ReconnectCount;
        private static int receive_DanmakuCount_PerSec;
        private static DateTime before_ReceiveDm_Time;

        /// <summary>
        /// 连接状态的房间数（未断开）
        /// </summary>
        private static int connectedRoomCount;
        private static Dictionary<LiveRoomModel, DanmakuLoader> liveRoomMonitorDic = new Dictionary<LiveRoomModel, DanmakuLoader>();

        private static string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data/liveRooms.txt");

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (this.BtnStart.Content == "开始")
            {
                new Thread(() =>
                {
                    StartListen();
                })
                { IsBackground = true }.Start();
                this.BtnStart.Content = "暂停";
            }
            else
            {
                foreach (var item in liveRoomMonitorDic)
                {
                    item.Value.Disconnect();
                }
                this.BtnStart.Content = "开始";
            }
        }

        void InitRoomIds()
        {
            List<string> roomIds = System.IO.File.ReadAllText(filePath).Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var item in roomIds)
            {
                all_roomIds.Add(Convert.ToInt32(item.Trim()));
            }
            connectedRoomCount = all_roomIds.Count;
        }

        void StartListen()
        {
            foreach (var roomId in all_roomIds)
            {
                DanmakuLoader loader = new DanmakuLoader();
                InitLoader(loader, roomId);
                liveRoomMonitorDic.Add(new LiveRoomModel(roomId), loader);
            }
        }

        void InitLoader(DanmakuLoader loader, int roomId)
        {
            new Thread(async () =>
            {
                await loader.ConnectAsync(roomId);
            })
            { IsBackground = true }.Start();
            loader.ReceivedDanmaku += Loader_ReceivedDanmaku;
            loader.Disconnected += Loader_Disconnected;
        }

        private void Loader_Disconnected(object sender, DisconnectEvtArgs e)
        {
            connectedRoomCount--;
            try
            {
                if (this.BtnStart.Content == "暂停")
                {
                    try_ReconnectCount++;
                    int roomId = ((DanmakuLoader)sender).RoomId;
                    new Thread(async () =>
                    {
                        await ((DanmakuLoader)sender).ConnectAsync(roomId);
                        connectedRoomCount++;
                    })
                    { IsBackground = true }.Start();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Loader_ReceivedDanmaku(object sender, ReceivedDanmakuArgs e)
        {
            receive_DanmakuCount++;
            FlushInfo();
            int roomId = ((DanmakuLoader)sender).RoomId;
        }

        private void FlushInfo()
        {
            this.LbRoomCount.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.LbRoomCount.Content = $"----当前房间数: {connectedRoomCount} / {all_roomIds.Count}---";
            }));
            this.LbTryReconCount.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.LbTryReconCount.Content = $"尝试重新连接: {try_ReconnectCount}次";
            }));


            //warning($"收录弹幕消息: {receive_DanmakuCount_PerSec}条/秒");
            //$"收录总弹幕消息数: {receive_DanmakuCount}条";

        }
    }
}
