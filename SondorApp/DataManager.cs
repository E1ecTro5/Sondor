using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SondorApp
{
    //Что я написал....
    static class DataManager
    {
        //Засунул всё в .dat файл рядом с .exe'шником || Можно было бы и SQLite использовать, но для маленького проекта решил проигнорировать...
        private static string path = Directory.GetCurrentDirectory() + "\\main.dat";
        private static List<Item> items = new List<Item>();

        public static void LoadItems()
        {
            List<Item> loaded = new List<Item>();

            using (StreamReader reader = new(path))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    string itemName = line.Split(";")[0];
                    int itemCount = Convert.ToInt32(line.Split(";")[1]); // Исключения не обработал тут, при AddItem проверять буду наверное..
                    DateTime transactionTime = Convert.ToDateTime(line.Split(";")[2]);

                    //check for IsDeleting
                    if (line.Split(";")[3] == "True")
                    {
                        Item target = loaded.Where(x => x.Name == line.Split(";")[0]).First();
                        loaded[loaded.IndexOf(target)].Count -= Convert.ToInt32(line.Split(";")[1]);

                        if(loaded[loaded.IndexOf(target)].Count == 0)
                            loaded.Remove(loaded.Where(x => x.Name == line.Split(";")[0]).First());
                    }
                    //В файле повторяться могут предметы, поэтому тут их объединять буду; файл как историю добавления/удаления можно использовать
                    else if (loaded.Select(x => x.Name).Contains(itemName))
                    {
                        Item previous = loaded.Where(x => x.Name == itemName).First();
                        Item change = new Item(itemName, previous.Count + itemCount, previous.TransactionTime);
                        loaded[loaded.IndexOf(previous)] = change;
                    }
                    else
                        loaded.Add(new Item(itemName, itemCount, transactionTime));
                }
            }

            items = loaded;
        }

        public static void AddItem(Item item)
        {
            if (item is null)
                return;

            bool isIncreaseExisting = false;

            //Existency check
            if (items.Select(x => x.Name).Contains(item.Name))
            {
                isIncreaseExisting = true;
            }

            using (StreamWriter writer = new(path, true))
            {
                writer.WriteLine($"{item.Name};{item.Count};{item.TransactionTime};{item.IsDeleting}"); //Здесь ; используется как separator
                if (isIncreaseExisting)
                    ConsoleLog.ItemMessageLog($"Добавлено {item.Count}шт к {item.Name}");
                else
                    ConsoleLog.ItemMessageLog($"{item.Name} добавлен в количестве {item.Count}шт");
            }

            LoadItems();
        }

        public static void TakeItems(Item inputItem)
        {
            //LoadItems();

            Item target = null;

            try
            {
                target = items.Where(x => x.Name == inputItem.Name).First();
            }
            catch (InvalidOperationException)
            {
                ConsoleLog.ErrorMessage("Такого продукта нет!");
                return;
            }

            if(inputItem.Count > target.Count)
            {
                ConsoleLog.ErrorMessage("Столько нет на складе!");
                return;
            }

            int index = items.IndexOf(target);
            target.Count -= inputItem.Count;
            items[index] = target;

            using (StreamWriter writer = new(path, true))
            {
                writer.WriteLine($"{inputItem.Name};{inputItem.Count};{DateTime.Now};True");
            }

            ConsoleLog.ItemMessageLog($"Взято {inputItem.Count}шт {inputItem.Name} со склада.");
        }

        public static void GetData()
        {
            LoadItems();

            //Получилось костыльно, но зато работает :) И да, тут всё в хардкоде, неадеюсь не убьёте за такое))
            Console.WriteLine("+" + new string('-', 29) + "+" + new string('-', 20) + "+");
            Console.WriteLine("| Название товара".PadRight(18) + "|" + " Кол-во".PadLeft(10) + " | " + "Дата появления".PadRight(19) + "|");
            for (int i = 0; i < items.Count; i++)
            {
                Console.Write("| " + items[i].Name.PadRight(16, '.') + "|" + items[i].Count.ToString().PadLeft(10, '.') + " | " + items[i].TransactionTime.ToString().PadRight(19) + "|\n");
            }
            Console.WriteLine("+" + new string('-', 29) + "+" + new string('-', 20) + "+");
        }

        public static void GetAllHistory()
        {
            //LoadItems(); это тут не сработает
            //List<string> transactions = new List<string>();

            Console.WriteLine("+" + new string('-', 17) + "+" + new string('-', 11) + "+" + new string('-', 20) + "+" + new string('-', 20) + "+");
            Console.WriteLine("| Название товара".PadRight(18) + "|" + " Кол-во".PadLeft(10) + " | " + "Дата транзакции".PadRight(18) + " | " + "Тип транзакции".PadRight(18) + " |");

            using (StreamReader reader = new(path))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] itemInfo = line.Split(";"); //Надо было и в других методах так сделать для удобства.

                    string itemName = itemInfo[0];
                    string itemCount = itemInfo[1];
                    string itemTransactionTime = itemInfo[2];
                    string itemTransactionType = itemInfo[3];
                    if (itemTransactionType == "True")
                        itemTransactionType = "Убавление";
                    else
                        itemTransactionType = "Добавление";

                        Console.WriteLine($"| {itemName.PadRight(15)} | {itemCount.PadLeft(9)} | {itemTransactionTime.PadRight(18)} | {itemTransactionType.PadRight(18)} |");
                }
            }

            Console.WriteLine("+" + new string('-', 17) + "+" + new string('-', 11) + "+" + new string('-', 20) + "+" + new string('-', 20) + "+");
        }

        public static void DeleteItem(string itemName)
        {
            Item target = null;

            try
            {
                target = items.Where(x => x.Name == itemName).First();
            }
            catch (InvalidOperationException)
            {
                ConsoleLog.ErrorMessage("Такого продукта нет!");
                return;
            }

            target.IsDeleting = true;
            items.Remove(target);

            using (StreamWriter writer = new(path, true))
            {
                writer.WriteLine($"{target.Name};{target.Count};{DateTime.Now};{target.IsDeleting}");
            }

            ConsoleLog.ItemMessageLog($"Продукт {target.Name} удален.");

            LoadItems();
        }

        /// <summary>
        /// ИСКЛЮЧИТЕЛЬНО ДЛЯ ДЕБАГА; Этот метод также стирает всю историю в main.dat
        /// </summary>
        public static void DeleteAllData()
        {
            using (StreamWriter writer = new(path, false))
            {
                writer.Write(string.Empty);
            }

            ConsoleLog.SystemLog("ALL DATA DELETED");

            LoadItems();
        }
    }
}
