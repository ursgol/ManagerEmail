using System.ComponentModel.DataAnnotations;

namespace ManagerEmail.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
