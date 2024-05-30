namespace Contracts
{
    public record CarModelImageSaveResultDto
    {
        public int CarModelId { get; set; }
        public bool Success { get; set; }

        public string Url { get; set;  }
    }
}
