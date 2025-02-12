
$(function ()
{
    //var TotalPesos = getNro($("#CajaSaldoInicial_ImporteFinalPesos").val()) + getNro($("#ProcesadoPesos").val());
    //var TotalDolares = getNro($("#CajaSaldoInicial_ImporteFinalDolares").val()) + getNro($("#ProcesadoDolares").val());
    //var TotalDepositos = getNro($("#CajaSaldoInicial_ImporteFinalDepositos").val()) + getNro($("#ProcesadoDepositos").val());
    //var TotalTarjetas = getNro($("#CajaSaldoInicial_ImporteFinalTarjetas").val()) + getNro($("#ProcesadoTarjetas").val());

    //$("#FinalPesos").val(TotalPesos)  ;
    //$("#FinalDolares").val(TotalDolares) ;
    //$("#FinalDepositos").val(TotalDepositos) ;
    //$("#FinalTarjetas").val(TotalTarjetas) ;

    //obtener nro cheque
    $("#IdTarjeta").change(function () {
        var $this = $("#IdTarjeta").val()
        if ($this > 0) {
            $("#ImporteTarjeta").removeAttr("disabled");
        } else {
            $("#ImporteTarjeta").attr('disabled', 'disabled');
        }             
    });

    $("#ModalCheques").on("hidden.bs.modal", function () {

        var $this = $("#IdTarjeta").val()
        if ($this > 0) {
            $("#ImporteTarjeta").removeAttr("disabled");
        } else {
            $("#ImporteTarjeta").attr('disabled', 'disabled');
        }    

    });

    $('#tblCajaCierre').DataTable({
    "language": { "url": "../Content/assets/plugins/datatables/es.txt" },
    "order": [[5, 'asc']],
    'paging': false,
    'lengthChange': false,
    'searching': false,
    'ordering': false,
    'info': false,
    'autoWidth': true,
    'scrollY': '200px',
    'scrollCollapse': true,
    'dom': 'Bfrtip',
    'buttons': []
});
   
    $('.Calendario').datepicker({
        language: 'es',
        autoclose: true,
        format: 'dd/mm/yyyy',
        todayHighlight: true
    }).datepicker('setDate', new Date());

    if ($("#IdProveedor").val() > 0) {
        getProvedor($("#IdProveedor").val());
    }

});

   //$("#birthday").datepicker({ dateFormat: "dd/mm/yyyy", 'setDate': new Date())}).mask("99/99/9999");
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



function getTotales()
{
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
  
function getNro(x)
{
    x = parseFloat(x);
    return (x == null || isNaN(x) || x == undefined || x == "") ? 0 : x;
}


//---- SCRIPT SUMA Y RESTA EN CAMPOS--------
//function fncSumar() {
//    caja = document.forms["sumar"].elements;
//    var numero1 = Number(caja["numero1"].value);
//    var numero2 = Number(caja["numero2"].value);
//    var numero3 = Number(caja["numero3"].value);
//    resultado = numero1 + numero2 - numero3;
//    if (!isNaN(resultado)) {
//        caja["resultado"].value = numero1 + numero2 - numero3;
//    }
//}
//-----SCRIPT SEPARADOR DE MILES---------

function format(input) {
    var num = input.value.replace(/\./g, '');
    if (isNaN(num)) {       
        toastr.warning('Solo se permiten numeros');
        input.value = input.value.replace(/[^\d\.]*/g, '');
    }
}



