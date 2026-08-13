using DailyScriptureApi.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DailyScriptureApi.EndPoints
{
    public static class VerseEndPoint
    {
        public static void MapVerseEndPoint(this IEndpointRouteBuilder app) 
        {
            app.MapGet("/api/verse", async (IVerseRepository verseRepository, [FromQuery]string? filterOn = null, [FromQuery]string? filterQuery = null
                , [FromQuery]string? sortOn = null, [FromQuery]bool? isAscending = null, [FromQuery]int pageNumber = 1, [FromQuery]int pageSize = 20) =>
            {


                var verseDomain = await verseRepository.GetAllAsync(filterOn,filterQuery,sortOn,isAscending,pageNumber,pageSize);

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

            app.MapGet("/api/random/verse", async (IVerseRepository verseRepository, [FromQuery]string translation) =>
            {
                var verseDomain = await verseRepository.GetRandomAsync(translation);
                if (verseDomain is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(verseDomain);

            });
        }
    }
}
