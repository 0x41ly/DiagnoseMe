using System.ComponentModel.DataAnnotations;

namespace MedicalBlog.Domain.Entities;

public abstract class BaseEntity
{
    public string? Id {get; set;}
    public DateTime CreatedOn {get; set;} = DateTime.Now;
    [ConcurrencyCheck]
    public string? ConcurrencyStamp { get; set; }
}