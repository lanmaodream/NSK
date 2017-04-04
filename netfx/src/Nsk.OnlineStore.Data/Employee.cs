//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nsk.OnlineStore.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee
    {
        public Employee()
        {
            this.Reports = new HashSet<Employee>();
            this.Orders = new HashSet<Order>();
            this.Territories = new HashSet<Territory>();
            this.HomeAddress = new PostalAddress();
        }
    
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public Nullable<System.DateTime> HireDate { get; set; }
        public string HomePhone { get; set; }
        public string Extension { get; set; }
        public byte[] Photo { get; set; }
        public string Notes { get; set; }
        public Nullable<int> ReportsTo { get; set; }
        public string PhotoPath { get; set; }
    
        public PostalAddress HomeAddress { get; set; }
    
        public virtual ICollection<Employee> Reports { get; set; }
        public virtual Employee Manager { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Territory> Territories { get; set; }
    }
}
