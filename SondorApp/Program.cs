namespace SondorApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataManager.LoadItems();

            DataManager.DeleteAllData();

            DataManager.AddItem(new Item("Kolog", 23));
            DataManager.AddItem(new Item("ororo", 23));
            DataManager.AddItem(new Item("Appa", 23));
            DataManager.AddItem(new Item("ororo", 23));

            DataManager.GetData();
        }
    }
}
