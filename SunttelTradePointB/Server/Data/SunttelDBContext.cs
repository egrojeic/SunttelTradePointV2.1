using Microsoft.EntityFrameworkCore;
using SunttelTradePointB.Shared.SquadsMgr;

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

        public virtual DbSet<RolesSystemTools> RolesSystemTools { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RolesSystemTools>()
                .HasKey(rt => new { rt.RoleId, rt.SystemToolId });

            modelBuilder.Entity<RolesSystemTools>()
                .HasOne(rt => rt.SystemTool)
                .WithMany()
                .HasForeignKey(rt => rt.SystemToolId);
        }

    }
}
