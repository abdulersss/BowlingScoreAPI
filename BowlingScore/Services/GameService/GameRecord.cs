using BowlingScore.Services.FrameService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingScore.Services.GameService
{
    public record GameRecord
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public List<FrameRecord> Frames { get; set; }
    }
}
