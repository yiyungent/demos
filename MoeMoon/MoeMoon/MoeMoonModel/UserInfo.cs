using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MoeMoonModel
{
    public partial class UserInfo
    {
        public int UID { get; set; }

        public string UCode { get; set; }

        public string UPassword { get; set; }

        public string UName { get; set; }

        public string UPhone { get; set; }

        public string UEmail { get; set; }

        public short UDelFlag { get; set; }
    }
}
