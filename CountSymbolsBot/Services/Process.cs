using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountSymbolsBot.Services
{
    public class Process
    {
        static public string Processing(string BotMode, string message)
        {
            switch (BotMode)
            {
                case "count":
                    return $"Количество символов = {message.Length}";
                case "sum":
                    string[] nums = message.Split(' ');
                    int sum = 0;
                    foreach (string num in nums) { sum += int.Parse(num); }
                    return $"Сумма чисел  = {sum}";
                default:
                    return "Выберите режим в главном меню";
            }

        }
    }
}
