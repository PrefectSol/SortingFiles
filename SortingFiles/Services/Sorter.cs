namespace SortingFiles.Services
{
    internal class Sorter
    {
        private static string FolderPath;
        private static Dictionary<string, string[]> sortConfig;

        public Sorter(string FolderPath, Dictionary<string, string[]> sortConfig) 
        {
            Sorter.FolderPath = FolderPath;
            Sorter.sortConfig = sortConfig;
        }
        public void Sort()
        {   
            string[] files = Directory.GetFiles($@"{FolderPath}");

            foreach (var e in sortConfig)
            {
                Directory.CreateDirectory(@$"{FolderPath}\{e.Key}");
            }

            foreach(var e in sortConfig)
            {
                foreach(var f in e.Value)
                {
                    foreach (string file in files)
                    {
                        if (Path.GetExtension(file) == f)
                        {
                            File.Move(file, @$"{FolderPath}\{e.Key}\{Path.GetFileName(file)}");
                            File.Delete(file);
                        }
                    }
                }
            }

            Console.WriteLine("Complete");
        }
    }
}