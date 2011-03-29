using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Media;
using WPFColorPad;

namespace BCharppe.WPFColorManager
{
    /// <summary>
    /// View model for the main window
    /// </summary>
    public class ColorPadViewModel : INotifyPropertyChanged
    {
        private static readonly Type ColorType = typeof(Colors);
        private static readonly PropertyInfo[] _tColors;
        private readonly RelayCommand _addColor;
        private byte _alphaValue = 255;
        private byte _blueValue;
        private string _colorName;
        private ColorAdapter _currentColor;
        private byte _greenValue;
        private byte _redValue;

        static ColorPadViewModel()
        {
            _tColors = ColorType.GetProperties(BindingFlags.Public | BindingFlags.Static);
        }

        public ColorPadViewModel()
        {
            ColorSourceList = new ObservableCollection<ColorAdapter>();
            SelectedColorsList = new ObservableCollection<ColorAdapter>();

            PropertyInfo[] items = typeof(Colors).GetProperties();

            foreach (PropertyInfo c in items)
            {
                var col = (Color)c.GetValue(c, null);
                string colName = GetColorName(col);
                var ca = new ColorAdapter(col, colName);

                ColorSourceList.Add(ca);
            }

            PropertyChanged += ColorPadViewModelPropertyChanged;

            _addColor = new RelayCommand(ExecuteAddColor, CanExecuteAddcolor);
        }

        public RelayCommand AddColor
        {
            get { return _addColor; }
        }

        public ObservableCollection<ColorAdapter> ColorSourceList { get; set; }

        public ObservableCollection<ColorAdapter> SelectedColorsList { get; set; }

        private static readonly PropertyChangedEventArgs CurrentColorPropertyChangedEventArgs = new PropertyChangedEventArgs("CurrentColor");
        /// <summary>
        /// Gets or sets the CurrentColor
        /// </summary>
        public ColorAdapter CurrentColor
        {
            get { return _currentColor; }
            set
            {
                if (CurrentColor != value)
                {
                    _currentColor = value;
                    InvokePropertyChanged(CurrentColorPropertyChangedEventArgs);
                }
            }
        }

        private static readonly PropertyChangedEventArgs AlphaValuePropertyChangedEventArgs = new PropertyChangedEventArgs("AlphaValue");
        /// <summary>
        /// Gets or sets the AlphaValue
        /// </summary>
        public byte AlphaValue
        {
            get { return _alphaValue; }
            set
            {
                if (AlphaValue != value)
                {
                    _alphaValue = value;
                    InvokePropertyChanged(AlphaValuePropertyChangedEventArgs);
                }
            }
        }

        private static readonly PropertyChangedEventArgs RedValuePropertyChangedEventArgs = new PropertyChangedEventArgs("RedValue");
        /// <summary>
        /// Gets or sets the AlphaValue
        /// </summary>
        public byte RedValue
        {
            get { return _redValue; }
            set
            {
                if (RedValue != value)
                {
                    _redValue = value;
                    InvokePropertyChanged(RedValuePropertyChangedEventArgs);
                }
            }
        }

        private static readonly PropertyChangedEventArgs GreenValuePropertyChangedEventArgs = new PropertyChangedEventArgs("GreenValue");
        /// <summary>
        /// Gets or sets the AlphaValue
        /// </summary>
        public byte GreenValue
        {
            get { return _greenValue; }
            set
            {
                if (GreenValue != value)
                {
                    _greenValue = value;
                    InvokePropertyChanged(GreenValuePropertyChangedEventArgs);
                }
            }
        }

        private static readonly PropertyChangedEventArgs BlueValuePropertyChangedEventArgs = new PropertyChangedEventArgs("BlueValue");
        /// <summary>
        /// Gets or sets the AlphaValue
        /// </summary>
        public byte BlueValue
        {
            get { return _blueValue; }
            set
            {
                if (BlueValue != value)
                {
                    _blueValue = value;
                    InvokePropertyChanged(BlueValuePropertyChangedEventArgs);
                }
            }
        }

        private static readonly PropertyChangedEventArgs ColorNamePropertyChangedEventArgs = new PropertyChangedEventArgs("ColorName");
        /// <summary>
        /// Gets or sets the AlphaValue
        /// </summary>
        public string ColorName
        {
            get { return _colorName; }
            set
            {
                if (ColorName != value)
                {
                    _colorName = value;
                    InvokePropertyChanged(ColorNamePropertyChangedEventArgs);
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public void InvokePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }

        private static string GetColorName(Color col)
        {
            PropertyInfo colExists = _tColors.FirstOrDefault(pi => (Color)pi.GetValue(null, null) == col);
            if (colExists != null)
            {
                return colExists.Name;
            }
            return string.Empty;
        }

        private bool CanExecuteAddcolor(object obj)
        {
            return ColorName != string.Empty;
        }

        private void ExecuteAddColor(object obj)
        {
            CurrentColor.Name = ColorName;

            ColorSourceList.Add(CurrentColor);
        }

        private void ColorPadViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            string pn = e.PropertyName;

            if (pn == "AlphaValue" || pn == "RedValue" || pn == "GreenValue" || pn == "BlueValue")
            {
                ColorChanged();
            }
        }

        private void ColorChanged()
        {
            ColorAdapter existingColor = ColorSourceList
                .FirstOrDefault(
                    caSource =>
                    caSource.A == _alphaValue
                    && caSource.B == BlueValue
                    && caSource.G == GreenValue
                    && caSource.R == RedValue);
            ColorName = existingColor != null ? existingColor.Name : String.Empty;
            Color col = Color.FromArgb(AlphaValue, RedValue, GreenValue, BlueValue);
            string colName = GetColorName(col);
            var cc = new ColorAdapter(col, colName != string.Empty ? colName : ColorName);
            CurrentColor = cc;
        }
    }
}