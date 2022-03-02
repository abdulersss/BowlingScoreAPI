using BowlingScore.Data;
using BowlingScore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingScore.Repositories.GameRepository
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _context;

        public GameRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Game> Create(Game entity)
        {
            _context.Games.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Games.FindAsync(id);
            _context.Games.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Game>> Get()
        {
            return await _context.Games.Include(x => x.Frames).AsSplitQuery().ToListAsync();
        }

        public async Task<Game> Get(int id)
        {
            return await _context.Games.Include(x => x.Frames).AsSplitQuery().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(Game entity)
        {
            _context.Games.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
