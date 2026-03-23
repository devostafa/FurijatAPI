using Microsoft.Extensions.Logging;

namespace Furijat.Services.Logger;

public class LoggerService : ILoggerService
{
    private readonly ILogger<LoggerService> _logger;

    public LoggerService(ILogger<LoggerService> logger)
    {
        _logger = logger;
    }

    public void Log(string message)
    {
        _logger.LogInformation("{ DateTime.UtcNow}: {Message}", DateTime.UtcNow, message);
    }

    public void Error(string message, Exception ex)
    {
        _logger.LogError(ex, "{ DateTime.UtcNow}: {Message}", DateTime.UtcNow, message);
    }
}