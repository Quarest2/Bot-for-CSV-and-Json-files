using Telegram.Bot;
using Telegram.Bot.Exceptions;

namespace MyBot;

/// <summary>
/// Error handler.
/// </summary>
public class ErrHandler
{
    /// <summary>
    /// Main handler for ErrHandler.
    /// </summary>
    /// <param name="botClient">Client.</param>
    /// <param name="error">Error</param>
    /// <param name="cancellationToken">Cansellation handler.</param>
    /// <returns>Task.</returns>
    public static Task ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
    {
        var ErrorMessage = error switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => error.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }
}

