using Caliburn.Micro;
using RGBcube.Models;
using RGBcube.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
            var colorGrid = new ColorGrid
            {
                R = 0,
                G = 0,
                B = 0
            };
            Colors.Add(colorGrid);
        }

        public void Copy()
        {
            if (SelectedItem == null) return;
            int copy = Colors.IndexOf(SelectedItem);

            var colorGrid = new ColorGrid
            {
                R = SelectedItem.R,
                G = SelectedItem.G,
                B = SelectedItem.B
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
            Colors.Clear();

            _workingFile = new WorkingFile();
            FileName = "New file";

            NotifyOfPropertyChange(() => IsWorkingFile);
        }

        public void Open()
        {
            Colors.Clear();

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
            string tempString = Convert.ToString(color.R) + ' ';
            tempString += Convert.ToString(color.G) + ' ';
            tempString += Convert.ToString(color.B) + ' ' + '/' + ' ';

            return tempString;

        }

        private static ColorGrid ParsStringToColor(string colorContent)
        {
            char[] splitchar = { ' ' };
            var strArr = colorContent.Trim().Split(splitchar);

            return new ColorGrid
            {
                R = Convert.ToInt32(strArr[0]),
                G = Convert.ToInt32(strArr[1]),
                B = Convert.ToInt32(strArr[2])
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
