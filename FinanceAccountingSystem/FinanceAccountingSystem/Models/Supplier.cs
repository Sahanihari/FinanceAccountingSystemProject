using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace FinanceAccountingSystem.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    [Required]
    public string? Name { get; set; }

    [Required]
    public string? ContactNumber { get; set; }

    [Required]
    public string? Email { get; set; }

    public string? TaxId { get; set; }

    public int? AddressId { get; set; }
    
    public virtual Address? Address { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
