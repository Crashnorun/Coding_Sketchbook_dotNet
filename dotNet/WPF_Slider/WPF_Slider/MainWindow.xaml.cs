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

namespace WPF_Slider
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region ---- PROPERTIES ----

        Color SelectColor = Color.FromArgb(255, 0, 0, 1);
        Color OriginalColor = Color.FromArgb(255, 60, 175, 240);

        #endregion


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SolidColorBrush SelectBrush = new SolidColorBrush(SelectColor);

            Ellipse elle = (Ellipse)sender;
            elle.Fill = SelectBrush;

            

        }

        private void Ellipse_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SolidColorBrush OriginalBrush = new SolidColorBrush(OriginalColor);

            Ellipse elle = (Ellipse)sender;
            elle.Fill = OriginalBrush;
        }

        private void Ellipse_MouseLeave(object sender, MouseEventArgs e)
        {
            SolidColorBrush OriginalBrush = new SolidColorBrush(OriginalColor);

            Ellipse elle = (Ellipse)sender;
            elle.Fill = OriginalBrush;
        }
    }
}
