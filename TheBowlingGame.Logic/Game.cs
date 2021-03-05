using System;
using System.Collections.Generic;
using System.Linq;
using Cashing.Infrastructure;
using Cashing.Models;
using TheBowlingGame.Logic.Data;
using TheBowlingGame.Models;

namespace TheBowlingGame.Logic
{
    public class Game : IGame
    {
        private readonly ICacheProvider _cacheProvider;
        private CreateFrames createFrames;
        private List<Frame> frames;

        public Game(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
            frames = new List<Frame>();
        }

        public IEnumerable<Frame> Roll(int pins)
        {
            return GetCachedResponse(Keys.Frames, pins);
        }

        public Frame Score(int id)
        {
            return frames.FirstOrDefault(f => f.FrameId == id);
        }

        public IEnumerable<Frame> Scores()
        {
            var cashedFrames = _cacheProvider.GetFromCache<IEnumerable<Frame>>(Keys.Frames);

            if (cashedFrames != null)
                return (from Frame frame in cashedFrames
                        select frame).ToList();
            else
                return (from Frame frame in frames
                        select frame).ToList();
        }

        private IEnumerable<Frame> GetCachedResponse(string cacheKey, int pins)
        {
            var cashedFrames = _cacheProvider.GetFromCache<IEnumerable<Frame>>(cacheKey);

            if (cashedFrames != null)
                frames = (List<Frame>)cashedFrames;

            createFrames = new CreateFrames(frames, pins);
            frames = createFrames.AddRoll();
            _cacheProvider.SetCache(cacheKey, frames, DateTimeOffset.Now.AddDays(1));

            return frames;
        }
    }
}
