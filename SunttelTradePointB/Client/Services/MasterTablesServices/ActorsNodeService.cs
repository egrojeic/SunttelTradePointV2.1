using SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Client.Shared.EntityShareComponents.EntitySubComponents;
using SunttelTradePointB.Shared.Common;
using System.Net;
using System.Net.Http.Json;


namespace SunttelTradePointB.Client.Services.MasterTablesServices
{
    public class ActorsNodeService : IEntityNodes
    {

        private readonly HttpClient _httpClient;

        List<EntityActor>? entityNodesList;

        public ActorsNodeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public Task<EntityActor> CreateNewEntityActor(EntityActor entityActor)
        {

            throw new NotImplementedException();

        }

        /// <summary>
        /// Returns the list of possible Roles for Entities
        /// </summary>
        /// <returns></returns>
        public async Task<List<EntityRole>> EntityRolesList()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<EntityRole>>($"api/EntityNodesMaintenance/EntityRolesListSelector");

                if (response != null)
                {
                    return response;
                }
                else {
#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo.
                    return null;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo.
                }

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo.
                return null;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo.
            }
        }

        /// <summary>
        /// Retrieves a list of address of an specific Actor
        /// </summary>
        /// <param name="entityActorId"></param>
        /// <returns></returns>
        public async Task<List<Address>> GetEntityActorAddressList(string entityActorId)
        {
            try
            {
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                List<Address>  entityNodesAddressList = await _httpClient.GetFromJsonAsync<List<Address>>($"api/EntityNodesMaintenance/GetEntityDetailsAddressList?EntityId={entityActorId}");
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo.
                return entityNodesAddressList;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo.

                //if (entityNodesList != null)
                //    return entityNodesList.FindAll(c => c.EntityFace.Name.Contains(namteToFind)).ToList();
                //else
                //    return null;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo.
                return null;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo.
            }
        }

        public Task<EntityActor> GetEntityActorById(string entityActorId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves a table of Entities with with their main fields
        /// </summary>
        /// <param name="nameLike"></param>
        /// <param name="entityType"></param>
        /// <param name="entityCode"></param>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public async Task<List<EntityActor>> GetEntityActorFaceList(string? nameLike = null, string? entityType = null, string? entityCode = null, bool forceRefresh = false)
        {
           
            string namteToFind = nameLike !=null ? nameLike:"";

            try
            {
                entityNodesList = await _httpClient.GetFromJsonAsync<List<EntityActor>>($"api/EntityNodesMaintenance/GetEntityNodes?filterName={namteToFind}");
#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo.
                return entityNodesList;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo.

                //if (entityNodesList != null)
                //    return entityNodesList.FindAll(c => c.EntityFace.Name.Contains(namteToFind)).ToList();
                //else
                //    return null;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo.
                return null;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo.
            }
        }

        /// <summary>
        /// Retrieves a particular Details Table of an Entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityActorId"></param>
        /// <param name="entityDetailsSection"></param>
        /// <returns></returns>
        public async Task<List<T>> GetEntityDetailsOf<T>(string entityActorId, EntityDetailsSection entityDetailsSection)
        {
#pragma warning disable CS0472 // El resultado de la expresión siempre es 'true' porque un valor del tipo 'EntityDetailsSection' nunca es igual a 'NULL' de tipo 'EntityDetailsSection?'
            string patternToFind = entityDetailsSection != null ? entityDetailsSection.ToString() : "";
#pragma warning restore CS0472 // El resultado de la expresión siempre es 'true' porque un valor del tipo 'EntityDetailsSection' nunca es igual a 'NULL' de tipo 'EntityDetailsSection?'

            try
            {
                var entityNodesList = await _httpClient.GetFromJsonAsync<List<T>>($"api/EntityNodesMaintenance/GetEntityDetails{entityDetailsSection.ToString()}?filterName={patternToFind}");
#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo.
                return entityNodesList;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo.

                //if (entityNodesList != null)
                //    return entityNodesList.FindAll(c => c.EntityFace.Name.Contains(namteToFind)).ToList();
                //else
                //    return null;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo.
                return null;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo.
            }
        }

        public Task<EntityActor> UpdateEntityActor(EntityActor entityActor)
        {
            throw new NotImplementedException();
        }

        public async Task<List<IdentificationEntity>> GetEntityIdentifierList(string entityId)
        {
            try
            {
                var entityIdentifcationList = await _httpClient.GetFromJsonAsync<List<IdentificationEntity>>($"/api/EntityNodesMaintenance/GetEntityDetailsIdentifiersList?EntityId={entityId}");
                return entityIdentifcationList;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<PhoneNumber>> GetPhoneNumber(string entityActorId)
        {
            try
            {
                var entityPhoneList = await _httpClient.GetFromJsonAsync<List<PhoneNumber>>($"api/EntityNodesMaintenance/GetEntityDetailsPhoneDirectory?EntityId={entityActorId}");
                return entityPhoneList;

                //if (entityNodesList != null)
                //    return entityNodesList.FindAll(c => c.EntityFace.Name.Contains(namteToFind)).ToList();
                //else
                //    return null;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<EntityActor> GetEntityActor(string entityActorId)
        {
            try
            {
                var actor = await _httpClient.GetFromJsonAsync<EntityActor>($"/api/EntityNodesMaintenance/GetEntityActorById?entityActorId={entityActorId}");
                Console.WriteLine(actor);
                return actor;

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        //Get groups 
        public async Task<List<ConceptGroup>> GetEntityGroups(string filterCondition)
        {
            try
            {
                List<ConceptGroup> groups = await _httpClient.GetFromJsonAsync<List<ConceptGroup>>($"api/ConceptsSelector/GetSelectorListEntityGroups?filterCondition={filterCondition}");
                return groups.Take(10).ToList();
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<EntityGroup> GetGroupById(string groupId)
        {
            try
            {
                var group = await _httpClient.GetFromJsonAsync<EntityGroup>($"/api/EntityNodesMaintenance/GetEntityGroup?entityGroupId={groupId}");
                return group;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<IdentificationType>> GetDetailsIdentifiers(string entityActorId)
        {
            try
            {
                var detailsList = await _httpClient.GetFromJsonAsync<List<IdentificationType>>($"api/EntityNodesMaintenance/GetEntityDetailsIdentifiersList?EntityId={entityActorId}");
                return detailsList;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<IdentificationType>> GetIdentificationTypes()
        {
            try
            {
                var identificationTypes = await _httpClient.GetFromJsonAsync<List<IdentificationType>>($"/api/ConceptsSelector/GetSelectorListIdentificationTypes");
                return identificationTypes;
            }
            catch (Exception ex)
            {
                string errMesaage = ex.Message;
                return null;
            }
        }

        public async Task<AtomConcept> GetIdentificationTypeById(string identificationId)
        {
            try
            {
                var identificationType = await _httpClient.GetFromJsonAsync<AtomConcept>($"/api/EntityActorsRelatedConcepts/GetIdentificationType?identicationTypeId={identificationId}");
                return identificationType;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<EntityGroup>> GetListEntityGroups(string filterGroup)
        {
            try
            {
                var listGroups = await _httpClient.GetFromJsonAsync<List<EntityGroup>>($"/api/ConceptsSelector/GetSelectorListEntityGroups?filterCondition={filterGroup}");
                return listGroups;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<EntityRole>> GetListEntityRoles(string filterGroup)
        {
            try
            {
                var listRoles = await _httpClient.GetFromJsonAsync<List<EntityRole>>($"/api/ConceptsSelector/GetSelectorListEntityRoles?filterCondition={filterGroup}");
                return listRoles;
            }
            catch (Exception ex)
            {
                string Message = ex.Message;
                return null;
            }
        }

        public async Task<EntityRole> GetRoleById(string roleId)
        {
            try
            {
                var roleList = await _httpClient.GetFromJsonAsync<EntityRole>($"/api/EntityActorsRelatedConcepts/GetEntityRole?entityRoleId={roleId}");
                return roleList;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<AtomConcept>> GetListElectronicAddress()
        {
            try
            {
                var listElectronicAddress = await _httpClient.GetFromJsonAsync<List<AtomConcept>>("/api/ConceptsSelector/GetSelectorElectronicAddressEntities");
                return listElectronicAddress;
            }
            catch (Exception ex)
            {
                string exMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<Address>> GetEntityDetailsAddressList(string entityId)
        {
            try
            {
                var entityAddress = await _httpClient.GetFromJsonAsync<List<Address>>($"/api/EntityNodesMaintenance/GetEntityDetailsAddressList?EntityId={entityId}");
                return entityAddress;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<ElectronicAddress>> GetElectronicAddressList(string entityActorId)
        {
            try
            {
                var electronicList = await _httpClient.GetFromJsonAsync<List<ElectronicAddress>>($"/api/EntityNodesMaintenance/GetElectronicAddressById?electronicAddressId={entityActorId}");
                return electronicList;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<ShippingInfo>> GetShippingInfos(string shippingId)
        {
            try
            {
                var shippingInfoList = await _httpClient.GetFromJsonAsync<List<ShippingInfo>>($"/api/EntityNodesMaintenance/GetShippingSetupById?shippingInfoId={shippingId}");
                return shippingInfoList;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<EntityComercialConditions>> GetCommercialConditions(string commercialConditionId)
        {
            try
            {
                var commercialConditionsList = await _httpClient.GetFromJsonAsync<List<EntityComercialConditions>>($"/api/EntityNodesMaintenance/GetEntitiesCommercialRelationShipById?entitiesCommercialRelationShipId={commercialConditionId}");
                return commercialConditionsList;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<Warehouse>> GetWarehouseList(string filterWarehouse)
        {
            try
            {
                var listWarehouse = await _httpClient.GetFromJsonAsync<List<Warehouse>>($"/api/GeographicPlaces/GetWarehouses?nameLike={filterWarehouse}");
                return listWarehouse;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task <List<EntitiyRelationshipType>> GetEntityRelationType()
        {
            try
            {
                var listRelationType = await _httpClient.GetFromJsonAsync<List<EntitiyRelationshipType>>("/api/ConceptsSelector/GetSelectorEntitiyRelationshipTypes");
                return listRelationType;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        ///
        /// Save EntityAddress
        ///

        public async Task SaveEntity(string EntityActorId, EntityActor entityActor)
        {
            await _httpClient.PostAsJsonAsync($"/api/EntityNodesMaintenance/SaveEntity?entityActorId={EntityActorId}", entityActor);
        }


        public async Task SaveEntityAddress(string EntityActorId, Address address)
        {
            await _httpClient.PostAsJsonAsync($"/api/EntityNodesMaintenance/SaveEntityAddress?entityActorId={EntityActorId}", address);
        }

        public async Task SavePhone(string EntityActorId, PhoneNumber phoneNumber)
        {
            try
            {
                await _httpClient.PostAsJsonAsync($"/api/EntityNodesMaintenance/SavePhone?entityActorId={EntityActorId}", phoneNumber);
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
            }
        }

        public async Task SaveIdentificationCode(string EntityActorId, IdentificationEntity identificationEntity)
        {
            await _httpClient.PostAsJsonAsync($"/api/EntityNodesMaintenance/SaveIdentificationCode?entityActorId={EntityActorId}", identificationEntity);
        }

        public async Task SaveEntityGroup(string entityGroupId, EntityGroup entityGroup)
        {
            await _httpClient.PostAsJsonAsync($"/api/EntityNodesMaintenance/SaveEntityGroup?entityGroupId={entityGroupId}", entityGroup);
        }

        public async Task SaveElectronicAddress(string electronicAddressId, ElectronicAddress electronicAddress)
        {
            await _httpClient.PostAsJsonAsync($"/api/EntityNodesMaintenance/SaveElectronicAddress?entityActorId={electronicAddressId}", electronicAddress);
        }

        public async Task SaveShippingSetup(string shippinSetupId, ShippingInfo shippingInfo)
        {
            await _httpClient.PostAsJsonAsync($"/api/EntityNodesMaintenance/SaveShippingSetup?entityActorId={shippinSetupId}", shippingInfo);
        }

        public async Task SaveCommercialConditions(string commercialConditionId, EntitiesCommercialRelationShip comercialConditions)
        {
            await _httpClient.PostAsJsonAsync($"/api/EntityNodesMaintenance/SaveShippingSetup?entityActorId={commercialConditionId}", comercialConditions);
        }

        public async Task SaveEntityRole(string roleId, EntityRole entityRole)
        {
            await _httpClient.PostAsJsonAsync($"/api/EntityActorsRelatedConcepts/SaveEntityRole?entityRoleId={roleId}", entityRole);
        }
    }
}
