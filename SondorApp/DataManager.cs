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
        static string path = Directory.GetCurrentDirectory() + "\\main.dat";

        public static void AddItem(Item item)
        {
            using(StreamWriter writer = new(path, true))
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
