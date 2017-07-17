$(document).ready(function () {

    $("#test-button").click(function () {
        $.ajax({
            type: "GET",
            url: "http://localhost:64224/ArpTest/getBillingData",
            dataType: "json",
            context: document.body
        }).success(function (data) {
            data.forEach(function (element) {
                $("#connected-device-table").append("<tr><td>"+element[0]+"</td><td>"+element[1]+"</td><td>"+element[2]+"</td><td>"+element[3]+"</td></tr>");
            });
        });
    });


});