using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.Json;
using AniMate_app.DTOs.Account;
using AniMate_app.DTOs.Anime;                                                 
using AniMate_app.Interfaces;

namespace AniMate_app.ViewModels
{
    [QueryProperty(nameof(TitleDto), "TheTitle")]
    [QueryProperty(nameof(TitleCode), "TitleCode")]
    public partial class TitleViewModel : ObservableObject
    {
        private readonly IAnimeClient _animeClient;
        private readonly IAccountClient _accountClient;

        [ObservableProperty]
        private ProfileDto profile = null;

        public TitleViewModel(IAnimeClient animeClient, IAccountClient accountClient)
        {
            _animeClient = animeClient;
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

        private TitleDto _titleDto;
        public TitleDto TitleDto
        {
            get => _titleDto;
            set
            {
                _titleDto = value;
                Genres = string.Join(", ", _titleDto.Genres);
                ShortDescription = string.Join(" ", _titleDto.RuDescription.Split(' ').Take(7));
                OnPropertyChanged(nameof(TitleDto));
                if(Profile != null)
                {
                    IsTitleInLikes = Profile?.LikedTitles?.Any(likedTitle => likedTitle == _titleDto.Code) ?? false;
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
            TitleDto = await _animeClient.GetTitleByCode(code);
        }

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

        public async Task<bool> WatchButtonClicked()
        {
            if (Profile != null)
            {
                var titleCode = TitleDto.Code;
                var token = Preferences.Default.Get("AccessToken", string.Empty);
                if (Profile.WatchedTitles.Contains(titleCode))
                {
                    return true;
                }
                else
                {
                    Profile.WatchedTitles.Add(titleCode);
                    bool success = await _accountClient.AddTitleToHistory(token, titleCode);

                    if (success)
                    {
                        var jsonProfile = JsonSerializer.Serialize(Profile);
                        Preferences.Default.Set("Profile", jsonProfile);
                        return true;
                    }
                }
            }
            return false;
            
        }
    }
}
