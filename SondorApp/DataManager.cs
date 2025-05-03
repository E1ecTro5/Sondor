using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SondorApp
{
    static class DataManager
    {
        //Засунул всё в .dat файл рядом с .exe'шником || Можно было бы и SQLite использовать, но для маленького проекта решил проигнорировать...
        static string path = Directory.GetCurrentDirectory() + "\\main.dat";

        public static string GetData()
        {
            return path;
        }
    }
}
