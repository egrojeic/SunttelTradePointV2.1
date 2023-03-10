using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SunttelTradePointB.Shared.Models
{
    /// <summary>
    /// User class
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Associated Entity ID
        /// </summary>
        [NotMapped]
        public string? EntityID { get; set; }

        /// <summary>
        /// Skin Image from the Entity
        /// </summary>
        [NotMapped]
        public string? SkingImage { get; set; }

    }
}
