$(document).ready(function () {

    $(".addressBtn").click(function (event)
    {
        var customerId = event.target.name;        
        GetCustomerAddressFromServer(customerId);
    });

});

function GetCustomerAddressFromServer(customerId) {    
    $.ajax({
        url: '/api/CustomerAddress/' + customerId,
        success: function (data)
        {     
            var name = data[0];
            var address = data[1];

            $('#customerModalName').html(name);
            $('#customerModalAddress').html(address);
        },
        statusCode: {
            404: function (content) { alert('cannot find resource'); },
            500: function (content) { alert('internal server error'); }
        },
        error: function (req, status, errorObj)
        {
            alert("An error occurred communicating with the server, check the log for more information");
            console.log(errorObj);
        }
    });
}



