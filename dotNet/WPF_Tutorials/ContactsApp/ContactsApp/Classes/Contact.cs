using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsApp.Classes
{
    public class Contact
    {
        [PrimaryKey, AutoIncrement]
        public int ID{ get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string  PhoneNumber{ get; set; }

        public override string ToString()
        {
            return $"{Name} - {Email} - {PhoneNumber}";
        }
    }
}