using Microsoft.EntityFrameworkCore;

namespace Domain.Entities;

[PrimaryKey("FileName")]
public class Document
{
    public string Name { get; set; }
    
    public string FileName { get; set; }
}