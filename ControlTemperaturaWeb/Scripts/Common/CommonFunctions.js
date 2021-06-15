//Le envia al usuario una alerta con el texto ingresado
//PARAMS - message: mensaje de la alerta, titleText: titulo de la alerta, buttonText: texto del boton de cerrar el dialogo - por defecto es Aceptar, 
//bootstrapType: es el estilo bootstrap que va a tener el dialog - los valores pueden ser WARNING, DANGER, PRIMARY, SUCCESS, DEFAULT - por defecto es DANGER,
function BootstrapDialogShow(message, titleText, buttonText) {
    buttonText = (buttonText == undefined) ?  Aceptar   : buttonText;

    BootstrapDialog.show({
        type: BootstrapDialog.TYPE_DANGER,
        title: '::: ' + titleText + ' :::',
        message: message,
        buttons: [{
            label: buttonText,
            cssClass: 'btn-primary',
            action: function (dialogItself) {
                dialogItself.close();
            }
        }]
    });
}
function ErrorDialogShow(message, titleText, buttonText, detail) {
    var htmlMessage = "<div>"
        + "<div>" + message + "</div><br>"
        + "<div><a href='#' onclick='togglerVerDetalle(this);'>" +  "Ver Detalle"  + "</a></div>"
        + "<div id='dialogErrorDetail' style='overflow-x:scroll;'><p>" + detail + "</p></div>"
        + "</div>";
    
    BootstrapDialog.show({
        type: BootstrapDialog.TYPE_DANGER,
        title: '::: ' + titleText + ' :::',
        message: htmlMessage,
        buttons: [{
            label: buttonText,
            cssClass: 'btn-primary',
            action: function (dialogItself) {
                dialogItself.close();
            }
        }]
    });
}
function togglerVerDetalle(self) {
    var detail = $(self).parents('div').first().next();
    detail.toggle();
}

function colorToHex(color) {
    if (color.substr(0, 1) === '#') {
        return color;
    }
    var digits = /(.*?)rgb\((\d+), (\d+), (\d+)\)/.exec(color);

    var red = parseInt(digits[2]);
    var green = parseInt(digits[3]);
    var blue = parseInt(digits[4]);

    var rgb = blue | (green << 8) | (red << 16);
    return digits[1] + '#' + rgb.toString(16);
}

function padLeft(str, max) {
    str = str.toString();
    return str.length < max ? padLeft("0" + str, max) : str;
};