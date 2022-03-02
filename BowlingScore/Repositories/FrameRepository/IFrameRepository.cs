using BowlingScore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingScore.Repositories.FrameRepository
{
    public interface IFrameRepository
    {
        Task<IEnumerable<FrameScore>> Get();
        Task<FrameScore> Get(int id);
        Task<FrameScore> Create(FrameScore entity);
        Task Update(FrameScore entity);
        Task Delete(int id);
    }
}
