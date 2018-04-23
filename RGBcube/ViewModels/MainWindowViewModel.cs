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
   
        public bool IsWorkingFile => workingFile != null;

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

        public void Open()
        {
            workingFile = FileManager.Open();
            if (workingFile != null)
            {
                Colors = Pars(workingFile.Content);                 
                FileName = workingFile.FileName;
                NotifyOfPropertyChange(() => IsWorkingFile);
            }
        }

        public void Save()
        {
            if (String.IsNullOrEmpty(workingFile.FileName))
            {
                SaveAs();
                return;
            }

            workingFile.Content = ParsColorToString(Colors);
            FileManager.Save(workingFile);
            NotifyOfPropertyChange(() => IsWorkingFile);

        }

        public void SaveAs()
        {

            workingFile.Content = ParsColorToString(Colors);
            workingFile = FileManager.SaveAs(workingFile);
            FileName = workingFile.FileName;
            NotifyOfPropertyChange(() => IsWorkingFile);
        }

        private string ParsColorToString(ObservableCollection<ColorGrid> colors)
        {           
            return colors.Aggregate(string.Empty, (s, c) => s + Pars(c)); 
        }

        private string Pars(ColorGrid color)
        {
            string tempString;

            tempString = Convert.ToString(color.Color.Value.R) + ' ';
            tempString += Convert.ToString(color.Color.Value.G) + ' ';
            tempString += Convert.ToString(color.Color.Value.B) + '/';

            return tempString;
        }

        private ColorGrid ParsStringToColor(string colorContent)
        {
            char[] splitchar = { ' ' };
            var strArr = colorContent.Trim().Split(splitchar);

            var color = new Color
            {
                R = Convert.ToByte(strArr[0]),
                G = Convert.ToByte(strArr[1]),
                B = Convert.ToByte(strArr[2]),
                A = 255
            };

            return new ColorGrid
            {
                Color = color
            };
        }

        private ObservableCollection<ColorGrid> Pars(string fileContent)
        {
            char[] splitchar = { '/' };
            var colorArr = fileContent.Trim().Split(splitchar).ToList();

            var colorList = colorArr.Where(i => !string.IsNullOrEmpty(i)).Select(ParsStringToColor);

            return new ObservableCollection<ColorGrid>(colorList);
        }
    }
}
