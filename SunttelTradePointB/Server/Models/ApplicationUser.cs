using Microsoft.AspNetCore.Identity;
using SunttelTradePointB.Shared.Enums;
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
        public string? EntityID { get; set; }

        /// <summary>
        /// Skin Image from the Entity
        /// </summary>
        [NotMapped]
        public string? SkingImage { get; set; }

        public string? DefaultSquadId { get; set; } = "11111111-1111-1111-1111-111111111111";

    }
}
