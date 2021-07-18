using BiliLib;
using BiliLib.Common;
using BiliLib.Live;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        private static IList<int> _allRoomIdList = new List<int>();
        private static int _receive_DanmakuCount;
        private static int _try_ReconnectCount;
        private static string _cookie;
        private static BiliClient _biliClient;

        private static string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data/liveRooms.txt");

        /// <summary>
        /// 连接状态的房间数（未断开）
        /// </summary>
        private static int _connectedRoomCount { get { return _liveRoomMonitorDic.Where(m => m.Value.IsConnected).Count(); } }
        private static Dictionary<LiveRoomModel, DanmakuLoader> _liveRoomMonitorDic = new Dictionary<LiveRoomModel, DanmakuLoader>();

        private static IList<int> _connectingRoomIdList = new List<int>();

        static void Main(string[] args)
        {
            Console.WriteLine("请将 B站直播 cookie 保存到 程序目录/Data/cookie.txt");
            string cookieFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data/cookie.txt");
            _cookie = File.ReadAllText(cookieFilePath).Trim();
            _biliClient = new BiliClient(new CookieModel(_cookie));

            InitRoomIds();
            StartListen();


            while (true)
            {
                var key = Console.ReadKey().Key;
                if (key == ConsoleKey.E)
                {
                    return;
                }
            }
        }

        static void InitRoomIds()
        {
            List<string> roomIds = File.ReadAllText(_filePath).Split(new string[] { "#" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (var item in roomIds)
            {
                _allRoomIdList.Add(Convert.ToInt32(item.Trim()));
            }
        }

        static void StartListen()
        {
            new Thread(() =>
            {
                foreach (var roomId in _allRoomIdList)
                {
                    DanmakuLoader loader = new DanmakuLoader();
                    InitLoader(loader, roomId);
                    _liveRoomMonitorDic.Add(new LiveRoomModel(roomId), loader);
                }
            })
            { IsBackground = true }.Start();
        }

        static void InitLoader(DanmakuLoader loader, int roomId)
        {
            try
            {
                bool isConnected = loader.Connect(roomId);
                if (isConnected)
                {
                    Console.WriteLine($"直播间 {roomId} 连接成功 | 当前共监控 {_connectedRoomCount} 个房间 | {ThreadInfo()}");
                }
                else
                {
                    Console.WriteLine($"直播间 {roomId} 连接失败 |当前共监控 {_connectedRoomCount} 个房间 | {ThreadInfo()}");
                }

                loader.ReceivedDanmaku += Loader_ReceivedDanmaku;
                loader.Disconnected += Loader_Disconnected;
            }
            catch { }
        }

        private static void Loader_Disconnected(object sender, DisconnectEvtArgs e)
        {
            try
            {
                _try_ReconnectCount++;
                //_connectedRoomCount--;

                int roomId = ((DanmakuLoader)sender).RoomId;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{ThreadInfo()}: 房间 {roomId} 断开, 当前共监控 {_connectedRoomCount} 个房间, 3秒后将重新连接");
                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception ex)
            {
            }
        }

        private static void Loader_ReceivedDanmaku(object sender, ReceivedDanmakuArgs e)
        {
            _receive_DanmakuCount++;
            //FlushConsole();
            int roomId = ((DanmakuLoader)sender).RoomId;

            var jToken = e.Danmaku.RawDataJToken;
            int msg_type = Convert.ToInt32(jToken["msg_type"]);
            if (msg_type == 2 || jToken["msg_common"].ToString().Contains("抽奖"))
            {
                int real_roomid = Convert.ToInt32(jToken["real_roomid"].ToString());

                Console.WriteLine($"---{DateTime.Now.ToString()}----{jToken["msg_common"].ToString()}-----");

                _biliClient.LotteryCheck(real_roomid);
            }
        }

        private static void Message(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.Black;
        }

        private static void warning(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.Black;
        }

        private static void FlushConsole()
        {
            //Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"----当前房间数: {_connectedRoomCount}/{_allRoomIdList.Count}---");
            //warning($"收录弹幕消息: {receive_DanmakuCount_PerSec}条/秒");
            warning($"收录总弹幕消息数: {_receive_DanmakuCount}条");
            warning($"尝试重新连接: {_try_ReconnectCount}次");
            Console.ForegroundColor = ConsoleColor.Black;
        }

        private static string ThreadInfo()
        {
            var threads = Process.GetCurrentProcess().Threads;
            int runningThCount = 0;
            int totalThCount = threads.Count;
            foreach (ProcessThread item in threads)
            {
                if (item.ThreadState == System.Diagnostics.ThreadState.Running)
                {
                    runningThCount++;
                }
            }
            string rtn = $"当前线程[{ Thread.CurrentThread.Name}] 线程数: {runningThCount}/{totalThCount}";

            return rtn;
        }

        private class TaskConnect
        {
            public int RoomId { get; set; }

            public Task<bool> ConnTask { get; set; }
        }
    }
}
