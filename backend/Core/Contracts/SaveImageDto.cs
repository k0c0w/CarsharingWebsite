namespace Contracts;

public record SaveImageDto
{
    public string Name { get; set; }
    public string BucketName = "models";
    public byte[] Image { get; set; }
}