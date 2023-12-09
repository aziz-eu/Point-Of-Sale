let datatable;

$(document).ready(function () {

    loadDataTable();
})

function loadDataTable() {

    datatable = $("#tblData").DataTable({
        ajax: {
            "url": "/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "name" },
            { "data": "supplier.company" },
            { "data": "category.name" },
            { "data": "quantity"},
            { "data": "unitsOfMeasurement.name" },
            { "data": "unitPrice" },


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
