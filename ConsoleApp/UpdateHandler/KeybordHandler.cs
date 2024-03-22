using Telegram.Bot;
using Telegram.Bot.Types;

namespace MyBot;

/// <summary>
/// Handler for keyboard.
/// </summary>
public static class KeybordHandler
{
    /// <summary>
    /// Main method for KeyboardHandler.
    /// </summary>
    /// <param name="botClient">Client.</param>
    /// <param name="update">Update.</param>
	public static async void Handle(ITelegramBotClient botClient, Update update)
	{
        var callbackQuery = update.CallbackQuery;
        if (callbackQuery == null)
        {
            return;
        }
        var user = callbackQuery.From;

        Console.WriteLine($"{user.FirstName} ({user.Id}) нажал на кнопку: {callbackQuery.Data}");

        if (callbackQuery.Message == null)
        {
            return;
        }
        var chat = callbackQuery.Message.Chat;
        switch (callbackQuery.Data)
        {
            case "buttonCSV":
                {
                    await botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "Надо помыслить");

                    await botClient.SendTextMessageAsync(
                    chat.Id,
                        $"Вы нажали на {callbackQuery.Data}. Теперь отправьте CSV-файл.");
                    UpdHandler.mode = false;
                    return;
                }

            case "buttonJSON":
                {
                    await botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "Надо помыслить");

                    await botClient.SendTextMessageAsync(
                    chat.Id,
                        $"Вы нажали на {callbackQuery.Data}. Теперь отправьте JSON-файл.");
                    UpdHandler.mode = true;
                    return;
                }
            case "buttonSelectionDistrict":
                {
                    await botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "Введите значение, которое хотите искать.", showAlert: true);
                    UpdHandler.waiting = WaitingMode.WaitingDistrict;
                    await botClient.SendTextMessageAsync(
                    chat.Id,
                        $"Вы нажали на {callbackQuery.Data}, ожидается значение");
                    return;
                }
            case "buttonSelectionLocationType":
                {
                    await botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "Введите значение, которое хотите искать.", showAlert: true);
                    UpdHandler.waiting = WaitingMode.WaitingLocType;

                    await botClient.SendTextMessageAsync(
                    chat.Id,
                        $"Вы нажали на {callbackQuery.Data}, ожидается значение");
                    return;
                }

            case "buttonSelectionAdmAreaAndLocation":
                {
                    await botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "Введите два значения (сначала AdmArea, потом Location, через точку с запятой)" +
                        ", которые хотите искать.", showAlert: true);
                    UpdHandler.waiting = WaitingMode.WaitingAdmAndLoc;

                    await botClient.SendTextMessageAsync(
                    chat.Id,
                        $"Вы нажали на {callbackQuery.Data}, ожидается два значения");
                    return;
                }

            case "buttonSortingAdmAreaAlphabet":
                {
                    await botClient.SendTextMessageAsync(
                        chat.Id,
                        $"Вы нажали на {callbackQuery.Data}");
                    if (Program.attractions == null)
                    {
                        await botClient.SendTextMessageAsync(chat.Id, "Нечего сортировать, сначала загрузите файл.");
                        return;
                    }
                    try
                    {
                        Program.attractions = WorkWithCSVLib.Sorting.SortAdmAreaAlphabet(Program.attractions);
                        await botClient.SendTextMessageAsync(chat.Id, "Сортировка совершена успешно!");

                        if (!UpdHandler.mode)
                        {
                            var csv = new WorkWithCSVLib.CSVProcessing();
                            var stream = csv.Write(Program.attractions);
                            await botClient.SendDocumentAsync(
                                chatId: chat.Id,
                                document: InputFile.FromStream(stream: stream, fileName: "result.csv"),
                                caption: "Attraction_TC");
                        }
                        else
                        {
                            var json = new WorkWithCSVLib.JSONProcessing();
                            var stream = json.Write(Program.attractions);
                            await botClient.SendDocumentAsync(
                                chatId: chat.Id,
                                document: InputFile.FromStream(stream: stream, fileName: "result.json"),
                                caption: "Attraction_TC");
                        }

                    }
                    catch
                    {
                        await botClient.SendTextMessageAsync(chat.Id, "Не получилось провести сортировку" +
                            "(Если было сообщение об успешном выполнении сортировки, случилась проблема с выгрузкой файла с результатом)");
                        return;
                    }
                    return;
                }
            case "buttonSortingAdmAreaDescendingAlphabet":
                {
                    await botClient.SendTextMessageAsync(
                        chat.Id,
                        $"Вы нажали на {callbackQuery.Data}");
                    await botClient.SendTextMessageAsync(
                       chat.Id,
                       $"Вы нажали на {callbackQuery.Data}");
                    if (Program.attractions == null)
                    {
                        await botClient.SendTextMessageAsync(chat.Id, "Нечего сортировать, сначала загрузите файл.");
                        return;
                    }
                    try
                    {
                        Program.attractions = WorkWithCSVLib.Sorting.SortAdmAreaDescendingAlphabet(Program.attractions);
                        await botClient.SendTextMessageAsync(chat.Id, "Сортировка совершена успешно!");

                        if (!UpdHandler.mode)
                        {
                            var csv = new WorkWithCSVLib.CSVProcessing();
                            var stream = csv.Write(Program.attractions);
                            await botClient.SendDocumentAsync(
                                chatId: chat.Id,
                                document: InputFile.FromStream(stream: stream, fileName: "result.csv"),
                                caption: "Attraction_TC");
                        }
                        else
                        {
                            var json = new WorkWithCSVLib.JSONProcessing();
                            var stream = json.Write(Program.attractions);
                            await botClient.SendDocumentAsync(
                                chatId: chat.Id,
                                document: InputFile.FromStream(stream: stream, fileName: "result.json"),
                                caption: "Attraction_TC");
                        }

                    }
                    catch
                    {
                        await botClient.SendTextMessageAsync(chat.Id, "Не получилось провести сортировку" +
                            "(Если было сообщение об успешном выполнении сортировки, случилась проблема с выгрузкой файла с результатом)");
                        return;
                    }
                    return;
                }
            default:
                {
                    await botClient.SendTextMessageAsync(chat.Id, "Как ты вообще это сделал!?");
                    return;
                }
        }
    }
}

