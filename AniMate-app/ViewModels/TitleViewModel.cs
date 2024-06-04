using AniMate_app.Services.AccountService;
using AniMate_app.Services.AnilibriaService;
using AniMate_app.Services.AnilibriaService.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using AniMate_app.Services.AccountService.Dtos;
using System.Text.Json;
using AniMate_app.Views;

namespace AniMate_app.ViewModels
{
    [QueryProperty(nameof(Title), "TheTitle")]
    [QueryProperty(nameof(TitleCode), "TitleCode")]
    public partial class TitleViewModel : ObservableObject
    {
        private readonly AnilibriaService _service;

        private readonly AccountService _accountService;

        [ObservableProperty]
        private ProfileDto profile = null;

        public TitleViewModel(AnilibriaService anilibriaService, AccountService accountService)
        {
            _service = anilibriaService;

            _accountService = accountService;

            string jsonProfile = Preferences.Default.Get("Profile", string.Empty);

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

        private Title _title;
        public Title Title
        {
            get => _title;
            set
            {
                _title = value;
                Genres = string.Join(", ", _title.Genres);
                ShortDescription = string.Join(" ", _title.RuDescription.Split(' ').Take(7));
                OnPropertyChanged(nameof(Title));
                if(Profile != null)
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
            Title = await _service.GetTitleByCode(code);
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
                    bool success = await _accountService.RemoveTitleFromLiked(token, titleCode);

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
                    bool success = await _accountService.AddTitleToLiked(token, titleCode);

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
}
