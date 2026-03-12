using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinanceAccountingSystem.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    [Required]
    [StringLength(50)]
    public string? InvoiceNumber { get; set; }

    [Required]
    public DateTime? InvoiceDate { get; set; }

    [Required]
    public int? SupplierId { get; set; }

    [Required]
    public int? CustomerId { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be a positive number.")]
    public decimal? Amount { get; set; }

    [Required]
    public decimal? TaxRate { get; set; }

    public decimal? TaxAmount { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Supplier? Supplier { get; set; }
}
