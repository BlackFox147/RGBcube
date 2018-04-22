using Caliburn.Micro;
using RGBcube.Models;
using RGBcube.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace RGBcube.ViewModels
{
    public class MainWindowViewModel : PropertyChangedBase
    {
        private string fileNameTextBox;
        private WorkingFile workingFile;
        private string fileName;
        private ColorGrid selectedItem;

        private ObservableCollection<ColorGrid> colors = new ObservableCollection<ColorGrid>();
        public ObservableCollection<ColorGrid> Colors
        {
            get { return colors; }
            set
            {
                if (colors == value)
                    return;
                colors = value;
                NotifyOfPropertyChange(() => Colors);
            }
        }

        public ColorGrid SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem == value)
                    return;
                selectedItem = value;
                NotifyOfPropertyChange(() => SelectedItem);
            }
        }

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

        public bool IsWorkingFile => workingFile != null ? true : false;

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

        public void Create()
        {
            var temp = new Color
            {
                R = 50,
                G = 50,
                B = 50,
                A = 255
            };

            var colorGrid = new ColorGrid
            {
                Color = temp
            };
            Colors.Add(colorGrid);
        }

        public void Copy()
        {
            if (SelectedItem != null)
            {
                int copy = Colors.IndexOf(SelectedItem);

                var colorGrid = new ColorGrid
                {
                    Color = SelectedItem.Color.Value
                };
                Colors.Insert(copy, colorGrid);
            }
           
        }

        public void Remove()
        {
            if (SelectedItem != null)
            {
                Colors.Remove(SelectedItem);
            }
        }

        public void New()
        {
            workingFile = new WorkingFile();
            FileName = "New file";

            NotifyOfPropertyChange(() => IsWorkingFile);
        }

        //public void Open()
        //{
        //    workingFile = FileManager.Open();
        //    if (workingFile != null)
        //    {
        //        var color = ParsStringToColor(workingFile.Content);

        //        Color = color;
        //        FileName = workingFile.FileName;
        //    }            
        //}

        //public void Save()
        //{
        //    if (String.IsNullOrEmpty(workingFile.FileName))
        //    {
        //        SaveAs();
        //        return;
        //    }

        //    if (!FileNameTextBox.Equals(workingFile.Content))
        //    {
        //        workingFile.Content = ParsColorToString(Color);
        //        FileManager.Save(workingFile);
        //    }                  

        //}

        //public void SaveAs()
        //{

        //    workingFile.Content = ParsColorToString(Color);
        //    workingFile = FileManager.SaveAs(workingFile);
        //    FileName = workingFile.FileName;
        //}

        private string ParsColorToString(Color? color)
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
