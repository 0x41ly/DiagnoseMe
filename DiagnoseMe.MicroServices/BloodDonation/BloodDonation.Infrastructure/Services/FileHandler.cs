using BloodDonation.Application.Common.Interfaces.Services;
using ErrorOr;

namespace BloodDonation.Infrastructure.Services;

public class FileHandler : IFileHandler
{
    public ErrorOr<string> SaveFile(byte[] file)
    {
        throw new NotImplementedException();
    }
}