using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using AForge.Imaging.Filters;

/*
 * Tutorial site: http://milindapro.blogspot.com/2016/12/simple-aforgenet-tutorial-getting-start.html
 */

namespace AForge_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string filePath = @"C:\Users\Charlie\Pictures\IMG_6517_A.jpg";                   // image path
        private Grayscale filter;
        private string tempPath;
        string tempFile;

        public MainWindow()
        {
            InitializeComponent();

            filterScale.Visibility = Visibility.Hidden;
            tempPath = System.IO.Path.GetTempPath();                                            // get temp path
            tempFile = System.IO.Path.Combine(tempPath, "tempImg.jpg");                         // get full temp path
        }

        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            applyFilter(0.2);
            ImgBox1.Source = new BitmapImage(new Uri(filePath));                                // original image
            OriginalName.Text = System.IO.Path.GetFileName(filePath);
            TempName.Text = System.IO.Path.GetFileName(tempFile);
            filterScale.Visibility = Visibility.Visible;
            filterScale.Value = 0.2;
        }


        private void applyFilter(double filterValue)
        {
            if (System.IO.File.Exists(tempFile))
            {
                ImgBox2.Source = null;
                System.IO.File.Delete(tempFile);                                                // delete the old file
            }

            filter = new Grayscale(filterValue, filterValue, filterValue);                      // create grey filter
            Bitmap img = (Bitmap)System.Drawing.Image.FromFile(filePath);                       // get image 
            Bitmap grayImage = filter.Apply(img);                                               // apply filter
                                                                                                //grayImage.Save(tempFile);                                                           // save new image
                                                                                                //ImgBox2.Source = new BitmapImage(new Uri(tempFile));                                // new image
            IntPtr hbitmap = grayImage.GetHbitmap();
            ImgBox2.Source = Imaging.CreateBitmapSourceFromHBitmap(hbitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

        }


        private void filterScale_DragStarted(object sender, DragStartedEventArgs e)
        {
            System.Diagnostics.Debug.Print(((Slider)sender).Value.ToString());
            applyFilter(((Slider)sender).Value);
        }


        private void filterScale_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).UpdateLayout();
        }


        private void filterScale_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            System.Diagnostics.Debug.Print(((Slider)sender).Value.ToString());
            applyFilter(((Slider)sender).Value);
        }
    }

}
