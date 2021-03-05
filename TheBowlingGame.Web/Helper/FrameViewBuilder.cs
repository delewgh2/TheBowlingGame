using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBowlingGame.Models;
using TheBowlingGame.Models.ViewModels;

namespace TheBowlingGame.Web.Helper
{
    public static class FrameViewBuilder
    {
        public static List<FrameViewModel> FrameView(IEnumerable<Frame> frames)
        {
            var framesView = new List<FrameViewModel>();

            foreach (var frame in frames)
            {
                framesView.Add(new FrameViewModel
                {
                    FrameId = frame.FrameId,
                    FirstRoll = frame.Rolls.FirstRoll,
                    SecondRoll = frame.Rolls.SecondRoll,
                    ExtraRoll = frame.Rolls.ExtraRoll,
                    RollCount = frame.RollCount,
                    RollsRemain =
                        (frame.IsClosed || (frame.FrameId == 10 &&
                         frame.RollCount == 2 && frame.Rolls.FirstRoll + frame.Rolls.SecondRoll < 10)) ? 0:
                        frame.FrameId == 10 && frame.RollCount == 2 && frame.Rolls.FirstRoll + frame.Rolls.SecondRoll == 10 ?
                        1 : frame.RollCount,
                    Score = frame.Score,
                    IsExtraRoll = frame.IsClosed && frame.FrameId == 10 && frame.RollCount == 3,
                    IsReset = 
                        frame.FrameId == 10 && frame.RollCount == 2 && frame.Rolls.FirstRoll + frame.Rolls.SecondRoll < 10 ||
                        frame.IsClosed && frame.FrameId == 10,
                    ErrorMessage = (frame.RollCount == 1 && frame.Rolls.FirstRoll > 10) ||
                        (frame.RollCount == 2 && frame.Rolls.SecondRoll > 10) ||
                        (frame.RollCount == 3 && frame.Rolls.ExtraRoll > 10) ? "Pins can only be [0 - 10]" :
                        (frame.RollCount == 2 && frame.Rolls.FirstRoll + frame.Rolls.SecondRoll > 10) ? $"Pin can only be {10 - frame.Rolls.FirstRoll}" :
                        string.Empty
                });
            }

            //foreach (var frame in frames)
            //{
            //    if (frame.IsClosed)
            //    {
            //        if (frame.FrameId == 10)
            //        {
            //            if (frame.RollCount == 3)
            //            {
            //                framesView.Add(new FrameViewModel
            //                {
            //                    FrameId = frame.FrameId,
            //                    FirstRoll = frame.Rolls.FirstRoll,
            //                    SecondRoll = frame.Rolls.SecondRoll,
            //                    ExtraRoll = frame.Rolls.ExtraRoll,
            //                    RollsRemain = 0,
            //                    Score = frame.Score
            //                });
            //            }
            //            else
            //            {
            //                framesView.Add(new FrameViewModel
            //                {
            //                    FrameId = frame.FrameId,
            //                    FirstRoll = frame.Rolls.FirstRoll,
            //                    SecondRoll = frame.Rolls.SecondRoll,
            //                    RollsRemain = 0,
            //                    Score = frame.Score
            //                });
            //            }

            //        }
            //        else
            //        {
            //            framesView.Add(new FrameViewModel
            //            {
            //                FrameId = frame.FrameId,
            //                FirstRoll = frame.Rolls.FirstRoll,
            //                SecondRoll = frame.Rolls.SecondRoll,
            //                RollsRemain = 0,
            //                Score = frame.Score
            //            });
            //        }
            //    }
            //    else
            //    {
            //        if (frame.FrameId == 10)
            //        {
            //            if (frame.RollCount == 2)
            //            {
            //                if (frame.Rolls.FirstRoll + frame.Rolls.SecondRoll == 10)
            //                {
            //                    framesView.Add(new FrameViewModel
            //                    {
            //                        FrameId = frame.FrameId,
            //                        FirstRoll = frame.Rolls.FirstRoll,
            //                        SecondRoll = frame.Rolls.SecondRoll,
            //                        RollsRemain = 1,
            //                        Score = frame.Score
            //                    });
            //                }
            //                else
            //                {
            //                    framesView.Add(new FrameViewModel
            //                    {
            //                        FrameId = frame.FrameId,
            //                        FirstRoll = frame.Rolls.FirstRoll,
            //                        SecondRoll = frame.Rolls.SecondRoll,
            //                        RollsRemain = 0,
            //                        Score = frame.Score
            //                    });
            //                }
            //            }
            //            else
            //            {
            //                framesView.Add(new FrameViewModel
            //                {
            //                    FrameId = frame.FrameId,
            //                    FirstRoll = frame.Rolls.FirstRoll,
            //                    RollsRemain = frame.RollCount,
            //                    Score = frame.Score
            //                });
            //            }
            //        }
            //        else
            //        {
            //            framesView.Add(new FrameViewModel
            //            {
            //                FrameId = frame.FrameId,
            //                FirstRoll = frame.Rolls.FirstRoll,
            //                RollsRemain = frame.RollCount,
            //                Score = frame.Score
            //            });
            //        }
            //    }
            //}
            return framesView;
        }
    }
}
