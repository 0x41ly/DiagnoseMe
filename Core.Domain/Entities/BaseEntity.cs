using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id {get; set;}
    public DateTime CreationDate {get; set;}
    [ConcurrencyCheck]
    public string? ConcurrencyStamp { get; set; }
}