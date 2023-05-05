using System.ComponentModel.DataAnnotations;

namespace delivery_server_api.Models.ApplicationUser
{
    public class UserSignUpModel
    {
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string Addres { get; set; }
    }
}
