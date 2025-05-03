using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SondorApp
{
    public class Item
    {
        public Item(string name, int count, bool isDeleting = false)
        {
            Name = name;
            Count = count;
            IsDeleting = isDeleting;
        }

        public string Name { get; set; }
        public int Count { get; set; }
        public bool IsDeleting { get; set; }
    }
}
