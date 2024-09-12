using System;
using CommunityToolkit.Maui.Views;

namespace AniMate_app.Views.Components;

public partial class FilterPopUp : Popup
{
    public FilterPopUp()
	{
		InitializeComponent();
    }

    void OnCancelButtonClicked(object sender, EventArgs args)
    {
        Close();
    }

    void OnApplyButtonClicked(object sender, EventArgs args)
    {
        
    }
}