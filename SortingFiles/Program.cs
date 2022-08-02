namespace SortingFiles
{
    using SortingFiles.Services;

    internal class Program
    {
        public static void Main()
        {
            CommandHandler.SetFolder();

            while (true)
            {
                var message = Console.ReadLine();
                if(message != null && message != String.Empty && message[0] == CommandHandler.Prefix)
                {
                    string[] commandLine = message.Split(' ');

                    string command = commandLine[0][1..];
                    string folderName;
                    string[] extensions;

                    try
                    {
                        folderName = commandLine[1];
                        extensions = commandLine[2..];
                    }
                    catch
                    {
                        CommandHandler chStart = new CommandHandler(command);
                        chStart.Run();
                        continue;
                    }

                    CommandHandler ch = new CommandHandler(command, folderName, extensions);
                    ch.Run();
                }
            }
        }
    }
}