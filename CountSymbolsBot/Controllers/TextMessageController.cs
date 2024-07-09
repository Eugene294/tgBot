using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using CountSymbolsBot.Services;

namespace CountSymbolsBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($" Подсчет символов" , $"count"),
                        InlineKeyboardButton.WithCallbackData($" Сумма чисел" , $"sum")
                    });

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Наш бот имеет две функции.</b> {Environment.NewLine}" +
                        $"{Environment.NewLine}Можно посчитать количество символов в сообщении или сложить ряд цифр.{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;
                default:
                    
                    break;
            }

            string BotMode = _memoryStorage.GetSession(message.Chat.Id).BotMode;
            string text = Process.Processing(BotMode, message.Text.ToString());
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, text, cancellationToken: ct);



        }
    }
}