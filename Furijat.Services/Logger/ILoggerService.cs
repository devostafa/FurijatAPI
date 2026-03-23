namespace Furijat.Services.Logger;

public interface ILoggerService
{
    public void Log(string message);

    public void Error(string message, Exception ex);
}