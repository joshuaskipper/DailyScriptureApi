namespace DailyScriptureApi.Models.Domains
{
    public class Verse
    {
        public int Id { get; set; }
        public string? VerseAbbreviation { get; set; }
        public string? BookName { get; set; }
        public string? Translation { get; set; }
        public string? Content { get; set; }
    }
}
