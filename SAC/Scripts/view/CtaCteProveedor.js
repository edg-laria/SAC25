//PagarFactura
//
var importeChequePropio = 0;
var importeChequeTercero = 0;


function fnAsignarChequeCaja() {

    var total = 0;
    total = importeChequePropio + importeChequeTercero;
    $("#montoChequesSeleccionados").val(total.toFixed(2));
    $("#ModalCheques").modal('hide');
    //fnCalcularDirefencia();

    //agregue bre 29/01/2021
    //SumarTotalPago();
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

function fnVerModalCheques() {

    $("#ImporteAPagarCheque").html($("#TotalAPagar").val());    
    $("#ModalCheques").modal('show');
}

function fnAsignarCheque() {

    var total = 0;
    total = importeChequePropio + importeChequeTercero;
    $("#montoChequesSeleccionados").val(total.toFixed(2));
    $("#ModalCheques").modal('hide');
    fnCalcularDirefencia();

    //agregue bre 29/01/2021
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

function fnVerModalTarjetas() {
    $.ajax({
        type: "POST",
        url: "/CuentaCteProveedor/CargarTarjetas",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        beforeSend: function () {
        }, complete: function () {
        },
        success: function (response) {
            $('#Resultados2').html(response);
            $("#ModalTarjetas").modal('show');
            $("#montoTarjetaSeleccionados").val('');
        },
        failure: function (response) {
            Console.log(response.responseText);
        },
        error: function (response) {
            Console.log(response.responseText);
        }
    });
}

function fnAsignarTarjeta() {
    $("#idTarjeta").val($("#idTarjetasdrop").val());
    $("#montoTarjetaSeleccionados").val(parseFloat($("#MontoTarjeta").val()).toFixed(2));
    $("#ModalTarjetas").modal('hide');

    //console.log($("#idTarjeta").val());

    fnCalcularDirefencia()
}

function fnCalcularDirefencia() {
    var TotalaPagar = isNaN($("#TotalAPagar").val()) ? 0 : parseFloat($("#TotalAPagar").val());
    var Efectivo = isNaN(parseFloat($("#efectivo").val())) ? 0 : parseFloat($("#efectivo").val());
    var Cheque = isNaN(parseFloat($("#montoChequesSeleccionados").val())) ? 0 : parseFloat($("#montoChequesSeleccionados").val());
    var tranferencia = isNaN(parseFloat($("#montoTranferenciaSeleccionados").val())) ? 0 : parseFloat($("#montoTranferenciaSeleccionados").val());
    var tarjeta = isNaN(parseFloat($("#montoTarjetaSeleccionados").val())) ? 0 : parseFloat($("#montoTarjetaSeleccionados").val());

    var resultado = TotalaPagar - (Efectivo + Cheque + tranferencia + tarjeta);

    $("#Diferencia").val(parseFloat(resultado));

}

$("#efectivo").keyup(function () {
    fnCalcularDirefencia()
});

function fnVerModalTranferencia() {
    $("#montoTranferenciaSeleccionados").val("");
    $("#idCuentasBancarias").val("");

    $.ajax({
        type: "POST",
        url: "/CuentaCteProveedor/CargarCuentas",
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        beforeSend: function () {
        }, complete: function () {
        },
        success: function (response) {
            $('#Resultados1').html(response);
            $("#ModalTranferencia").modal('show');
        },
        failure: function (response) {
            Console.log(response.responseText);
        },
        error: function (response) {
            Console.log(response.responseText);
        }
    });
}

function fnAsignarCuentaBancaria() {
    $("#montoTranferencia").val(parseFloat($("#MontoCuenta").val()).toFixed(2));
    $("#idCuentasBancarias").val($("#idCuentaBancaria").val());
    $("#ModalTranferencia").modal('hide');
    fnCalcularDirefencia();
}

//maneja los check
$(".check").on("click", function () {


    $("#btnSelCheque").removeAttr("disabled");
    $("#btnSelTransferencia").removeAttr("disabled");
    $("#btnSelTarjeta").removeAttr("disabled");


    var total = 0;
    var valortotalSincoma = $("#TotalAPagar").val().replace(",", ".");
    var idFacturas = $("#idFacturas").val();
    if (idFacturas.length == 1) {
        $("#idFacturas").val("");
    }
    var valor = 0;

    if ($(this).is(":checked")) {
        var valorSincoma = "";
        valorSincoma = this.value.replace(",", ".");
        valor = parseFloat(valorSincoma);

        if ((valortotalSincoma != "")) {

            if (isNaN(valortotalSincoma)) {
                valortotalSincoma = 0;
            }
            total = parseFloat(valortotalSincoma);
            total = total + valor;
            $("#TotalAPagar").val(total.toFixed(2));
            // $("#TotalAPagar").val(total);
            if (valortotalSincoma == 0) {
                idFacturas = this.id;
            }
            else {
                idFacturas = idFacturas + ";" + this.id;
            }
        }
        else {
            $("#TotalAPagar").val(valor.toFixed(2));
            //$("#TotalAPagar").val(valor);
            idFacturas = this.id;
        }

        //agrego de la lista de facturas a pagar el registro seleccionado
        $("#idFacturas").val(idFacturas);
    }
    else {
        valorSincoma = this.value.replace(",", ".");
        valor = parseFloat(valorSincoma);
        var arr = idFacturas;
        var arrayDeCadenas = arr.split(";");

        if (valortotalSincoma != "0") {
            total = parseFloat(valortotalSincoma);
            total -= valor;
            if (isNaN(total) || total == 0) {
                $("#TotalAPagar").val("0");
                //console.log("0", arrayDeCadenas);
                removeItemFromArr(arrayDeCadenas, this.id);
                $("#btnSelCheque").attr('disabled', 'disabled');
                $("#btnSelTransferencia").attr('disabled', 'disabled');
                $("#btnSelTarjeta").attr('disabled', 'disabled');

            }
            else {
                $("#TotalAPagar").val(total.toFixed(2));

                //console.log("1", arrayDeCadenas);
                removeItemFromArr(arrayDeCadenas, this.id);
            }
            $("#idFacturas").val(arrayDeCadenas.join(';'));
        }
        else {
            $("#TotalAPagar").val(this.value);
        }
        //quito de la lista de facturas a pagar el registro eliminado
        //$.get("/CuentaCteProveedor/QuitarFacturaAPagar/?idFactura=" + this.id, function (data) {
        //    if (data == "0") {
        //        alert("Error, algo no salio bien");
        //    }
        //});
    }

    // console.log($("#idFacturas").val());
});

//elimina elemento de array por elemento
function removeItemFromArr(arr, item) {
    var i = arr.indexOf(item);
    arr.splice(i, 1);
}

function fntablaChequesPropios() {
    if ($.fn.dataTable.isDataTable('#TblChequesPropios')) {
        table = $('#TblChequesPropios').DataTable();
        table.destroy();
    }

    $('#TblChequesPropios').DataTable({
        "language": { "url": "../Content/assets/plugins/datatables/es.txt" },
        "order": [[4, 'asc']],
        'paging': false,
        'lengthChange': false,
        'searching': false,
        'ordering': false,
        'info': false,
        'autoWidth': true,
        'scrollY': '230px',
        'scrollCollapse': true
    });
}

function fntablaChequesTerceros() {
    if ($.fn.dataTable.isDataTable('#TblChequesTerceros')) {
        table = $('#TblChequesTerceros').DataTable();
        table.destroy();
    }
    $('#TblChequesTerceros').DataTable({
        "language": { "url": "../Content/assets/plugins/datatables/es.txt" },
        "order": [[4, 'asc']],
        'paging': false,
        'lengthChange': false,
        'searching': true,
        'ordering': false,
        'info': false,
        'autoWidth': true,
        'scrollY': '230px',
        'scrollCollapse': true
    });

}



///* ---------quitar cheque-------- *///


function ConfirmaEliminar(valor) {
    event.preventDefault();
    document.getElementById("IdCheque").value = valor;
    $("#ModalConfirmaEliminar").modal('show');
}

function onCompleteQuitarCheque(result) {
    importeChequePropio = 0;
    $("#idChequesPropios").val("");
    $("#TotalChequeSeleccionados").html(parseFloat(importeChequeTercero + importeChequePropio));
    $("#ModalConfirmaEliminar").modal("hide");
 
}

function onSuccess(data, status, xhr) {

    //$("#ajaxFormContainer").html(data.View);

   
    if (data.Success == false || data.Success != undefined) {

        toastr.info(data.Message);

    }

}

function onFailure(xhr, status, error) {
    response = xhr.responseJSON;
    window.location.href = response.RedirectUrl;
}
function onCompleteIngresarCheque(xhr, status) {

    $("#TblChequesPropios tbody tr").each(function (index) {
        $("#idChequesPropios").val(parseFloat($(this)[0].attributes[0].nodeValue));
        importeChequePropio = parseFloat($(this)[0].attributes[1].nodeValue);
    })

    $("#TotalChequeSeleccionados").html(importeChequeTercero + importeChequePropio);
}





//* apilar model */
$(document).ready(function () {

    $(document).on('show.bs.modal', '.modal', function (event) {
        var zIndex = 1040 + (10 * $('.modal:visible').length);
        $(this).css('z-index', zIndex);
        setTimeout(function () {
            $('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
        }, 0);
    });

});

$(function () {


    ///visualiza importes de cheques seleccionados
    $(".checkCheques").on("click", function () {
        var totalTercero = 0;
        //cheques terceros para vista de monto seleccionado
        $('.checkChequeTercero:checked').each(function () {
            valor = parseFloat($(this)[0].value);
            if (totalTercero == 0) {

                totalTercero = valor
            }
            else {
                totalTercero += valor;
            }
        });
        importeChequeTercero = totalTercero;
        $("#TotalChequeSeleccionados").html(totalTercero + importeChequePropio);
    });

    $('#TblFacturas').DataTable({
        "language": { "url": "../Content/assets/plugins/datatables/es.txt" },
        "order": [[5, 'asc']],
        'paging': false,
        'lengthChange': false,
        'searching': true,
        'ordering': false,
        'info': false,
        'autoWidth': true,
        'scrollY': '230px',
        'scrollCollapse': true
    });

    $('.tabPropios').on('click', function (event) {
        event.preventDefault();
        fntablaChequesPropios();
    });

    $('.tabTerceros').on('click', function (event) {
        event.preventDefault();
        fntablaChequesTerceros();
    });

    $('#ModalCheques').on('shown.bs.modal', function () {
        fntablaChequesPropios();
        fntablaChequesTerceros();
    });


    $('.Calendario').datepicker({
        format: "dd/mm/yyyy",
        language: 'es',
        autoclose: true,
        todayHighlight: true
    }).datepicker('setDate', new Date()); 

    //obtener nro cheque
    $("#idCuentaBancariaSeleccionada").change(function () {
        $.ajax({
            type: "Get",
            url: "/CuentaCteProveedor/GetNroChequePorCta/",
            data: { Id: $("#idCuentaBancariaSeleccionada").val() },
            success: function (result) {
                if (result.data != "null" && result.result) {
                    $("#NumeroCheque").val(parseInt(result.data + 1));
                } else {
                    toastr.error('result.data');
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                toastr.error(' Ops!, A ocurriodo un error. Contacte al Administrador ');
            }

        });

    });
});




