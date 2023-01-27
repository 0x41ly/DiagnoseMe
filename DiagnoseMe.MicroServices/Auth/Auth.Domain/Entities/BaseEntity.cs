using System.ComponentModel.DataAnnotations;

namespace Auth.Domain.Entities;

public abstract class BaseEntity
{
    public string? Id {get; set;}
    public DateTime CreatedOn {get; set;}
    [ConcurrencyCheck]
    public string? ConcurrencyStamp { get; set; }
}