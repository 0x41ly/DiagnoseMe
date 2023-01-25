using System;
using MedicalBlog.Application.Common.Interfaces.Services;

namespace MedicalBlog.Infrastructure.Services;



public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}