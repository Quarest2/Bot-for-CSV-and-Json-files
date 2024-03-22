using Telegram.Bot;
using Telegram.Bot.Types;
using WorkWithCSVLib;

namespace MyBot;

/// <summary>
/// Handler for document message.
/// </summary>
public static class DocumentHandler
{
    /// <summary>
    /// Main method for DocumentHandler.
    /// </summary>
    /// <param name="botClient">Client.</param>
    /// <param name="update">Update.</param>
    /// <param name="cancellationToken">Canselation token.</param>
    /// <exception cref="Exception">Exception if something wrong with file.</exception>
	public static async void Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
	{
        var message = update.Message;
        if (message == null)
        {
            return;
        }
        var user = message.From;
        if(user == null)
        {
            return;
        }
        Console.WriteLine($"{user.FirstName} ({user.Id}) отправил файл.");

        var chat = message.Chat;
        if (update.Message == null || update.Message.Document == null)
        {
            return;
        }
        var fileId = update.Message.Document.FileId;
        var fileInfo = await botClient.GetFileAsync(fileId);
        var filePath = fileInfo.FilePath;
        if (filePath == null)
        {
            await botClient.SendTextMessageAsync(chat.Id, "Ошибка при работе с файлом");
            return;
        }

        try
        {
            string destinationFilePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/{update.Message.Document.FileName}";
            await using Stream fileStream = System.IO.File.Create(destinationFilePath);
            await botClient.DownloadFileAsync(filePath, fileStream, cancellationToken);
            fileStream.Close();

            // CSV
            if (!UpdHandler.mode)
            {
                FileStream fs = new FileStream(destinationFilePath, FileMode.OpenOrCreate);
                var CSV = new CSVProcessing();
                Program.attractions = CSV.Read(fs);
                fs.Close();
            }
            // JSON
            else
            {
                FileStream fs = new FileStream(destinationFilePath, FileMode.OpenOrCreate);
                var JSON = new JSONProcessing();
                Program.attractions = JSON.Read(fs);
                fs.Close();
            }

            await botClient.SendTextMessageAsync(chat.Id, "Файл успешно прочитан! Еще раз откройте клавиатуру и выберете действие [/bottons]");
        }
        catch
        {
            throw new Exception("Something wrong with file.");
        }



    }
}

