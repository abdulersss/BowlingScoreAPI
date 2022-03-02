using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingScore.Services.FrameService
{
    public class FrameRecord
    {
        public int Id { get; init; }
        public List<int> PinsKnockedDown { get; set; }
        public int Score { get; init; }
        public int FrameNumber { get; init; }
    }
}
