using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinanceAccountingSystem.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    [Required]
    public string? Name { get; set; }

    public int? AddressId { get; set; }

    [Required]
    public string? ContactNumber { get; set; }

    [Required]
    public string? Email { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
