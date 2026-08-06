using DailyScriptureApi.Data;
using DailyScriptureApi.Models.Domains;
using DailyScriptureApi.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace DailyScriptureApi.Services.Repository
{
    public class VerseRepository : IVerseRepository
    {
        private readonly AppDbContext dbContext;

        public VerseRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Verse>> GetAllAsync()
        {
            var verseDomain = await dbContext.Verses.ToListAsync();

            return verseDomain;
        }

        public async Task<Verse> GetById(int id)
        {
            var verseDomain = await dbContext.Verses.FirstOrDefaultAsync(x => x.Id == id);

            if (verseDomain is null)
            {
                return null;
            }

            return verseDomain;
        }
    }
}
