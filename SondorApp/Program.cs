namespace SondorApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleLog.SystemLog("Starting program..\n");
            ConsoleLog.LoadingLog();
            ConsoleLog.Welcome();

            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();

                if (input == "quit")
                    break;

                UserCommands.GetCommand(input);
            }
        }
    }
}
