using Caliburn.Micro;
using RGBcube.Models;
using RGBcube.Service;
using System;

namespace RGBcube.ViewModels
{
    public class MainWindowViewModel : PropertyChangedBase
    {
        private string fileNameTextBox;
        private WorkingFile workingFile;        
        private string fileName;

        public string FileNameTextBox
        {
            get { return fileNameTextBox; }
            set
            {
                if (fileNameTextBox == value)
                    return;
                fileNameTextBox = value;
                NotifyOfPropertyChange(() => FileNameTextBox);
            }
        }

        public string FileName
        {
            get { return fileName; }
            set
            {
                if (fileName == value)
                    return;
                fileName = value;
                NotifyOfPropertyChange(() => FileName);
            }
        }

        public void Open()
        {
            workingFile = FileManager.Open();
            if (workingFile != null)
            {
                FileNameTextBox = workingFile.Content;
                FileName = workingFile.FileName;
            }            
        }

        public void New()
        {   
            workingFile = new WorkingFile();
            FileNameTextBox = string.Empty;
            FileName = string.Empty;
        }

        public void Save()
        {
            if (String.IsNullOrEmpty(workingFile.FileName))
            {
                SaveAs();
                return;
            }

            if (!FileNameTextBox.Equals(workingFile.Content))
            {
                workingFile.Content = FileNameTextBox;
                FileManager.Save(workingFile);
            }                  
           
        }

        public void SaveAs()
        {
            workingFile.Content = FileNameTextBox;
            workingFile = FileManager.SaveAs(workingFile);
            FileName = workingFile.FileName;
        }


    }
}
