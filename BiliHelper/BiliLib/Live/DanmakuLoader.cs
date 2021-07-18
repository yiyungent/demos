using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using BiliLib.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BiliLib.Live
{
    public class DanmakuLoader
    {
        #region Fields
        private string[] _defaulthosts = new string[] { "livecmt-2.bilibili.com", "livecmt-1.bilibili.com" };
        private string _chatHost = "chat.bilibili.com";
        private int _chatPort = 2243; // TCP协议默认端口疑似修改到 2243
        private TcpClient _client;
        private NetworkStream _netStream;
        private string _CIDInfoUrl = "https://api.live.bilibili.com/room/v1/Danmu/getConf?room_id=";
        private bool _connected = false;
        private bool _debuglog = true;
        private short _protocolversion = 1;
        private static int _lastroomid;
        private static string _lastserver;
        private static HttpClient _httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(5) };
        //        private object shit_lock=new object();//ReceiveMessageLoop 似乎好像大概會同時運行兩個的bug, 但是不修了, 鎖上算了 
        #endregion

        #region Properties
        public int RoomId { get { return _lastroomid; } }
        public bool IsConnected { get { return _connected; } }
        public Exception Error { get; set; }
        #endregion

        #region Events
        public event ReceivedDanmakuEvt ReceivedDanmaku;
        public event DisconnectEvt Disconnected;
        public event ReceivedRoomCountEvt ReceivedRoomCount;
        public event LogMessageEvt LogMessage;
        #endregion

        #region Methods
        public bool Connect(int roomId)
        {
            try
            {
                if (this._connected) throw new InvalidOperationException();
                int channelId = roomId;
                //
                //                var request = WebRequest.Create(RoomInfoUrl + roomId + ".json");
                //                var response = request.GetResponse();
                //
                //                int channelId;
                //                using (var stream = response.GetResponseStream())
                //                using (var sr = new StreamReader(stream))
                //                {
                //                    var json = await sr.ReadToEndAsync();
                //                    Debug.WriteLine(json);
                //                    dynamic jo = JObject.Parse(json);
                //                    channelId = (int) jo.list[0].cid;
                //                }

                if (channelId != _lastroomid)
                {
                    try
                    {
                        var req = _httpClient.GetStringAsync(_CIDInfoUrl + channelId);
                        var roomobj = JObject.Parse(req.Result);

                        _chatHost = roomobj["data"]["host"] + "";

                        _chatPort = roomobj["data"]["port"].Value<int>();
                        if (string.IsNullOrEmpty(_chatHost))
                        {
                            throw new Exception();
                        }

                    }
                    catch (WebException ex)
                    {
                        _chatHost = _defaulthosts[new Random().Next(_defaulthosts.Length)];

                        HttpWebResponse errorResponse = ex.Response as HttpWebResponse;
                        if (errorResponse.StatusCode == HttpStatusCode.NotFound)
                        {
                            // 直播间不存在（HTTP 404）
                            string msg = "该直播间疑似不存在，弹幕姬只支持使用原房间号连接";
                            LogMessage?.Invoke(this, new LogMessageArgs() { message = msg });
                        }
                        else
                        {
                            // B站服务器响应错误
                            string msg = "B站服务器响应弹幕服务器地址出错，尝试使用常见地址连接";
                            LogMessage?.Invoke(this, new LogMessageArgs() { message = msg });
                        }
                    }
                    catch (Exception)
                    {
                        // 其他错误（XML解析错误？）
                        _chatHost = _defaulthosts[new Random().Next(_defaulthosts.Length)];
                        string msg = "获取弹幕服务器地址时出现未知错误，尝试使用常见地址连接";
                        LogMessage?.Invoke(this, new LogMessageArgs() { message = msg });
                    }


                }
                else
                {
                    _chatHost = _lastserver;
                }
                _client = new TcpClient();

                _client.Connect(_chatHost, _chatPort);

                _netStream = _client.GetStream();


                if (SendJoinChannel(channelId))
                {
                    _connected = true;
                    this.HeartbeatLoopAsync();
                    this.ReceiveMessageLoopAsync();
                    _lastserver = _chatHost;
                    _lastroomid = roomId;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                this.Error = ex;
                return false;
            }
        }

        public async Task<bool> ConnectAsync(int roomId)
        {
            try
            {
                if (this._connected) throw new InvalidOperationException();
                int channelId = roomId;
                //
                //                var request = WebRequest.Create(RoomInfoUrl + roomId + ".json");
                //                var response = request.GetResponse();
                //
                //                int channelId;
                //                using (var stream = response.GetResponseStream())
                //                using (var sr = new StreamReader(stream))
                //                {
                //                    var json = await sr.ReadToEndAsync();
                //                    Debug.WriteLine(json);
                //                    dynamic jo = JObject.Parse(json);
                //                    channelId = (int) jo.list[0].cid;
                //                }

                if (channelId != _lastroomid)
                {
                    try
                    {
                        var req = await _httpClient.GetStringAsync(_CIDInfoUrl + channelId);
                        var roomobj = JObject.Parse(req);

                        _chatHost = roomobj["data"]["host"] + "";

                        _chatPort = roomobj["data"]["port"].Value<int>();
                        if (string.IsNullOrEmpty(_chatHost))
                        {
                            throw new Exception();
                        }

                    }
                    catch (WebException ex)
                    {
                        _chatHost = _defaulthosts[new Random().Next(_defaulthosts.Length)];

                        HttpWebResponse errorResponse = ex.Response as HttpWebResponse;
                        if (errorResponse.StatusCode == HttpStatusCode.NotFound)
                        {
                            // 直播间不存在（HTTP 404）
                            string msg = "该直播间疑似不存在，弹幕姬只支持使用原房间号连接";
                            LogMessage?.Invoke(this, new LogMessageArgs() { message = msg });
                        }
                        else
                        {
                            // B站服务器响应错误
                            string msg = "B站服务器响应弹幕服务器地址出错，尝试使用常见地址连接";
                            LogMessage?.Invoke(this, new LogMessageArgs() { message = msg });
                        }
                    }
                    catch (Exception)
                    {
                        // 其他错误（XML解析错误？）
                        _chatHost = _defaulthosts[new Random().Next(_defaulthosts.Length)];
                        string msg = "获取弹幕服务器地址时出现未知错误，尝试使用常见地址连接";
                        LogMessage?.Invoke(this, new LogMessageArgs() { message = msg });
                    }


                }
                else
                {
                    _chatHost = _lastserver;
                }
                _client = new TcpClient();

                await _client.ConnectAsync(_chatHost, _chatPort);

                _netStream = _client.GetStream();


                if (SendJoinChannel(channelId))
                {
                    _connected = true;
                    this.HeartbeatLoopAsync();
                    this.ReceiveMessageLoopAsync();
                    _lastserver = _chatHost;
                    _lastroomid = roomId;
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                this.Error = ex;
                return false;
            }
        }

        private async void ReceiveMessageLoopAsync()
        {
            await Task.Run(() =>
            {
                //            lock (shit_lock)
                //            //ReceiveMessageLoop 似乎好像大概會同時運行兩個的bug, 但是不修了, 鎖上算了
                //            {
                try
                {
                    var stableBuffer = new byte[_client.ReceiveBufferSize];

                    while (this._connected)
                    {

                        _netStream.ReadB(stableBuffer, 0, 4);
                        var packetlength = BitConverter.ToInt32(stableBuffer, 0);
                        packetlength = IPAddress.NetworkToHostOrder(packetlength);

                        if (packetlength < 16)
                        {
                            throw new NotSupportedException("协议失败: (L:" + packetlength + ")");
                        }

                        _netStream.ReadB(stableBuffer, 0, 2);//magic
                        _netStream.ReadB(stableBuffer, 0, 2);//protocol_version 

                        _netStream.ReadB(stableBuffer, 0, 4);
                        var typeId = BitConverter.ToInt32(stableBuffer, 0);
                        typeId = IPAddress.NetworkToHostOrder(typeId);

                        //Console.WriteLine(typeId);
                        _netStream.ReadB(stableBuffer, 0, 4);//magic, params?
                        var playloadlength = packetlength - 16;
                        if (playloadlength == 0)
                        {
                            continue;//没有内容了

                        }
                        typeId = typeId - 1;//和反编译的代码对应 
                        var buffer = new byte[playloadlength];
                        _netStream.ReadB(buffer, 0, playloadlength);
                        switch (typeId)
                        {
                            case 0:
                            case 1:
                            case 2:
                                {


                                    var viewer = BitConverter.ToUInt32(buffer.Take(4).Reverse().ToArray(), 0); //观众人数
                                                                                                               //Console.WriteLine(viewer);
                                    if (ReceivedRoomCount != null)
                                    {
                                        ReceivedRoomCount(this, new ReceivedRoomCountArgs() { UserCount = viewer });
                                    }
                                    break;
                                }
                            case 3:
                            case 4://playerCommand
                                {

                                    var json = Encoding.UTF8.GetString(buffer, 0, playloadlength);
                                    if (_debuglog)
                                    {
                                        //Console.WriteLine(json);

                                    }
                                    try
                                    {
                                        DanmakuModel dama = new DanmakuModel(json, 2);
                                        if (ReceivedDanmaku != null)
                                        {
                                            ReceivedDanmaku(this, new ReceivedDanmakuArgs() { Danmaku = dama });
                                        }

                                    }
                                    catch (Exception)
                                    {
                                        // ignored
                                    }

                                    break;
                                }
                            case 5://newScrollMessage
                                {

                                    break;
                                }
                            case 7:
                                {

                                    break;
                                }
                            case 16:
                                {
                                    break;
                                }
                            default:
                                {

                                    break;
                                }
                                //                     
                        }
                    }
                }
                catch (NotSupportedException ex)
                {
                    this.Error = ex;
                    _disconnect();
                }
                catch (Exception ex)
                {
                    this.Error = ex;
                    _disconnect();

                }
                //            }
            });
        }

        private async void HeartbeatLoopAsync()
        {

            try
            {
                while (this._connected)
                {
                    this.SendHeartbeat();
                    await Task.Delay(30000);
                }
            }
            catch (Exception ex)
            {
                this.Error = ex;
                _disconnect();

            }
        }

        public void Disconnect()
        {

            _connected = false;
            try
            {
                _client.Close();
            }
            catch (Exception)
            {

            }


            _netStream = null;
        }

        private void _disconnect()
        {
            if (_connected)
            {
                Debug.WriteLine("Disconnected");

                _connected = false;

                _client.Close();

                _netStream = null;
                if (Disconnected != null)
                {
                    Disconnected(this, new DisconnectEvtArgs() { Error = Error });
                }
            }

        }

        private void SendHeartbeat()
        {
            SendSocketData(2);
            Debug.WriteLine("Message Sent: Heartbeat");
        }

        private void SendSocketData(int action, string body = "")
        {
            SendSocketData(0, 16, _protocolversion, action, 1, body);
        }

        private void SendSocketData(int packetlength, short magic, short ver, int action, int param = 1, string body = "")
        {
            var playload = Encoding.UTF8.GetBytes(body);
            if (packetlength == 0)
            {
                packetlength = playload.Length + 16;
            }
            var buffer = new byte[packetlength];
            using (var ms = new MemoryStream(buffer))
            {


                var b = BitConverter.GetBytes(buffer.Length).ToBE();

                ms.Write(b, 0, 4);
                b = BitConverter.GetBytes(magic).ToBE();
                ms.Write(b, 0, 2);
                b = BitConverter.GetBytes(ver).ToBE();
                ms.Write(b, 0, 2);
                b = BitConverter.GetBytes(action).ToBE();
                ms.Write(b, 0, 4);
                b = BitConverter.GetBytes(param).ToBE();
                ms.Write(b, 0, 4);
                if (playload.Length > 0)
                {
                    ms.Write(playload, 0, playload.Length);
                }
                _netStream.WriteAsync(buffer, 0, buffer.Length);
                _netStream.FlushAsync();
            }
        }

        private bool SendJoinChannel(int channelId)
        {

            Random r = new Random();
            var tmpuid = (long)(1e14 + 2e14 * r.NextDouble());
            var packetModel = new { roomid = channelId, uid = tmpuid };
            var playload = JsonConvert.SerializeObject(packetModel);
            SendSocketData(7, playload);
            return true;
        }
        #endregion

        #region Ctor
        public DanmakuLoader()
        { }
        #endregion

    }

    public delegate void LogMessageEvt(object sender, LogMessageArgs e);
    public class LogMessageArgs
    {
        public string message = string.Empty;
    }
}