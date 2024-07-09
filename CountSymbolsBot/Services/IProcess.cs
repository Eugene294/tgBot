using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountSymbolsBot.Services
{
    public interface IProcess
    {
        string Processing(string BotMode, string message);
    }
}
