using AniMate_app.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniMate_app.ViewModel
{
    
    class MainViewModel : BindableObject
    {
        public ObservableCollection<Title> _yourItemsSource;

        public ObservableCollection<Title> YourItemsSource
        {
            get => _yourItemsSource;
            set
            {
                if (_yourItemsSource != value)
                {
                    _yourItemsSource = value;
                    OnPropertyChanged(nameof(YourItemsSource));
                }
            }
        }

        public MainViewModel()
        {
            // Initialize the collection and add some sample data
            YourItemsSource = new ObservableCollection<Title>
            {
                new Title { Name = "Title1", Description = "Description1", Image = "image1.png" },
                new Title { Name = "Title2", Description = "Description2", Image = "image2.png" },
                // Add more items as needed
            };
        }
    }
}
