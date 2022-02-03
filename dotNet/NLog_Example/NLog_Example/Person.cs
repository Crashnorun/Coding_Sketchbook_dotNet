using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NLog_Example
{
    public class Person
    {

        #region ---- PROPERTIES ----

        /// <summary>
        /// Call the application logger
        /// </summary>
        private NLog.Logger _logger = LogHelper.GetLogger();

        public string Name { get; set; }
        
        public int Age { get; set; }

        #endregion


        #region ---- CONSTRUCTORS ----

        public Person(string Name, int Age)
        {
            LogHelper.LogMethodInputs(MethodBase.GetCurrentMethod(), Name, Age);
            this.Name = Name;
            this.Age = Age;
        }

        #endregion


        #region ---- METHODS ----

        public override string ToString()
        {
            return string.Format("{0} , Age: {1}", this.Name, this.Age);
        }

        #endregion 

    }
}
