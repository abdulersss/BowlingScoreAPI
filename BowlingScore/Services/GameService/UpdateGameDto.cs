using BowlingScore.Services.FrameService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingScore.Services.GameService
{
    public class UpdateGameDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public List<UpdateFrameDto> Frames { get; set; }
    }
}

