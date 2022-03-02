using BowlingScore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingScore.Repositories.GameRepository
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> Get();
        Task<Game> Get(int id);
        Task<Game> Create(Game entity);
        Task Update(Game entity);
        Task Delete(int id);
    }
}
