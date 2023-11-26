/* Author:  Lindy Stewart
 * Editor:  Eric Robinson L00709820
 * Date:    11/25/23
 * Course:  Lane Community College CS234 Advanced Programming: C# (.NET)
 * Lab:     6 
 * Purpose: Defines customers
 */

using System;
using System.Collections.Generic;

namespace MMABooksEFClasses.Models
{
    public partial class Customer
    {
        // Default constructor
        public Customer()
        {
            Invoices = new HashSet<Invoice>();
        }

        // Properties
        public int CustomerId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        //This is the foreign key...this is statecode
        public string ZipCode { get; set; } = null!;

        public virtual State StateNavigation { get; set; } = null!;
        // The entity frame work is smart enough to know that there is referential integrity
        // relationship between the state and customer table.
        // So we use StateNavigation which is a navagational property that allows for that
        // foreign key relationship. 

        // public virtual State? State { get; set; } = null!; // Allows for foreign key relationships. This USED to be called StateNavigation.

        public virtual ICollection<Invoice> Invoices { get; set; }
        // Similarly there is a relationship with invoices, here we are able to
        // see the collection of invoices.
        // Each class has fields that corrisponds with the database and have a 
        // way to implement the foreign key relationships between tables. 

        public override string ToString()
        {
            return CustomerId + ", " + Name + ", " + Address + ", " + City + ", " + State + ", " + ZipCode;
        }

    } // end class Customer
} // end namespace MMABooksEFClasses.Models
