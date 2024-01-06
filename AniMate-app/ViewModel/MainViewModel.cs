using AniMate_app.Model;
using AniMate_app.View;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AniMate_app.ViewModel
{
    class MainViewModel : BindableObject
    {
        public ObservableCollection<Title> _titles;

        public ObservableCollection<String> _genres;


        public ObservableCollection<String> Genres
        {
            get => _genres;
            set
            {
                if (_genres != value)
                {
                    _genres = value;
                    OnPropertyChanged(nameof(Genres));
                }
            }
        }
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
                new Title { Name = "Title1", Description = "Description1sdfjkhifglurfhgoirlgjetrpogjtrofghl;xjkgodl;ghjzdoglrdjg\rio;fgjzrdo;ilgdrjglidzrjrdklifjh\rsklie\fholiejfsdolfjdsoisdjio;hi;udhfksdhisdhd", Image = "https://oimages.anime-pictures.net/230/230c7555d89b8e8643ad2382ba6c688c.jpg?if=ANIME-PICTURES.NET_-_820453-1333x2000-sekiro%3A+shadows+die+twice-from+software-isshin+ashina-genichiro+ashina-emma+the+gentle+blade-great+shinobi+owl.jpg", Genre = new List<string> { "Genre1", "Genre2" } },
                new Title { Name = "Title2", Description = "Description2", Image = "image2.png", Genre = new List<string> { "Genre1", "Genre2", "Genre1", "Genre2", "Genre1", "Genre2"} },
                new Title { Name = "Title3", Description = "Description1sdfjkhifglurfhgoirlgjetrpogjtrofghl;xjkgodl;ghjzdoglrdjg\rio;fgjzrdo;ilgdrjglidzrjrdklifjh\rsklie\fholiejfsdolfjdsoisdjio;hi;udhfksdhisdhd", Image = "https://oimages.anime-pictures.net/230/230c7555d89b8e8643ad2382ba6c688c.jpg?if=ANIME-PICTURES.NET_-_820453-1333x2000-sekiro%3A+shadows+die+twice-from+software-isshin+ashina-genichiro+ashina-emma+the+gentle+blade-great+shinobi+owl.jpg", Genre = new List<string> { "Genre1", "Genre2" } },
                new Title { Name = "Title4", Description = "Description2", Image = "image2.png", Genre = new List<string> { "Genre1", "Genre2", "Genre1", "Genre2", "Genre1", "Genre2"} },
                new Title { Name = "Title5", Description = "Description1sdfjkhifglurfhgoirlgjetrpogjtrofghl;xjkgodl;ghjzdoglrdjg\rio;fgjzrdo;ilgdrjglidzrjrdklifjh\rsklie\fholiejfsdolfjdsoisdjio;hi;udhfksdhisdhd", Image = "https://oimages.anime-pictures.net/230/230c7555d89b8e8643ad2382ba6c688c.jpg?if=ANIME-PICTURES.NET_-_820453-1333x2000-sekiro%3A+shadows+die+twice-from+software-isshin+ashina-genichiro+ashina-emma+the+gentle+blade-great+shinobi+owl.jpg", Genre = new List<string> { "Genre1", "Genre2" } },
                new Title { Name = "Title6", Description = "Description2", Image = "image2.png", Genre = new List<string> { "Genre1", "Genre2", "Genre1", "Genre2", "Genre1", "Genre2"} },
                new Title { Name = "Title7", Description = "Description1sdfjkhifglurfhgoirlgjetrpogjtrofghl;xjkgodl;ghjzdoglrdjg\rio;fgjzrdo;ilgdrjglidzrjrdklifjh\rsklie\fholiejfsdolfjdsoisdjio;hi;udhfksdhisdhd", Image = "https://oimages.anime-pictures.net/230/230c7555d89b8e8643ad2382ba6c688c.jpg?if=ANIME-PICTURES.NET_-_820453-1333x2000-sekiro%3A+shadows+die+twice-from+software-isshin+ashina-genichiro+ashina-emma+the+gentle+blade-great+shinobi+owl.jpg", Genre = new List<string> { "Genre1", "Genre2" } },
                new Title { Name = "Title8", Description = "Description2", Image = "image2.png", Genre = new List<string> { "Genre1", "Genre2", "Genre1", "Genre2", "Genre1", "Genre2"} },
                new Title { Name = "Title9", Description = "Description1sdfjkhifglurfhgoirlgjetrpogjtrofghl;xjkgodl;ghjzdoglrdjg\rio;fgjzrdo;ilgdrjglidzrjrdklifjh\rsklie\fholiejfsdolfjdsoisdjio;hi;udhfksdhisdhd", Image = "https://oimages.anime-pictures.net/230/230c7555d89b8e8643ad2382ba6c688c.jpg?if=ANIME-PICTURES.NET_-_820453-1333x2000-sekiro%3A+shadows+die+twice-from+software-isshin+ashina-genichiro+ashina-emma+the+gentle+blade-great+shinobi+owl.jpg", Genre = new List<string> { "Genre1", "Genre2" } },
                new Title { Name = "Title10", Description = "Description2", Image = "image2.png", Genre = new List<string> { "Genre1", "Genre2", "Genre1", "Genre2", "Genre1", "Genre2"} },
                new Title { Name = "Title11", Description = "Description1sdfjkhifglurfhgoirlgjetrpogjtrofghl;xjkgodl;ghjzdoglrdjg\rio;fgjzrdo;ilgdrjglidzrjrdklifjh\rsklie\fholiejfsdolfjdsoisdjio;hi;udhfksdhisdhd", Image = "https://oimages.anime-pictures.net/230/230c7555d89b8e8643ad2382ba6c688c.jpg?if=ANIME-PICTURES.NET_-_820453-1333x2000-sekiro%3A+shadows+die+twice-from+software-isshin+ashina-genichiro+ashina-emma+the+gentle+blade-great+shinobi+owl.jpg", Genre = new List<string> { "Genre1", "Genre2" } },
                new Title { Name = "Title12", Description = "Description2", Image = "image2.png", Genre = new List<string> { "Genre1", "Genre2", "Genre1", "Genre2", "Genre1", "Genre2"} },
                // Add more items as needed
            };
            Genres = new ObservableCollection<string>
            {
                "Genre1",
                "Genre2",
                "Genre3",
                "Genre4",
                "Genre5",
                "Genre6",
                "Genre7",
                "Genre8",
                "Genre9",
                "Genre10",
                "Genre11",
                "Genre12",
                "Genre13",
                "Genre14",
                "Genre15",
            };
        }
    }
}
