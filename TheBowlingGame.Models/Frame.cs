using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBowlingGame.Models
{
    public class Frame
    {
        public int FrameId { get; set; }
        public Rolls Rolls { get; set; }
        public int RollCount { get; set; }
        public string FrameType { get; set; }
        public bool IsClosed { get; set; }
        public int Score { get; set; }
    }

    public class Rolls
    {
        public int FirstRoll { get; set; }
        public int SecondRoll { get; set; }
        public int ExtraRoll { get; set; }
    }
}
