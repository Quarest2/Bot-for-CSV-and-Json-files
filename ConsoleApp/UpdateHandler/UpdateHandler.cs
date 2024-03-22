using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace MyBot;

/// <summary>
/// Main handler.
/// </summary>
public class UpdHandler
{
    /// <summary>
    /// Inline keyboard.
    /// </summary>
    public static InlineKeyboardMarkup _inlineKeyboard = new InlineKeyboardMarkup(new List<InlineKeyboardButton[]>()
                                                {
                                        new InlineKeyboardButton[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Загрузить CSV-файл", "buttonCSV"),
                                            InlineKeyboardButton.WithCallbackData("Загрузить JSON-файл", "buttonJSON"),
                                        },
                                        new InlineKeyboardButton[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Сделать выборку по District", "buttonSelectionDistrict"),
                                            InlineKeyboardButton.WithCallbackData("Сделать выборку по LocationType", "buttonSelectionLocationType"),
                                        },
                                        new InlineKeyboardButton[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Сделать выборку по AdmArea и Location", "buttonSelectionAdmAreaAndLocation"),
                                        },
                                        new InlineKeyboardButton[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Сортировка AdmArea(прямой порядок)", "buttonSortingAdmAreaAlphabet"),
                                        },
                                        new InlineKeyboardButton[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("Сортировка AdmArea(обратный порядок)", "buttonSortingAdmAreaDescendingAlphabet"),
                                        },
                                                });

    // false - CSV files mode.
    // true - JSON files mode.
    public static bool mode = false;
    public static WaitingMode waiting;
    public static string? exampleLocType;
    public static string? exampleDist;
    public static string? exampleAdm;
    public static string? exampleLoc;

    /// <summary>
    /// Update handler.
    /// </summary>
    /// <param name="botClient">Client.</param>
    /// <param name="update">Update.</param>
    /// <param name="cancellationToken">Canselation token.</param>
    /// <returns>Task.</returns>
    public static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    {
                        var message = update.Message;
                        if (message == null)
                        {
                            return;
                        }
                        var user = message.From;

                        var chat = message.Chat;

                        switch (message.Type)
                        {
                            case MessageType.Text:
                                {
                                    // TODO Переделать в логирование
                                    Console.WriteLine($"{user.FirstName} ({user.Id}) написал сообщение: {message.Text}");
                                    TextHandler.Handle(botClient, update, _inlineKeyboard);
                                    return;
                                }
                            case MessageType.Document:
                                {
                                    DocumentHandler.Handle(botClient, update, cancellationToken);
                                    return;
                                }
                            default:
                                {
                                    await botClient.SendTextMessageAsync(chat.Id, "Извините, создатель рассчитывал, что вы будете использовать только текст на данном этапе:(");
                                    return;
                                }
                        }
                    }
                case UpdateType.CallbackQuery:
                    {
                        KeybordHandler.Handle(botClient, update);
                        return;
                    }
                default:
                    {
                        return;
                    }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}

/// <summary>
/// Enum for mode.
/// </summary>
public enum WaitingMode
{
    WaitingCommand,
    WaitingLocType,
    WaitingDistrict,
    WaitingAdmAndLoc,
    WaitingAlphabetOrDescending,
}

