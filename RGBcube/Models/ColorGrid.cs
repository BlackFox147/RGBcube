using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RGBcube.Models
{
    public class ColorGrid : PropertyChangedBase
    {
        public string Name => Color?.ToString() ?? null;

        private Color? color;
        public Color? Color
        {
            get { return color; }
            set
            {
                if (color == value)
                    return;
                color = value;
                
                NotifyOfPropertyChange(() => Color);
                NotifyOfPropertyChange(() => Name);
            }
        }

    }
}
