

$(document).ready(function () {
     
    $("#ProfilePic").click(function () { 
        $("#PhotoUpload").click();
    });

});


function onInputChange(e) {

    $("#ProfilePic").attr('src', URL.createObjectURL(event.target.files[0]));
    //var res = "";
    //for (var i = 0; i < $("#PhotoUpload").get(0).files.length; i++) {
    //    res += $("#PhotoUpload").get(0).files[i].name + "<br />";
    //}

    //$('#result').html(res);
}