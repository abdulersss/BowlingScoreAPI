using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingScore.Services.GameService
{
    public interface IGameService
    {
        Task<IEnumerable<GameRecord>> GetGames();
        Task<GameRecord> GetGame(int id);
        Task<int> CreateGame(CreateGameDto dto);
        Task<int> UpdateGame(UpdateGameDto dto);
        Task DeleteGame(int id);
    }
}
