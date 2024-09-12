using System;
using System.Collections.Generic;
using AniMate_app.ViewModels;
using Microsoft.Maui.Controls;

namespace AniMate_app.Views;

public partial class GenrePage : ContentPage
{
    private readonly GenreViewModel viewModel;

    private bool _isOpeningPlayer = false;

    public GenrePage()
    {
        InitializeComponent();

        BindingContext = viewModel = new GenreViewModel();
    }

    private async void TitleSelected(object sender, SelectionChangedEventArgs e)
    {
        var collectionView = sender as CollectionView;

        if (_isOpeningPlayer)
        {
            collectionView.SelectedItem = null;

            return;
        }

        _isOpeningPlayer = true;

        if (collectionView.SelectedItem != null)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                {"TheTitle", collectionView.SelectedItem}
            };

            await Shell.Current.GoToAsync($"TitlePage", navigationParameter);

            collectionView.SelectedItem = null;

            _isOpeningPlayer = false;
        }
    }

    private async void OnAppearing(object sender, EventArgs e)
    {
        await viewModel.LoadMoreContent();
    }
}