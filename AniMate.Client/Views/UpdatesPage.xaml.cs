using System;
using System.Collections.Generic;
using AniMate_app.ViewModels;
using Microsoft.Maui.Controls;

namespace AniMate_app.Views;

public partial class UpdatesPage : ContentPage
{
	private UpdatesViewModel _viewModel;

	private bool _isFirstLoad = true;
    private bool _isOpeningPlayer;

    public UpdatesPage(UpdatesViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = _viewModel = viewModel;
	}

    private async void ContentPage_Loaded(object sender, EventArgs e)
    {
        if (_isFirstLoad)
            await _viewModel.LoadContent();

		_isFirstLoad = false;
    }

    private async void TitleSelected(object sender, SelectionChangedEventArgs e)
    {
        var collection = sender as CollectionView;

        if (_isOpeningPlayer)
        {
            collection.SelectedItem = null;

            return;
        }

        _isOpeningPlayer = true;

        var navigationParameter = new Dictionary<string, object>
            {
                {"TheTitle", collection.SelectedItem}
            };

        await Shell.Current.GoToAsync($"TitlePage", navigationParameter);

        collection.SelectedItem = null;

        _isOpeningPlayer = false;
    }
}