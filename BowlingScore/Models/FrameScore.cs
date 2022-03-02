using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingScore.Models
{
    public class FrameScore
    {
        private const int _maxPinCount = 10;

        public FrameScore(List<int> pinsKnockedDown)
        {
            PinsKnockedDown = pinsKnockedDown;
        }

        [Key]
        public int Id { get; set; }
        public List<int> PinsKnockedDown { get; private set; } = new List<int>();
        public int Score { get; set; }
        public ScoreType ScoreType { get; private set; }
        public int FrameNumber { get; set; }

        public void UpdatePinsKnockedDownm(List<int> pinsKnockedDown)
        {
            ClearPinsKnockedDown();
            foreach (var pins in pinsKnockedDown)
            {
                AddPinsKnockedDown(pins);
            }
        }

        public void AddPinsKnockedDown(int numOfPins)
        {
            if (numOfPins > _maxPinCount || numOfPins < 0)
                throw new ArgumentException("Invalid Number of Pins.");

            if ((FrameNumber < 10 && PinsKnockedDown.Count == 2) || (FrameNumber == 10 && PinsKnockedDown.Count == 3))
                throw new InvalidOperationException("Maximum number of shots reached.");

            if(FrameNumber != 10 && (numOfPins + PinsKnockedDown.Sum()) > _maxPinCount)
                throw new InvalidOperationException($"Number of pins should not exceed {_maxPinCount}.");

            if (!PinsKnockedDown.Any() && numOfPins == _maxPinCount) 
            {
                ScoreType = ScoreType.Strike;
            }
            else if(PinsKnockedDown.Any() && numOfPins + PinsKnockedDown.Sum() == _maxPinCount)
            {
                ScoreType = ScoreType.Spare;
            }
            else
            {
                ScoreType = ScoreType.OpenFrame;
            }

            PinsKnockedDown.Add(numOfPins);

            Score = PinsKnockedDown.Sum();

        }

        public void ClearPinsKnockedDown()
        {
            PinsKnockedDown.Clear();
        }
    }

    public enum ScoreType
    {
        Strike = 1,
        Spare = 2,
        OpenFrame = 3
    }
}
