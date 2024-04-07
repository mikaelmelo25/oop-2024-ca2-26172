using BankingConsoleApp.Console;
using System;

namespace BankingConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleBank consoleBank = new ConsoleBank();
            consoleBank.MainMenu();
        }
    }
}
