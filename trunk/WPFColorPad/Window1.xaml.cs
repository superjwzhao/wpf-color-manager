using System.Windows;
using System.Windows.Controls;
using BCharppe.WPFColorManager;

namespace WPFColorPad
{
    /// <summary>
    /// Interaction logic for Window1.xaml    
    /// </summary>    
    public partial class Window1 : Window
    {
        private readonly ColorPadViewModel _vm;

        public Window1()
        {
            InitializeComponent();
            _vm = new ColorPadViewModel();
            DataContext = _vm;
        }


        private void ColorSelectionListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (object ai in e.AddedItems)
            {
                _vm.SelectedColorsList.Add((ColorAdapter) ai);
            }
            foreach (object ri in e.RemovedItems)
            {
                _vm.SelectedColorsList.Remove((ColorAdapter) ri);
            }
        }
    }
}