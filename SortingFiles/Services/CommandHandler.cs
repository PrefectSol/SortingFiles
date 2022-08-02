namespace SortingFiles.Services
{
    internal class CommandHandler
    {
        public const char Prefix = '-';

        private readonly string command;
        private static string? folderName;
        private static string[]? exts;
        private static string? FolderPath { get; set; }

        private delegate void _del();

        private static readonly Dictionary<string, string[]> sortConfig = new();
        private readonly Dictionary<string, _del> commandList = new()
        {
            { "Add",  new _del(Add) },
            { "Delete", new _del(Delete) },
            { "Start", new _del(Start) }
        };

        public CommandHandler(string command, string? folderName = null, string[]? exts = null)
        {
            this.command = command;
            CommandHandler.folderName = folderName;
            CommandHandler.exts = exts;
        }

        public void Run()
        {
            if (commandList.ContainsKey(command))
            {
                commandList[command]();
            }
            else
            {
                ErrorCommand();
            }
        }

        private static void Start()
        {
            if(sortConfig.Count != 0)
            {
                Sorter sorter = new(FolderPath, sortConfig);
                sorter.Sort();
            }
            else
            {
                ErrorCommand();
            }
        }

        public static void SetFolder()
        {
            Console.Write("Path: ");
            FolderPath = Console.ReadLine();
            if(FolderPath == string.Empty || FolderPath == null)
            {
                ErrorCommand();
                SetFolder();
            }
        }

        private static void Add()
        {
            if (folderName != null && exts != null)
            {
                sortConfig.Add(folderName, exts);
                ShowList();
                return;
            }

            ErrorCommand();
        }

        private static void Delete()
        {
            if (folderName != null && exts != null)
            {
                sortConfig.Remove(folderName);
                ShowList();
                return;
            }

            ErrorCommand();
        }

        private static void ErrorCommand()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Command not found");
            Console.ResetColor();
        }

        private static void ShowList()
        {
            Console.Write("Стек: ");
            foreach (var e in sortConfig)
            {
                Console.Write(e.Key + ": ");
                foreach(var j in e.Value)
                {
                    Console.Write(j + " ");
                }
                Console.Write(" | ");
            }
            Console.WriteLine();
        }
    }
}