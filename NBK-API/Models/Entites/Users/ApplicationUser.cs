using System.ComponentModel.DataAnnotations;

namespace NBK_API.Models.Entites.Users
{

    public class ApplicationUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}