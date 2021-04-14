using System.Collections.Generic;
using System.Reflection;

namespace Reflection
{
    /// <summary>
    /// Person class
    /// </summary>
    public class Person
    {

        #region ----PROPERTIES----

        /// <summary>
        /// Private constant string
        /// </summary>
        private const string PrivateConstantString = "PRIVATE CONSTANT STRING";

        /// <summary>
        /// public constant string
        /// </summary>
        public const string PublicConstantString = "PUBLIC CONSTANT STRING";

        /// <summary>
        /// Person name
        /// </summary>
        public string PersonName { get; set; }

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

        [OptionalProperty(Name = "CP")]
        public int Age { get; set; }
        public List<string> ChildrenNames { get; set; }
        public decimal[] Nums { get; set; }

        [OptionalProperty(true)]
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
            if (string.IsNullOrEmpty(PersonName)) PersonName = FirstName;
            return PersonName.Length;
        }


        public int AddAge(int AddYears)
        {
            return PersonAge + AddYears;
        }


        public override string ToString()
        {
            List<int> Restraints = new List<int>() { 1, 1, 0, 0, 1, 0 };
            string restraint = string.Empty;
            foreach (int i in Restraints) restraint += i.ToString();

            return string.Format(restraint);
        }

        #endregion
    }
}
