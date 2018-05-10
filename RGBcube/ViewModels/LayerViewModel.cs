using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using RGBcube.Models;

namespace RGBcube.ViewModels
{
    public class LayerViewModel : PropertyChangedBase
    {
        private ColorGrid _selectedItem;
        private Condition _condition;
        private ColorGrid _valueForAll = new ColorGrid();

        public LayerViewModel(Condition condition)
        {
            _condition = condition;
        }

        public ColorGrid ValueForAll
        {
            get => _valueForAll;
            set
            {
                _valueForAll = value;
                NotifyOfPropertyChange(() => ValueForAll);
            }
        }

        public ObservableCollection<ColorGrid> Colors => Condition.LayerColors;


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

        public Condition Condition
        {
            get => _condition;
            set
            {
                _condition = value;
                NotifyOfPropertyChange(() => Condition);
                NotifyOfPropertyChange(() => Colors);
            }
        }

        public void UseForAll()
        {
            foreach (var color in Colors)
            {
                color.R = ValueForAll.R;
                color.G = ValueForAll.G;
                color.B = ValueForAll.B;
            }
        }


    }
}
