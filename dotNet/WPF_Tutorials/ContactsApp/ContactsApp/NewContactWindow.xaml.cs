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
using ContactsApp.Classes;
using SQLite;

namespace ContactsApp
{
    /// <summary>
    /// Interaction logic for NewContactWindow.xaml
    /// </summary>
    public partial class NewContactWindow : Window
    {
        public NewContactWindow()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            // save contact
            Contact contact = new Contact()
            {
                Name = nameTextBox.Text,
                Email = emailTextBox.Text,
                PhoneNumber = phoneNumberTextBox.Text
            };

            // once the connection object is disposed, the connection will be closed
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                // if table already exists, it won't be recreated
                connection.CreateTable<Contact>();      // creates a table that only accepts Contact types

                connection.Insert(contact);             // inserst into the corrisponding table
            }


            this.Close();
        }
    }
}
