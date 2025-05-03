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

                    //check for IsDeleting
                    if (line.Split(";")[2] == "True")
                    {
                        loaded.Remove(loaded.Where(x => x.Name == line.Split(";")[0]).First());
                    }
                    //В файле повторяться могут предметы, поэтому тут их объединять буду; файл как историю добавления/удаления можно использовать
                    else if (loaded.Select(x => x.Name).Contains(itemName))
                    {
                        Item previous = loaded.Where(x => x.Name == itemName).First();
                        Item change = new Item(itemName, previous.Count + itemCount);
                        loaded[loaded.IndexOf(previous)] = change;
                    }
                    else
                        loaded.Add(new Item(itemName, itemCount));
                }
            }

            items = loaded;
        }

        public static void AddItem(Item item)
        {
            bool added = false;

            //Negative value check
            if(item.Count <= 0)
            {
                Console.WriteLine("Значение должно быть больше нуля!"); //Я потом более красиво это оформлю, пока что так для проверки оставим
                return;
            }

            //Existency check
            if(items.Select(x => x.Name).Contains(item.Name))
            {
                added = true;
            }

            //Если новый
            using (StreamWriter writer = new(path, true))
            {
                writer.WriteLine($"{item.Name};{item.Count};{item.IsDeleting}"); //Здесь ; используется как separator
                if(added)
                    Console.WriteLine($"Добавлено {item.Count}шт к {item.Name}");
                else
                    Console.WriteLine($"{item.Name} добавлен в количестве {item.Count}шт");
            }

            LoadItems();
        }

        public static void GetData()
        {
            LoadItems();

            foreach(Item item in items)
            {
                Console.WriteLine($"{item.Name} в количестве: {item.Count}шт");
            }
        }

        public static void DeleteItem(string itemName)
        {
            //LoadItems();

            Item target = items.Where(x => x.Name == itemName).First();
            target.IsDeleting = true;
            items.Remove(target);

            using (StreamWriter writer = new(path, true))
            {
                writer.WriteLine($"{target.Name};{target.Count};{target.IsDeleting}");
            }
        }

        /// <summary>
        /// ИСКЛЮЧИТЕЛЬНО ДЛЯ ДЕБАГА
        /// </summary>
        public static void DeleteAllData()
        {
            using (StreamWriter writer = new(path, false))
            {
                writer.Write(string.Empty);
            }

            Console.WriteLine("DELETED");

            LoadItems();
        }
    }
}
