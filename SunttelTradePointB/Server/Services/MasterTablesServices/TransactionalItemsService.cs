using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SunttelTradePointB.Server.Interfaces.MasterTablesInterfaces;
using SunttelTradePointB.Shared.Common;
using System.Net;
using System.Reflection;

namespace SunttelTradePointB.Server.Services.MasterTablesServices
{


    /// <summary>
    /// Service to deal with operations with Products, Services, and any transactional Item.
    /// </summary>
    public class TransactionalItemsService : ITransactionalItemsBack
    {

        IMongoCollection<TransactionalItem> _TransactionalItemsCollection;
        IMongoCollection<TransactionalItemType> _transactionalItemTypes;
        IMongoCollection<Box> _box;
        IMongoCollection<SeasonBusiness> _season;
        //IMongoCollection<TransactionalItemCharacteristicPair> _TransactionalItemsCharacteristicsCollection;
        //IMongoCollection<TransactItemImage> _TransactionalItemsImagesCollection;
        //IMongoCollection<TransactionalItemProcessStep> _TransactionalItemsProductionSpecsCollection;
        //IMongoCollection<PackingSpecs> _TransactionalItemsPackingSpecsCollection;
        //IMongoCollection<TransactionalItemQualityPair> _TransactionalItemsQualityCollection;
        //IMongoCollection<TransactionalItemTag> _TransactionalItemsTagsCollection;



        /// <summary>
        /// Initialize the component passing the configuration to get the connection string
        /// </summary>
        /// <param name="config"></param>
        public TransactionalItemsService(IConfiguration config)
        {
            var mongoClient = new MongoClient(config.GetConnectionString("MongoConectionString"));
            string DataBaseName = config["DatabaseMongo"];

            var mongoDatabase = mongoClient.GetDatabase(DataBaseName);
            _TransactionalItemsCollection = mongoDatabase.GetCollection<TransactionalItem>("TransactionalItems");
            _transactionalItemTypes = mongoDatabase.GetCollection<TransactionalItemType>("TransactionalItemTypes");
            _box = mongoDatabase.GetCollection<Box>("Box");
            _season = mongoDatabase.GetCollection<SeasonBusiness>("BusinessSeasons");
            //_TransactionalItemsCharacteristicsCollection = mongoDatabase.GetCollection<TransactionalItemCharacteristicPair>("TransactionalItems");
            //_TransactionalItemsImagesCollection = mongoDatabase.GetCollection<TransactItemImage>("TransactionalItems");
            //_TransactionalItemsProductionSpecsCollection = mongoDatabase.GetCollection<TransactionalItemProcessStep>("TransactionalItems");
            //_TransactionalItemsPackingSpecsCollection = mongoDatabase.GetCollection<PackingSpecs>("TransactionalItems");
            //_TransactionalItemsQualityCollection = mongoDatabase.GetCollection<TransactionalItemQualityPair>("TransactionalItems");
            //_TransactionalItemsTagsCollection = mongoDatabase.GetCollection<TransactionalItemTag>("TransactionalItems");


        }

        /// <summary>
        /// Retrives a list of PRoducts, services, and all Transactional Items based on a search criteria
        /// </summary>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="nameLike"></param>
        /// <param name="groupName"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<TransactionalItem>? TransactionalItemRelatedList, string? ErrorDescription)> GetTransactionItemList(int? page = 1, int? perPage = 10, string? nameLike = null, string? groupName = null, string? code = null)
        {
            try
            {
                string strNameFiler = nameLike == null ? "" : nameLike;
                var skip = (page - 1) * perPage;

                var pipeline = new List<BsonDocument>();

                if(!(strNameFiler.ToLower()=="all" || strNameFiler.ToLower() == "todos"))
                {
                    pipeline.Add(
                    new BsonDocument{
                        { "$match",  new BsonDocument {
                            { "$text",
                                new BsonDocument {
                                    { "$search",strNameFiler },
                                    { "$language","english" },
                                    { "$caseSensitive",false },
                                    { "$diacriticSensitive",false }
                                }
                            }
                        }}
                    }
                );
                }
                

                pipeline.Add(
                    new BsonDocument(
                        "$match",
                        new BsonDocument("Status.IsEnabledForTransactions",true)

                    )
                );

                pipeline.Add(
                    new BsonDocument {
                        { "$project",
                            new BsonDocument {
                                { "ProductionSpecs", 0 },
                                { "ProductPackingSpecs", 0 },
                                { "ItemCharacteristics", 0},
                                { "PathImages", 0},
                                { "QualityParameters", 0},
                                { "TransactionalItemModels", 0}
                            }
                        }
                    }
                );

                pipeline.Add(
                    new BsonDocument{
                        {"$skip",  skip}
                    }
                );

                pipeline.Add(
                    new BsonDocument{
                        {"$limit",  perPage}
                    }
                );

                List<TransactionalItem> results = await _TransactionalItemsCollection.Aggregate<TransactionalItem>(pipeline).ToListAsync();

                //var results = await _entityActorsCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();

                return (true, results, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves an object of a transactional Item by id
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, AtomConcept transactionalItem, string? ErrorDescription)> GetTransactionalItemById(string userId, string ipAddress, string transactionalItemId)
        {

            try
            {
                var pipeline = new List<BsonDocument>();
                
                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(transactionalItemId)))
                );

                var resultPrev = await _TransactionalItemsCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();


                TransactionalItem result = resultPrev.Select(d => BsonSerializer.Deserialize<TransactionalItem>(d)).ToList()[0];


                return (true, result, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }

        }


        /// <summary>
        /// Retrieve details of a Transactional Items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="transactItemId"></param>
        /// <param name="transactionalItemDetailsSection"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, List<T>? TransactionalItemRelatedList, string? ErrorDescription)> GetTransactionalItemDetailsOf<T>(string userId, string ipAddress, string transactItemId, TransactionalItemDetailsSection transactionalItemDetailsSection)
        {
            string detailsArrayListName = "";
            if (transactItemId.Length == 0)
                return (false, null, "Not valid Entity Id");

            switch (transactionalItemDetailsSection)
            {
                case TransactionalItemDetailsSection.PackingRecipe:
                    detailsArrayListName = "ProductPackingSpecs";
                    break;

                case TransactionalItemDetailsSection.ProductionSpecs:
                    detailsArrayListName = "ProductionSpecs";
                    break;

                case TransactionalItemDetailsSection.QualityParameters:
                    detailsArrayListName = "QualityParameters";
                    break;
                case TransactionalItemDetailsSection.PathImages:
                    detailsArrayListName = "PathImages";
                    break;
                case TransactionalItemDetailsSection.Tags:
                    detailsArrayListName = "TransactionalItemTags";
                    break;
                case TransactionalItemDetailsSection.Characteristics:
                    detailsArrayListName = "ItemCharacteristics";
                    break;
                default:
                    return (false, null, "Not option Found");
            }


            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(transactItemId)))
                );

                pipeline.Add(
                    new BsonDocument("$project", new BsonDocument(){
                        { $"{detailsArrayListName}", 1},
                        { "_id", 0}
                    })
                );

                pipeline.Add(
                    new BsonDocument("$unwind", $"${detailsArrayListName}")
                );

                pipeline.Add(
                    new BsonDocument("$replaceRoot", new BsonDocument("newRoot", $"${detailsArrayListName}"))
                );

                var result = await _TransactionalItemsCollection.Aggregate<BsonDocument>(pipeline).ToListAsync();


                switch (transactionalItemDetailsSection)
                {
                    case TransactionalItemDetailsSection.PackingRecipe:
                        var listPackingSpecs = result.Select(d => BsonSerializer.Deserialize<PackingSpecs>(d)).ToList();
                        return (true, listPackingSpecs.Cast<T>().ToList(), null);

                    case TransactionalItemDetailsSection.ProductionSpecs:
                        var listProductionSpecs = result.Select(d => BsonSerializer.Deserialize<TransactionalItemProcessStep>(d)).ToList();
                        return (true, listProductionSpecs.Cast<T>().ToList(), null);

                    case TransactionalItemDetailsSection.QualityParameters:
                        var listQualityParameters = result.Select(d => BsonSerializer.Deserialize<TransactionalItemQualityPair>(d)).ToList();
                        return (true, listQualityParameters.Cast<T>().ToList(), null);

                    case TransactionalItemDetailsSection.PathImages:
                        var listImages = result.Select(d => BsonSerializer.Deserialize<TransactItemImage>(d)).ToList();
                        return (true, listImages.Cast<T>().ToList(), null);
                    case TransactionalItemDetailsSection.Tags:
                        var listTags = result.Select(d => BsonSerializer.Deserialize<TransactionalItemTag>(d)).ToList();
                        return (true, listTags.Cast<T>().ToList(), null);

                    case TransactionalItemDetailsSection.Characteristics:
                        var listTransactionalItemCharacteristicPair = result.Select(d => BsonSerializer.Deserialize<TransactionalItemCharacteristicPair>(d)).ToList();
                        return (true, listTransactionalItemCharacteristicPair.Cast<T>().ToList(), null);

                    default:
                        return (false, null, "Not option Found");

                }
                return (false, null, "Not option Found");

            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Updates/ Insert a new Transactional Item
        /// </summary>
        /// <param name="transactionalItem"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, TransactionalItem? transactionalItem, string? ErrorDescription)> SaveTransactionalItem(string userId, string ipAddress, TransactionalItem transactionalItem)
        {
            try
            {
                if(transactionalItem.Id == null)
                {
                    transactionalItem.Id = ObjectId.GenerateNewId().ToString();
                }


                // For each list inside the object, we will update the list setting the null Id of each element 
                // with a new ObjectId
                GeneralServerUtilities.SetListIds(transactionalItem);



                var type = transactionalItem.GetType();
                var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);


                var filter = Builders<TransactionalItem>.Filter.Eq("_id", new ObjectId(transactionalItem.Id));


                var updateOptions = new UpdateOptions { IsUpsert = true };
                var update = Builders<TransactionalItem>.Update;
                var updates = new List<UpdateDefinition<TransactionalItem>>();

                foreach (var property in properties)
                {
                    var propertyName = property.Name;
                    var propertyValue = property.GetValue(transactionalItem, null);

                    if (propertyValue != null)
                    {
                        updates.Add(update.Set(propertyName, propertyValue));
                    }

                }


                var result = await _TransactionalItemsCollection.UpdateOneAsync(filter, update.Combine(updates), updateOptions);

                return (true, transactionalItem, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Insert / Update a Transactional Item Characteristic
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="transactionalItemCharacteristicPair"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, TransactionalItemCharacteristicPair? transactionalItemCharacteristicPairs, string? ErrorDescription)> SaveCharacteristics(string userId, string ipAddress, string transactionalItemId, TransactionalItemCharacteristicPair transactionalItemCharacteristicPair)
        {
            try
            {
                if(transactionalItemCharacteristicPair.Id == null)
                {
                    transactionalItemCharacteristicPair.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterPrev = Builders<TransactionalItem>.Filter.Eq(x => x.Id, transactionalItemId);
                var resultPrev = await _TransactionalItemsCollection.Find(filterPrev).FirstOrDefaultAsync();

                if (resultPrev != null && resultPrev.ItemCharacteristics != null && resultPrev.ItemCharacteristics.Any(x => x.Id == transactionalItemCharacteristicPair.Id))
                {
                    //Update Element
                    var filter = Builders<TransactionalItem>.Filter.And(
                        Builders<TransactionalItem>.Filter.Eq(x => x.Id, transactionalItemId),
                        Builders<TransactionalItem>.Filter.ElemMatch(x => x.ItemCharacteristics, y => y.Id == transactionalItemCharacteristicPair.Id)
                    );
                    var update = Builders<TransactionalItem>.Update.Set(x => x.ItemCharacteristics[-1], transactionalItemCharacteristicPair);
                    await _TransactionalItemsCollection.UpdateOneAsync(filter, update);
                }
                else
                {
                    //Add Element
                    var filter = Builders<TransactionalItem>.Filter.Eq(x => x.Id, transactionalItemId);
                    if(resultPrev.ItemCharacteristics == null)
                    {
                        var ItemCharacteristicsLst = new List<TransactionalItemCharacteristicPair>();
                        ItemCharacteristicsLst.Add(transactionalItemCharacteristicPair);
                        var updateWithoutList = Builders<TransactionalItem>.Update.Set(x => x.ItemCharacteristics, ItemCharacteristicsLst);
                        await _TransactionalItemsCollection.UpdateOneAsync(filter, updateWithoutList);
                    }
                    else
                    {
                        var update = Builders<TransactionalItem>.Update.AddToSet(x => x.ItemCharacteristics, transactionalItemCharacteristicPair);
                        await _TransactionalItemsCollection.UpdateOneAsync(filter, update);
                    }
                   
                }



                return (true, transactionalItemCharacteristicPair, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }

        }



        /// <summary>
        /// Saves (INSERT/UPDATE) A transactional item model
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemId"></param>
        /// <param name="productModel"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, ProductModel?  productModel, string? ErrorDescription)> SaveTransactionalItemModels(string userId, string ipAddress, string transactionalItemId, ProductModel  productModel)
        {
            try
            {
                if (productModel.Id == null)
                {
                    productModel.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterPrev = Builders<TransactionalItem>.Filter.Eq(x => x.Id, transactionalItemId);
                var resultPrev = await _TransactionalItemsCollection.Find(filterPrev).FirstOrDefaultAsync();

                if (resultPrev != null && resultPrev.TransactionalItemModels != null && resultPrev.TransactionalItemModels.Any(x => x.Id == productModel.Id))
                {
                    //Update Element
                    var filter = Builders<TransactionalItem>.Filter.And(
                        Builders<TransactionalItem>.Filter.Eq(x => x.Id, transactionalItemId),
                        Builders<TransactionalItem>.Filter.ElemMatch(x => x.TransactionalItemModels, y => y.Id == productModel.Id)
                    );
                    var update = Builders<TransactionalItem>.Update.Set(x => x.TransactionalItemModels[-1], productModel);
                    await _TransactionalItemsCollection.UpdateOneAsync(filter, update);
                }
                else
                {
                    //Add Element
                    var filter = Builders<TransactionalItem>.Filter.Eq(x => x.Id, transactionalItemId);
                    if (resultPrev.TransactionalItemModels == null)
                    {
                        var ItemModels = new List<ProductModel>();
                        ItemModels.Add(productModel);
                        var updateWithoutList = Builders<TransactionalItem>.Update.Set(x => x.TransactionalItemModels, ItemModels);
                        await _TransactionalItemsCollection.UpdateOneAsync(filter, updateWithoutList);
                    }
                    else
                    {
                        var update = Builders<TransactionalItem>.Update.AddToSet(x => x.TransactionalItemModels, productModel);
                        await _TransactionalItemsCollection.UpdateOneAsync(filter, update);
                    }

                }



                return (true, productModel, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }

        }

        /// <summary>
        /// Insert / Update a Transactional Item Image
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="transactItemImage"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, TransactItemImage? transactItemImage, string? ErrorDescription)> SaveImage(string userId, string ipAddress, string transactionalItemId, TransactItemImage transactItemImage)
        {
            try
            {
                if (transactItemImage.Id == null)
                {
                    transactItemImage.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterPrev = Builders<TransactionalItem>.Filter.Eq(x => x.Id, transactionalItemId);
                var resultPrev = await _TransactionalItemsCollection.Find(filterPrev).FirstOrDefaultAsync();

                if (transactItemImage.Id != "" && resultPrev != null && resultPrev.PathImages.Any(x => x.Id == transactItemImage.Id))
                {
                    //Update Element
                    var filter = Builders<TransactionalItem>.Filter.And(
                        Builders<TransactionalItem>.Filter.Eq(x => x.Id, transactionalItemId),
                        Builders<TransactionalItem>.Filter.ElemMatch(x => x.PathImages, y => y.Id == transactItemImage.Id)
                    );
                    var update = Builders<TransactionalItem>.Update.Set(x => x.PathImages[-1], transactItemImage);
                    await _TransactionalItemsCollection.UpdateOneAsync(filter, update);
                }
                else
                {
                    //Add Element

                    transactItemImage.Id = ObjectId.GenerateNewId().ToString();
                    var filter = Builders<TransactionalItem>.Filter.Eq(x => x.Id, transactionalItemId);
                    var update = Builders<TransactionalItem>.Update.AddToSet(x => x.PathImages, transactItemImage);
                    await _TransactionalItemsCollection.UpdateOneAsync(filter, update);
                }



                return (true, transactItemImage, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }

        }

        /// <summary>
        /// Insert / Update a Transactional Item Production Step
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="transactionalItemProcessStep"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, TransactionalItemProcessStep? transactionalItemProcessStep, string? ErrorDescription)> SaveProductionSpecs(string userId, string ipAddress, string transactionalItemId, TransactionalItemProcessStep transactionalItemProcessStep)
        {
            try
            {

                if (transactionalItemProcessStep.Id == null)
                {
                    transactionalItemProcessStep.Id = ObjectId.GenerateNewId().ToString();
                }


                if(transactionalItemProcessStep.TransactionalItemProcessTags !=null)
                    transactionalItemProcessStep.TransactionalItemProcessTags.ForEach(t=> { if (t.Id == null) t.Id = ObjectId.GenerateNewId().ToString(); });

                if (transactionalItemProcessStep.CostExceptionsByQuantity != null)
                    transactionalItemProcessStep.CostExceptionsByQuantity.ForEach(t => { if (t.Id == null) t.Id = ObjectId.GenerateNewId().ToString(); });


                var filterPrev = Builders<TransactionalItem>.Filter.Eq(x => x.Id, transactionalItemId);
                var resultPrev = await _TransactionalItemsCollection.Find(filterPrev).FirstOrDefaultAsync();


                if (transactionalItemProcessStep.Id != "" && resultPrev != null && resultPrev.ProductionSpecs.Any(x => x.Id == transactionalItemProcessStep.Id))
                {
                    //Update Element
                    var filter = Builders<TransactionalItem>.Filter.And(
                        Builders<TransactionalItem>.Filter.Eq(x => x.Id, transactionalItemId),
                        Builders<TransactionalItem>.Filter.ElemMatch(x => x.ProductionSpecs, y => y.Id == transactionalItemProcessStep.Id)
                    );
                    var update = Builders<TransactionalItem>.Update.Set(x => x.ProductionSpecs[-1], transactionalItemProcessStep);
                    await _TransactionalItemsCollection.UpdateOneAsync(filter, update);
                }
                else
                {
                    //Add Element
                    transactionalItemProcessStep.Id = ObjectId.GenerateNewId().ToString();

                    var filter = Builders<TransactionalItem>.Filter.Eq(x => x.Id, transactionalItemId);
                    var update = Builders<TransactionalItem>.Update.AddToSet(x => x.ProductionSpecs, transactionalItemProcessStep);
                    await _TransactionalItemsCollection.UpdateOneAsync(filter, update);
                }



                return (true, transactionalItemProcessStep, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }

        }

        /// <summary>
        /// Insert / Update a Transactional Item Quality Parameter
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="transactionalItemQualityPair"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, TransactionalItemQualityPair? transactionalItemQualityPair, string? ErrorDescription)> SaveQualityParameters(string userId, string ipAddress, string transactionalItemId, TransactionalItemQualityPair transactionalItemQualityPair)
        {
            try
            {
                if (transactionalItemQualityPair.Id == null)
                {
                    transactionalItemQualityPair.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterPrev = Builders<TransactionalItem>.Filter.Eq(x => x.Id, transactionalItemId);
                var resultPrev = await _TransactionalItemsCollection.Find(filterPrev).FirstOrDefaultAsync();

                if (transactionalItemQualityPair.Id != "" && resultPrev != null && resultPrev.QualityParameters.Any(x => x.Id == transactionalItemQualityPair.Id))
                {
                    //Update Element
                    var filter = Builders<TransactionalItem>.Filter.And(
                        Builders<TransactionalItem>.Filter.Eq(x => x.Id, transactionalItemId),
                        Builders<TransactionalItem>.Filter.ElemMatch(x => x.QualityParameters, y => y.Id == transactionalItemQualityPair.Id)
                    );
                    var update = Builders<TransactionalItem>.Update.Set(x => x.QualityParameters[-1], transactionalItemQualityPair);
                    await _TransactionalItemsCollection.UpdateOneAsync(filter, update);
                }
                else
                {
                    //Add Element
                    transactionalItemQualityPair.Id = ObjectId.GenerateNewId().ToString();
                    var filter = Builders<TransactionalItem>.Filter.Eq(x => x.Id, transactionalItemId);
                    var update = Builders<TransactionalItem>.Update.AddToSet(x => x.QualityParameters, transactionalItemQualityPair);
                    await _TransactionalItemsCollection.UpdateOneAsync(filter, update);
                }



                return (true, transactionalItemQualityPair, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }

        }

        /// <summary>
        /// Insert / Update a Transactional Item Packing Instructions
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="packingSpecs"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, PackingSpecs? packingSpecs, string? ErrorDescription)> SaveProductPackingSpecs(string userId, string ipAddress, string transactionalItemId, PackingSpecs packingSpecs)
        {
            try
            {
                if (packingSpecs.Id == null)
                {
                    packingSpecs.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterPrev = Builders<TransactionalItem>.Filter.Eq(x => x.Id, transactionalItemId);
                var resultPrev = await _TransactionalItemsCollection.Find(filterPrev).FirstOrDefaultAsync();

                if (packingSpecs.Id != "" && resultPrev != null && resultPrev.ProductPackingSpecs.Any(x => x.Id == packingSpecs.Id))
                {
                    //Update Element
                    var filter = Builders<TransactionalItem>.Filter.And(
                        Builders<TransactionalItem>.Filter.Eq(x => x.Id, transactionalItemId),
                        Builders<TransactionalItem>.Filter.ElemMatch(x => x.ProductPackingSpecs, y => y.Id == packingSpecs.Id)
                    );
                    var update = Builders<TransactionalItem>.Update.Set(x => x.ProductPackingSpecs[-1], packingSpecs);
                    await _TransactionalItemsCollection.UpdateOneAsync(filter, update);
                }
                else
                {
                    //Add Element
                    packingSpecs.Id = ObjectId.GenerateNewId().ToString();

                    var filter = Builders<TransactionalItem>.Filter.Eq(x => x.Id, transactionalItemId);
                    var update = Builders<TransactionalItem>.Update.AddToSet(x => x.ProductPackingSpecs, packingSpecs);
                    await _TransactionalItemsCollection.UpdateOneAsync(filter, update);
                }
                return (true, packingSpecs, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }

        /// <summary>
        /// Insert / Update a Transactional Item Tag
        /// </summary>
        /// <param name="transactionalItemId"></param>
        /// <param name="transactionalItemTag"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, TransactionalItemTag? transactionalItemTag, string? ErrorDescription)> SaveTags(string userId, string ipAddress, string transactionalItemId, TransactionalItemTag transactionalItemTag)
        {
            try
            {
                if (transactionalItemTag.Id == null)
                {
                    transactionalItemTag.Id = ObjectId.GenerateNewId().ToString();
                }


                var filterPrev = Builders<TransactionalItem>.Filter.Eq(x => x.Id, transactionalItemId);
                var resultPrev = await _TransactionalItemsCollection.Find(filterPrev).FirstOrDefaultAsync();

                if (transactionalItemTag.Id != "" && resultPrev != null && resultPrev.TransactionalItemTags.Any<TransactionalItemTag>(x => x.Id == transactionalItemTag.Id))
                {
                    //Update Element
                    var filter = Builders<TransactionalItem>.Filter.And(
                        Builders<TransactionalItem>.Filter.Eq(x => x.Id, transactionalItemId),
                        Builders<TransactionalItem>.Filter.ElemMatch(x => x.TransactionalItemTags, y => y.Id == transactionalItemTag.Id)
                    );
                    var update = Builders<TransactionalItem>.Update.Set(x => x.TransactionalItemTags[-1], transactionalItemTag);
                    await _TransactionalItemsCollection.UpdateOneAsync(filter, update);
                }
                else
                {
                    //Add Element
                    transactionalItemTag.Id = ObjectId.GenerateNewId().ToString();
                    var filter = Builders<TransactionalItem>.Filter.Eq(x => x.Id, transactionalItemId);
                    var update = Builders<TransactionalItem>.Update.AddToSet(x => x.TransactionalItemTags, transactionalItemTag);
                    await _TransactionalItemsCollection.UpdateOneAsync(filter, update);


                }
                return (true, transactionalItemTag, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves a list of Transactional Item Types with the posibility to receive an optional paremeter
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, List<TransactionalItemType>? transactionalItemTypes, string? ErrorDescription)> GetTransactionalItemTypes(string userId, string ipAddress, string? filterCondition = null)
        {
            try
            {
                string filter = filterCondition == null ? "" : filterCondition;

                if (filter.Length > 0)
                {
                    var pipeline = new List<BsonDocument>();

                    if(filter.ToLower() != "all")
                    {
                        pipeline.Add(
                       new BsonDocument(
                           "$match", new BsonDocument(
                               "Name", new BsonDocument("$regex", new BsonRegularExpression($"/{filter}/i"))
                           )
                       )
                   );
                    }
                   

                    List<TransactionalItemType> results = await _transactionalItemTypes.Aggregate<TransactionalItemType>(pipeline).ToListAsync();
                    return (true, results, null);
                }
                else
                {
                    var transactionalItemTypes = await _transactionalItemTypes.Find<TransactionalItemType>(new BsonDocument()).ToListAsync();

                    if (transactionalItemTypes == null || transactionalItemTypes.Count == 0)
                    {
                        return (false, null, "Unpopulated Transactional Item Types");
                    }
                    else
                    {
                        return (true, transactionalItemTypes, null);
                    }
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves a particular Transactional Item Type by Id
        /// </summary>
        /// <param name="TransactionalItemTypeId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, TransactionalItemType? transactionalItemType, string? ErrorDescription)> GetTransactionalItemType(string userId, string ipAddress, string TransactionalItemTypeId)
        {
            try
            {
                var filterTransactionalItemType = Builders<TransactionalItemType>.Filter.Eq(x => x.Id, TransactionalItemTypeId);
                var resultTransactionalItemType = await _transactionalItemTypes.Find(filterTransactionalItemType).FirstOrDefaultAsync();

                if (resultTransactionalItemType == null)
                {
                    return (false, null, "Unpopulated Transactional Item Types");
                }
                else
                {
                    return (true, resultTransactionalItemType, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Inserts / Updates an Transactional Item Type object
        /// </summary>
        /// <param name="transactionalItemType"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, TransactionalItemType? transactionalItemType, string? ErrorDescription)> SaveTransactionalItemType(string userId, string ipAddress, TransactionalItemType transactionalItemType)
        {
            try
            {
                if(transactionalItemType.Id == null)
                {
                    transactionalItemType.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterTransactionalItemType = Builders<TransactionalItemType>.Filter.Eq("_id", new ObjectId(transactionalItemType.Id));
                var updateTransactionalItemType = new ReplaceOptions { IsUpsert = true };
                var resultTransactionalItemType = await _transactionalItemTypes.ReplaceOneAsync(filterTransactionalItemType, transactionalItemType, updateTransactionalItemType);

                return (true, transactionalItemType, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves a list of Boxes with the posibility to receive an optional paremeter
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, List<Box>? boxes, string? ErrorDescription)> GetBoxes(string userId, string ipAddress, string? filterCondition = null)
        {
            try
            {
                string filter = filterCondition == null ? "" : filterCondition;

                if (filter.Length > 0)
                {
                    var pipeline = new List<BsonDocument>();

                    pipeline.Add(
                        new BsonDocument(
                            "$match", new BsonDocument(
                                "Name", new BsonDocument("$regex", new BsonRegularExpression($"/{filter}/i"))
                            )
                        )
                    );
                    List<Box> results = await _box.Aggregate<Box>(pipeline).ToListAsync();
                    return (true, results, null);
                }
                else
                {
                    var boxes = await _box.Find<Box>(new BsonDocument()).ToListAsync();

                    if (boxes == null || boxes.Count == 0)
                    {
                        return (false, null, "Unpopulated Boxes");
                    }
                    else
                    {
                        return (true, boxes, null);
                    }
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves a particular Box by Id
        /// </summary>
        /// <param name="boxId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task<(bool IsSuccess, Box? box, string? ErrorDescription)> GetBoxeById(string userId, string ipAddress, string boxId)
        {
            try
            {
                var filterBox = Builders<Box>.Filter.Eq(x => x.Id, boxId);
                var resultBox = await _box.Find(filterBox).FirstOrDefaultAsync();

                if (resultBox == null)
                {
                    return (false, null, "Unpopulated Boxes");
                }
                else
                {
                    return (true, resultBox, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Inserts / Updates a Box object
        /// </summary>
        /// <param name="box"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, Box? box, string? ErrorDescription)> SaveBox(string userId, string ipAddress, Box box)
        {
            try
            {
                if(box.Id == null)
                {
                    box.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterBox = Builders<Box>.Filter.Eq("_id", new ObjectId(box.Id));
                var updateBoxOptions = new ReplaceOptions { IsUpsert = true };
                var resultBox = await _box.ReplaceOneAsync(filterBox, box, updateBoxOptions);

                return (true, box, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }

        }


        /// <summary>
        /// Retrieves a list of Business Seasons with the posibility to receive an optional paremeter
        /// </summary>
        /// <param name="filterCondition"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, List<SeasonBusiness>? seasonBusinesses, string? ErrorDescription)> GetSeasons(string userId, string ipAddress, string? filterCondition = null)
        {
            try
            {
                string filter = filterCondition == null ? "" : filterCondition;

                if (filter.Length > 0)
                {
                    var pipeline = new List<BsonDocument>();

                    pipeline.Add(
                        new BsonDocument(
                            "$match", new BsonDocument(
                                "Name", new BsonDocument("$regex", new BsonRegularExpression($"/{filter}/i"))
                            )
                        )
                    );
                    List<SeasonBusiness> results = await _season.Aggregate<SeasonBusiness>(pipeline).ToListAsync();
                    return (true, results, null);
                }
                else
                {
                    var season = await _season.Find<SeasonBusiness>(new BsonDocument()).ToListAsync();

                    if (season == null || season.Count == 0)
                    {
                        return (false, null, "Unpopulated Business Seasons");
                    }
                    else
                    {
                        return (true, season, null);
                    }
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves a particular Business Season by Id
        /// </summary>
        /// <param name="seasonId"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, SeasonBusiness? seasonBusiness, string? ErrorDescription)> GetSeason(string userId, string ipAddress, string seasonId)
        {
            try
            {
                var filterSeason = Builders<SeasonBusiness>.Filter.Eq(x => x.Id, seasonId);
                var resultSeason = await _season.Find(filterSeason).FirstOrDefaultAsync();

                if (resultSeason == null)
                {
                    return (false, null, "Unpopulated Boxes");
                }
                else
                {
                    return (true, resultSeason, null);
                }
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Inserts / Updates a Business Season object
        /// </summary>
        /// <param name="seasonBusiness"></param>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, SeasonBusiness? seasonBusiness, string? ErrorDescription)> SaveSeason(string userId, string ipAddress, SeasonBusiness seasonBusiness)
        {
            try
            {
                if(seasonBusiness.Id == null)
                {
                    seasonBusiness.Id = ObjectId.GenerateNewId().ToString();
                }

                var filterSeason = Builders<SeasonBusiness>.Filter.Eq("_id", new ObjectId(seasonBusiness.Id));
                var updateSeasonOptions = new ReplaceOptions { IsUpsert = true };
                var resultSeason = await _season.ReplaceOneAsync(filterSeason, seasonBusiness, updateSeasonOptions);

                return (true, seasonBusiness, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves the Models of a Transactional Items
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="trasnsactionalItemId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<(bool IsSuccess, List<ProductModel>? productModels, string? ErrorDescription)> GetListModelsByTransactionalItemId(string userId, string ipAddress, string trasnsactionalItemId)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Retrieves a list with the different posble process for a Transactional Item Type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemTypeId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, List<TransactionalItemProcessStep>? transactionalItemProcessSteps, string? ErrorDescription)> GetTransactionalItemProcessStepsByTypeID(string userId, string ipAddress, string transactionalItemTypeId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument("$unwind", "$TransactionalItemProcesses")
                );

                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(transactionalItemTypeId)))
                );

                pipeline.Add(
                    new BsonDocument("$project", new BsonDocument("TransactionalItemProcesses", 1))
                );

                pipeline.Add(
                    new BsonDocument("$replaceRoot", new BsonDocument("newRoot", "$TransactionalItemProcesses"))
                );

                var resultPrev = await _transactionalItemTypes.Aggregate<BsonDocument>(pipeline).ToListAsync();

                List<TransactionalItemProcessStep> result = resultPrev.Select(d => BsonSerializer.Deserialize<TransactionalItemProcessStep>(d)).ToList();

                return (true, result, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves a list with the different possible characterisitics for a Transactional Item Type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemTypeId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, List<TransactionalItemTypeCharacteristic>? transactionalItemTypeCharacteristics, string? ErrorDescription)> GetTransactionalItemTypeCharacteristicByTypeID(string userId, string ipAddress, string transactionalItemTypeId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument("$unwind", "$TransactionalItemTypeCharacteristics")
                );

                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(transactionalItemTypeId)))
                );

                pipeline.Add(
                    new BsonDocument("$project", new BsonDocument("TransactionalItemTypeCharacteristics", 1))
                );

                pipeline.Add(
                    new BsonDocument("$replaceRoot", new BsonDocument("newRoot", "$TransactionalItemTypeCharacteristics"))
                );

                var resultPrev = await _transactionalItemTypes.Aggregate<BsonDocument>(pipeline).ToListAsync();

                List<TransactionalItemTypeCharacteristic> result = resultPrev.Select(d => BsonSerializer.Deserialize<TransactionalItemTypeCharacteristic>(d)).ToList();

                return (true, result, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves a list with the different possible Quality Parameters for a Transactional Item Type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemTypeId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, List<TransactionalItemQuality>? transactionalItemQualities, string? ErrorDescription)> GetQualityParametersByTypeID(string userId, string ipAddress, string transactionalItemTypeId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument("$unwind", "$QualityParameters")
                );

                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(transactionalItemTypeId)))
                );

                pipeline.Add(
                    new BsonDocument("$project", new BsonDocument("QualityParameters", 1))
                );

                pipeline.Add(
                    new BsonDocument("$replaceRoot", new BsonDocument("newRoot", "$QualityParameters"))
                );

                var resultPrev = await _transactionalItemTypes.Aggregate<BsonDocument>(pipeline).ToListAsync();

                List<TransactionalItemQuality> result = resultPrev.Select(d => BsonSerializer.Deserialize<TransactionalItemQuality>(d)).ToList();

                return (true, result, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }


        /// <summary>
        /// Retrieves a list with the different possible Quality Parameters for a Transactional Item Type
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ipAddress"></param>
        /// <param name="transactionalItemTypeId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(bool IsSuccess, List<RecipeModifier>? recipeModifiers, string? ErrorDescription)> GetRecipeModifiersByTypeID(string userId, string ipAddress, string transactionalItemTypeId)
        {
            try
            {
                var pipeline = new List<BsonDocument>();

                pipeline.Add(
                    new BsonDocument("$unwind", "$InRecipeModifiers")
                );

                pipeline.Add(
                    new BsonDocument("$match", new BsonDocument("_id", new ObjectId(transactionalItemTypeId)))
                );

                pipeline.Add(
                    new BsonDocument("$project", new BsonDocument("InRecipeModifiers", 1))
                );

                pipeline.Add(
                    new BsonDocument("$replaceRoot", new BsonDocument("newRoot", "$InRecipeModifiers"))
                );

                var resultPrev = await _transactionalItemTypes.Aggregate<BsonDocument>(pipeline).ToListAsync();

                List<RecipeModifier> result = resultPrev.Select(d => BsonSerializer.Deserialize<RecipeModifier>(d)).ToList();

                return (true, result, null);
            }
            catch (Exception e)
            {
                return (false, null, e.Message);
            }
        }
    }
}
