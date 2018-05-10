using Caliburn.Micro;
using RGBcube.Models;
using RGBcube.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Bootstrap;

namespace RGBcube.ViewModels
{
    public class MainWindowViewModel : PropertyChangedBase
    {
        private WorkingFile _workingFile;
        private string _fileName;
        private СonditionsViewModel _conditionsViewModel;
        private LayerViewModel _layerViewModel;
        private object _workViewModel;
       
        public MainWindowViewModel()
        {
            _conditionsViewModel = new СonditionsViewModel();
            _workViewModel = _conditionsViewModel;
            
        }

        public object WorkViewModel
        {
            get => _workViewModel;
            set
            {
                _workViewModel = value;
                NotifyOfPropertyChange(() => WorkViewModel);
                NotifyOfPropertyChange(() => IsBack);
                NotifyOfPropertyChange(() => IsWorkingFile);
            }
        }

        public СonditionsViewModel СonditionsViewModel
        {
            get => _conditionsViewModel;
            set
            {
                _conditionsViewModel = value;
                NotifyOfPropertyChange(() => СonditionsViewModel);
            }
        }

        public LayerViewModel LayerViewModel
        {
            get => _layerViewModel;
            set
            {
                _layerViewModel = value;
                NotifyOfPropertyChange(() => LayerViewModel);
            }
        }

        public ObservableCollection<Condition> Conditions
        {
            get => СonditionsViewModel.Conditions;
            set
            {
                if (СonditionsViewModel.Conditions == value)
                    return;
                СonditionsViewModel.Conditions = value;
                NotifyOfPropertyChange(() => Conditions);
            }
        }

        public bool IsWorkingFile => _workingFile != null && WorkViewModel is СonditionsViewModel;
        public bool IsBack => WorkViewModel is LayerViewModel;

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
            Conditions.Add(new Condition());
        }

        public void Copy()
        {
            if (СonditionsViewModel.SelectedItem == null) return;
            int copy = СonditionsViewModel.Conditions.IndexOf(СonditionsViewModel.SelectedItem);
            
            СonditionsViewModel.Conditions.Insert(copy, new Condition(СonditionsViewModel.SelectedItem));

        }

        public void Remove()
        {
            if (СonditionsViewModel.SelectedItem != null)
            {
                СonditionsViewModel.Conditions.Remove(СonditionsViewModel.SelectedItem);
            }
        }

        public void Back()
        {
            WorkViewModel = СonditionsViewModel;
        }

        public void ConditionDetail()
        {
            if (СonditionsViewModel.SelectedItem != null)
            {
                LayerViewModel = new LayerViewModel(СonditionsViewModel.SelectedItem);
                WorkViewModel = LayerViewModel;
            }
        }

        public void New()
        {
            Conditions.Clear();

            _workingFile = new WorkingFile();
            FileName = "New file";

            NotifyOfPropertyChange(() => IsWorkingFile);
        }

        public void Open()
        {
            Conditions.Clear();

            _workingFile = FileManager.Open();

            if (_workingFile == null) return;

            Conditions = Pars(_workingFile.Content);
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

            _workingFile.Content = Conditions.Aggregate(string.Empty, (s, c) => s + ParsColorToString(c.LayerColors));
            FileManager.Save(_workingFile);
            NotifyOfPropertyChange(() => IsWorkingFile);

        }

        public void SaveAs()
        {

            _workingFile.Content = Conditions.Aggregate(string.Empty, (s, c) => s + ParsColorToString(c.LayerColors));
            _workingFile = FileManager.SaveAs(_workingFile);
            FileName = _workingFile.FileName;
            NotifyOfPropertyChange(() => IsWorkingFile);
        }

        private static string ParsColorToString(IEnumerable<ColorGrid> colors)
        {
            return colors.Aggregate(string.Empty, (s, c) => s + Pars(c))+ " * ";
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

        private static ObservableCollection<Condition> Pars(string fileContent)
        {
            char[] split = { '*' };
            var conditionArr = fileContent.Trim().Split(split).ToList();

            var result = new ObservableCollection<Condition>();
            foreach (string conditionString in conditionArr.Where(i => !string.IsNullOrEmpty(i.Trim())))
            {
                char[] splitchar = { '/' };
                var colorArr = conditionString.Trim().Split(splitchar).ToList();

                var colorList = colorArr.Where(i => !string.IsNullOrEmpty(i.Trim())).Select(ParsStringToColor);
                var conditiion = new Condition
                {
                    LayerColors = new ObservableCollection<ColorGrid>(colorList)
                };

                result.Add(conditiion);
            }
            return result;

        }
    }
}
