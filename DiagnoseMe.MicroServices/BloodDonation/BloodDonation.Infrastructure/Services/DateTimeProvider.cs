using System;
using BloodDonation.Application.Common.Interfaces.Services;

namespace BloodDonation.Infrastructure.Services;



public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}