using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json;
using AniMate_app.DTOs.Account;
using AniMate_app.DTOs.Anime;                                                 
using AniMate_app.Interfaces;
using AniMate_app.Models;

namespace AniMate_app.ViewModels;

[QueryProperty(nameof(TitleCode), "TitleCode")]
[QueryProperty(nameof(Title), "TheTitle")]
public partial class TitleViewModel : ObservableObject
{
    private readonly IAccountClient _accountClient;

    private readonly IAnimeClient _animeClient;

    private readonly IApplicationLinkService _linkService;

    [ObservableProperty] 
    private Profile _profile;

    public TitleViewModel(
        IAccountClient accountClient,
        IAnimeClient animeClient,
        IApplicationLinkService linkService,
        Profile profile)
    {
        _accountClient = accountClient;
        _animeClient = animeClient;
        _linkService = linkService;
        _profile = profile;
        
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
            if (_profile != null)
            {
                IsTitleInLikes = _profile?.LikedTitles?.Any(likedTitle => likedTitle == _title.Code) ?? false;
            }
        }
    }

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
        if (_profile != null)
        {
            var titleCode = Title.Code;
            var token = Preferences.Default.Get("AccessToken", string.Empty);
            if (_profile.LikedTitles.Contains(titleCode))
            {
                _profile.LikedTitles.Remove(titleCode);
                bool success = await _accountClient.RemoveTitleFromLiked(token, titleCode);

                if (success)
                {
                    var jsonProfile = JsonSerializer.Serialize(_profile);
                    Preferences.Default.Set("Profile", jsonProfile);
                    IsTitleInLikes = false;
                    return true;
                }
            }
            else
            {
                _profile.LikedTitles.Add(titleCode);
                bool success = await _accountClient.AddTitleToLiked(token, titleCode);

                if (success)
                {
                    var jsonProfile = JsonSerializer.Serialize(_profile);
                    Preferences.Default.Set("Profile", jsonProfile);
                    IsTitleInLikes = true;
                    return true;
                }
            }
        }

        return false;
    }

    public async void ShareTitleUrl()
    {
        var link = _linkService.CreateTitleLink(Title);

        await _linkService.ShareText(link);
    }
}