using Microsoft.EntityFrameworkCore;
using SunttelTradePointB.Shared.Squad;

namespace SunttelTradePointB.Server.Data
{
    public class SunttelDBContext : ApplicationDbContext
    {
        public SunttelDBContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Squad> Squads { get; set; }
        public virtual DbSet<SystemTool> SystemTools { get; set; }

        public virtual DbSet<ToolSet> ToolSets { get; set; }
    }
}
