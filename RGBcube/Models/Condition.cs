using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bootstrap;
using Caliburn.Micro;

namespace RGBcube.Models
{
    public class Condition : PropertyChangedBase
    {
        private ObservableCollection<ColorGrid> _layerColors;

        public ObservableCollection<ColorGrid> LayerColors
        {
            get => _layerColors;
            set
            {
                _layerColors = value;
                NotifyOfPropertyChange(() => _layerColors);
            }
        }

        public Condition()
        {
            LayerColors = new ObservableCollection<ColorGrid>();
            for (int i = 0; i < 512; i++)
            {
                LayerColors.Add(new ColorGrid());
            }
        }

        public Condition(Condition condition)
        {
            LayerColors = new ObservableCollection<ColorGrid>();
            for (int i = 0; i < 512; i++)
            {
                LayerColors.Add(condition.LayerColors[i]);
            }
        }
    }
}
