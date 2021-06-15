UserName = "";
SitePath = "";
var Main = Main || {

    init: function (sitepath, username) {
        UserName = username;
        SitePath = sitepath;
        // Limpia la información del usuario
        $('#displayName').html("");
        $('#usrPhoto').attr("src", "/Content/images/user.png");

        // Obtiene la información del usuario: foto y nombre a partir del UserName
        Main.getLoginData();

        //$(document).ajaxStop($.unblockUI);
    },

    resize: function () {
        $("#menu").css("height", ($(window).height() - 62) + 'px');
    },

    orientationchange: function () {

    }, 
    getLoginData: function () {

        localStorage.usrPhoto = "";
        localStorage.displayName = "";

        //$.ajax({
        //    url: SitePath + "/LoginInfo.axd?username=" + UserName,
        //    success: function (result) {
        //        if (result.displayName) {

        //            localStorage.displayName = result.displayName;
        //            $("#displayName").html(result.displayName);

        //            if (result.usrPhoto) {
        //                localStorage.usrPhoto = result.usrPhoto;
        //                $("#usrPhoto").attr("src", "data:image/jpg;base64," + result.usrPhoto);
        //            }
        //        }
        //    },
        //    error: function (msg) {
        //    }
        //});

    }

};