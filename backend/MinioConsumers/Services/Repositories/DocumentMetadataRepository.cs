using Microsoft.Extensions.Options;
using MinioConsumer.DependencyInjection.ConfigSettings;
using MinioConsumer.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MinioConsumer.Services.Repositories
{
    public class DocumentMetadataRepository : IMetadataRepository<DocumentMetadata>
    {
        private readonly IMongoCollection<DocumentMetadata> _collection;

        public DocumentMetadataRepository(IMongoClient client, IOptions<MongoDbSettings> settings)
        {
            _collection = client.GetDatabase(settings.Value.DatabaseName).GetCollection<DocumentMetadata>("Documents");
        }

        public async Task<Guid> AddAsync(DocumentMetadata metadata)
        {
            await _collection.InsertOneAsync(metadata);
            return metadata.Id;
        }

        public async Task<IEnumerable<DocumentMetadata>> GetAllAsync()
        {
            var cursor = await _collection.FindAsync(x => true);

            return await cursor.ToListAsync();
        }

        public async Task<DocumentMetadata?> GetByIdAsync(Guid id)
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

        public Task UpdateAsync(DocumentMetadata metadata)
        {
            var filter = Builders<DocumentMetadata>.Filter.Eq(s => s.Id, metadata.Id);
            var update = Builders<DocumentMetadata>.Update
                //.Set(s => s.Annotation, metadata.Annotation)
                .Set(s => s.IsPublic, metadata.IsPublic);
            return _collection.UpdateOneAsync(filter, update);
        }
    }
}
