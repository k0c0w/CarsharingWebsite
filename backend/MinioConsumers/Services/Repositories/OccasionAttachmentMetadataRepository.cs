using Microsoft.Extensions.Options;
using MinioConsumer.DependencyInjection.ConfigSettings;
using MinioConsumer.Models;
using MongoDB.Driver;

namespace MinioConsumer.Services.Repositories
{
    public class OccasionAttachmentMetadataRepository : IMetadataRepository<OccasionAttachmentMetadata>
    {
        private readonly IMongoCollection<OccasionAttachmentMetadata> _collection;

        public OccasionAttachmentMetadataRepository(IMongoClient client, IOptions<MongoDbSettings> settings)
        {
            _collection = client.GetDatabase(settings.Value.DatabaseName).GetCollection<OccasionAttachmentMetadata>("OccasionAttachment");
        }

        public async Task<Guid> AddAsync(OccasionAttachmentMetadata metadata)
        {
            await _collection.InsertOneAsync(metadata);
            return metadata.Id;
        }

        public async Task<IEnumerable<OccasionAttachmentMetadata>> GetAllAsync()
        {
            var cursor = await _collection.FindAsync(x => true);

            return await cursor.ToListAsync();
        }

        public async Task<OccasionAttachmentMetadata?> GetByIdAsync(Guid id)
        {
            var cursor = await _collection.FindAsync(x => x.Id == id);
            return await cursor.FirstOrDefaultAsync();
        }

        public async Task<bool> MetadataExistsByIdAsync(Guid id)
        {
            return (await _collection.FindAsync(x => x.Id == id)).Any();
        }

        public async Task RemoveByIdAsync(Guid id)
        {
            await _collection.FindOneAndDeleteAsync(x => x.Id == id);
        }

        public Task UpdateAsync(OccasionAttachmentMetadata metadata)
        {
            var filter = Builders<OccasionAttachmentMetadata>.Filter.Eq(s => s.Id, metadata.Id);
            var update = Builders<OccasionAttachmentMetadata>.Update
                .Set(s => s.AccessUserList, metadata.AccessUserList);
            return _collection.UpdateOneAsync(filter, update);
        }
    }
}
