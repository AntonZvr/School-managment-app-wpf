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
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for TabBarView.xaml
    /// </summary>
    public partial class TabBarView : Window
    {
        public TabBarView()
        {
            InitializeComponent();
        }

        private void ButtonClick2(object sender, RoutedEventArgs e)
        {
            Main.Content = new SecondPage();
        }

        private void ButtonClick1(object sender, RoutedEventArgs e)
        {
            Main.Content = new MainWindow();
        }
    }
}
