using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json;
using AniMate_app.DTOs.Anime;                                                 
using AniMate_app.Interfaces;
using System.Collections.ObjectModel;
using AniMate_app.Clients;

namespace AniMate_app.ViewModels;

[QueryProperty(nameof(TitleCode), "TitleCode")]
[QueryProperty(nameof(Title), "TheTitle")]
public partial class TitleViewModel : ObservableObject
{
    private readonly AccountClient _accountClient;

    private readonly IAnimeClient _animeClient;

    private readonly IApplicationLinkService _linkService;

    private CancellationTokenSource _tokenSource = new();

    public TitleViewModel(
        AccountClient accountClient,
        IAnimeClient animeClient,
        IApplicationLinkService linkService)
    {
        _accountClient = accountClient;
        _animeClient = animeClient;
        _linkService = linkService;
    }

    private bool _isTitleInLikes;
    public bool IsTitleInLikes
    {
        get => _isTitleInLikes;
        set => SetProperty(ref _isTitleInLikes, value);
    }

    private TitleDto _title;
    public TitleDto Title
    {
        get => _title;
        set
        {
            _title = value;
            Genres = string.Join(", ", _title.Genres);
            ShortDescription = string.Join(" ", _title.RuDescription.Split(' ').Take(7));
            OnPropertyChanged(nameof(Title));
            Task.Factory.StartNew(LoadEpisodes, _tokenSource.Token);
        }
    }

    private async void LoadEpisodes()
    {
        for(int i = 0; i < Title.Player.Episodes.Count; i+=20)
        {
            for(int j = i; j < i + 20; j++)
            {
                if (j >= Title.Player.Episodes.Count)
                    return;

                Episodes.Add(Title.Player.Episodes.Values.ElementAt(j));
            }

            await Task.Delay(1500);
        }
    }

    public ObservableCollection<EpisodeDto> Episodes { get; private set; } = new();

    public string TitleCode
    {
        set
        {
            LoadTitleFromCode(value);
        }
    }

    private async void LoadTitleFromCode(string code)
    {
        Title = await _animeClient.GetTitleByCode(code);
    }

    [ObservableProperty]
    private string _genres;

    [ObservableProperty]
    private string _shortDescription;

    public async Task<bool> LikesButtonClicked()
    {
        throw new NotImplementedException();
    }

    public async void ShareTitleUrl()
    {
        var link = _linkService.CreateTitleLink(Title);

        await _linkService.ShareText(link);
    }

    public void StopButtonLoad()
    {
        _tokenSource.Cancel();
    }
}