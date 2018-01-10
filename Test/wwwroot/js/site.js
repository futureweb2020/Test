
document.getElementById("submit").addEventListener("click", function () {
    var data = {
        input: $("#input").val()
    };
    $.ajax({
        url: "/Home/Check",
        type: "POST",
        dataType: "json",
        data: data,
        success: function (mydata) {
            if (!mydata) $("#output").val("Error occured");
            else $("#output").val(mydata);
        }
    });
});