using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliLib.Live
{
    public class LiveRoomModel
    {
        public int RoomId { get; set; }

        public LiveRoomModel(int roomId)
        {
            this.RoomId = roomId;
        }
    }
}
