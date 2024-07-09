using CountSymbolsBot.Controllers;
using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using CountSymbolsBot.Services;
using CountSymbolsBot.Configuration;

namespace CountSymbolsBot
{
    public class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            // Объект, отвечающий за постоянный жизненный цикл приложения
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
                .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
                .Build(); // Собираем

            Console.WriteLine("Сервис запущен");
            // Запускаем сервис
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }


        static void ConfigureServices(IServiceCollection services)
        {
            // Подключаем контроллеры сообщений и кнопок
            services.AddTransient<DefaultMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();

            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(BuildAppSettings());

            services.AddSingleton<IStorage, MemoryStorage>();

            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("7352283408:AAFPshWXhmoxpdiCC1YPaMPhDQfFQBOqohE"));
            services.AddHostedService<Bot>();

        }

        static AppSettings BuildAppSettings()
        {
            return new AppSettings()
            {
                BotToken = "7352283408:AAFPshWXhmoxpdiCC1YPaMPhDQfFQBOqohE",
            };
        }
    }
}
       