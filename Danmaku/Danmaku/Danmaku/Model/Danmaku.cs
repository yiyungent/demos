using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum DanmakuMode
    {
        TopAnchoredScrolling = 1,
        BottomAnchoredScrolling = 2,
        BottomAnchoredStatic = 4,
        TopAnchoredStatic = 5,
        TopAnchoredReverse = 6,
        AnimatedPositioned = 7,
        CodeComments = 8,
        ZoomeType = 9,
        Image = 17,
        Subtitles = 21
    }

    public class Danmaku
    {
        public DanmakuMode Mode { get; set; }

        public string Text { get; set; }

        public int Stime { get; set; }

        public int Size { get; set; }

        public int Color { get; set; }
    }
}
