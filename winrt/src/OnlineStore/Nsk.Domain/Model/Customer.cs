using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace Nsk.Domain.Model
{
    /// <summary>
    /// Defines a customer
    /// </summary>
    public class Customer : Organization, IAggregateRoot
    {
        /// <summary>
        /// Creates a new customer
        /// </summary>
        /// <param name="id">The customer Id</param>
        /// <param name="companyName">The company name</param>
        /// <param name="contactName">The contact name</param>
        /// <returns>The customer</returns>
        public static Customer CreateNewCustomer(string id, string companyName, string contactName)
        {
            Contract.Requires<ArgumentNullException>(id != null, "id");
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(id), "id");
            Contract.Requires<ArgumentNullException>(companyName != null, "companyName");
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(companyName), "companyName");
            Contract.Requires<ArgumentNullException>(contactName != null, "contactName");
            Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(contactName), "contactName");

            var c = new Customer() { Id=id, Name=companyName, m_Orders = new List<Order>() };
            c.ContactInfo = new ContactInfo { ContactName = contactName };
            return c;
        }

        /// <summary>
        /// Creates a customer instance
        /// </summary>
        protected Customer()
        {

        }

        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the phone number
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the fax number
        /// </summary>
        public virtual string FaxNumber { get; set; }

        /// <summary>
        /// gets or sets the contact informations
        /// </summary>
        public virtual ContactInfo ContactInfo { get; set; }

        protected virtual ICollection<Order> m_Orders { get; set; }
        
        /// <summary>
        /// Gets the customer's orders
        /// </summary>
        public virtual IEnumerable<Order> Orders
        {
            get
            {
                return this.m_Orders;
            }
        }

        /// <summary>
        /// Returns a System.String that represents the current Customer
        /// </summary>
        /// <returns>A System.String that represents the current Customer</returns>
        public override string ToString()
        {
            return this.Name;
        }

        /// <summary>
        /// Calculates the income generated by the customer
        /// </summary>
        /// <returns>The total income</returns>
        public virtual decimal CalculateTotalIncome()
        {
            Contract.Ensures(Contract.Result<decimal>()>=0);

            decimal income = this.Orders.Sum(o => o.CalculatePrice());
            return income;
        }

        /// <summary>
        /// Returns whether the customer is valid for registration
        /// </summary>
        public virtual bool IsValidForRegistration
        {
            get
            {
                ValidationResults results = ValidateForRegistration();
                return results.IsValid;
            }
        }

        /// <summary>
        /// Returns validation info for new customer registation
        /// </summary>
        /// <returns>The validation informations</returns>
        public virtual ValidationResults ValidateForRegistration()
        {
            Validator validator = ValidationFactory.CreateValidator<Customer>("IsValidForRegistration");
            ValidationResults results = validator.Validate(this);
            return results;
        }

        /// <summary>
        /// Returns whether the customer can make orders
        /// </summary>
        public virtual bool CanPlaceOrders
        {
            get
            {
                Validator validator = ValidationFactory.CreateValidator<Customer>("CanPlaceOrders");
                ValidationResults results = validator.Validate(this);

                return results.IsValid && IsValidForRegistration;
            }
        }

        /// <summary>
        /// Gets wether the current aggregate can be saved
        /// </summary>
        bool IAggregateRoot.CanBeSaved
        {
            get
            {
                return this.IsValidForRegistration;
            }
        }

        /// <summary>
        /// Gets wether the current aggregate can be deleted
        /// </summary>
        bool IAggregateRoot.CanBeDeleted
        {
            get
            {
                return false;
            }
        }
    }
}
