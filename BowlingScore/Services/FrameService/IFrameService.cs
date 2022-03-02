using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingScore.Services.FrameService
{
    public interface IFrameService
    {
        Task<IEnumerable<FrameRecord>> GetFrames();
        Task<FrameRecord> GetFrame(int id);
        Task CreateFrame(CreateFrameDto dto);
        Task UpdateFrame(UpdateFrameDto dto);
        Task DeleteFrame(int id);
    }
}
