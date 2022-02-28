using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KeystrokeToMidi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly double ratio = 4.0 / 3.0;
        private MainWindowModel ViewModel { get { return DataContext as MainWindowModel; } }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void root_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.WidthChanged)
            {
                this.Height = e.NewSize.Width / ratio;
            }
            else if (e.HeightChanged)
            {
                this.Width = e.NewSize.Height * ratio;
            }
        }

    }
}
