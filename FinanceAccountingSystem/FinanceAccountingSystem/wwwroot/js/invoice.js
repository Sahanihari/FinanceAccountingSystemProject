$(document).ready(function () {

    function showInvoiceErrors(errors) {
        $("#InvoiceNumberError").text(errors.InvoiceNumber || "");
        $("#InvoiceDateError").text(errors.InvoiceDate || "");
        $("#InvoiceSupplierIdError").text(errors.SupplierId || "");
        $("#InvoiceCustomerIdError").text(errors.CustomerId || "");
        $("#InvoiceAmountError").text(errors.Amount || "");
        $("#InvoiceTaxRateError").text(errors.TaxRate || "");
    }

    function clearInvoiceErrors() {
        $("#InvoiceNumberError").text("");
        $("#InvoiceDateError").text("");
        $("#InvoiceSupplierIdError").text("");
        $("#InvoiceCustomerIdError").text("");
        $("#InvoiceAmountError").text("");
        $("#InvoiceTaxRateError").text("");
    }

    function calculateInvoiceTax() {
        var amount = parseFloat($("#InvoiceAmount").val()) || 0;
        var taxRate = parseFloat($("#InvoiceTaxRate").val()) || 0;
        var tax = (amount * taxRate) / 100;
        var total = amount + tax;
        $("#InvoiceTaxAmount").val(tax.toFixed(2));
        $("#InvoiceTotalAmount").val(total.toFixed(2));
    }

    $(document).on("change keyup", "#InvoiceAmount,#InvoiceTaxRate", function () {
        calculateInvoiceTax();
    });

    // Handle form submit for AJAX save
    $("#invoiceForm").on("submit", function (e) {
        e.preventDefault();
        clearInvoiceErrors();
        var invoiceNumber = $("#InvoiceNumber").val();
        var invoiceDate = $("#InvoiceDate").val();
        var supplierId = $("#InvoiceSupplierId").val();
        var customerId = $("#InvoiceCustomerId").val();
        var amount = $("#InvoiceAmount").val();
        var taxRate = $("#InvoiceTaxRate").val();
        var errors = {};
        if (!invoiceNumber) errors.InvoiceNumber = "Invoice Number is required.";
        if (!invoiceDate) errors.InvoiceDate = "Date is required.";
        if (!supplierId) errors.SupplierId = "Supplier is required.";
        if (!customerId) errors.CustomerId = "Customer is required.";
        if (!amount || parseFloat(amount) <= 0) errors.Amount = "Amount must be positive.";
        if (taxRate === null || taxRate === undefined || taxRate === "") errors.TaxRate = "Tax Rate is required.";
        if (Object.keys(errors).length > 0) {
            showInvoiceErrors(errors);
            return;
        }
        calculateInvoiceTax();
        var invoice = {
            InvoiceNumber: invoiceNumber,
            InvoiceDate: invoiceDate,
            SupplierId: parseInt(supplierId, 10),
            CustomerId: parseInt(customerId, 10),
            Amount: parseFloat(amount),
            TaxRate: parseFloat(taxRate),
            TaxAmount: parseFloat($("#InvoiceTaxAmount").val()),
            TotalAmount: parseFloat($("#InvoiceTotalAmount").val())
        };
        $.ajax({
            url: '/Invoice/Create',
            method: 'POST',
            data: invoice
        }).done(function () {
            window.location.href = '/Invoice/Index';
        }).fail(function (xhr) {
            alert('Save failed. See console for details.');
            console.error(xhr);
        });
    });

    calculateInvoiceTax();

    // Helper to clear the form (if used)
    window.clearInvoiceForm = function () {
        $("#InvoiceNumber").val('');
        $("#InvoiceDate").val('');
        $("#InvoiceSupplierId").val('');
        $("#InvoiceCustomerId").val('');
        $("#InvoiceAmount").val('');
        $("#InvoiceTaxRate").val('0');
        $("#InvoiceTaxAmount").val('');
        $("#InvoiceTotalAmount").val('');
        clearInvoiceErrors();
    }

});});