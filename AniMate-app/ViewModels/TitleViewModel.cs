using AniMate_app.Services.AccountService;
using AniMate_app.Services.AnilibriaService;
using AniMate_app.Services.AnilibriaService.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using AniMate_app.Services.AccountService.Dtos;
using System.Text.Json;

namespace AniMate_app.ViewModels
{
    [QueryProperty(nameof(Title), "TheTitle")]
    [QueryProperty(nameof(TitleCode), "TitleCode")]
    public partial class TitleViewModel : ObservableObject
    {
        private readonly AnilibriaService _service;

        private readonly AccountService _accountService;

        private ProfileDto _profileDto;

        public TitleViewModel(AnilibriaService anilibriaService, AccountService accountService)
        {
            _service = anilibriaService;

            _accountService = accountService;

            string jsonProfile = Preferences.Default.Get("Profile", string.Empty);
            ProfileDto profileDto = null;

            if (!string.IsNullOrEmpty(jsonProfile))
            {
                _profileDto = JsonSerializer.Deserialize<ProfileDto>(jsonProfile);
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

                IsTitleInLikes = _profileDto?.LikedTitles?.Any(likedTitle => likedTitle == _title.Id) ?? false;
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
            var token = Preferences.Default.Get("AccessToken", string.Empty);
            var titleCode = Title.Id;

            if (_profileDto != null)
            {
                if (_profileDto.LikedTitles.Contains(titleCode))
                {
                    _profileDto.LikedTitles.Remove(titleCode);
                    bool success = await _accountService.RemoveTitleFromLiked(token, titleCode);

                    if (success)
                    {
                        var jsonProfile = JsonSerializer.Serialize(_profileDto);
                        Preferences.Default.Set("Profile", jsonProfile);
                        IsTitleInLikes = false;
                        return true;
                    }
                }
                else
                {
                    _profileDto.LikedTitles.Add(titleCode);
                    bool success = await _accountService.AddTitleToLiked(token, titleCode);

                    if (success)
                    {
                        var jsonProfile = JsonSerializer.Serialize(_profileDto);
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
