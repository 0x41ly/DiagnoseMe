using System;
using Auth.Application.Common.Interfaces.Services;

namespace Auth.Infrastructure.Services;



public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}