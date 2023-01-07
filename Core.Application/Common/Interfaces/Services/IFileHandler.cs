namespace Core.Application.Common.Interfaces.Services;


public interface IFileHandler
{
    ErrorOr<bool> SaveFile(byte[] file);
}