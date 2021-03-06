﻿using System;
using System.Collections.Generic;
using TheBowlingGame.Models;

namespace TheBowlingGame.Logic.Data
{
    public class CreateFrames
    {
        private List<Frame> frames;
        private int pins;
        private int frameCount;
        public CreateFrames()
        {

        }

        public CreateFrames(List<Frame> frames, int pins)
        {
            this.frames = frames;
            this.pins = pins;

            if (frames.Count > 0)
                frameCount = frames.Count - 1;
            else
                frameCount = 0;
        }
        /// <summary>
        /// Register the result of a roll
        /// </summary>
        public List<Frame> AddRoll()
        {
            if (frames.Count == 0)
                frames.Add(new Frame { FrameId = frameCount + 1, Rolls = new Rolls { FirstRoll = pins }, RollCount = 1 });
            else
            {
                if (!frames[frameCount].IsClosed)
                {
                    if (frames[frameCount].RollCount == 1)
                    {
                        frames[frameCount].Rolls.SecondRoll = pins;

                        if (!IsLastFrame())
                            frames[frameCount].IsClosed = true;
                    }
                    else
                    {
                        if (IsLastFrame() &&
                            frames[frameCount].Rolls.FirstRoll + frames[frameCount].Rolls.SecondRoll == 10)
                        {
                            frames[frameCount].Rolls.ExtraRoll = pins;
                            frames[frameCount].IsClosed = true;
                        }
                        else
                            return frames;                        
                    }

                    frames[frameCount].RollCount++;
                    FrameType();
                }
                else
                {
                    if (!IsLastFrame())
                        frameCount++;
                    frames.Add(new Frame { FrameId = frameCount + 1, Rolls = new Rolls { FirstRoll = pins }, RollCount = 1 });
                }
            }
            Scores();
            return frames;
        }

        /// <summary>
        /// Determine Last Frames has been reached
        /// </summary>
        private bool IsLastFrame() =>
            frames.Count == 10;

        /// <summary>
        /// Calculate the Frame Type for each Frame [Strike, Spare, Miss]
        /// </summary>
        private void FrameType()
        {
            if (frames[frameCount].IsClosed)
            {
                if (frames[frameCount].Rolls.FirstRoll + frames[frameCount].Rolls.SecondRoll == 10)
                {
                    if (frames[frameCount].Rolls.FirstRoll == 10)
                        frames[frameCount].FrameType = "X";
                    else
                        frames[frameCount].FrameType = "/";
                }
                else
                {
                    if (frames[frameCount].Rolls.FirstRoll == 0 ||
                        frames[frameCount].Rolls.SecondRoll == 0)
                        frames[frameCount].FrameType = "-";
                    else
                        frames[frameCount].FrameType = "";
                }
            }
        }

        /// <summary>
        /// Calculate the Score for each Frame
        /// </summary>
        private void Scores()
        {
            if (!frames[frameCount].IsClosed)
            {
                ScoresBasic();
                return;
            }

            if (frames[frameCount].Rolls.FirstRoll + frames[frameCount].Rolls.SecondRoll < 10)
                ScoresBasic();

            if (frameCount >= 1)
            {
                if (frames.Count - 1 == frameCount)
                {
                    ScoresPrevious();

                    if (frames[frameCount].RollCount == 2)
                        ScoresCurrent();
                    else
                        frames[frameCount].Score =
                            frames[frameCount - 1].Score +
                            frames[frameCount].Rolls.FirstRoll +
                            frames[frameCount].Rolls.SecondRoll +
                            frames[frameCount].Rolls.ExtraRoll;
                }
                else if (frames[frameCount + 1].Score == 0)
                {
                    ScoresPrevious();
                    ScoresCurrent();
                }
            }
            else
                ScoresTwo();
        }

        /// <summary>
        /// Helper method to calculate the Score for each Frame
        /// </summary>
        private void ScoresTwo()
        {
            frames[frameCount].Score =
                frames[frameCount].Rolls.FirstRoll +
                frames[frameCount].Rolls.SecondRoll;
        }

        /// <summary>
        /// Helper method to calculate the Score for each Frame
        /// </summary>
        private void ScoresBasic()
        {
            if (frameCount >= 1)
                frames[frameCount].Score =
                    frames[frameCount - 1].Score +
                    frames[frameCount].Rolls.FirstRoll +
                    frames[frameCount].Rolls.SecondRoll;
            else
                ScoresTwo();
        }

        /// <summary>
        /// Helper method to calculate the Score for each Frame
        /// </summary>
        private void ScoresPrevious()
        {
            if (frames[frameCount - 1].FrameType == "/")
                frames[frameCount - 1].Score =
                    frames[frameCount - 1].Score +
                    frames[frameCount].Rolls.FirstRoll;

            if (frames[frameCount - 1].FrameType == "X")
                frames[frameCount - 1].Score =
                    frames[frameCount - 1].Score +
                    frames[frameCount].Rolls.FirstRoll +
                    frames[frameCount].Rolls.SecondRoll;
        }

        /// <summary>
        /// Helper method to calculate the Score for each Frame
        /// </summary>
        private void ScoresCurrent()
        {
            frames[frameCount].Score =
                frames[frameCount - 1].Score +
                frames[frameCount].Rolls.FirstRoll +
                frames[frameCount].Rolls.SecondRoll;
        }
    }
}
