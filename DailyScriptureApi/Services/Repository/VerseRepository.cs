using DailyScriptureApi.Data;
using DailyScriptureApi.Models.Domains;
using DailyScriptureApi.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace DailyScriptureApi.Services.Repository
{
    public class VerseRepository : IVerseRepository
    {
        Random random = new Random();
        private readonly AppDbContext dbContext;

        public VerseRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Verse>> GetAllAsync(string? filterOn, string? filterQuery,
            string? sortOn, bool? isAscending, int pageNumber = 1, int pageSize = 20)
        {


            pageSize = Math.Clamp(pageSize, 1, 50);
            pageNumber = Math.Clamp(pageNumber, 1, 1000);
          



            var verseDomain = dbContext.Verses.AsQueryable();

            if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
            {
                if (filterOn.Equals("translation", StringComparison.OrdinalIgnoreCase))
                {
                    verseDomain = verseDomain.Where(x => x.Translation.Contains(filterQuery));
                }
                if (filterOn.Equals("BookName", StringComparison.OrdinalIgnoreCase))
                {
                    verseDomain = verseDomain.Where(x => x.BookName.Contains(filterQuery));
                }
                if (filterOn.Equals("Content", StringComparison.OrdinalIgnoreCase))
                {
                    verseDomain = verseDomain.Where(x => x.Content.Contains(filterQuery));
                }

               
            }

            if (!string.IsNullOrEmpty(sortOn))
            {
                if (sortOn.Equals("bookName", StringComparison.OrdinalIgnoreCase))
                {

                    verseDomain = (isAscending ?? true)
                        ? verseDomain.OrderBy(x => x.BookName)
                        : verseDomain.OrderByDescending(x => x.BookName);

                }
            }

            var skipResults = (pageNumber - 1) * pageSize;

            return await verseDomain.Skip(skipResults).Take(pageSize).ToListAsync();
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

        public async Task<Verse> GetRandomAsync()
        {
            var listCount = dbContext.Verses.ToList().Count();
            var ranNum = random.Next(1, listCount);

            var verseDomain = await dbContext.Verses.FirstOrDefaultAsync(x => x.Id == ranNum);

            if (verseDomain is null)
            {
                return null;
            }

            return verseDomain;
        }
    }
}
