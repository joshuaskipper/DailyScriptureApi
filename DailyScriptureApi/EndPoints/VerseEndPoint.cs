using DailyScriptureApi.Services.Interface;

namespace DailyScriptureApi.EndPoints
{
    public static class VerseEndPoint
    {
        public static void MapVerseEndPoint(this IEndpointRouteBuilder app) 
        {
            app.MapGet("/api/verse", async (IVerseRepository verseRepository) =>
            {
                var verseDomain = await verseRepository.GetAllAsync();

                return Results.Ok(verseDomain);
            });

            app.MapGet("/api/verse/{id:int}", async (IVerseRepository verseRepository, int id) =>
            {
                var verseDomain = await verseRepository.GetById(id);

                if (verseDomain is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(verseDomain);
            });
        }
    }
}
