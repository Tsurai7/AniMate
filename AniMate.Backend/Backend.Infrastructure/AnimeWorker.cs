using Backend.Infrastructure.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Backend.Infrastructure;

public class AnimeWorker : BackgroundService
{
    private readonly ILogger<AnimeWorker> _logger;
    private readonly AnilibriaClient _anilibriaClient;
    private readonly AnimeRepository _animeRepository;

    public AnimeWorker(
        ILogger<AnimeWorker> logger,
        AnilibriaClient anilibriaClient,
        AnimeRepository animeRepository)
    {
        _logger = logger;
        _anilibriaClient = anilibriaClient;
        _animeRepository = animeRepository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var genres = await _anilibriaClient.GetAllGenres();

        while (!stoppingToken.IsCancellationRequested)
        {
            foreach (var genre in genres)
            {
                _logger.LogInformation($"Fetching data for genre: {genre}");

                var currentPage = 1;
                var moreDataAvailable = true;
                do
                {
                    var response = await _anilibriaClient.GetTitlesByGenre(genre, currentPage++);
                    moreDataAvailable = response.Count != 0;

                    _logger.LogInformation($"{response.Count} titles fetched for genre: {genre}");

                    await _animeRepository.AddMany(response);

                    _logger.LogInformation("Successfully saved to mongo");
                } while (moreDataAvailable);
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}