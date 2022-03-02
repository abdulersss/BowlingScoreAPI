using BowlingScore.Models;
using BowlingScore.Repositories.GameRepository;
using BowlingScore.Services.FrameService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingScore.Services.GameService
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly ILogger<GameService> _logger;

        public GameService(IGameRepository gameRepository, ILogger<GameService> logger)
        {
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> CreateGame(CreateGameDto dto)
        {
            try
            {
                var game = new Game(dto.Name);
                foreach (var frame in dto.Frames)
                {
                    game.AddFrame(new FrameScore(frame.PinsKnockedDown));
                }
                var result = await _gameRepository.Create(game);
                return result.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return default;
            }
        }

        public async Task DeleteGame(int id)
        {
            try
            {
                await _gameRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task<GameRecord> GetGame(int id)
        {
            try
            {
                var game = await _gameRepository.Get(id);
                if (game == default)
                    return default;
                var result = new GameRecord
                {
                    Id = game.Id,
                    Name = game.Name,
                    Frames = game.Frames?.Select(frame => new FrameRecord
                    {
                        Id = frame.Id,
                        FrameNumber = frame.FrameNumber,
                        PinsKnockedDown = frame.PinsKnockedDown,
                        Score = frame.Score
                    }).ToList()
                };
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return default;
            }
        }

        public async Task<IEnumerable<GameRecord>> GetGames()
        {
            try
            {
                var games = await _gameRepository.Get();
                if (games == default)
                    return default;

                var result = games.Select(game => new GameRecord
                {
                    Id = game.Id,
                    Name = game.Name,
                    Frames = game.Frames?.Select(frame => new FrameRecord
                    {
                        Id = frame.Id,
                        FrameNumber = frame.FrameNumber,
                        PinsKnockedDown = frame.PinsKnockedDown,
                        Score = frame.Score
                    }).ToList()
                });

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return default;
            }
        }

        public async Task<int> UpdateGame(UpdateGameDto dto)
        {
            try
            {
                var game = await _gameRepository.Get(dto.Id);
                if (game == default)
                    return default;

                game.Update(dto.Name);
                foreach (var frameDto in dto.Frames)
                {
                    game.Frames.FirstOrDefault(x => x.Id == frameDto.Id).UpdatePinsKnockedDownm(frameDto.PinsKnockedDown);
                }

                await _gameRepository.Update(game);

                return game.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return default;
            }
        }
    }
}
