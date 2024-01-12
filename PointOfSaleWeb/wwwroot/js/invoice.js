let datatable;

$(document).ready(function () {

    loadDataTable();
})

function loadDataTable() {

    datatable = $("#tblData").DataTable({
        ajax: {
            "url": "/Admin/Invoice/GetAll"
        },
        order: [0, 'desc'],
        "columns": [

            { "data": "id" },
            {
                "data": "createdAt",
                "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return date.getDate() + "/" + (month.toString().length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                }
            },
            { "data": "name" },
            { "data": "phoneNumbar" },
            {
                "data": "total",
                "render": function (data) {
                    var total = data.toFixed(2);
                    return total;
                }

            },
            { "data": "paidAmount" },
            { "data": "paymentSataus" },
            { "data": "unpaidAmount" },
            {
                "data": "id",
                "render": function (data) {

                    return `
                    
                     <div class="btn-column">
                         <a href="/Admin/Invoice/Details?id=${data}" class="btn btn-primary me-1">Details</a>
                        <a onClick="Delete('/Admin/Invoice/Delete/${data}')" class="btn btn-danger">Delete</a>
                     </div>
                                          
                    `

                }

            }
        ],
        "columnDefs": [{
            "targets": '_all',
            "defaultContent": "###"
        }],
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
                        toastr.success(data.message);

                    }
                }

            })

        }
    });

}
