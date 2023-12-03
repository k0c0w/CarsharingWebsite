namespace Contracts;

public record SaveCarModelImageDto
{
    public int CarModelId { get; set; }
    public byte[] Image { get; set; }

    public string ImageName { get; set; }
}