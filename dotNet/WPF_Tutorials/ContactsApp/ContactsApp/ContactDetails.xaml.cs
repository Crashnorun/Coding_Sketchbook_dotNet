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
    /// Interaction logic for ContactDetails.xaml
    /// </summary>
    public partial class ContactDetails : Window
    {
        Contact contact;
        public ContactDetails(Contact contact)
        {
            InitializeComponent();

            this.contact = contact;

            this.nameTextBox.Text = contact.Name;
            this.emailTextBox.Text = contact.Email;
            this.phoneNumberTextBox.Text = contact.PhoneNumber;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            using(SQLiteConnection conn = new SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<Contact>();
                conn.Delete(contact);
            }
            Close();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            contact.Name = this.nameTextBox.Text;
            contact.Email = this.emailTextBox.Text;
            contact.PhoneNumber = this.phoneNumberTextBox.Text;

            using (SQLiteConnection conn = new SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<Contact>();
                conn.Update(contact);
            }
            Close();
        }
    }
}
