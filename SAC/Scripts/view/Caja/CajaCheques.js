//import { Console } from "console";

var importeChequePropio = 0;
var importeChequeTercero = 0;


function fnAsignarChequeCaja() {

    var total = 0;
    total = importeChequePropio + importeChequeTercero;

    $("#montoChequesSeleccionados").val(total.toFixed(2));
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
    $("#idChequesPropios").val("");
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
    // set idChequesPropios
    $("#TblChequesPropios tbody tr").each(function (index) {

        $("#idChequesPropios").val(parseFloat($(this)[0].attributes[0].nodeValue));
        importeChequePropio = parseFloat($(this)[0].attributes[1].nodeValue);
    })

    $("#TotalChequeSeleccionados").html(importeChequeTercero + importeChequePropio);
}




$(function () {

    

    $(document).on('show.bs.modal', '.modal', function (event) {
        var zIndex = 1040 + (10 * $('.modal:visible').length);
        $(this).css('z-index', zIndex);
        setTimeout(function () {
            $('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
        }, 0);
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
        $("#TotalChequeSeleccionados").html(totalTercero + importeChequePropio);
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
                    toastr.error(result.data);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                toastr.error(' Ops!, A ocurriodo un error. Contacte al Administrador ');
            }

        });

    });



});


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



