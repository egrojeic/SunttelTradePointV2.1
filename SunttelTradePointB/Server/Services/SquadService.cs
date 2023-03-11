using Microsoft.EntityFrameworkCore;
using SunttelTradePointB.Server.Data;
using SunttelTradePointB.Server.Interfaces;
using SunttelTradePointB.Shared.SquadsMgr;
using System.Data;
using System.Data.SqlClient;

namespace SunttelTradePointB.Server.Services
{

    public class SquadService : ISquadBack
    {

        SunttelDBContext _sunttelDBContext;

        public SquadService(SunttelDBContext sunttelDBContext)
        {
            _sunttelDBContext = sunttelDBContext;
        }


        /// <summary>
        /// Retrieves the Squad ID by Squad Name
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="SquadName"></param>
        /// <returns></returns>
        public async Task<string> GetSquadIdByName(Guid userId, string SquadName)
        {
            var squadId = await _sunttelDBContext.Squads
                .Where(s => s.Nombre == SquadName)
                .Select(s => s.ID)
                .FirstOrDefaultAsync();

           
            return squadId.ToString();
        }
      

        public async Task<List<SquadsByUser>> SquadInfo(string? userId)
        {
            //var squadInfo = await _sunttelDBContext.Squads.FindAsync(Guid.Parse("7B66BBE8-C288-4BAD-8DB7-3DAA32108FED"));

            string connectionString = _sunttelDBContext.Database.GetConnectionString();
            string storedProcedureName = "GetSquadsByUser";

            List<SquadsByUser> squadsByUserList = new List<SquadsByUser>();

            if(userId == null)
            {
                return squadsByUserList;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(storedProcedureName, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@IDAspNetUsers", userId));

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        SquadsByUser squadByUser = new SquadsByUser();

                        squadByUser.IDSquads = Guid.Parse(reader["IDSquads"].ToString());
                        squadByUser.SquadName = Convert.ToString(reader["SquadName"]);
                        squadByUser.SkinImage = reader["SkinImage"].ToString();

                        squadsByUserList.Add(squadByUser);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           


            return squadsByUserList;
        }



        /// <summary>
        /// Retrieves the list of tools for a given user and squad
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="squadId"></param>
        /// <returns></returns>
        public async Task<List<SystemTool>> SystemToolsByUser(Guid userId, string? squadId = "")
        {
            var systemTools = await _sunttelDBContext.SystemTools
                .Include(s => s.ToolSetsNavigation)
                .ToListAsync();

            return systemTools;
        }


    }
}
