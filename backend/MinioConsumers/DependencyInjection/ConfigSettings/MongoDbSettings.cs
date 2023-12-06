namespace MinioConsumer.DependencyInjection.ConfigSettings
{
    public class MongoDbSettings
    {
        public string User { get; set; }

        public string Password { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

        public string DatabaseName { get; set; }

        public string GetConnectionString() => $"mongodb://{User}:{Password}@{Host}:{Port}/";
    }
}
