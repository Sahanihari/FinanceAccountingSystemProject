using System;
using System.Collections.Generic;

namespace FinanceAccountingSystem.Models;

public partial class Address
{
    public int AddressId { get; set; }= new int();

    public string? Street { get; set; }

    public string? City { get; set; }

    public string? PostCode { get; set; }

    public string? Country { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
}
