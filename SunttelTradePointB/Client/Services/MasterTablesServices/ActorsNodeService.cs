using MongoDB.Driver.Core.WireProtocol.Messages;
using MongoDB.Driver.Linq;
using Radzen;
using SunttelTradePointB.Client.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Client.Models;
using SunttelTradePointB.Client.Shared.EntityShareComponents.EntitySubComponents;
using SunttelTradePointB.Shared.Accounting;
using SunttelTradePointB.Shared.Common;
using SunttelTradePointB.Shared.Communications;
using System.Net;
using System.Net.Http.Json;
using System.Net.WebSockets;



namespace SunttelTradePointB.Client.Services.MasterTablesServices
{
    public class ActorsNodeService : IEntityNodes
    {

        private readonly HttpClient _httpClient;

        List<EntityActor>? entityNodesList;
        EntityActor? tempEntityActor { get; set; }
        public Address? tempAddress { get; set; }

        public City? tempCity { get; set; }
        public GeoRegion tempRegion { get; set; }
        public Country tempCountry { get; set; }
        public string SkinImage
        {
            get;
            set;
        }

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



        public async Task<List<EntityActor>> EntityImport(MultipartFormDataContent file)
        {
            try
            {
                var response = await _httpClient.PostAsync($"/api/EntityNodesMaintenance/SaveEntitiesCSV?userId={UIClientGlobalVariables.UserId}&ipAddress={UIClientGlobalVariables.PublicIpAddress}", file);

                if (response != null)
                {
                    return await response.Content.ReadFromJsonAsync<List<EntityActor>>();
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
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
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
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

                var response = await Gethttp($"/api/EntityNodesMaintenance/GetEntityDetailsAddressList?userId={userId}&ipAdress={ipAddress}&EntityId={entityActorId}");
                var list = await response.Content.ReadFromJsonAsync<List<Address>>();
                return list;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
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

            string nameToFind = nameLike != null ? nameLike : "";
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == "")
                    ipAddress = "127.0.0.0";

                var response = await Gethttp($"/api/EntityNodesMaintenance/GetEntityNodes?userId={userId}&ipAdress={ipAddress}&filterName={nameToFind}");
                entityNodesList = await response.Content.ReadFromJsonAsync<List<EntityActor>>();

                return entityNodesList;

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

        /// <summary>
        /// Retrieves a particular Details Table of an Entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityActorId"></param>
        /// <param name="entityDetailsSection"></param>
        /// <returns></returns>
        public async Task<List<Concept>> GetEntityDetailsOf(string entityActorId, EntityDetailsSection entityDetailsSection)
        {
            string patternToFind = entityDetailsSection != null ? entityDetailsSection.ToString() : "";
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                if (ipAddress == "")
                    ipAddress = "127.0.0.0";

                var response = await Gethttp($"api/EntityNodesMaintenance/GetEntityDetails{entityDetailsSection.ToString()}?filterName={patternToFind}");
                var list = await response.Content.ReadFromJsonAsync<List<Concept>>();


                return list;

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

                var response = await Gethttp($"/api/EntityNodesMaintenance/GetEntityDetailsIdentifiersList?userId={userId}&ipAdress={ipAddress}&EntityId={entityId}");
                var list = await response.Content.ReadFromJsonAsync<List<IdentificationEntity>>();

                return list;
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

                var response = await Gethttp($"/api/EntityNodesMaintenance/GetEntityDetailsPhoneDirectory?userId={userId}&ipAdress={ipAddress}&EntityId={entityActorId}");
                var list = await response.Content.ReadFromJsonAsync<List<PhoneNumber>>();

                return list;

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

                var response = await Gethttp($"/api/EntityNodesMaintenance/GetEntityActorById?userId={userId}&ipAdress={ipAddress}&entityActorId={entityActorId}");
                var item = await response.Content.ReadFromJsonAsync<EntityActor>();

                return item;

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
                if (ipAddress == "") ipAddress = "127.0.0.0";

                var response = await Gethttp($"/api/ConceptsSelector/GetSelectorListEntityGroups?filterCondition={filterCondition}");
                var item = await response.Content.ReadFromJsonAsync<List<ConceptGroup>>();

                return item;
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

                var response = await Gethttp($"/api/EntityNodesMaintenance/GetEntityGroup?userId={userId}&ipAdress={ipAddress}&entityGroupId={groupId}");
                var list = await response.Content.ReadFromJsonAsync<EntityGroup>();

                return list;
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

                var response = await Gethttp($"/api/EntityNodesMaintenance/GetEntityDetailsIdentifiersList?userId={userId}&ipAdress={ipAddress}&EntityId={entityActorId}");
                var list = await response.Content.ReadFromJsonAsync<List<IdentificationType>>();

                return list;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<IdentificationType>> GetIdentificationTypes(string filter = "")
        {
            try
            {
                var response = await Gethttp($"/api/ConceptsSelector/GetSelectorListIdentificationTypes?filterName={filter}");
                var list = await response.Content.ReadFromJsonAsync<List<IdentificationType>>();

                return list;
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

                var response = await Gethttp($"/api/EntityActorsRelatedConcepts/GetIdentificationType?userId={userId}&ipAdress={ipAddress}&identicationTypeId={identificationId}");
                var item = await response.Content.ReadFromJsonAsync<AtomConcept>();

                return item;
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

                var response = await Gethttp($"/api/EntityNodesMaintenance/GetEntityGroups?userId={userId}&ipAdress={ipAddress}&filterCondition={filterGroup}");
                var list = await response.Content.ReadFromJsonAsync<List<EntityGroup>>();

                return list;
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
                var response = await Gethttp($"/api/ConceptsSelector/GetSelectorListEntityRoles?filterCondition={filterGroup}");
                var list = await response.Content.ReadFromJsonAsync<List<EntityRole>>();

                return list;
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

                var response = await Gethttp($"/api/EntityActorsRelatedConcepts/GetEntityRole?userId={userId}&ipAdress={ipAddress}&entityRoleId={roleId}");
                var list = await response.Content.ReadFromJsonAsync<EntityRole>();

                return list;
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

                var response = await Gethttp("/api/ConceptsSelector/GetSelectorElectronicAddressEntities");
                var list = await response.Content.ReadFromJsonAsync<List<AtomConcept>>();

                return list;
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

                var response = await Gethttp($"/api/EntityNodesMaintenance/GetEntityDetailsAddressList?userId={userId}&ipAdress={ipAddress}&EntityId={entityId}");
                var list = await response.Content.ReadFromJsonAsync<List<Address>>();


                return list;
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

                var response = await Gethttp($"/api/EntityNodesMaintenance/GetElectronicAddressById?userId={userId}&ipAdress={ipAddress}&electronicAddressId={electronicAddressId}");
                var item = await response.Content.ReadFromJsonAsync<ElectronicAddress>();


                return item;
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

                var response = await Gethttp($"/api/EntityNodesMaintenance/GetShippingSetup?userId={userId}&ipAdress={ipAddress}&entityActorId={shippingId}");
                var list = await response.Content.ReadFromJsonAsync<List<ShippingInfo>>();

                return list;
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

                var response = await Gethttp($"/api/EntityNodesMaintenance/GetCommercialConditiosOfEntity?userId={userId}&ipAdress={ipAddress}&entityActorId={commercialConditionId}");
                var list = await response.Content.ReadFromJsonAsync<List<EntitiesCommercialRelationShip>>();

                return list;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<EntitiyRelationshipType>> GetEntityRelationType()
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var response = await Gethttp("/api/ConceptsSelector/GetSelectorEntitiyRelationshipTypes");
                var list = await response.Content.ReadFromJsonAsync<List<EntitiyRelationshipType>>();

                return list;
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
                var response = await Gethttp($"/api/EntityNodesMaintenance/GetElectronicAddresses?userId={userId}&ipAdress={ipAddress}&entityActorId={entityActorId}");
                var list = await response.Content.ReadFromJsonAsync<List<ElectronicAddress>>();

                return list;
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

                var response = await Gethttp($"/api/EntityNodesMaintenance/GetEntitiesCommercialRelationShipById?userId={userId}&ipAdress={ipAddress}&entitiesCommercialRelationShipId={entityActorId}");
                var item = await response.Content.ReadFromJsonAsync<EntitiesCommercialRelationShip>();

                return item;
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
                var response = await Gethttp("/api/ConceptsSelector/GetSelectorListPalletTypes");
                var list = await response.Content.ReadFromJsonAsync<List<PalletType>>();


                return list;
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
                var response = await Gethttp($"/api/EntityNodesMaintenance/GetShippingSetupById?userId={userId}&ipAdress={ipAddress}&shippingInfoId={shippingInfoId}");
                var list = await response.Content.ReadFromJsonAsync<ShippingInfo>();

                return list;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null; ;
            }
        }

        public async Task<List<EntityType>> GetEntityTypeList(string filter = "")
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {

                var response = await Gethttp($"/api/ConceptsSelector/GetSelectorListEntityTypes?filterName={filter}");
                var list = await response.Content.ReadFromJsonAsync<List<EntityType>>();


                return list;
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

                var response = await Gethttp($"/api/EntityActorsRelatedConcepts/GetEntityType?userId={userId}&ipAdress={ipAddress}&entityTypeId={entityTypeId}");
                var item = await response.Content.ReadFromJsonAsync<AtomConcept>();


                return item;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<PalletType>> GetPalletTypeList(string filter = "")
        {
            try
            {
                var response = await Gethttp($"/api/ConceptsSelector/GetSelectorListPalletTypes?filterName={filter}");
                var list = await response.Content.ReadFromJsonAsync<List<PalletType>>();

                return list;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }


        public async Task<ChannelCommunicationsGroup> GetChannelCommunicationsGroupById(string channelCommunicationsGroupId)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var response = await Gethttp($"api/CommunicationsManagement/GetChannelCommunicationsGroupById?userId={userId}&ipAdress={ipAddress}&channelCommunicationsGroupId={channelCommunicationsGroupId}");
                var list = await response.Content.ReadFromJsonAsync<ChannelCommunicationsGroup>();

                return list;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<List<ChannelCommunicationsGroup>> GetChannelCommunicationsGroups()
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var response = await Gethttp($"api/CommunicationsManagement/GetChannelCommunicationsGroups?userId={userId}&ipAdress={ipAddress}");
                var list = await response.Content.ReadFromJsonAsync<List<ChannelCommunicationsGroup>>();

                return list;
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

                var response = await Gethttp($"/api/EntityActorsRelatedConcepts/GetPalletType?userId={userId}&ipAdress={ipAddress}&palletTypeId={palletTypeId}");
                var item = await response.Content.ReadFromJsonAsync<PalletType>();

                return item;
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

        public async Task<(bool IsSuccess, string? EntityId, string? error)> SaveEntity(string? EntityActorId, EntityActor entityActor)
        {
            EntityActorId = EntityActorId != null ? EntityActorId : "";
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            entityActor.SquadId = UIClientGlobalVariables.ActiveSquad.ID.ToString();
            try
            {
                userId = userId == "" ? "UX" : userId;
                var result = await _httpClient.PostAsJsonAsync<EntityActor>($"/api/EntityNodesMaintenance/SaveEntity?userId={userId}&ipAdress={ipAddress}", entityActor);

                if (result.Content != null)
                {
                    EntityActor item = await result.Content.ReadFromJsonAsync<EntityActor>();


                    return (result.IsSuccessStatusCode, item.Id, null);

                }
                else
                {
                    return (false, null, "Saving Error");
                }



            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return (false, null, errMessage);
            }
        }


        public async Task<bool> SaveEntityAddress(string EntityActorId, Address address)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            var resul = await _httpClient.PostAsJsonAsync($"/api/EntityNodesMaintenance/SaveEntityAddress?userId={userId}&ipAdress={ipAddress}&entityActorId={EntityActorId}", address);

            return resul.IsSuccessStatusCode;
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

        public async Task<ChannelCommunicationsGroup> SaveChannelCommunicationsGroup(ChannelCommunicationsGroup channelCommunicationsGroup)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var result = await _httpClient.PostAsJsonAsync<ChannelCommunicationsGroup>($"api/CommunicationsManagement/SaveChannelCommunicationsGroup?userId={userId}&ipAdress={ipAddress}", channelCommunicationsGroup);
                return await result.Content.ReadFromJsonAsync<ChannelCommunicationsGroup>();
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
                if (tempEntityActor == null)
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
                if (tempAddress == null)
                    tempAddress = address;
                return tempAddress;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<Country> NewCountry(Country country)
        {
            try
            {
                if (tempCountry == null || tempCountry != null)
                    tempCountry = country;
                return tempCountry;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<GeoRegion> NewGeoRegion(GeoRegion geoRegion)
        {
            try
            {
                if (tempRegion == null || tempRegion != null)
                    tempRegion = geoRegion;
                return tempRegion;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }

        public async Task<City> NewCity(City? city)
        {
            try
            {
                if (tempCity == null || tempCity != null)
                    tempCity = city;
                return tempCity;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return null;
            }
        }



        public async Task<bool> DeleteEntityTypeById(string entityTypeId)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {

                var response = await Deletehttp($"/api/EntityActorsRelatedConcepts/DeleteEntityTypeById?userId={userId}&ipAddress={ipAddress}&entityTypeId={entityTypeId}");
                bool item = response.IsSuccessStatusCode;

                return item;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }
        }

        public async Task<bool> DeleteEntityGroupById(string entityGroupId)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var response = await Deletehttp($"/api/EntityActorsRelatedConcepts/DeleteEntityGroupById?userId={userId}&ipAddress={ipAddress}&entityGroupId={entityGroupId}");
                bool item = response.IsSuccessStatusCode;

                return item;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }
        }
        public async Task<bool> DeleteIdentificationTypeById(string identificationTypeId)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var response = await Deletehttp($"/api/EntityActorsRelatedConcepts/DeleteIdentificationTypeById?userId={userId}&ipAddress={ipAddress}&identificationTypeId={identificationTypeId}");
                bool item = response.IsSuccessStatusCode;

                return item;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }
        }


        public async Task<bool> DeleteEntityRoleById(string entityRoleId)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var response = await Deletehttp($"/api/EntityActorsRelatedConcepts/DeleteEntityRoleById?userId={userId}&ipAddress={ipAddress}&entityRoleId={entityRoleId}");
                bool item = response.IsSuccessStatusCode;

                return item;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }
        }

          public async Task<bool> DeletePalletTypeById(string palletTypeId)
        {
            var userId = UIClientGlobalVariables.UserId;
            var ipAddress = UIClientGlobalVariables.PublicIpAddress;
            try
            {
                var response = await Deletehttp($"/api/EntityActorsRelatedConcepts/DeletePalletTypeById?userId={userId}&ipAddress={ipAddress}&palletTypeId={palletTypeId}");
                bool item = response.IsSuccessStatusCode;

                return item;
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                return false;
            }
        }




        public async Task<HttpResponseMessage> Gethttp(string Url)
        {

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, Url);
                if (UIClientGlobalVariables.ActiveSquad != null)
                    request.Headers.Add("SquadId", UIClientGlobalVariables.ActiveSquad!.ID.ToString());


                var response = await _httpClient.SendAsync(request);
                System.Diagnostics.Debug.WriteLine(response.IsSuccessStatusCode);
                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
                else { return null; }

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                System.Diagnostics.Debug.WriteLine(errMessage);

            }
            return null;

        }

        private async Task<HttpResponseMessage> Deletehttp(string Url)
        {

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, Url);
                if (UIClientGlobalVariables.ActiveSquad != null)
                    request.Headers.Add("SquadId", UIClientGlobalVariables.ActiveSquad!.ID.ToString());


                var response = await _httpClient.SendAsync(request);
                System.Diagnostics.Debug.WriteLine(response.IsSuccessStatusCode);
                if (response.IsSuccessStatusCode)
                {
                    return response;
                }
                else { return null; }

            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                System.Diagnostics.Debug.WriteLine(errMessage);

            }
            return null;

        }
    }
}
