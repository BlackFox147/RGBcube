using Caliburn.Micro;
using RGBcube.Models;
using RGBcube.Service;
using System;
using System.Windows.Media;

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

        private Color? color;
        public Color? Color
        {
            get { return color; }
            set
            {
                if (color == value)
                    return;
                color = value;
                FileNameTextBox = color.Value.ToString();
                NotifyOfPropertyChange(() => Color);
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
                var color = ParsStringToColor(workingFile.Content);

                Color = color;
                FileName = workingFile.FileName;
            }            
        }

        public void New()
        {
            workingFile = new WorkingFile();
            FileNameTextBox = string.Empty;
            FileName = string.Empty;

            var temp = new Color
            {
                R = 50,
                G = 50,
                B = 50,
                A = 255
            };

            Color = temp;
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
                workingFile.Content = ParsColorToString(Color);
                FileManager.Save(workingFile);
            }                  
           
        }

        public void SaveAs()
        {

            workingFile.Content = ParsColorToString(Color);
            workingFile = FileManager.SaveAs(workingFile);
            FileName = workingFile.FileName;
        }

        private string ParsColorToString (Color? color)
        {
            string tempString;

            tempString = Convert.ToString(color.Value.R) + ' ';
            tempString += Convert.ToString(color.Value.G) + ' ';
            tempString += Convert.ToString(color.Value.B) + ' ';

            return tempString;
        }

        private Color ParsStringToColor(string fileContent)
        {
            char[] splitchar = { ' ' };
            var strArr = fileContent.Trim().Split(splitchar);

            return new Color
            {
                R = Convert.ToByte(strArr[0]),
                G = Convert.ToByte(strArr[1]),
                B = Convert.ToByte(strArr[2]),
                A = 255
            };
        }
    }
}
