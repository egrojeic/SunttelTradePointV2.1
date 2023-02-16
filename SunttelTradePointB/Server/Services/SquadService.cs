using Microsoft.EntityFrameworkCore;
using SunttelTradePointB.Server.Data;
using SunttelTradePointB.Server.Interfaces;
using SunttelTradePointB.Shared.Squad;

namespace SunttelTradePointB.Server.Services
{

    public class SquadService : ISquad
    {

        SunttelDBContext _sunttelDBContext;

        public SquadService(SunttelDBContext sunttelDBContext)
        {
            _sunttelDBContext = sunttelDBContext;
        }
        public async Task<Squad> SquadInfo()
        {
            var squadInfo = await _sunttelDBContext.Squads.FindAsync(Guid.Parse("7B66BBE8-C288-4BAD-8DB7-3DAA32108FED"));
            return squadInfo;
        }

        public async Task<List<SystemTool>> SystemToolsByUser(Guid userId)
        {
            var systemTools = await _sunttelDBContext.SystemTools
                .Include(s=>s.ToolSetsNavigation)
                .ToListAsync();
                                    
            return systemTools;
        }
    }
}
