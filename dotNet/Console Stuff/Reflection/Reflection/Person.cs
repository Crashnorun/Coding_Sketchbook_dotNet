using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Reflection
{
    /// <summary>
    /// Person class
    /// </summary>
    public class Person : IEquatable<Person>
    {

        #region ----PROPERTIES----

        /// <summary>
        /// Private constant string
        /// </summary>
        // private const string PrivateConstantString = "PRIVATE CONSTANT STRING";

        /// <summary>
        /// public constant string
        /// </summary>
        // public const string PublicConstantString = "PUBLIC CONSTANT STRING";

        /// <summary>
        /// Person name
        /// </summary>
        public string PersonName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        /// <summary>
        /// Person age
        /// </summary>
        public int PersonAge { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        public string LastName { get; set; }

        [DefaultValue(40)]
        public int Age { get; set; }


        public string MiddleName { get { return "Anthony"; } }

        #endregion


        #region ----CONSTRUCTORS----

        public Person() { }


        public Person(string Name, int Age)
        {
            this.Age = Age;
            this.FirstName = Name;
        }


        /// <summary>
        /// Create object from object
        /// </summary>
        /// <param name="person"></param>
        public Person(Person person)
        {
            PropertyInfo[] pinfo = this.GetType().GetProperties();

            foreach (PropertyInfo p in pinfo)
            {
                if (p.CanWrite)
                    p.SetValue(this, person.GetType().GetProperty(p.Name).GetValue(person));
            }
        }

        #endregion


        #region ----METHODS----

        public int NameLength()
        {
            if (string.IsNullOrEmpty(PersonName)) return -1;
            return PersonName.Length;
        }


        public int AddAge(int AddYears)
        {
            return PersonAge + AddYears;
        }


        public override string ToString()
        {
            return string.Format(FirstName + " " + LastName);
        }


        public bool Equals(Person x, Person y)
        {
            if (x == null || y == null) return false;
            else
            {
                return x.FirstName.ToLower() == y.FirstName.ToLower() ? true : false;
            }
        }

        public int GetHashCode(Person obj)
        {
            return obj.GetHashCode();
        }

        public bool Equals(Person other)
        {
            return Equals(this, other);
        }

        #endregion
    }

    public class PersonComparer : IEqualityComparer<Person>
    {
        public bool Equals(Person x, Person y)
        {
            if (x == null || y == null) return false;
            else
            {
                return x.FirstName.ToLower() == y.FirstName.ToLower() ? true : false;
            }
        }

        public int GetHashCode(Person obj)
        {
            return obj.GetHashCode();
        }
    }
}
