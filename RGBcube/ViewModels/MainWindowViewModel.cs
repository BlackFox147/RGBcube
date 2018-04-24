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
        private WorkingFile _workingFile;
        private string _fileName;
        private ColorGrid _selectedItem;

        private ObservableCollection<ColorGrid> _colors = new ObservableCollection<ColorGrid>();
        public ObservableCollection<ColorGrid> Colors
        {
            get => _colors;
            set
            {
                if (_colors == value)
                    return;
                _colors = value;
                NotifyOfPropertyChange(() => Colors);
            }
        }

        public ColorGrid SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem == value)
                    return;
                _selectedItem = value;
                NotifyOfPropertyChange(() => SelectedItem);
            }
        }
   
        public bool IsWorkingFile => _workingFile != null;

        public string FileName
        {
            get => _fileName;
            set
            {
                if (_fileName == value)
                    return;
                _fileName = value;
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
            if (SelectedItem == null) return;
            int copy = Colors.IndexOf(SelectedItem);

            if (SelectedItem.Color == null) return;
            var colorGrid = new ColorGrid
            {
                Color = SelectedItem.Color.Value
            };
            Colors.Insert(copy, colorGrid);

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
            _workingFile = new WorkingFile();
            FileName = "New file";

            NotifyOfPropertyChange(() => IsWorkingFile);
        }

        public void Open()
        {
            _workingFile = FileManager.Open();

            if (_workingFile == null) return;

            Colors = Pars(_workingFile.Content);                 
            FileName = _workingFile.FileName;
            NotifyOfPropertyChange(() => IsWorkingFile);
        }

        public void Save()
        {
            if (string.IsNullOrEmpty(_workingFile.FileName))
            {
                SaveAs();
                return;
            }

            _workingFile.Content = ParsColorToString(Colors);
            FileManager.Save(_workingFile);
            NotifyOfPropertyChange(() => IsWorkingFile);

        }

        public void SaveAs()
        {

            _workingFile.Content = ParsColorToString(Colors);
            _workingFile = FileManager.SaveAs(_workingFile);
            FileName = _workingFile.FileName;
            NotifyOfPropertyChange(() => IsWorkingFile);
        }

        private static string ParsColorToString(IEnumerable<ColorGrid> colors)
        {           
            return colors.Aggregate(string.Empty, (s, c) => s + Pars(c)); 
        }

        private static string Pars(ColorGrid color)
        {
            if (color.Color == null) return string.Empty;

            string tempString = Convert.ToString(color.Color.Value.R) + ' ';
            tempString += Convert.ToString(color.Color.Value.G) + ' ';
            tempString += Convert.ToString(color.Color.Value.B) + '/';

            return tempString;

        }

        private static ColorGrid ParsStringToColor(string colorContent)
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

        private static ObservableCollection<ColorGrid> Pars(string fileContent)
        {
            char[] splitchar = { '/' };
            var colorArr = fileContent.Trim().Split(splitchar).ToList();

            var colorList = colorArr.Where(i => !string.IsNullOrEmpty(i)).Select(ParsStringToColor);

            return new ObservableCollection<ColorGrid>(colorList);
        }
    }
}
