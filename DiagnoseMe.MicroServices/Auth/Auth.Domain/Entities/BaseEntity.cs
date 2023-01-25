using System.ComponentModel.DataAnnotations;

namespace Auth.Domain.Entities;

public abstract class BaseEntity
{
    public string? Id {get; set;}
    public DateTime CreationDate {get; set;}
    [ConcurrencyCheck]
    public string? ConcurrencyStamp { get; set; }
}