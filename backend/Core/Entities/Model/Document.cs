using Microsoft.EntityFrameworkCore;

namespace Entities.Model;

[PrimaryKey("FileName")]
public class Document
{
    public string Name { get; set; }
    
    public string FileName { get; set; }
}