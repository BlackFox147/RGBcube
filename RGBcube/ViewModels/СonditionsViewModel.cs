using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Bootstrap;
using Caliburn.Micro;
using RGBcube.Models;

namespace RGBcube.ViewModels
{
    public class СonditionsViewModel : PropertyChangedBase
    {
        private ObservableCollection<Condition> _conditions = new ObservableCollection<Condition>();
        private Condition _selectedItem;

        public ObservableCollection<Condition> Conditions
        {
            get => _conditions;
            set
            {
                _conditions = value;
                NotifyOfPropertyChange(() => Conditions);
            }
        }

        public Condition SelectedItem
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
    }
}
