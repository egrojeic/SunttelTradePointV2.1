using Dapper;
using SunttelTradePointB.Server.Interfaces.DAM_FilePreOders;
using SunttelTradePointB.Server.InterfacesMigration;
using SunttelTradePointB.Server.Services.DAM_FilePreOders;
using SunttelTradePointB.Shared.Common;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;

namespace SunttelTradePointB.Server.ProcessMigration
{
    /// <summary>
    /// 
    /// </summary>
    public class SerDBMigration : ISerDBMigration
    {
        private readonly string _conexionString;

        private readonly ISerDamCustomer _serDamCustomer;



        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public SerDBMigration(IConfiguration configuration, ISerDamCustomer serDamCustomer)
        {
            _serDamCustomer = serDamCustomer;
            _conexionString = configuration.GetConnectionString("SUNTTEL_TRADEPOINT");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<string> MigrateCustomer()
        {
            string res = string.Empty;
            List<EntityActor> lsCustomerId = new List<EntityActor>();
            List<EntityActor> lsCustomerUpdate = new List<EntityActor>();
            try
            {
                lsCustomerId = await ListCustomerToMigrate();

                

                foreach (EntityActor customer in lsCustomerId)
                {

                    int migrado = 0;
                    //migrado = await _serDamCustomer.SearchActorMongo(customer.LegacyId);

                    if(migrado==0)
                    {
                        lsCustomerUpdate.Add(await CustomerAdress(customer));
                    }                    
                }                

                foreach (var item in lsCustomerUpdate)
                {
                    item.Id = String.Empty;
                   var resp = await _serDamCustomer.SaveCustomerMigration(item);
                }

                return "Migracion Finalizada";
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<List<EntityActor>> ListCustomerToMigrate()
        {
            List<EntityActor> ls = new List<EntityActor>();
            try
            {
                using var connection = new SqlConnection(_conexionString);
                var resp = await connection.QueryAsync<EntityActor>("dbo.GetCustomersToNewTP",
                    new { QueryTypeint = 1, LegacyId = 0 },
                    commandType: System.Data.CommandType.StoredProcedure);
                ls = resp.ToList();
                return ls;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<EntityActor> CustomerAdress(EntityActor actor)
        {
            EntityActor objActor = new EntityActor();
            try
            {
                using (SqlConnection conn = new SqlConnection(_conexionString))
                {
                    //using (var multi = await conn.QueryMultipleAsync("dbo.GetCustomersToNewTP",
                    //    new { QueryTypeint = 2, LegacyId = actor.LegacyId },
                    //    commandType: System.Data.CommandType.StoredProcedure))
                    //{
                    //    var phoneNumber = multi.ReadAsync<PhoneNumber>().Result.ToList();
                    //    var address = multi.ReadAsync<Address>().Result.ToList();

                    //    actor.PhoneNumber1 = phoneNumber.Count > 0 ? phoneNumber[0] : null;
                    //    actor.PhoneNumber2 = phoneNumber.Count > 1 ? phoneNumber[1] : null;

                    //    actor.AddressList = address;

                    //}
                }
                return actor;
            }
            catch (Exception)
            {

                throw;
            }

        }




    }
}
