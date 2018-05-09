using Caliburn.Micro;

namespace RGBcube.Models
{
    public class ColorGrid : PropertyChangedBase
    {
        private int _r;
        private int _g;
        private int _b;

        public int R
        {
            get => _r;
            set
            {
                if (value < 0 || value >= 4095) return;
                _r = value;
                NotifyOfPropertyChange(() => R);
            }
        }

        public int G
        {
            get => _g;
            set
            {
                if (value < 0 || value >= 4095) return;
                _g = value;
                NotifyOfPropertyChange(() => G);
            }
        }

        public int B
        {
            get => _b;
            set
            {
                if (value < 0 || value >= 4095) return;
                _b = value;
                NotifyOfPropertyChange(() => B);
            }
        }


    }
}
