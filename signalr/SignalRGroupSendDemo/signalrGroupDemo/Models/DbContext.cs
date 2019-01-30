using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace signalrGroupDemo.Models
{
    public class DbContext
    {
        /// <summary>
        /// 用户集合
        /// </summary>
        public List<User> Users { get; set; }

        /// <summary>
        /// 连接集合
        /// </summary>
        public List<Connection> Connections { get; set; }

        /// <summary>
        /// 房间集合
        /// </summary>
        public List<Room> Rooms { get; set; }

        public DbContext()
        {
            Users = new List<User>();
            Connections = new List<Connection>();
            Rooms = new List<Room>();
        }
    }

    public class User
    {
        // 不知道这个Key特性是干嘛的?
        /// <summary>
        /// 用户名
        /// </summary>
        [Key]
        public string UserName { get; set; }

        /// <summary>
        /// 用户连接
        /// </summary>
        public List<Connection> Connections { get; set; }

        /// <summary>
        /// 用户房间集合
        /// </summary>
        public virtual List<Room> Rooms { get; set; }

        public User()
        {
            // 搞不懂为什么需要手动初始化List ?
            Connections = new List<Connection>();
            Rooms = new List<Room>();
        }
    }

    public class Connection
    {
        /// <summary>
        /// 连接ID
        /// </summary>
        public string ConnectionID { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 是否连接
        /// </summary>
        public bool Connected { get; set; }
    }

    public class Room
    {
        /// <summary>
        /// 房间名
        /// </summary>
        [Key]
        public string RoomName { get; set; }

        /// <summary>
        /// 用户集合
        /// </summary>
        public virtual List<User> Users { get; set; }

        public Room()
        {
            Users = new List<User>();
        }
    }
}