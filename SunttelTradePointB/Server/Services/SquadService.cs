using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using SunttelTradePointB.Server.Data;
using SunttelTradePointB.Server.Interfaces;
using SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.SquadsMgr;
using System.Data;
using System.Data.SqlClient;

namespace SunttelTradePointB.Server.Services
{

    public class SquadService : ISquadBack
    {

        SunttelDBContext _sunttelDBContext;
        IActorsNodes _actorsNodes;

        public SquadService(SunttelDBContext sunttelDBContext, IActorsNodes actorsNodes)
        {
            _sunttelDBContext = sunttelDBContext;
            _actorsNodes = actorsNodes;



        }


        /// <summary>
        /// Retrieves the Squad by Squad ID
        /// </summary>
        /// <param name="squadId"></param>
        /// <returns></returns>
        public async Task<Squad?> GetSquad(string squadId)
        {
            try
            {
                var squadGuid = Guid.Parse(squadId);
                var response = await _sunttelDBContext.Squads
                .Where(s => s.ID == squadGuid)
                .ToListAsync();

                return response.First();
            }
            catch (Exception ex)
            {
                string errDesc = ex.Message;
                return null;
            }
            
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

        public async Task<(bool IsSuccess, Squad?  squad, string? error)> SaveSquad(string userId, string ipAdress, Squad squad)
        {

            var squadValid = _sunttelDBContext.Squads.Where(s => s.ID == squad.ID);

            if (squadValid.Any())
            {
                _sunttelDBContext.Update(squad);
            }
            else
            {

                EntityActor entityActor = new EntityActor { 
                    Name = squad.Nombre,
                    SkinImageName =squad.SkinImage,
                    TypeOfConcept = new ConceptType
                    {
                        Id = "000000000000000000000002",
                        Name = "COMPANY"

                    }
                };

                var responseEntityCreation = _actorsNodes.SaveEntity(userId, ipAdress, entityActor);

                if (responseEntityCreation != null && responseEntityCreation.Result.IsSuccess)
                {

                    squad.EntityID = responseEntityCreation.Result.entityActorResponse.Id;
                    await _sunttelDBContext.Squads.AddAsync(squad);
                }

                

            }

            _sunttelDBContext.SaveChanges();

            return (true, squad, null);

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
