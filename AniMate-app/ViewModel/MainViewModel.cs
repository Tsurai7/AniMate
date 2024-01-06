using AniMate_app.Model;
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
        public ObservableCollection<Title> _titles;

        public ObservableCollection<Title> Titles
        {
            get => _titles;
            set
            {
                if (_titles != value)
                {
                    _titles = value;
                    OnPropertyChanged(nameof(Titles));
                }
            }
        }

        public MainViewModel()
        {
            // Initialize the collection and add some sample data
            Titles = new ObservableCollection<Title>
            {
                new Title { Name = "Title1", Description = "Description1", Image = "image1.png" },
                new Title { Name = "Title2", Description = "Description2", Image = "image2.png" },
                // Add more items as needed
            };
        }
    }
}
