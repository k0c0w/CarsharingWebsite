namespace MinioConsumer.DependencyInjection.ConfigSettings;
public class RedisDbSettings
{
    public string Host { get; set; }

    public int Port { get; set; }

    public string Password { get; set; }

    public string GetConnectionString() => $"{Host}:{Port},password={Password}";
}
