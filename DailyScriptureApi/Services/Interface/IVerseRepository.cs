using DailyScriptureApi.Models.Domains;

namespace DailyScriptureApi.Services.Interface
{
    public interface IVerseRepository
    {
        public Task<List<Verse>> GetAllAsync(string? filterOn, string? filterQuery, string? sortOn, bool? isAscending,
            int pageNumber = 1, int pageSize = 20);
        public Task<Verse> GetById(int id);
    }
}
