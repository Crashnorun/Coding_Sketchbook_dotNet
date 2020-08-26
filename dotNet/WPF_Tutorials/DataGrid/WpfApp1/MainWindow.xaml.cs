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
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Obj1> Objs1 = new ObservableCollection<Obj1>();
        public ObservableCollection<Obj2> Objs2 = new ObservableCollection<Obj2>();

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < 10; i++)
            {
                Obj1 obj1 = new Obj1() { Property = "ab", Value = "ac" };
                Obj2 obj2 = new Obj2() { Property = i, Value = i * 2 };

                Objs1.Add(obj1);
                Objs2.Add(obj2);
            }

            dg.ItemsSource = Objs1;
            //dg.DataContext = Objs1;
            Debug.Print("Column Count: " + dg.Columns.Count.ToString());
            Debug.Print("Row Count: " + dg.Items.Count.ToString());
            Debug.Print(((Obj1)dg.Items[1]).Value.ToString());
            Debug.Print(((Obj1)dg.Items[0]).Value.ToString());

            dg2.ItemsSource = LoadCollectionData();
        }


        /// <summary>  
        /// List of Authors  
        /// </summary>  
        /// <returns></returns>  
        private List<Author> LoadCollectionData()
        {
            List<Author> authors = new List<Author>();
            authors.Add(new Author()
            {
                ID = 101,
                Name = "Mahesh Chand",
                BookTitle = "Graphics Programming with GDI+",
                DOB = new DateTime(1975, 2, 23),
                IsMVP = false
            });

            authors.Add(new Author()
            {
                ID = 201,
                Name = "Mike Gold",
                BookTitle = "Programming C#",
                DOB = new DateTime(1982, 4, 12),
                IsMVP = true
            });

            authors.Add(new Author()
            {
                ID = 244,
                Name = "Mathew Cochran",
                BookTitle = "LINQ in Vista",
                DOB = new DateTime(1985, 9, 11),
                IsMVP = true
            });

            return authors;
        }
    }

    public class Obj1
    {
        public string Property { get; set; }
        public string Value { get; set; }
    }

    public class Obj2
    {
        public int Property { get; set; }
        public int Value { get; set; }
    }

    public class Author
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string BookTitle { get; set; }
        public bool IsMVP { get; set; }
    }
     
}
