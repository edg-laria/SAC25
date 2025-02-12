$(function () {

 
});


//$(".ShowBtnConfirmarCobro").click(function () {
//    confirmarCobro();
//});

function confirmarCobro(totalAplicacion) {
    //var totalFact = parseFloat($("#LblImporteAPagarCheque").html().trim());//total de factura
    //var totalCobro = parseFloat($("#LblTotalPago").html().trim());
    //var diferencia = parseFloat($("#LblTotalSaldo").html().trim());
   // if ((totalFact > 0 || totalCobro != 0) && diferencia == 0) {       
    if (totalAplicacion == 0) {
        $("#btnGenerarPago").css("display", "block");
    } else {
        $("#btnGenerarPago").css("display", "none");
    }
}


//-------new cheque cliente
function fnNewModalCheques() {
    $("#ImporteAPagarCheque").html($("#TotalAPagar").val());
    $('#IdCliente').val($('#IdCliente').val());
    $("#ModalNewCheque").modal('show');
}

function fnAsignarNewCheque() {

    var total = 0;
    total = importeChequePropio + importeChequeTercero;
    $("#montoChequesSeleccionados").val(total.toFixed(2));
    $("#ModalNewCheque").modal('hide');
    fnCalcularDirefencia();
    
    SumarTotalPago();
    //id cheque propio si obtiene en onCompleteIngresarCheque
    //id cheques terceros

    var i = 0;
    $('.checkChequeTercero:checked').each(function (index) {
        if (i > 0) {
            $("#idChequesTerceros").val($("#idChequesTerceros").val() + ';' + $(this)[0].id);
        }
        else {
            $("#idChequesTerceros").val("");
            $("#idChequesTerceros").val($(this)[0].id);

        }
        i++;
    });


}


function ConfirmaEliminarCheque(valor) {
    event.preventDefault();
    document.getElementById("IdCheque").value = valor;
    $("#ModalConfirmaEliminar").modal('show');
}
function onCompleteQuitarCheque(result) {
    importeChequePropio = 0;
    $("#idChequesCliente").val("");
    $("#TotalChequeSeleccionados").html(parseFloat(importeChequeTercero + importeChequePropio));
    $("#ModalConfirmaEliminar").modal("hide");
}
function onSuccessNewCheque(data, status, xhr) {

    //$("#ajaxFormContainer").html(data.View);


    if (data.Success == false || data.Success != undefined) {

        toastr.info(data.Message);

    }

}
function onFailureNewCheque(xhr, status, error) {
    response = xhr.responseJSON;
    window.location.href = response.RedirectUrl;
}

function onCompleteIngresarNewCheque(xhr, status) {

    $("#TblChequeCliente tbody tr").each(function (index) {
        $("#idChequesCliente").val(parseFloat($(this)[0].attributes[0].nodeValue));        
        importeChequePropio = parseFloat($(this)[0].attributes[1].nodeValue);
    })

    $("#TotalChequeSeleccionados").html(importeChequeTercero + importeChequePropio);
}


///---------------




function PagarFinal() {
    $("#frmPagar").trigger("submit");
}

function ValidarIngresoPago() {

    if ($("#ConceptoPago_").val() != '') {
        if ($("#montoTotal_").val() != '') {

            if ($("#montoTotal_").val() != "0") {
                return true;
            }
            else {
                return false;
            }
        }
        else {
            return false;
        }
    }
    else {
        return false;
    }
}

function mostrarBoton() {

    var table = document.getElementById('tableRetencion');
    var rowLength = table.rows.length;
    if (rowLength > 0) {
        document.getElementById("BtnSetear").style.display = 'block';
    }
    else {
        document.getElementById("BtnSetear").style.display = 'none';
    }
}

function SetearRetencion() {
    var total_col1 = 0;
    //Recorro todos los tr ubicados en el tbody
    $('#tableRetencion tbody').find('tr').each(function (i, el) {
        total_col1 += parseFloat($(this).find('td').eq(1).text());
    });
    document.getElementById("montoRetencion").value = total_col1;


    SumarTotalPago();
    //retenciones
    var i = 0;
    $("#tableRetencion tbody tr").each(function (index) {          
        if (i > 0) {    
            $("#idRetencionesCliente").val($("#idRetencionesCliente").val() + ';' + parseFloat($(this)[0].attributes[0].nodeValue));
        }
        else {
            $("#idRetencionesCliente").val("");
            $("#idRetencionesCliente").val(parseFloat($(this)[0].attributes[0].nodeValue));
        }
        i++;
    });


    $("#ModalRetenciones").modal('hide');
}

function mostrar() {
    $("#ModalRetenciones").modal('show');
    $("#ModalEliminar").modal('hide');

    var table = document.getElementById('tableRetencion');
    if (table != null) {
        document.getElementById("BtnSetear").style.display = 'block';
    }
    else {
        document.getElementById("BtnSetear").style.display = 'none';
    }
}


function CancelarEliminacion() {
    $("#ModalRetenciones").modal('show');
    $("#ModalEliminar").modal('hide');
}

function ConfirmaEliminarRetencion(valor, valor1) {
    event.preventDefault();
    $("#ModalRetenciones").modal('hide');
    document.getElementById("IdRetencionEliminar").value = valor;
    document.getElementById("IdFacturaEliminar").value = valor1;
    $("#ModalEliminar").modal('show');
}

function Eliminar() {
    $("#frmEliminarRetencion").trigger("submit");
}

///6
function fnVerModalRetencion() {
    $("#ModalRetenciones").modal('show');
}

///5
function CancelarPago() {

    $("#frmBusqueda").trigger("submit");

}

///4
function EjecutarPago() {

    $("#frmCrearCobro").trigger("submit");
    $("#ConceptoCobro").val('');
    $("#NumeroRecibo").val('');
    $("#montoEfectivo").val('');
    $("#montoTarjeta").val('');
    $("#montoChequesSeleccionados").val('');
    $("#montoCuentaBancaria_").val('');
    $("#montoRetencion").val('');
    $("#montoTotal").val('');
    $("#idPresupuesto").val(0);
    //$("#btnGenerarPago").css("display", "block");
    confirmarCobro();
}


///3
function SumarTotalPago() {
    var comision = parseFloat(document.getElementById("montoComision").value);
    if (isNaN(comision)) { comision = 0 }

    var efectivo = parseFloat(document.getElementById("montoEfectivo").value);
    if (isNaN(efectivo)) { efectivo = 0 }
    var Tarjeta = parseFloat(document.getElementById("montoTarjeta").value);
    if (isNaN(Tarjeta)) { Tarjeta = 0 }
    var Cheque = parseFloat(document.getElementById("montoChequesSeleccionados").value);
    if (isNaN(Cheque)) { Cheque = 0 }
    var CtaBco = parseFloat(document.getElementById("montoCuentaBancaria").value);
    if (isNaN(CtaBco)) { CtaBco = 0 }
    var Retencion = parseFloat(document.getElementById("montoRetencion").value);
    if (isNaN(Retencion)) { Retencion = 0 }
    total = efectivo + Tarjeta + Cheque + CtaBco + Retencion + comision;
    document.getElementById("montoTotal").value = total.toFixed(2);
}


///2
function ConfirmarModoPago() {

    document.getElementById("CotizacionActual").value = document.getElementById("Cotizacion").value;
    document.getElementById("idMoneda").value = $("#IdTipoMoneda option:selected").val();
    tipoComprobante = document.getElementById("tipoComprobante").value;
    var MontoParcial = parseFloat(document.getElementById("txtMonto").value);
    var valorFactura = parseFloat($("#lblMonto").text());  

   

    //var newIdMoneda = $("#IdTipoMoneda option:selected").val();
    //var newCotiza = parseFloat($("#Cotizacion").val());
    //var idMonedaFactura =  $("#idMonedaFactura").val();
    //if (idMonedaFactura != newIdMoneda) {
    //    if (idMonedaFactura == 1 ) { // pesos to new money
    //        valorFactura = valorFactura / newCotiza;
    //    }
    //    if (idMonedaFactura > 1 && newIdMoneda == 1) { // to pesos
    //        valorFactura = valorFactura * newCotiza;
    //    }
    //}


    if (isNaN(MontoParcial)) { MontoParcial = 0; }


    if (tipoComprobante != 34) {
        //verifico que el monto no exceda al valor de factura
        if (MontoParcial < valorFactura) {


            $.get("/Cobro/VerificarCobro", function (data) {
                if (data == "0") {
                    //monto total
                    if (MontoParcial == 0) {
                        document.getElementById("idModoCobro").value = 1;                        
                        $("#frmCargarFactura").trigger("submit");
                        $("#Retencion_IdFactVenta").append("<option value='" + $("#idFacturaSeleccionada").val() + "'> Nro. Fact." + $("#nroFacturaModal").html() + " - Total $" + valorFactura + "</option>");
                    }
                    else {
                        //monto parcial
                        if (MontoParcial != 0) {
                            document.getElementById("idModoCobro").value = 2;
                            document.getElementById("MontoCobro").value = MontoParcial;                          
                            $("#frmCargarFactura").trigger("submit");
                            $("#Retencion_IdFactVenta").append("<option value='" + $("#idFacturaSeleccionada").val() + "'> Nro. Fact." + $("#nroFacturaModal").html() + " - Total $" + valorFactura + "</option>");
                        }
                    }
                }
                else {
                    alert("ya tiene un cobro realizado");
                }
            });


        }
        else {
            alert("El monto no debe superar al monto de la factura");
        }
    }
    else {
        //modo 3 es pago con salgo favor
        document.getElementById("idModoCobro").value = 3;
        document.getElementById("MontoCobro").value = parseFloat($("#lblMonto").text());   
        $("#frmCargarFactura").trigger("submit");
        $("#Retencion_IdFactVenta").append("<option value='" + $("#idFacturaSeleccionada").val() + "'> Nro. Fact." + $("#nroFacturaModal").html() + " - Total $" + valorFactura  + "</option>");

    }

    $("#ModalModoPago").modal('hide');



}

///1 adjunto cbte a resumen
function Cobrar(valor, valor1, valor2, tipoComprobante, IdMoneda) {

 //grabo en hidden para usar despues
    document.getElementById("idFacturaSeleccionada").value = valor;      
    document.getElementById("tipoComprobante").value = tipoComprobante;    
    $("#idMonedaFactura").val(IdMoneda);
    if (tipoComprobante != 34) {
        $("#tituloPago").css("display", "none");
        $("#tituloFactura").css("display", "block");
        document.getElementById("txtMonto").value = "";
        document.getElementById("lblMonto").innerHTML = valor1;
        document.getElementById("nroFacturaModal").innerHTML = valor2;
    }
    else {
        $("#tituloFactura").css("display", "none");
        $("#tituloPago").css("display", "block");
        document.getElementById("lblMonto").innerHTML = valor1;
        document.getElementById("nroPagoModal").innerHTML = valor2;
    }

    $("#ModalModoPago").modal('show');
    
}


///---------------

$('#Cliente_Nombre').autocomplete({
    source: function (request, response) {
        $.getJSON("/Cobro/GetListClienteJson/", request, function (data) {
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
        $("#IdCliente").val(ui.item.id);
        this.value = ui.item.value;
       // $("#frmBusqueda").submit();
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

$("#IdTipoMoneda").change(function () {
    var idMoneda = $("#IdTipoMoneda option:selected").val();
    $('#IdMonedaDeOperacion').val(idMoneda);
        //$.getJSON("/Compras/GetCotizacionMoneda",
        //    { idMoneda: idMoneda },
        //    function (data) {
        //        var cotizacion = JSON.parse(data);
        //        $("#Cotizacion").empty();
        //        $('#Cotizacion').val(cotizacion.Importe);
        //        $("#Cotizacion_Monto").empty();
        //        $('#Cotizacion_Monto').val(cotizacion.Importe);
        //    });   
});

$("#Cotizacion").change(function () {
    //if ( != NaN) {
    var cotiza = parseFloat($("#Cotizacion").val());
    if (isNaN(cotiza)) {
        toastr.warning("Ingrese una Cotización Valida: " + cotiza.toFixed(2));      
    } else {
        $("#Cotizacion_Monto").empty();
        $('#Cotizacion_Monto').val(cotiza.toFixed(2));
        $("#Cotizacion").empty();
        $('#Cotizacion').val(cotiza.toFixed(2));
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


$('.Calendario').datepicker({
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
    $('#CompraIva_PercepcionImporteIva').val(calculoporcentaje($("#CompraIva_PercepcionIva").val(), $('#CompraIva_NetoGravado').val()));
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



