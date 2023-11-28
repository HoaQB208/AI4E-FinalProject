using System.Diagnostics;
using System.IO;

namespace FinalProject.ML.Models
{
    public class FileUtils
    {
        public static string ReadFile(string pathFile)
        {
            string st = "";
            if (File.Exists(pathFile))
            {
                try
                {
                    StreamReader sr = new(pathFile);
                    st = sr.ReadToEnd();
                    sr.Close();
                }
                catch { }
            }
            return st;
        }

        public static string WriteFile(string pathFile, string content)
        {
            try
            {
                string pathfolder = Path.GetDirectoryName(pathFile)!;
                if (!Directory.Exists(pathfolder)) Directory.CreateDirectory(pathfolder);
                StreamWriter sw = new(pathFile);
                sw.Write(content);
                sw.Close();
            }
            catch (Exception ex) { return ex.ToString(); }
            return "";
        }

        public static List<string> GetJsonFileNames(string folderPath)
        {
            List<string> files = new();
            if (Directory.Exists(folderPath))
            {
                string[] paths = Directory.GetFiles(folderPath);
                foreach (string item in paths)
                {
                    if (item.ToLower().EndsWith(".json"))
                    {
                        files.Add(Path.GetFileName(item));
                    }
                }
            }
            files.Reverse();
            return files;
        }

        public static void ShowFolder(string folder)
        {
            if (Directory.Exists(folder)) Process.Start(new ProcessStartInfo(folder) { UseShellExecute = true });
        }
    }
}