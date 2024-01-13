let datatable;

$(document).ready(function () {
    var url = window.location.search;
    let productListTitle = $("#productListTitle");
    if (url.includes("outOfStock")) {
        $("#productListTitle").addClass("customer-info-hide");
        $("#outOfStock").removeClass("customer-info-hide");
       
        loadDataTable("outOfStock");
    }
    else if (url.includes("lowStock")) {
        $("#productListTitle").addClass("customer-info-hide");
        $("#lowStockTitle").removeClass("customer-info-hide");
        loadDataTable("lowStock");
    }
    else {
        loadDataTable();
    }

   
})

function loadDataTable(status) {

    datatable = $("#tblData").DataTable({
        ajax: {
            "url": "/Admin/Product/GetAll?status="+status
        },
        "columns": [
            { "data": "name" },
            { "data": "supplier.company" },
            { "data": "category.name" },
            {
                "data": "quantity",
                "render": function (data) {
                    if (data % 1 != 0) {
                       let qty =  data.toFixed(2);
                        return qty;
                    }
                    return data;
                }
            },
            { "data": "unitsOfMeasurement.name" },
            {
                "data": "sellPrice",
                "render": function (data) {
                    if (data != null) {
                        let total = data.toFixed(2);
                        return total;
                    }

                    return "-";
                   
                }
            },


            {
                "data": "id",
                "render": function (data) {
                    return `
                    
                     <div class="btn-column">
                        <a href="/Admin/Product/Upsert?id=${data}" class="btn btn-primary me-1">Edit</a>
                        <a onClick="Delete('/Admin/Product/Delete/${data}')" class="btn btn-danger">Delete</a>
                     </div>
                        
               
                    
                    
                    `
                }

            }
        ]
    })

}

function Delete(url) {

    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                url: url,
                type: "Delete",
                success: function (data) {
                    if (data.success) {
                        datatable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);

                    }
                }

            })

        }
    });

}
