$(function () {

    //var formatDate = new Date();
    //var responseDate = moment(new Date()).format('DD/MM/YYYY');

    //$("#Fecha").attr("defaultValue",moment(new Date()).format('DD/MM/YYYY'));


    //$(".birthday").datepicker({ dateFormat: "dd/mm/yyyy", 'setDate': new Date() }).mask("99/99/9999");
    //$.validator.addMethod('date',
    //    function (value, element, params) {
    //        if (this.optional(element)) {
    //            return true;
    //        }
    //        var ok = true;
    //        try {
    //            $.datepicker.parseDate('dd/mm/yyyy', value);
    //        }
    //        catch (err) {
    //            ok = false;
    //        }
    //        return ok;
    //    });

});
$('#NombreProveedor').autocomplete({  
    source: function (request, response) {
        $.getJSON("/Compras/GetListProveedorJson/", request, function (data) {
            response($.map(data, function (item) {
            
                return {
                    id: item.id,
                    value: item.label
                }
               
            }))
        })
    },
    minLength: 3,
    select: function (event, ui) {
        getProvedor(ui.item.id)
        $("#IdProveedor").val(ui.item.id); 
        $("#Proveedor_Id").val(ui.item.id); 
        
        this.value = ui.item.value;
        return false;
    }

});

$("#btnBuscarFacturaAplica").click(function () {
  
    $.getJSON("/Compras/GetCompraFactura",
        { idCompraFactura: $('#txtBusacarFactura').val() },
        function (r) {          
            if (r.data != "null" && r.result) {
                var facturaAplicada = JSON.parse(r.data);
                $('#CompraIva_Importe25').val(facturaAplicada.Importe25);
                $('#CompraIva_Importe5').val(facturaAplicada.Importe5);
                $('#CompraIva_Importe105').val(facturaAplicada.Importe105);
                $('#CompraIva_Importe21').val(facturaAplicada.Importe21);                
                $('#CompraIva_Importe27').val(facturaAplicada.Importe27);

                $('#CompraIva_Iva25').val(facturaAplicada.Iva25);
                $('#CompraIva_Iva5').val(facturaAplicada.Iva5);
                $('#CompraIva_Iva105').val(facturaAplicada.Iva105);
                $('#CompraIva_Iva21').val(facturaAplicada.Iva21);
                $('#CompraIva_Iva27').val(facturaAplicada.Iva27);

                $('#CompraIva_PercepcionIva').val(facturaAplicada.PercepcionIva);
                $('#CompraIva_PercepcionIB').val(facturaAplicada.PercepcionIB);
                $('#CompraIva_PercepcionProvincia').val(facturaAplicada.PercepcionProvincia);
                $('#CompraIva_PercepcionImporteIva').val(facturaAplicada.PercepcionImporteIva);
                $('#CompraIva_PercepcionImporteIB').val(facturaAplicada.PercepcionImporteIB);
                $('#CompraIva_PercepcionImporteProvincia').val(facturaAplicada.PercepcionImporteProvincia);
                $('#CompraIva_OtrosImpuestos').val(facturaAplicada.OtrosImpuestos);

                $('#CompraIva_NetoGravado').val(facturaAplicada.NetoGravado);
                $('#CompraIva_NetoNoGravado').val(facturaAplicada.NetoNoGravado);
                $('#CompraIva_SubTotal').val(facturaAplicada.SubTotal);
                $('#CompraIva_TotalIva').val(facturaAplicada.TotalIva);
                $('#CompraIva_TotalPercepciones').val(facturaAplicada.TotalPercepciones);
                $('#CompraIva_Total').val(facturaAplicada.Total);


            } else {               
                toastr.warning(r.msj)
            }                  
        });
});

$("#IdTipoComprobante").change(function () {    
   
    var select_text = $("#IdTipoComprobante option:selected").text();
    if (select_text.indexOf("NOTAS") > -1) {         
                $("#AplicaFactura").removeClass("invisible");
                $("#AplicaFactura").addClass("visible");
            } else {
                $("#AplicaFactura").removeClass("visible");
                $("#AplicaFactura").addClass("invisible");
            }
    
});

$("#IdMoneda").change(function () {
    var idMoneda = $("#IdMoneda option:selected").val();
    if (idMoneda != 1) {
        $.getJSON("/Compras/GetCotizacionMoneda",
            { idMoneda: idMoneda },
            function (data) {
                var cotizacion = JSON.parse(data);
                $("#Cotizacion").empty();
                $('#Cotizacion').val(cotizacion.Importe);
            });
    } else {
        $("#Cotizacion").empty();
        $('#Cotizacion').val(1);
    }   
});


function getProvedor(prov) {
 
    $.getJSON("/Compras/GetProveedorJson",
        { idProveedor: prov },
        function (data) {
            var proveedor = JSON.parse(data);

            $('#ivaProv').html(proveedor.TipoIva.Descripcion);
            $('#telProv').html(proveedor.Telefono);
            $('#dirProv').html(proveedor.Direccion);
            $('#IdTipoIva').val(proveedor.IdTipoIva);
            $('#IdImputacion').val(proveedor.IdImputacionFactura);
            $('#PuntoVenta').val(proveedor.UltimoPuntoVenta);

            $("#IdTipoComprobante").empty();
            $("#IdTipoComprobante").append("<option value> Seleccionar ...</option>")
            $.each(proveedor.ListTipoComprobante, function (index, row) {
                $("#IdTipoComprobante").append("<option value='" + row.Id + "'>" + row.Denominacion + "</option>")
            });
        });

}


jQuery('.fechadatepicker').datepicker({
    language: 'es',
    autoclose: true,
    format: 'dd/mm/yyyy',
    todayHighlight: true

}).datepicker('setDate', new Date());


$('#CompraIva_Importe25').focusout(function () { 
    $('#CompraIva_Iva25').val(calculoporcentaje('2.5', $("#CompraIva_Importe25").val()));
    getTotales()
    //$('#CompraIva_Importe5').focus();
});
$('#CompraIva_Importe5').focusout(function () {
    $('#CompraIva_Iva5').val(calculoporcentaje('5', $("#CompraIva_Importe5").val()));
    getTotales()
    //$('#CompraIva_Importe105').focus();
});
$('#CompraIva_Importe105').focusout(function () {
    $('#CompraIva_Iva105').val(calculoporcentaje('10.5', $("#CompraIva_Importe105").val()));
    getTotales()
    //$('#CompraIva_Importe21').focus();
});
$('#CompraIva_Importe21').focusout(function () {
    $('#CompraIva_Iva21').val(calculoporcentaje('21', $("#CompraIva_Importe21").val()));
    getTotales()
    //$('#CompraIva_Importe27').focus();
});
$('#CompraIva_Importe27').focusout(function () {
    $('#CompraIva_Iva27').val(calculoporcentaje('27', $("#CompraIva_Importe27").val()));    
    getTotales()
    //$('#CompraIva_PercepcionIva').focus();

});

$('#CompraIva_NetoNoGravado').focusout(function () {    
    getTotales()   
});

$('#CompraIva_PercepcionIva').focusout(function () {
    $('#CompraIva_PercepcionImporteIva').val(calculoporcentaje( $("#CompraIva_PercepcionIva").val(),$('#CompraIva_NetoGravado').val()));
    getTotales()
   // $('#CompraIva_PercepcionIB').focus();
});

$('#CompraIva_PercepcionIB').focusout(function () {
    $('#CompraIva_PercepcionImporteIB').val(calculoporcentaje($("#CompraIva_PercepcionIB").val(), $('#CompraIva_NetoGravado').val()));
    getTotales()
    //$('#CompraIva_PercepcionProvincia').focus();
});

$('#CompraIva_PercepcionProvincia').focusout(function () {
    $('#CompraIva_PercepcionImporteProvincia').val(calculoporcentaje($("#CompraIva_PercepcionProvincia").val(), $('#CompraIva_NetoGravado').val()));
    getTotales()
   // $('#CompraIva_OtrosImpuestos').focus();
});


function getTotales() {
    setNeto();
    setTotalIva();
    setTotalPercepciones();
    setTotal();
}
function setTotal() {
    $('#CompraIva_Total').val(
        getNro($("#CompraIva_SubTotal").val()) +
        getNro($('#CompraIva_TotalIva').val()) +
        getNro($('#CompraIva_TotalPercepciones').val()) 
    );

}
function setNeto() {
 
    $('#CompraIva_NetoGravado').val(
        getNro($("#CompraIva_Importe25").val()) +
        getNro($('#CompraIva_Importe5').val()) +
        getNro($('#CompraIva_Importe105').val()) +
        getNro($('#CompraIva_Importe21').val()) +
        getNro($("#CompraIva_Importe27").val())
    );

    $('#CompraIva_SubTotal').val(
        getNro($('#CompraIva_NetoGravado').val()) +
        getNro($('#CompraIva_NetoNoGravado').val()) 
        );

}

function setTotalIva() {
    $('#CompraIva_TotalIva').val(
        getNro($("#CompraIva_Iva25").val()) +
        getNro($('#CompraIva_Iva5').val()) +
        getNro($('#CompraIva_Iva105').val()) +
        getNro($('#CompraIva_Iva21').val()) +
        getNro($("#CompraIva_Iva27").val())
    );

}

function setTotalPercepciones() {
    $('#CompraIva_TotalPercepciones').val(
        getNro($("#CompraIva_PercepcionImporteIva").val()) +
        getNro($('#CompraIva_PercepcionImporteIB').val()) +
        getNro($('#CompraIva_PercepcionImporteProvincia').val()) +
        getNro($('#CompraIva_OtrosImpuestos').val()) 
    );

}

function getNro(x) {
    x = parseFloat(x);
    return (x == null || isNaN(x) || x == undefined || x == "") ? 0 : x;
}





if ($("#IdProveedor").val() > 0) {
  getProvedor($("#IdProveedor").val());
 }
  


//$('#CompraFacturaAplicada_NumeroFactura').autocomplete({
//    source: function (request, response) {
//        $.getJSON("/Compras/GetListCompraFacturaJson/", request, function (data) {
//            response($.map(data, function (item) {
//                return {
//                    id: item.id,
//                    value: item.label
//                }
//            }))
//        })
//    },
//    minLength: 2,
//    select: function (event, ui) {        
//        $("#IdCompraFacturaAplica").val(ui.item.id);
//        this.value = ui.item.value;
//        return false;
//    }
//});

