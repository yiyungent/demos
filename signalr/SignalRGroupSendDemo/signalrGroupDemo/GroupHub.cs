using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using signalrGroupDemo.Models;

namespace signalrGroupDemo
{
    [HubName("groupHub")]
    public class GroupHub : Hub
    {
        public static DbContext db = new DbContext();

        /// <summary>
        /// 重写Hub连接事件
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            // 查询用户
            User user = db.Users.Where(w => w.UserName == Context.ConnectionId).FirstOrDefault();
            // 判断用户是否存在
            if (user == null)
            {
                // 不存在则添加新用户
                user = new User()
                {
                    UserName = Context.ConnectionId
                };
                db.Users.Add(user);
            }
            // 房间名列表
            List<string> roomNames = db.Rooms.Select(p => p.RoomName).ToList();
            // 发送房间名列表
            Clients.Client(Context.ConnectionId).getRoomList(JsonConvert.SerializeObject(roomNames));
            return base.OnConnected();
        }

        /// <summary>
        /// 更新所有用户的房间列表
        /// </summary>
        private void GetRooms()
        {
            string roomNames = JsonConvert.SerializeObject(db.Rooms.Select<Room, string>(p => p.RoomName).ToList());
            Clients.All.getRoomList(roomNames);
        }

        /// <summary>
        /// 重写Hub链接断开事件
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            User user = db.Users.Where<User>(u => u.UserName == Context.ConnectionId).FirstOrDefault<User>();
            // 判断用户是否存在，存在则删除
            if (user != null)
            {
                // 删除用户
                db.Users.Remove(user);
            }
            return base.OnDisconnected(stopCalled);
        }

        /// <summary>
        /// 加入房间
        /// </summary>
        /// <param name="roomName"></param>
        public void AddRoom(string roomName)
        {
            // 查询房间
            Room room = db.Rooms.Find(r => r.RoomName == roomName);

            if (room != null)
            {
                // 存在此房间则加入
                // 查找房间中是否存在当前用户
                User isUser = room.Users.Where(u => u.UserName == Context.ConnectionId).FirstOrDefault();
                if (isUser == null)
                {
                    // 不存在则加入
                    User user = db.Users.Find(u => u.UserName == Context.ConnectionId);
                    user.Rooms.Add(room);
                    room.Users.Add(user);
                    Groups.Add(Context.ConnectionId, roomName);
                    // 发送成功加入房间的消息
                    Clients.Client(Context.ConnectionId).addRoom(roomName);
                }
                else
                {
                    // 存在说明已在房间内
                    // 发送"勿重复加入房间"消息
                    Clients.Client(Context.ConnectionId).showMessage("请勿重复加入房间");
                }
            }
            else
            {
                // 不存在此房间发送提示消息
                Clients.Client(Context.ConnectionId).showMessage("不存在此房间，加入失败") ;
            }
        }

        /// <summary>
        /// 创建房间
        /// </summary>
        /// <param name="roomName"></param>
        public void CreateRoom(string roomName)
        {
            Room room = db.Rooms.Find(r => r.RoomName == roomName);
            if (room == null)
            {
                // 不存在此房间名则创建
                room = new Room() { RoomName = roomName };
                // 将房间加入列表
                db.Rooms.Add(room);
                // 用户创建房间也会将自己加入此房间
                AddRoom(roomName);
                // 发送"房间创建完成"消息
                Clients.Client(Context.ConnectionId).showMessage("房间创建完成");
                GetRooms();
            }
            else
            {
                // 房间名重复
                Clients.Client(Context.ConnectionId).showMessage("房间名重复");
            }
        }

        /// <summary>
        /// 退出房间
        /// </summary>
        /// <param name="roomName"></param>
        public void ExitRoom(string roomName)
        {
            // 查找房间是否存在
            Room room = db.Rooms.Find(r => r.RoomName == roomName);
            if (room != null)
            {
                // 查找要删除的用户
                User user = room.Users.Where(u => u.UserName == Context.ConnectionId).FirstOrDefault();
                // 移除此用户
                room.Users.Remove(user);
                if (room.Users.Count == 0)
                {
                    // 房间数为0，则删除房间
                    db.Rooms.Remove(room);
                }
                // Groups Remove移除分组方法
                Groups.Remove(Context.ConnectionId, roomName);
                // 发送 成功退出房间 消息
                Clients.Client(Context.ConnectionId).removeRoom("退出成功");
            }
        }


        /// <summary>
        /// 给房间内（一个房间即一个分组）所有用户发送消息
        /// </summary>
        /// <param name="roomName"></param>
        /// <param name="message"></param>
        public void SendMsg(string roomName, string message)
        {
            Clients.Group(roomName, new string[0]).sendMessage(roomName, message + " " + DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss:ms"));
        }
    }
}