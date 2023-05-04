using SunttelTradePointB.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace SunttelTradePointB.Shared.Security
{
    public class RegisterRequest
    {
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match!")]
        public string PasswordConfirm { get; set; }

        [Required]
        public string EntityId { get; set; }

        public List<UserRole> Roles { get; set; }

        public string Email { get; set; }

    }
}
