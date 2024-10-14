using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json;
using AniMate_app.DTOs.Account;
using AniMate_app.DTOs.Anime;                                                 
using AniMate_app.Interfaces;

namespace AniMate_app.ViewModels;

[QueryProperty(nameof(TitleDto), "TheTitle")]
public partial class TitleViewModel : ObservableObject
{
    private readonly IAccountClient _accountClient;

    [ObservableProperty] 
    private ProfileDto profile;

    public TitleViewModel(IAccountClient accountClient)
    {
        _accountClient = accountClient;

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

    [ObservableProperty]
    private TitleDto _titleDto;

    [ObservableProperty]
    private string _genres;

    [ObservableProperty]
    private string _shortDescription;

    public async Task<bool> LikesButtonClicked()
    {
        if (Profile != null)
        {
            var titleCode = TitleDto.Code;
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
}

