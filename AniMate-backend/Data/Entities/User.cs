using System.ComponentModel.DataAnnotations;

namespace AniMate_backend.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Bio { get; set; }
        public string? Image { get; set; }

        [Required]
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        //public List<string> LikedTitles { get; set; }
        //public List<string> WatchHistory { get; set; }
        //public List<string> Achievements { get; set; }
    }
}
