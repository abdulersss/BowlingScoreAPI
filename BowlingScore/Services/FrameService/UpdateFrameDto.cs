using BowlingScore.Services.FrameService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingScore.Services.FrameService
{
    public class UpdateFrameDto
    {
        public int Id { get; init; }
        public int GameId { get; init; }
        public List<int> PinsKnockedDown { get; set; }
    }
}

