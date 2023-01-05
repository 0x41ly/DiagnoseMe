using System;
using Core.Application.Common.Interfaces.Services;

namespace Core.Infrastructure.Services;



public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}