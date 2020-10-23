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
    /// Interaction logic for MySlider.xaml
    /// </summary>
    public partial class MySlider : UserControl
    {
        #region ---- PROPERTIES ----

        Color SelectColor = Color.FromArgb(255, 0, 0, 1);
        Color OriginalColor = Color.FromArgb(255, 60, 175, 240);

        #endregion


        public MySlider()
        {
            InitializeComponent();
            Dot.Margin = new Thickness(Dot.Margin.Left, Dot.Margin.Top, Dot.Margin.Right, Dot.Margin.Bottom);
        }

        private void Dot_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SolidColorBrush SelectBrush = new SolidColorBrush(SelectColor);

            Ellipse elle = (Ellipse)sender;
            elle.Fill = SelectBrush;

            Point pos = Mouse.GetPosition(this);
            elle.Margin = new Thickness(pos.X - elle.Width / 2, elle.Margin.Top, elle.Margin.Right, elle.Margin.Bottom);
        }

        private void Dot_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SolidColorBrush OriginalBrush = new SolidColorBrush(OriginalColor);

            Ellipse elle = (Ellipse)sender;
            elle.Fill = OriginalBrush;
        }

        private void Dot_MouseLeave(object sender, MouseEventArgs e)
        {
            SolidColorBrush OriginalBrush = new SolidColorBrush(OriginalColor);

            Ellipse elle = (Ellipse)sender;
            elle.Fill = OriginalBrush;
        }

        private void Dot_MouseMove(object sender, MouseEventArgs e)
        {

            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Point pos = Mouse.GetPosition(this);
                Ellipse elle = (Ellipse)sender;
                elle.Margin = new Thickness(pos.X - elle.Width / 2, elle.Margin.Top,
                    elle.Margin.Right, elle.Margin.Bottom);

                startRect.Width = pos.X;
                startRect.Margin = new Thickness(0, 0, pos.X, 0);
            }

        }

    }
}
