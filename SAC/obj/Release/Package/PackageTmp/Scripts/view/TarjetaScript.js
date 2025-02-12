$(function () {

    $(".checkTarjeta").on("click", function () {
        var totalSeleccionado = 0;
        
            $("#btnConciliarTarjeta").removeClass("show");
            $("#btnConciliarTarjeta").addClass("hide");
            var i = 0;
            $('.checkTarjetaConciliacion:checked').each(function (index) {

                totalSeleccionado += parseFloat($(this)[0].value);

                $("#btnConciliarTarjeta").removeClass("hide");
                $("#btnConciliarTarjeta").addClass("show");
          
                if (i > 0) {
                    $("#IdTarjetaConciliar").val($("#IdTarjetaConciliar").val() + ';' + $(this)[0].id);
                }
                else {
                    $("#IdTarjetaConciliar").val("");
                    $("#IdTarjetaConciliar").val($(this)[0].id);
                }

                $("#MontoSeleccionado").val(totalSeleccionado);
             
                i++;
      
            });
        });
        if ($.fn.dataTable.isDataTable('#tblOperacionesTarjeta')) {
            table = $('#tblOperacionesTarjeta').DataTable();
            table.destroy();
        }
        $('#tblOperacionesTarjeta').DataTable({
            "language": { "url": "../Content/assets/plugins/datatables/es.txt" },
            "order": [0, 'desc'],
            'paging': false,
            'lengthChange': false,
            'searching': true,
            'ordering': false,
            'info': false,
            'autoWidth': true,
            'scrollY': '230px',
            'scrollCollapse': true,
        });

});


    //maneja los check
    //$(".check").on("click", function () {
    //    var MontoSeleccionado = $("#MontoSeleccionado").val().replace(",", ".");
    //    var idTarjetaConciliar = $("#idTarjetaConciliar").val();
    //    var total = 0;
    //    var valor = 0;
    //    $("#MontoSeleccionado").val("");
    //    $("#idTarjetaConciliar").val("");
    //    if ($(this).is(":checked")) {
    //        var valorSincoma = this.value.replace(",", ".");
    //        valor = parseFloat(valorSincoma);

    //        if ((MontoSeleccionado != "") || (isNaN(MontoSeleccionado))) {
    //            total = parseFloat(MontoSeleccionado);
    //            total += valor;
    //            $("#MontoSeleccionado").val(total.toFixed(2));
    //        } else {
    //            $("#MontoSeleccionado").val(valor.toFixed(2));
    //        }
    //        if (idTarjetaConciliar != "" || isNaN(idTarjetaConciliar)) {
    //            idTarjetaConciliar += ";" + this.id;
    //        } else {
    //            idTarjetaConciliar = this.id;
    //        }
    //        $("#idTarjetaConciliar").val(idTarjetaConciliar);
    //    }

    //});



    //$('input[type="checkbox"]').change(function () {
    //    ////debugger;
    //    //var deuda;
    //    //var total; // $('#total');
    //    //total = $('#total').val();
    //    //total = total.replace("$ ", "");
    //    //if (isNaN(total)) {
    //    //    total = 0.00;
    //    //}
    //    if ($(this).is(':checked')) {
    //        var tr = $(this).closest('tr');
    //        deuda = parseFloat($(tr).find('td:nth-child(5)').text());
    //        //total = parseFloat(total) + deuda;
    //    } else {
    //        var tr = $(this).closest('tr');
    //        deuda = parseFloat($(tr).find('td:nth-child(5)').text());
    //        //total = parseFloat(total) - deuda;
    //    }
    //    $('#MontoSeleccionado').val(total);
    //})


