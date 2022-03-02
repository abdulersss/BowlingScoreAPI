using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingScore.Models
{
    public class Game
    {
        private const int _maxPinCount = 10;
        public Game(string name)
        {
            Name = name;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<FrameScore> Frames { get; set; } = new List<FrameScore>();

        public void Update(string name)
        {
            Name = name;
        }

        public void AddFrame(FrameScore frame)
        {
            if (Frames.Count == 10)
                throw new InvalidOperationException("Maximum number of frames reached.");
            frame.FrameNumber = Frames.Count + 1;
            Frames.Add(frame);
        }

        public int GetLastFrame()
        {
            return Frames.OrderByDescending(x => x.FrameNumber).FirstOrDefault().FrameNumber;
        }

        public void CalculateScores()
        {
            var accumulatedScore = 0;
            foreach (var frame in Frames)
            {
                var score = 0;
                //1st to 9th frame logic
                if (frame.FrameNumber != 10)
                {
                    switch (frame.ScoreType)
                    {
                        case ScoreType.Strike:
                            score = _maxPinCount;
                            var nextFrame = Frames.OrderBy(x => x.FrameNumber).Skip(frame.FrameNumber).FirstOrDefault();
                            if (frame.FrameNumber == 9)
                            {
                                score += nextFrame.PinsKnockedDown.Take(2).Sum();
                            }
                            else
                            {
                                score += nextFrame.PinsKnockedDown.FirstOrDefault();
                                if (score == _maxPinCount)
                                {
                                    score += Frames.OrderBy(x => x.FrameNumber).Skip(nextFrame.FrameNumber).FirstOrDefault().PinsKnockedDown.FirstOrDefault();
                                }
                                else
                                {
                                    score += nextFrame.PinsKnockedDown.LastOrDefault();
                                }
                            }

                            break;
                        case ScoreType.Spare:
                            var nextShot = Frames.OrderBy(x => x.FrameNumber).Skip(frame.FrameNumber).FirstOrDefault().PinsKnockedDown.FirstOrDefault();
                            score = _maxPinCount + nextShot;
                            break;
                        case ScoreType.OpenFrame:
                            score = frame.PinsKnockedDown.Sum();
                            break;
                        default:
                            throw new InvalidOperationException("No Score Type was set for this frame.");
                    }
                }
                //10th frame logic
                else
                {
                    score = frame.PinsKnockedDown.Sum();
                }

                accumulatedScore += score;
                frame.Score = accumulatedScore;

            }
        }
    }
}
