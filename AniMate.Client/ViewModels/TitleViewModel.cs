using AniMate_app.Clients;
using AniMate_app.DTOs.Anime;
using AniMate_app.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AniMate_app.ViewModels;

[QueryProperty(nameof(Title), "TheTitle")]
public partial class TitleViewModel : ObservableObject
{
    private const string SHOW_MORE_TEXT = "... ещё";

    private const string NO_DESCRIPTION_TEXT = "Нет описания";

    private readonly AccountClient _accountClient;

    private readonly IApplicationLinkService _linkService;

    private CancellationTokenSource _tokenSource = new();

    private bool _isShortDescriptionOpen = true;

    public TitleViewModel(
        AccountClient accountClient,
        IApplicationLinkService linkService)
    {
        _accountClient = accountClient;
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
            ShortDescription = string.Join(" ", _title.RuDescription?.Split(' ').Take(7)) ?? NO_DESCRIPTION_TEXT;
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

    [ObservableProperty]
    private string _genres;

    [ObservableProperty]
    private string _shortDescription = string.Empty;

    [ObservableProperty]
    private string _descriptionEnding = SHOW_MORE_TEXT;

    [ObservableProperty]
    private string _titleDescription;

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

    public void ToggleDescription()
    {
        string description = (_isShortDescriptionOpen ? ShortDescription : Title.RuDescription) ?? NO_DESCRIPTION_TEXT;

        TitleDescription = description;

        DescriptionEnding = _isShortDescriptionOpen ? SHOW_MORE_TEXT : string.Empty;

        _isShortDescriptionOpen = !_isShortDescriptionOpen;
    }
}