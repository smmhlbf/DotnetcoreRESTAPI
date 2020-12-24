using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotnetcoreRESTAPI.Models;
using DotnetcoreRESTAPI.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DotnetcoreRESTAPI.Repositories
{
    public class MongoDBRepository : IRepository
    {
        private const string DatabaseName = "EleticDB";
        private const string CollectionName = "MobilePhones";
        private IMongoCollection<MobilePhone> collection;
        private FilterDefinitionBuilder<MobilePhone> filterBuilder = Builders<MobilePhone>.Filter;
        public MongoDBRepository(IMongoClient mongoClient)
        => collection = mongoClient.GetDatabase(DatabaseName).GetCollection<MobilePhone>(CollectionName);
        #region Sync module
        // public void CreateMobilePhoneAsync(MobilePhone mobilePhone) => collection.InsertOne(mobilePhone);

        // public void DeleteMobilePhoneAsync(Guid id) => collection.DeleteOne(filterBuilder.Eq(i => i.Id, id));

        // public MobilePhone GetMobilePhoneAsync(Guid id) => collection.Find(i => i.Id == id).SingleOrDefault();

        // public IEnumerable<MobilePhone> GetMobilePhonesAsync() => collection.Find(new BsonDocument()).ToEnumerable();

        // public void UpdateMobilePhoneAsync(MobilePhone mobilePhone)
        // => collection.ReplaceOne(filterBuilder.Eq(i => i.Id, mobilePhone.Id), mobilePhone);
        #endregion
        #region Async module
        public async Task CreateMobilePhoneAsync(MobilePhone mobilePhone) => await collection.InsertOneAsync(mobilePhone);

        public async Task DeleteMobilePhoneAsync(Guid id) => await collection.DeleteOneAsync(filterBuilder.Eq(i => i.Id, id));

        public async Task<MobilePhone> GetMobilePhoneAsync(Guid id) => await collection.Find(i => i.Id == id).SingleOrDefaultAsync();

        public async Task<IEnumerable<MobilePhone>> GetMobilePhonesAsync() => await collection.Find(new BsonDocument()).ToListAsync();

        public async Task UpdateMobilePhoneAsync(MobilePhone mobilePhone)
        => await collection.ReplaceOneAsync(filterBuilder.Eq(i => i.Id, mobilePhone.Id), mobilePhone);
        #endregion
    }
}