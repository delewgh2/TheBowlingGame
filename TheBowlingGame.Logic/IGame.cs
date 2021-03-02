using System.Collections.Generic;
using TheBowlingGame.Models;

namespace TheBowlingGame.Logic
{
    public interface IGame
    {
        public IEnumerable<Frame> Roll(int pins);
        Frame Score(int id);
        IEnumerable<Frame> Scores();
    }
}
