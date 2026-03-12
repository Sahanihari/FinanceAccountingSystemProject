function showCustomerErrors(errors) {
    $("#CustomerNameError").text(errors.Name || "");
    $("#CustomerEmailError").text(errors.Email || "");
    $("#CustomerContactError").text(errors.ContactNumber || "");
    $("#CustomerAddressIdError").text(errors.AddressId || "");
}

function clearCustomerErrors() {
    $("#CustomerNameError").text("");
    $("#CustomerEmailError").text("");
    $("#CustomerContactError").text("");
    $("#CustomerAddressIdError").text("");
}

function saveCustomer() {
    clearCustomerErrors();
    // Client-side validation
    var name = $("#CustomerName").val();
    var email = $("#CustomerEmail").val();
    var contact = $("#CustomerContact").val();
    var addressId = $("#CustomerAddressId").val();
    var errors = {};
    if (!name) errors.Name = "Name is required.";
    if (!email) errors.Email = "Email is required.";
    if (!contact) errors.ContactNumber = "Contact is required.";
    if (!addressId) errors.AddressId = "Address is required.";
    if (Object.keys(errors).length > 0) {
        showCustomerErrors(errors);
        return;
    }
    var customer = {
        CustomerId: parseInt($("#CustomerId").val() || '0', 10),
        Name: name,
        Email: email,
        ContactNumber: contact,
        AddressId: addressId ? parseInt(addressId, 10) : null
    };
    var url = customer.CustomerId === 0 ? "/Customer/Create" : "/Customer/Update";
    $.post(url, customer)
        .done(function (result) {
            if (result.success === false && result.errors) {
                // Show server-side errors
                var serverErrors = {};
                for (var key in result.errors) {
                    if (result.errors[key].length > 0) {
                        serverErrors[key] = result.errors[key][0];
                    }
                }
                showCustomerErrors(serverErrors);
                alert('Save failed. See errors below fields.');
            } else {
                location.reload();
            }
        })
        .fail(function (xhr) {
            alert('Save failed. See console for details.');
        });
}

function editCustomer(id) {
    clearCustomerErrors();
    $.get("/Customer/GetCustomer/" + id, function (data) {
        $("#CustomerId").val(data.customerId);
        $("#CustomerName").val(data.name);
        $("#CustomerEmail").val(data.email);
        $("#CustomerContact").val(data.contactNumber);
        $("#CustomerAddressId").val(data.addressId);
        $("#customerModal").modal("show");
    });
}

function deleteCustomer(id) {
    if (confirm("Are you sure to delete?")) {
        $.post("/Customer/Delete/" + id, function () {
            location.reload();
        });
    }
}

function clearCustomerForm() {
    $("#CustomerId").val('');
    $("#CustomerName").val('');
    $("#CustomerEmail").val('');
    $("#CustomerContact").val('');
    $("#CustomerAddressId").val('');
    clearCustomerErrors();
}