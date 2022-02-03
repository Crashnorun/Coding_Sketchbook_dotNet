using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reflection.InterfaceStuff
{
    public class Human : Mamal
    {
        private string _name;
        private int _age;
        private string _lastName;

        [DefaultValue("")]
        public string Name { get { return _name; } set { _name = value; } }

        [DefaultValue("")]
        public string LastName { get { return _lastName; } set { _lastName = value; } }

        [DefaultValue("")]
        public int Age { get { return _age; } set { _age = value; } }


        public Human() { }



        public Human(string Name, int Age)
        {
            _name = Name;
            _age = Age;
        }

        public override string ToString()
        {
            PropertyInfo[] props = this.GetType().GetProperties();
            string s = "";

            foreach (PropertyInfo p in props) s = s + p.GetValue(this);
            return s;
        }
    }

}
