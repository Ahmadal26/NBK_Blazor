using System.ComponentModel.DataAnnotations;

namespace NBK_API.Models.Entites.Users
{

    public class Customer
    {
        [Key]
        public int CustomerNumber { get; set; }

        [Required]
        [MaxLength(255)]
        public string CustomerName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(1)]
        public string Gender { get; set; }

    }
    public class CustomerReq
    {
        public string CustomerName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

    }
}

