using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SondorApp
{
    public static class UserCommands
    {
        public static void GetCommand(string userInput)
        {
            Console.Clear();
            ConsoleLog.ChooseOperation();
            Console.WriteLine();

            switch (userInput)
            {
                case "0":
                    Console.WriteLine("Каталог всех продуктов:\n");
                    DataManager.GetData();
                    break;
                case "1":
                    Console.WriteLine("Выбрано добавление продукта:\n");
                    DataManager.AddItem(UserItemAddRequest());
                    break;
                case "2":
                    Console.WriteLine("Выбрано взятие продуктов:\n");
                    DataManager.TakeItems(UserItemAddRequest());
                    break;
                case "3":
                    Console.WriteLine("Выбрано удаление продукта:\n");
                    DataManager.DeleteItem(UserItemDeleteRequest());
                    break;
                case "4":
                    Console.WriteLine("Предметы обновлены.");
                    DataManager.LoadItems();
                    break;
                case "getItemHistory":
                    Console.WriteLine("История транзакций продукта на складе:\n");
                    DataManager.GetItemHistory(UserItemDeleteRequest());
                    break;
                case "getAll":
                    Console.WriteLine("История транзакций на складе.\n");
                    DataManager.GetAllHistory();
                    break;
                case "deleteAll":
                    DataManager.DeleteAllData();
                    break;
            }
        }

        private static Item UserItemAddRequest()
        {
            //Изначально проверка на пустое имя и неправильное кол-во было в DataManager.AddItem, однако, мне показалось, что реализация
            //прямо тут будет более уместной. В самом AddItem я добавил проверку на null
            string name = ConsoleLog.UserInputRequest("имя продукта");

            if (name is null || name == string.Empty)
            {
                ConsoleLog.ErrorMessage("Имя должно быть указанно!");
                return null;
            }

            if (name.Contains(";"))
            {
                ConsoleLog.ErrorMessage("Нельзя указывать символ ';'!");
                return null;
            }

            int count = -1;
            try
            {
                count = Convert.ToInt32(ConsoleLog.UserInputRequest("кол-во продукта"));
            }
            catch (FormatException fe)
            {
                ConsoleLog.ErrorMessage("Введите число!");
            }

            if (count <= 0)
            {
                ConsoleLog.ErrorMessage("Значение должно быть больше нуля!");
                return null;
            }

            Item result = new Item(name, count, DateTime.Now);
            return result;
        }

        private static string UserItemDeleteRequest()
        {
            string name = ConsoleLog.UserInputRequest("имя продукта");
            return name;
        }
    }
}
