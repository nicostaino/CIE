
var PageStackerNavigator = PageStackerNavigator || {

    idstack:0,
    stack: [],


    pushPage: function (title, fHide, fRestore, fClose) {

        PageStackerNavigator.idstack++;

        var pageInfo = {
            Id: PageStackerNavigator.idstack,
            Title: title,
            HideFunction: fHide,
            RestoreFunction: fRestore,
            CloseFunction: fClose
        };

        // Oculta la page anterior
        if (PageStackerNavigator.stack.length > 0) {
            var pageAnterior = PageStackerNavigator.stack[PageStackerNavigator.stack.length - 1];
            pageAnterior.HideFunction();
        }

        // Agrega a la pila la nueva página 
        PageStackerNavigator.stack.push(pageInfo);

        // Actualiza el breadcrumb
        PageStackerNavigator.updateBreadcrumb();
       
        return PageStackerNavigator.idstack;
    },

    updateBreadcrumb: function () {

        $("#navegadorBreadcrumb").html("");

        for (var p in PageStackerNavigator.stack) {
            var pageInfo = PageStackerNavigator.stack[p];

            $("#navegadorBreadcrumb").append("<li><span id='breadcrumb" + pageInfo.Id + "' style='cursor:pointer;' data-id='" + pageInfo.Id + "'>" + pageInfo.Title + "</span></li>");

            $("#breadcrumb" + pageInfo.Id).click(function () {

                var id = $(this).data("id");
                PageStackerNavigator.goToPage(id);

            });
        }

    },

    popLastPage: function () {
        // Cierra la última página
        if (PageStackerNavigator.stack.length > 1) {
            var pageAnterior = PageStackerNavigator.stack[PageStackerNavigator.stack.length - 1];
            pageAnterior.CloseFunction();
            PageStackerNavigator.stack.pop();

            var pageNueva = PageStackerNavigator.stack[PageStackerNavigator.stack.length - 1];
            pageNueva.RestoreFunction();

            // Actualiza el breadcrumb
            PageStackerNavigator.updateBreadcrumb();
        }


    },

    goToPage: function (id) {

        for (var i = PageStackerNavigator.stack.length-1; i >= 0; i--) {

            var page = PageStackerNavigator.stack[i];
            if (page.Id > id) {
                PageStackerNavigator.popLastPage();
            }
        }

    },

    getCurrentPageTitle: function () {
        var page = PageStackerNavigator.stack[PageStackerNavigator.stack.length - 1];
        return page.Title;
    },

    changeCurrentPageTitle: function(nuevotitle) 
    {
        var page = PageStackerNavigator.stack[PageStackerNavigator.stack.length - 1];
        page.Title = nuevotitle;
        PageStackerNavigator.updateBreadcrumb();
    }

}