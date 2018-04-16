using Microsoft.Win32;
using RGBcube.Models;
using System.IO;

namespace RGBcube.Service
{
    public class FileManager
    {
        public static WorkingFile Open()
        {            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "d:\\";
            openFileDialog1.DefaultExt = ".txt";
            openFileDialog1.Filter = "Text documents (.txt)|*.txt";

            if (openFileDialog1.ShowDialog() == true)
            {
                string filename = openFileDialog1.FileName;
                var file = new WorkingFile
                {
                    Content = File.ReadAllText(filename),
                    FileName = filename
                };
                return file;
            }
            return null;
        }

        public static WorkingFile SaveAs(WorkingFile workingFile)
        {
            var dlg = new SaveFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".text"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension
           
            // Process save file dialog box results
            if (dlg.ShowDialog() == true)
            {
                string filename = dlg.FileName;               
                File.WriteAllText(filename, workingFile.Content);
                return new WorkingFile
                {
                    Content = workingFile.Content,
                    FileName = filename
                };
            }

            return workingFile;
        }

        public static bool Save(WorkingFile workingFile)
        {
            string filename = workingFile.FileName;           
            if (File.Exists(filename))
            {
                File.WriteAllText(filename, workingFile.Content);
                return true;
            }       
            return false;
        }

    }
}
