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
        EntityActor? tempEntityActor { get; set; }
        Address? tempAddress { get; set; }

        public string Host { get { return "https://localhost:7186/uploads/entityImages"; } }

        public enum UploadingFileTyoe
        {
            ActorItemImage,
            EntityImage
        }

        public ActorsNodeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == "")
                    ipAddress = "127.0.0.0";
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                List<Address>  entityNodesAddressList = await _httpClient.GetFromJsonAsync<List<Address>>($"/api/EntityNodesMaintenance/GetEntityDetailsAddressList?userId={userId}&ipAdress={ipAddress}&EntityId={entityActorId}");
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

        /// <summary>
        /// Retrieves a table of Entities with with their main fields
        /// </summary>
        /// <param name="nameLike"></param>
        /// <param name="entityType"></param>
        /// <param name="entityCode"></param>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public async Task<List<EntityActor>> GetEntityActorFaceList(string? nameLike = null)
        {
           
            string nameToFind = nameLike !=null ? nameLike:"";
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == "")
                    ipAddress = "127.0.0.0";
                entityNodesList = await _httpClient.GetFromJsonAsync<List<EntityActor>>($"/api/EntityNodesMaintenance/GetEntityNodes?userId={userId}&ipAdress={ipAddress}&filterName={nameToFind}");
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
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == "")
                    ipAddress = "127.0.0.0";
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
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == "")
                    ipAddress = "127.0.0.0";
                var entityIdentifcationList = await _httpClient.GetFromJsonAsync<List<IdentificationEntity>>($"/api/EntityNodesMaintenance/GetEntityDetailsIdentifiersList?userId={userId}&ipAdress={ipAddress}&EntityId={entityId}");
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
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == "")
                    ipAddress = "127.0.0.0";
                var entityPhoneList = await _httpClient.GetFromJsonAsync<List<PhoneNumber>>($"/api/EntityNodesMaintenance/GetEntityDetailsPhoneDirectory?userId={userId}&ipAdress={ipAddress}&EntityId={entityActorId}");
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
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == "")
                    ipAddress = "127.0.0.0";
                var actor = await _httpClient.GetFromJsonAsync<EntityActor>($"/api/EntityNodesMaintenance/GetEntityActorById?userId={userId}&ipAdress={ipAddress}&entityActorId={entityActorId}");
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
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == "")
                    ipAddress = "127.0.0.0";
                //List<ConceptGroup>? groups = await _httpClient.GetFromJsonAsync<List<ConceptGroup>>($"/api/EntityNodesMaintenance/GetEntityGroups?userId={userId}&ipAdress={ipAddress}&filterCondition={filterCondition}");
                List<ConceptGroup>? groups = await _httpClient.GetFromJsonAsync<List<ConceptGroup>>($"/api/ConceptsSelector/GetSelectorListEntityGroups?filterCondition={filterCondition}");
                return groups;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<EntityGroup> GetEntityGroup(string groupId)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == "")
                    ipAddress = "127.0.0.0";
                var group = await _httpClient.GetFromJsonAsync<EntityGroup>($"/api/EntityNodesMaintenance/GetEntityGroup?userId={userId}&ipAdress={ipAddress}&entityGroupId={groupId}");
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
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == "")
                    ipAddress = "127.0.0.0";
                var detailsList = await _httpClient.GetFromJsonAsync<List<IdentificationType>>($"/api/EntityNodesMaintenance/GetEntityDetailsIdentifiersList?userId={userId}&ipAdress={ipAddress}&EntityId={entityActorId}");
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
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == "")
                    ipAddress = "127.0.0.0";
                var identificationType = await _httpClient.GetFromJsonAsync<AtomConcept>($"/api/EntityActorsRelatedConcepts/GetIdentificationType?userId={userId}&ipAdress={ipAddress}&identicationTypeId={identificationId}");
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
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == "")
                    ipAddress = "127.0.0.0";    
                var listGroups = await _httpClient.GetFromJsonAsync<List<EntityGroup>>($"/api/EntityNodesMaintenance/GetEntityGroups?userId={userId}&ipAdress={ipAddress}&filterCondition={filterGroup}");
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
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == "")
                    ipAddress = "127.0.0.0";
                var roleList = await _httpClient.GetFromJsonAsync<EntityRole>($"/api/EntityActorsRelatedConcepts/GetEntityRole?userId={userId}&ipAdress={ipAddress}&entityRoleId={roleId}");
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
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == "")
                    ipAddress = "127.0.0.0";
                var entityAddress = await _httpClient.GetFromJsonAsync<List<Address>>($"/api/EntityNodesMaintenance/GetEntityDetailsAddressList?userId={userId}&ipAdress={ipAddress}&EntityId={entityId}");
                return entityAddress;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<ElectronicAddress> GetElectronicAddressById(string electronicAddressId)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == "")
                    ipAddress = "127.0.0.0";
                var electronicList = await _httpClient.GetFromJsonAsync<ElectronicAddress>($"/api/EntityNodesMaintenance/GetElectronicAddressById?userId={userId}&ipAdress={ipAddress}&electronicAddressId={electronicAddressId}");
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
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == "")
                    ipAddress = "127.0.0.0";
                var shippingInfoList = await _httpClient.GetFromJsonAsync<List<ShippingInfo>>($"/api/EntityNodesMaintenance/GetShippingSetup?userId={userId}&ipAdress={ipAddress}&entityActorId={shippingId}");
                return shippingInfoList;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<EntitiesCommercialRelationShip>> GetCommercialConditions(string commercialConditionId)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == "")
                    ipAddress = "127.0.0.0";
                var commercialConditionsList = await _httpClient.GetFromJsonAsync<List<EntitiesCommercialRelationShip>>($"/api/EntityNodesMaintenance/GetCommercialConditiosOfEntity?userId={userId}&ipAdress={ipAddress}&entityActorId={commercialConditionId}");
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
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == "")
                    ipAddress = "127.0.0.0";
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
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
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

        public async Task<List<ElectronicAddress>> GetEntityElectronicAddress(string entityActorId)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var listElectronicAddress = await _httpClient.GetFromJsonAsync<List<ElectronicAddress>>($"/api/EntityNodesMaintenance/GetElectronicAddresses?userId={userId}&ipAdress={ipAddress}&entityActorId={entityActorId}");
                return listElectronicAddress;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<EntitiesCommercialRelationShip> GetCommercialRelationShip(string entityActorId)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var commercialConditions = await _httpClient.GetFromJsonAsync<EntitiesCommercialRelationShip>($"/api/EntityNodesMaintenance/GetEntitiesCommercialRelationShipById?userId={userId}&ipAdress={ipAddress}&entitiesCommercialRelationShipId={entityActorId}");
                return commercialConditions;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<PalletType>> GetPalletTypes()
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var palletTypes = await _httpClient.GetFromJsonAsync<List<PalletType>>("/api/ConceptsSelector/GetSelectorListPalletTypes");
                return palletTypes;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<ShippingInfo> GetShippingSetUpById(string shippingInfoId)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var shipingInfo = await _httpClient.GetFromJsonAsync<ShippingInfo>($"/api/EntityNodesMaintenance/GetShippingSetupById?userId={userId}&ipAdress={ipAddress}&shippingInfoId={shippingInfoId}");
                return shipingInfo;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null; ;
            }
        }

        public async Task<List<EntityType>> GetEntityTypeList()
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var entityTypeList = await _httpClient.GetFromJsonAsync<List<EntityType>>("/api/ConceptsSelector/GetSelectorListEntityTypes");
                return entityTypeList;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<AtomConcept> GetEntityTypeById(string entityTypeId)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var entityType = await _httpClient.GetFromJsonAsync<AtomConcept>($"/api/EntityActorsRelatedConcepts/GetEntityType?userId={userId}&ipAdress={ipAddress}&entityTypeId={entityTypeId}");
                return entityType;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<PalletType>> GetPalletTypeList()
        {
            try
            {
                var palletList = await _httpClient.GetFromJsonAsync<List<PalletType>>("/api/ConceptsSelector/GetSelectorListPalletTypes");
                return palletList;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<PalletType> GetPalleTypeById(string palletTypeId)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var palletType = await _httpClient.GetFromJsonAsync<PalletType>($"/api/EntityActorsRelatedConcepts/GetPalletType?userId={userId}&ipAdress={ipAddress}&palletTypeId={palletTypeId}");
                return palletType;
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

        public async Task<bool> SaveEntity(string? EntityActorId, EntityActor entityActor)
        {
            EntityActorId = EntityActorId != null ? EntityActorId : "";
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var result = await _httpClient.PostAsJsonAsync($"/api/EntityNodesMaintenance/SaveEntity?userId={userId}&ipAdress={ipAddress}&entityActorId={EntityActorId}", entityActor);
                return result.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }
        }


        public async Task SaveEntityAddress(string EntityActorId, Address address)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            await _httpClient.PostAsJsonAsync($"/api/EntityNodesMaintenance/SaveEntityAddress?userId={userId}&ipAdress={ipAddress}&entityActorId={EntityActorId}", address);
        }

        public async Task SavePhone(string EntityActorId, PhoneNumber phoneNumber)
        {
            try
            {
                var userId = UIClientGlobalVariables.UserId;
                var ipAddress = UIClientGlobalVariables.PublicIpAddress;
                var result = await _httpClient.PostAsJsonAsync($"/api/EntityNodesMaintenance/SavePhone?userId={userId}&ipAdress={ipAddress}&entityActorId={EntityActorId}", phoneNumber);
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
            }
        }

        public async Task SaveIdentificationCode(string EntityActorId, IdentificationEntity identificationEntity)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            await _httpClient.PostAsJsonAsync($"/api/EntityNodesMaintenance/SaveIdentificationCode?userId={userId}&ipAdress={ipAddress}&entityActorId={EntityActorId}", identificationEntity);
        }

        public async Task<bool> SaveEntityGroup(string entityGroupId, EntityGroup entityGroup)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var result = await _httpClient.PostAsJsonAsync($"/api/EntityNodesMaintenance/SaveEntityGroup?userId={userId}&ipAdress={ipAddress}&entityGroupId={entityGroupId}", entityGroup);
                return result.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                throw;
            }
        }

        public async Task SaveElectronicAddress(string electronicAddressId, ElectronicAddress electronicAddress)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            await _httpClient.PostAsJsonAsync($"/api/EntityNodesMaintenance/SaveElectronicAddress?userId={userId}&ipAdress={ipAddress}&entityActorId={electronicAddressId}", electronicAddress);
        }

        public async Task SaveShippingSetup(string shippinSetupId, ShippingInfo shippingInfo)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            await _httpClient.PostAsJsonAsync($"/api/EntityNodesMaintenance/SaveShippingSetup?userId={userId}&ipAdress={ipAddress}&entityActorId={shippinSetupId}", shippingInfo);
        }

        public async Task<bool> SaveCommercialConditions(string commercialConditionId, EntitiesCommercialRelationShip comercialConditions)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var result = await _httpClient.PostAsJsonAsync($"/api/EntityNodesMaintenance/SaveCommercialConditions?userId={userId}&ipAdress={ipAddress}&entityActorId={commercialConditionId}", comercialConditions);
                return result.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }
        }

        public async Task<bool> SaveEntityRole(string roleId, EntityRole entityRole)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var result = await _httpClient.PostAsJsonAsync($"/api/EntityActorsRelatedConcepts/SaveEntityRole?userId={userId}&ipAdress={ipAddress}", entityRole);
                return result.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                throw;
            }
        }

        public async Task<string> UploadFiles(MultipartFormDataContent multipartFormDataContent)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var resul = await _httpClient.PostAsync($"api/UploadFiles", multipartFormDataContent);
                FilePath filePath = await resul.Content.ReadFromJsonAsync<FilePath>();
                return filePath.filePath;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return "";

            }
        }

        public async Task<bool> SaveEntityType(AtomConcept atomConcept)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var result = await _httpClient.PostAsJsonAsync($"/api/EntityActorsRelatedConcepts/SaveEntityType?userId={userId}&ipAdress={ipAddress}", atomConcept);
                return result.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }
        }

        public async Task SavePalletType(PalletType palletType)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            await _httpClient.PostAsJsonAsync($"/api/EntityActorsRelatedConcepts/SavePalletType?userId={userId}&ipAdress={ipAddress}", palletType);
        }


        public async Task<bool> SaveIdentificationType(AtomConcept identification)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var result = await _httpClient.PostAsJsonAsync($"/api/EntityActorsRelatedConcepts/SaveIdentificationType?userId={userId}&ipAdress={ipAddress}", identification);
                return result.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }
        }

        public async Task<TransactItemImage> GetImage(string? transactionalItemId = null)
        {
            transactionalItemId = transactionalItemId != null ? transactionalItemId : "";
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                List<TransactItemImage> imagenList = await _httpClient.GetFromJsonAsync<List<TransactItemImage>>($"api/TransactionalItems/GetTransactionalItemDetailsPathImages?userId={userId}&ipAddress={ipAddress}&transactionalItemId={transactionalItemId}");
                TransactItemImage image = imagenList.Where(s => s.Id == transactionalItemId).FirstOrDefault();
                return image;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;

                return null;

            }
        }

        public async Task<bool> SaveEntitySkinImage(string entityActorId, string skinImage)
        {
            string userId = UIClientGlobalVariables.UserId;
            string ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var result = await _httpClient.PostAsJsonAsync($"/api/EntityNodesMaintenance/SaveEntitySkinImage?userId={userId}&ipAdress={ipAddress}&entityActorId={entityActorId}&skinImage={skinImage}", "");
                return result.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }
        }



        /// Temporal Object
        
        public async Task<EntityActor> NewEntityActor(EntityActor entityActor)
        {
            try
            {
                if(tempEntityActor == null)
                    tempEntityActor = entityActor;
                return tempEntityActor;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<Address> NewEntityActorAddress(Address address)
        {
            try
            {
                if(tempAddress == null)
                    tempAddress = address;
                return tempAddress;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public Task<List<EntityActor>> GetEntityActorFaceList(string? nameLike = null, string? entityType = null, string? entityCode = null, bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<EntityActor> GetEntityActorById(string entityActorId)
        {
            throw new NotImplementedException();
        }

        public Task<EntityActor> CreateNewEntityActor(EntityActor entityActor)
        {
            throw new NotImplementedException();
        }
    }
}
