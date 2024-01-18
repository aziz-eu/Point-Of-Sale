let datatable;
let i = 1;

$(document).ready(function () {

    loadDataTable();
})

function loadDataTable() {

    datatable = $("#tblData").DataTable({
        ajax: {
            "url": "/Admin/Invoice/GetAll"
        },
        order: [[0, 'desc'], [9, 'asc']],
        "columns": [



            { "data": "id"},
            { 
              "data": "createdAt",
                "render": function (data) {
                    let dateTime = new Date(data);
                    let date = dateTime.getDate();
                    let month = dateTime.getMonth() + 1;
                    let year = dateTime.getFullYear();
                    return (date.toString().length > 1 ? date : "0" + date) + "/" + (month.toString().length > 1 ? month : "0" + month) + "/" + year;
                }
            },
            { "data": "name" },
            { "data": "phoneNumbar" },
            {
              "data": "total",
                "render": function (data) {
                    let total = data.toFixed(2);
                    return total;
                }

            },
            {
               "data": "paidAmount",

                "render": function (data) {
                     let paidAmount = data.toFixed(2);
                     return paidAmount;
                }
            },
            { "data": "paymentSataus" },
            {
                "data": "unpaidAmount",
                "render": function (data) {
                    let unpaidAmount = data.toFixed(2);
                    return unpaidAmount;
                }
            },
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

            },
            {
                "data": "id",
                "render": function (data) {
                    return "I"+data;
                }
            }
        ],
        "columnDefs": [{
            "targets": '_all',
            "defaultContent": "-",
            
        },
            {
                target: 9,
                visible: false,
                searchable: true
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
                        toastr.error(data.message);

                    }
                }

            })

        }
    });

}
