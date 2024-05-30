namespace MinioConsumer.DependencyInjection.ConfigSettings;
public record RedisDbSettings
{
    public string Host { get; set; }

    public int Port { get; set; }

    public string ConnectionUrl { get; set; }
}
