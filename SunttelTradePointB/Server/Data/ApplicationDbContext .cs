
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SunttelTradePointB.Shared.Models;
using SunttelTradePointB.Shared.Security;

namespace SunttelTradePointB.Server.Data
{
    public class ApplicationDbContext : 
        IdentityDbContext<ApplicationUser, UserRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }



    }
}
