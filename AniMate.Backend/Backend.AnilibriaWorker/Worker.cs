namespace Backend.AnilibriaWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly AnilibriaClient _anilibriaClient;
    private readonly AnimeRepository _animeRepository;

    public Worker(ILogger<Worker> logger,
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
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            
            foreach (var genre in genres)
            {
                _logger.LogInformation($"Fetching data for genre: {genre}");

                int currentPage = 1;
                bool moreDataAvailable = true; 
                do
                {
                    var response = await _anilibriaClient.GetTitlesByGenre(genre, currentPage++);
                    moreDataAvailable = response.Count != 0;
                        
                    _logger.LogInformation($"{response.Count} titles fetched for genre: {genre}");

                    await _animeRepository.AddMany(response);
                
                    _logger.LogInformation("Successfully saved to mongo");
                } while(moreDataAvailable);
            }
            
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}