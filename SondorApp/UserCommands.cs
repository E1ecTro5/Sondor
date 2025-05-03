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
            switch (userInput)
            {
                case "0":
                    DataManager.GetData();
                    break;
                case "1":
                    DataManager.AddItem(UserItemAddRequest());
                    break;
                case "2":
                    DataManager.TakeItems(UserItemAddRequest());
                    //ConsoleLog.ErrorMessage("Я не реализовал это пока-что))");
                    break;
                case "3":
                    DataManager.DeleteItem(UserItemDeleteRequest());
                    break;
                case "4":
                    DataManager.LoadItems();
                    break;
                case "deleteAll":
                    DataManager.DeleteAllData();
                    break;
            }
        }

        private static Item UserItemAddRequest()
        {
            string name = ConsoleLog.UserInputRequest("имя продукта");
            int count = -1;
            try
            {
                count = Convert.ToInt32(ConsoleLog.UserInputRequest("кол-во продукта"));
            }
            catch (FormatException fe)
            {
                ConsoleLog.ErrorMessage("Введите число!");
            }

            Item result = new Item(name, count);
            return result;
        }

        private static string UserItemDeleteRequest()
        {
            string name = ConsoleLog.UserInputRequest("имя продукта");
            return name;
        }
    }
}
