using Microsoft.Win32;
using RGBcube.Models;
using System.IO;

namespace RGBcube.Service
{
    public class FileManager
    {
        public static WorkingFile Open()
        {
            var openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = "d:\\",
                DefaultExt = ".txt",
                Filter = "Text documents (.txt)|*.txt"
            };

            if (openFileDialog1.ShowDialog() != true) return null;

            string filename = openFileDialog1.FileName;
            var file = new WorkingFile
            {
                Content = File.ReadAllText(filename),
                FileName = filename
            };
            return file;
        }

        public static WorkingFile SaveAs(WorkingFile workingFile)
        {
            var dlg = new SaveFileDialog
            {
                FileName = "Document",
                DefaultExt = ".text",
                Filter = "Text documents (.txt)|*.txt"
            };

            if (dlg.ShowDialog() != true) return workingFile;

            string filename = dlg.FileName;               
            File.WriteAllText(filename, workingFile.Content);
            return new WorkingFile
            {
                Content = workingFile.Content,
                FileName = filename
            };

        }

        public static bool Save(WorkingFile workingFile)
        {
            string filename = workingFile.FileName;

            if (!File.Exists(filename)) return false;

            File.WriteAllText(filename, workingFile.Content);
            return true;
        }

    }
}
