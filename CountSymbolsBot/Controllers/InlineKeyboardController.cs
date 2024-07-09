﻿using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using CountSymbolsBot.Services;
using CountSymbolsBot.Models;

namespace CountSymbolsBot.Controllers
{
    public class InlineKeyboardController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            // Обновление пользовательской сессии новыми данными
            _memoryStorage.GetSession(callbackQuery.From.Id).BotMode = callbackQuery.Data;

            // Генерим информационное сообщение
            string BotMode = callbackQuery.Data switch
            {
                "count" => "Подсчет символов в сообщении",
                "sum" => " Подсчет суммы чисел",
                _ => String.Empty
            };

            // Отправляем в ответ уведомление о выборе
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Выбранная функция - {BotMode}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}Можно поменять в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);
        }
    }
}