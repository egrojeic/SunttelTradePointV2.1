using System.Data.SqlClient;
using Dapper;
using System.Linq.Expressions;
using System.Diagnostics.Metrics;
using SunttelTradePointB.Shared.FilePreOders;
using SunttelTradePointB.Shared.Common;
using System.Collections.Generic;
using MongoDB.Driver;

namespace MigrationProcess.Server.ServiceSQL
{
    public class SerCustomer : ISerCustomer
    {
        private readonly string _conexionString;
        private readonly string SERVER_MONGO;
        private readonly string DATABASE_MONGO;
        public SerCustomer(IConfiguration configuration)
        {
            _conexionString = configuration.GetConnectionString("AdminConnection");
            SERVER_MONGO = configuration["SrvMongo"];
            DATABASE_MONGO = configuration["DatabaseMongo"];
        }

        public async Task<string> Migrate()
        {
            string res = string.Empty;
            List<EntityActor> lsCustomerId = new List<EntityActor>(); 
            try
            {
                lsCustomerId = await ListCustomerToMigrate();
                //foreach (var customer in lsCustomerId)
                //{
                //    EntityActor myCustomer = new EntityActor();
                //    myCustomer = await QueryCustomer(customer);

                //    if(myCustomer.LegacyId > 0)
                //    {
                //        SaveCustormerInMongo(myCustomer);
                //    }
                //}
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void  SaveCustormerInMongo(EntityActor entityActor)
        {
            try
            {
                var clientMongo = new MongoClient(SERVER_MONGO);
                var database = clientMongo.GetDatabase(DATABASE_MONGO);


                var collectionEntityActor = database.GetCollection<EntityActor>("EntityActor");
                collectionEntityActor.InsertOne(entityActor);

            }
            catch (Exception)
            {
                throw;
            }
        }


        private async Task<EntityActor> QueryCustomer(int id)
        {
            EntityActor customer = new EntityActor();
            try
            {
                using (SqlConnection conn = new SqlConnection(_conexionString))
                {
                    List<IdentificationEntity> lsIdentificationEntity = new List<IdentificationEntity>();
                    List<Address> lsAddress = new List<Address>();
                    
                    using (var multi = await conn.QueryMultipleAsync(
                        "dbo.GetCustomersToNewTP", 
                        new { Id = id }, 
                        commandType: System.Data.CommandType.StoredProcedure))
                    {
                        var objActor = multi.ReadAsync<EntityActor>().Result.ToList();
                        var identification = multi.ReadAsync<IdentificationEntity>().Result.ToList();
                        var adress = multi.ReadAsync<Address>().Result.ToList();

                        customer = objActor.ToList().FirstOrDefault()!;
                        customer.AddressList = adress;
                        customer.Identifications = identification;
                    }
                }
                return customer;
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
                    new { QueryTypeint = 1, LegacyId = 0}, 
                    commandType: System.Data.CommandType.StoredProcedure);
                ls = resp.ToList();
                return ls;
            }
            catch (Exception)
            {
                throw;
            }
        }






    }
}
