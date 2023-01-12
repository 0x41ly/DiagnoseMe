namespace Core.Application.Common.Interfaces.Services;


public interface IFileHandler
{
    ErrorOr<string> SaveFile(byte[] file);
}