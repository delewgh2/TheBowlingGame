using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBowlingGame.Models.ViewModels
{
    public class FrameViewModel
    {
        public int FrameId { get; set; }
        public int FirstRoll { get; set; }
        public int SecondRoll { get; set; }
        public int ExtraRoll { get; set; }
        public int RollCount { get; set; }
        public int RollsRemain { get; set; }
        public int Score { get; set; }
        public bool IsExtraRoll { get; set; }
        public bool IsReset { get; set; }
        public string ErrorMessage { get; set; }
    }
}
