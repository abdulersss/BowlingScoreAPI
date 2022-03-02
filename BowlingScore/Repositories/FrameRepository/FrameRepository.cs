using BowlingScore.Data;
using BowlingScore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingScore.Repositories.FrameRepository
{
    public class FrameRepository : IFrameRepository
    {
        private readonly ApplicationDbContext _context;

        public FrameRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FrameScore> Create(FrameScore entity)
        {
            _context.Frames.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Frames.FindAsync(id);
            _context.Frames.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FrameScore>> Get()
        {
            return await _context.Frames.ToListAsync();
        }

        public async Task<FrameScore> Get(int id)
        {
            return await _context.Frames.FindAsync(id);
        }

        public async Task Update(FrameScore entity)
        {
            _context.Frames.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
