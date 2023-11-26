/* Author:  Lindy Stewart
 * Editor:  Eric Robinson L00709820
 * Date:    11/25/23
 * Course:  Lane Community College CS234 Advanced Programming: C# (.NET)
 * Lab:     6 
 * Purpose: 
 */

using System;
using System.Collections.Generic;

namespace MMABooksEFClasses.Models
{
    public partial class State
    {
        // Default constructor
        public State()
        {
            Customers = new HashSet<Customer>();
        }

        // Properties
        public string StateCode { get; set; } = null!;
        public string StateName { get; set; } = null!;

        // <Customer>? is nullable to allow http post requests to work - per Bryce
        public virtual ICollection<Customer>? Customers { get; set; }

        public override string ToString()
        {
            return StateCode + ", " + StateName;
        }

    } // end class State
} // end namespace MMABooksEFClasses.Models
