using Backend.Infrastructure;
using Backend.Infrastructure.Repositories;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Backend.Application;

public class UpdateTitlesJob : BackgroundService
{
    private readonly AnilibriaClient _anilibriaClient;
    private readonly TitleRepository _titleRepository;
    private readonly TimeSpan _interval = TimeSpan.FromHours(8);
    private readonly ILogger<UpdateTitlesJob> _logger;
    private int _lastUpdated;

    public UpdateTitlesJob(
        AnilibriaClient anilibriaClient,
        TitleRepository titleRepository,
        ILogger<UpdateTitlesJob> logger)
    {
        _anilibriaClient = anilibriaClient;
        _titleRepository = titleRepository;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _lastUpdated = (int)((DateTimeOffset)DateTime.UtcNow.AddHours(-2)).ToUnixTimeSeconds();
        _logger.LogInformation($"{DateTime.Now}: started {nameof(UpdateTitlesJob)}");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var currentPage = 1;
                var moreDataAvailable = true;
                do
                {
                    var titlesToUpdate = await _anilibriaClient.GetUpdates(_lastUpdated, currentPage, stoppingToken);
                    moreDataAvailable = titlesToUpdate.Count != 0;
                    
                    await _titleRepository.BulkUpdate(titlesToUpdate, stoppingToken);

                    _logger.LogInformation("Successfully saved to mongo");
                } while (moreDataAvailable);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during title update job");
            }

            await Task.Delay(_interval, stoppingToken);
        }
        _logger.LogInformation($"{DateTime.Now}: finished {nameof(UpdateTitlesJob)}");
    }
}