using System.ComponentModel.DataAnnotations;

namespace delivery_server_api.Models.ApplicationUser
{
    public class FoodUserFormModel
    {
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
