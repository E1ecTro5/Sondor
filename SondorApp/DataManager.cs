using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SondorApp
{
    static class DataManager
    {
        //Засунул всё в .dat файл рядом с .exe'шником || Можно было бы и SQLite использовать, но для маленького проекта решил проигнорировать...
        private static string path = Directory.GetCurrentDirectory() + "\\main.dat";
        private static List<Item> items;

        public static void LoadItems()
        {
            using (StreamReader reader = new(path))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    string itemName = line.Split(";")[0];
                    int itemCount = Convert.ToInt32(line.Split(";")[1]); // Исключения не обработал тут, при AddItem проверять буду наверное..

                    items.Add(new Item(itemName, itemCount));
                }
            }
        }

        public static void AddItem(Item item)
        {
            //Negative value check
            if(item.Count <= 0)
            {
                Console.WriteLine("Значение должно быть больше нуля!"); //Я потом более красиво это оформлю, пока что так для проверки оставим
                return;
            }

            //Existency check
            if(items.Select(x => x.Name).Contains(item.Name))
            {
                Item previous = items.Where(x => x.Name == item.Name).First();
                Item change = new Item(item.Name, previous.Count + item.Count);
                items[items.IndexOf(previous)] = change;

                Console.WriteLine($"Добавлено {item.Count}шт к {item.Name} | Общее количество теперь: {change.Count}");
            }

            //Если новый
            using (StreamWriter writer = new(path, true))
            {
                writer.WriteLine($"{item.Name};{item.Count}"); //Здесь ; используется как separator
            }
        }

        public static void GetData()
        {
            using(StreamReader reader = new(path))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    string itemName = line.Split(";")[0];
                    string itemCount = line.Split(";")[1];

                    Console.WriteLine("Item: " + itemName + " | " + "Count: " + itemCount);
                }
            }
        }
    }
}
