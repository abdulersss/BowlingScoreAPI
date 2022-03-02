using BowlingScore.Models;
using BowlingScore.Repositories.FrameRepository;
using BowlingScore.Repositories.GameRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingScore.Services.FrameService
{
    public class FrameService : IFrameService
    {
        private readonly IFrameRepository _frameRepository;
        private readonly IGameRepository _gameRepository;
        private readonly ILogger<FrameService> _logger;

        public FrameService(IFrameRepository frameRepository, IGameRepository gameRepository, ILogger<FrameService> logger)
        {
            _frameRepository = frameRepository ?? throw new ArgumentNullException(nameof(frameRepository));
            _gameRepository = gameRepository ?? throw new ArgumentNullException(nameof(gameRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task CreateFrame(CreateFrameDto dto)
        {
            try
            {
                var game = await _gameRepository.Get(dto.GameId);
                if (game == default)
                    throw new KeyNotFoundException(nameof(game));

                game.AddFrame(new FrameScore(dto.PinsKnockedDown));
                await _gameRepository.Update(game);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task DeleteFrame(int id)
        {
            try
            {
                await _frameRepository.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task<FrameRecord> GetFrame(int id)
        {
            try
            {
                var frame = await _frameRepository.Get(id);
                if (frame == default)
                    return default;

                var result = new FrameRecord
                {
                    Id = frame.Id,
                    FrameNumber = frame.FrameNumber,
                    PinsKnockedDown = frame.PinsKnockedDown,
                    Score = frame.Score
                };

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return default;
            }
        }

        public async Task<IEnumerable<FrameRecord>> GetFrames()
        {
            try
            {
                var frames = await _frameRepository.Get();
                if (frames == default)
                    return default;

                var result = frames.Select(entity => new FrameRecord
                {
                    Id = entity.Id,
                    FrameNumber = entity.FrameNumber,
                    PinsKnockedDown = entity.PinsKnockedDown,
                    Score = entity.Score
                });

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return default;
            }
        }

        public async Task UpdateFrame(UpdateFrameDto dto)
        {
            try
            {
                var game = await _gameRepository.Get(dto.GameId);
                if (game == default)
                    throw new KeyNotFoundException(nameof(game));

                var frame = game.Frames.FirstOrDefault(x => x.Id == dto.Id);
                if (frame == default)
                    throw new KeyNotFoundException(nameof(frame));

                frame.UpdatePinsKnockedDownm(dto.PinsKnockedDown);
                await _gameRepository.Update(game);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
