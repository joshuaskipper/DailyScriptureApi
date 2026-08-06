using DailyScriptureApi.Models.Domains;

namespace DailyScriptureApi.Services.Interface
{
    public interface IVerseRepository
    {
        public Task<List<Verse>> GetAllAsync();
        public Task<Verse> GetById(int id);
    }
}
