function showSupplierErrors(errors) {
    $("#SupplierNameError").text(errors.Name || "");
    $("#SupplierEmailError").text(errors.Email || "");
    $("#SupplierContactError").text(errors.ContactNumber || "");
    $("#SupplierAddressIdError").text(errors.AddressId || "");
}

function clearSupplierErrors() {
    $("#SupplierNameError").text("");
    $("#SupplierEmailError").text("");
    $("#SupplierContactError").text("");
    $("#SupplierAddressIdError").text("");
}

function saveSupplier() {
    clearSupplierErrors();
    var name = $("#Name").val();
    var email = $("#Email").val();
    var contact = $("#ContactNumber").val();
    var addressId = $("#SupplierAddressId").val();
    var errors = {};
    if (!name) errors.Name = "Name is required.";
    if (!email) errors.Email = "Email is required.";
    if (!contact) errors.ContactNumber = "Contact is required.";
    if (!addressId) errors.AddressId = "Address is required.";
    if (Object.keys(errors).length > 0) {
        showSupplierErrors(errors);
        return;
    }
    var supplier = {
        SupplierId: parseInt($("#SupplierId").val() || '0', 10),
        Name: name,
        Email: email,
        ContactNumber: contact,
        TaxId: $("#TaxId").val(),
        AddressId: addressId ? parseInt(addressId, 10) : null
    };
    var url = supplier.SupplierId === 0 ? "/Supplier/Create" : "/Supplier/Update";
    $.post(url, supplier)
        .done(function () {
            location.reload();
        })
        .fail(function (xhr) {
            alert('Save failed. See console for details.');
        });
}

function editSupplier(id) {
    clearSupplierErrors();
    $.get("/Supplier/GetSupplier/" + id, function (data) {
        $("#SupplierId").val(data.supplierId);
        $("#Name").val(data.name);
        $("#Email").val(data.email);
        $("#ContactNumber").val(data.contactNumber);
        $("#TaxId").val(data.taxId);
        $("#SupplierAddressId").val(data.addressId);
        $("#supplierModal").modal("show");
    });
}

function deleteSupplier(id) {
    if (confirm("Are you sure to delete?")) {
        $.post("/Supplier/Delete/" + id, function () {
            location.reload();
        });
    }
}

function clearForm() {
    $("#SupplierId").val('');
    $("#Name").val('');
    $("#Email").val('');
    $("#ContactNumber").val('');
    $("#TaxId").val('');
    $("#SupplierAddressId").val('');
    clearSupplierErrors();
}