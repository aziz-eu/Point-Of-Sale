let datatable;

$(document).ready(function () {

    loadDataTable();
})

function loadDataTable() {

    datatable = $("#tblData").DataTable({
        ajax: {
            "url": "/Admin/Category/GetAll"
        },
        "columns": [
            { "data": "name" },
            { "data": "description" },

            {
                "data": "id",
                "render": function (data) {
                    return `
                    
                     <div class="btn-column">
                        <a href="/Admin/Category/Upsert?id=${data}" class="btn btn-primary me-1">Edit</a>
                        <a onClick="Delete('/Admin/Category/Delete/${data}')" class="btn btn-danger">Delete</a>
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
