using BowlingScore.Services.FrameService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingScore.Services.FrameService
{
    public class CreateFrameDto
    {
        public int GameId { get; set; }
        public List<int> PinsKnockedDown { get; set; }
    }
}
