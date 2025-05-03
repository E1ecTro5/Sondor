using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SondorApp
{
    public static class ConsoleLog
    {
        public static void Welcome()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("> Добро пожаловать в Sondor!");
            Console.WriteLine("> Пожалуйста, выбреите операцию: ");
            
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("> 0 - получить инфрмацию о всех продуктах на складе.");
            Console.WriteLine("> 1 - добавить элементы на склад.");
            Console.WriteLine("> 2 - взять элементы со склада.");
            Console.WriteLine("> 3 - удалить элемент со склада.");
            Console.WriteLine("> 4 - удалить все данные.");
            Console.WriteLine("> 5 - обновить данные.");
            Console.WriteLine("> quit - выйти.");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("> ");
            string userInput = Console.ReadLine();
        }

        //Ну типо имитация загрузки, чтобы скучно не было :)
        public static void LoadingLog()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Loading" + new string('.', i));
                Thread.Sleep(100);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Loaded");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void SystemLog(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ItemMessageLog(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
