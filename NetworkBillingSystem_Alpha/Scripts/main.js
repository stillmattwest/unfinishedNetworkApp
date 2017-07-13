$(document).ready(function () {

    $("#test-button").click(function () {
        $.ajax({
            type: "GET",
            url: "http://localhost:64224/ArpTest/getBillingData",
            dataType: "json",
            context: document.body
        }).success(function (data) {
            $(".test-output").html('');
            data.forEach(function (element) {
                $(".test-output").append("<p>" + element[0] + "," + element[1] + "</p>");
            });
        });
    });


});