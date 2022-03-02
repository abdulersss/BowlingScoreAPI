using BowlingScore.Services.FrameService;
using BowlingScore.Services.GameService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BowlingScore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IFrameService _frameService;
        private readonly ILogger<GamesController> _logger;

        public GamesController(IGameService gameService, IFrameService frameService, ILogger<GamesController> logger)
        {
            _gameService = gameService ?? throw new ArgumentNullException(nameof(gameService));
            _frameService = frameService ?? throw new ArgumentNullException(nameof(frameService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameRecord>>> GetGames()
        {
            var result = await _gameService.GetGames();
            if (result == default)
                return Ok(default);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameRecord>> GetGame(int id)
        {
            var result = await _gameService.GetGame(id);
            if (result == default)
                return Ok(default);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> PostGame([FromBody] CreateGameDto dto)
        {
            var result = await _gameService.CreateGame(dto);
            if (result == default)
                return Ok(default);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] UpdateGameDto dto)
        {
            var result = await _gameService.UpdateGame(dto);
            if (result == default)
                return Ok(default);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FrameRecord>>> GetFrames()
        {
            var result = await _frameService.GetFrames();
            if (result == default)
                return Ok(default);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FrameRecord>> GetFrame(int id)
        {
            var result = await _frameService.GetFrame(id);
            if (result == default)
                return Ok(default);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> PostFrame([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutFrame(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFrame(int id)
        {
        }
    }
}
