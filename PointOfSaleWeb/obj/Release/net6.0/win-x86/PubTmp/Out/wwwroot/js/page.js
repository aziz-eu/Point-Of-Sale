const toggler = document.querySelector(".btn");
toggler.addEventListener("click", function () {
    document.querySelector("#sidebar").classList.toggle("collapsed");
    document.querySelector("#main").classList.toggle("main-box");
});
toggleCustomer();



function toggleCustomer() {
    // Get the checkbox
    var checkBox = document.getElementById("toggle-customer"); 
   
    // Get the output text
    let regularCustomer = document.getElementById("regularCustomer");
    let newCustomerInfo = document.getElementById("newCustomerInfo");
    var customerList = document.getElementById("InvoiceHeader_CustomerID");
    
  


    checkBox.addEventListener('change', function () {
        if (this.checked) {
            regularCustomer.classList.add("customer-info-hide");
            newCustomerInfo.classList.remove("customer-info-hide")

            for (let i = 0; i < customerList.length; i++) {

                customerList.setAttribute("disabled", "disabled");
            }

        } else {
            newCustomerInfo.classList.remove("customer-info-hide")
        }
    });
}