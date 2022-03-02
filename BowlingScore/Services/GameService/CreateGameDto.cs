using BowlingScore.Services.FrameService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingScore.Services.GameService
{
    public class CreateGameDto
    {
        public string Name { get; init; }
        public List<CreateFrameDto> Frames { get; set; }
    }
}
