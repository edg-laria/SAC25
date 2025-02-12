//import { Console } from "console";

var importeChequePropio = 0;
var importeChequeTercero = 0;


function fnAsignarChequeCaja() {

    var total = 0;
    total = importeChequeTercero;
    $("#montoChequesSeleccionados").val(total.toFixed(2));
    $("#Importe").val(total.toFixed(2));
    $("#ModalCheques").modal('hide');
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



function ConfirmaEliminar(valor) {
    event.preventDefault();
    document.getElementById("IdCheque").value = valor;
    $("#ModalConfirmaEliminar").modal('show');
}

function onCompleteQuitarCheque(result) {
    importeChequePropio = 0;

    $("#TotalChequeSeleccionados").html(parseFloat(importeChequeTercero + importeChequePropio));
    $("#ModalConfirmaEliminar").modal("hide");

}

function onSuccess(data, status, xhr) {
    if (data.Success == false || data.Success != undefined) {
        toastr.info(data.Message);
    }
}

function onFailure(xhr, status, error) {
    console.log("error modal cheque");
    //response = xhr.responseJSON;
    //window.location.href = response.RedirectUrl;
}

function onCompleteIngresarCheque(xhr, status) {
    $("#TotalChequeSeleccionados").html(importeChequeTercero);
}


$(function () {



    $(document).on('show.bs.modal', '.modal', function (event) {
        var zIndex = 1040 + (10 * $('.modal:visible').length);
        $(this).css('z-index', zIndex);
        setTimeout(function () {
            $('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
        }, 0);
    });


    // conciliacion de movimiento
    ///visualiza btn
    $(".checkConciliacion").on("click", function () {
        $("#btnConfirmaConciliacion").removeClass("show");
        $("#btnConfirmaConciliacion").addClass("hide");

        var i = 0;     
        $('.checkConciliacionMovimiento:checked').each(function (index) {
            $("#btnConfirmaConciliacion").removeClass("hide");
            $("#btnConfirmaConciliacion").addClass("show");   
            if (i > 0) {
                $("#IdConciliacionMovimiento").val($("#IdConciliacionMovimiento").val() + ';' + $(this)[0].id);
            }
            else {
                $("#IdConciliacionMovimiento").val("");
                $("#IdConciliacionMovimiento").val($(this)[0].id);
            }
            i++;
        });
    });



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
        $("#TotalChequeSeleccionados").html(totalTercero);

        if (totalTercero == 0) {
            $("#btnAsignarCheque").removeClass("show");
            $("#btnAsignarCheque").addClass("hide");
        } else {
            $("#btnAsignarCheque").removeClass("hide");
            $("#btnAsignarCheque").addClass("show");
        }

    });



    $('.tabTerceros').on('click', function (event) {
        event.preventDefault();
        fntablaChequesTerceros();
    });

    $('#ModalCheques').on('shown.bs.modal', function () {
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
                    toastr.error(result.data);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                toastr.error(' Ops!, A ocurriodo un error. Contacte al Administrador ');
            }

        });

    });



});



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



