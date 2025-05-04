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

            ChooseOperation();
        }

        public static void ChooseOperation()
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("> 0 - получить инфрмацию о всех продуктах на складе.");
            Console.WriteLine("> 1 - добавить элементы на склад.");
            Console.WriteLine("> 2 - взять элементы со склада.");
            Console.WriteLine("> 3 - удалить элемент со склада.");
            Console.WriteLine("> 4 - обновить данные.");
            Console.WriteLine("> getItemHistory - история транзакций предмета.");
            Console.WriteLine("> getAll - вся история транзакций.");
            Console.WriteLine("> deleteAll - удалить все данные. (история также будет удалена)");
            Console.WriteLine("> quit - выйти.");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
        }

        //Ну типо имитация загрузки, чтобы скучно не было :)
        public static void LoadingLog()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Clear();

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("Loading" + new string('.', i));
                Thread.Sleep(500);
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Loaded");
            Thread.Sleep(1000);
            Console.Clear();

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

        public static string UserInputRequest(string requestItem)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"> Пожалуйста, введите {requestItem}: ");
            string userInput = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;

            //Check на null сделай
            return userInput;
        }

        public static void ErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
