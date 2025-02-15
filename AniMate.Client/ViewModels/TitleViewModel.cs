﻿using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json;
using AniMate_app.DTOs.Account;
using AniMate_app.DTOs.Anime;                                                 
using AniMate_app.Interfaces;

namespace AniMate_app.ViewModels;

[QueryProperty(nameof(TitleCode), "TitleCode")]
[QueryProperty(nameof(Title), "TheTitle")]
public partial class TitleViewModel : ObservableObject
{
    private readonly IAccountClient _accountClient;

    private readonly IAnimeClient _animeClient;

    private readonly IApplicationLinkService _linkService;

    [ObservableProperty] 
    private ProfileDto profile;

    public TitleViewModel(IAccountClient accountClient, IAnimeClient animeClient, IApplicationLinkService linkService)
    {
        _accountClient = accountClient;

        _animeClient = animeClient;

        _linkService = linkService;

        var jsonProfile = Preferences.Default.Get("Profile", string.Empty);

        if (!string.IsNullOrEmpty(jsonProfile))
        {
            Profile = JsonSerializer.Deserialize<ProfileDto>(jsonProfile);
        }
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
            if (Profile != null)
            {
                IsTitleInLikes = Profile?.LikedTitles?.Any(likedTitle => likedTitle == _title.Code) ?? false;
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
        if (Profile != null)
        {
            var titleCode = Title.Code;
            var token = Preferences.Default.Get("AccessToken", string.Empty);
            if (Profile.LikedTitles.Contains(titleCode))
            {
                Profile.LikedTitles.Remove(titleCode);
                bool success = await _accountClient.RemoveTitleFromLiked(token, titleCode);

                if (success)
                {
                    var jsonProfile = JsonSerializer.Serialize(Profile);
                    Preferences.Default.Set("Profile", jsonProfile);
                    IsTitleInLikes = false;
                    return true;
                }
            }
            else
            {
                Profile.LikedTitles.Add(titleCode);
                bool success = await _accountClient.AddTitleToLiked(token, titleCode);

                if (success)
                {
                    var jsonProfile = JsonSerializer.Serialize(Profile);
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