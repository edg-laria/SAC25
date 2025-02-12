
$('#searchFecha').datepicker({
    format: "dd/mm/yyyy",
    language: 'es',
    autoclose: true,
    todayHighlight: true
}).datepicker('setDate', new Date()); 

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
        //getProvedor(ui.item.id)
        $("#searchIdProveedor").val(ui.item.id);        
        this.value = ui.item.value;
        return false;
    }

});


function getProvedor(prov) {

    $.getJSON("/Compras/GetProveedorJson",
        { idProveedor: prov },
        function (data) {
            var proveedor = JSON.parse(data);
            $('#RazonSocialProv').html(proveedor.Nombre);
            $('#TipoIvaProv').html(proveedor.TipoIva.Descripcion);
            $('#CuitProv').html(proveedor.Cuit);
            $('#DireccionProv').html(proveedor.Direccion);
            if ($("#DatosProveedor").hasClass("hide")) {
                $('#DatosProveedor').removeClass("hide");
                $('#DatosProveedor').addClass("show");
            }
            
 
        });

}


$('#BtnBuscarInforme').click(function () {
    buscarInforme();
});

function buscarInforme() {   
    var filtros = new Object    
    filtros.IdProveedor = $('#searchIdProveedor').val() || 0;;
    filtros.fechaHasta  = $('#searchFechaDesde').val();
       
    $.ajax({
        type: 'get',
        url:  '/CuentaCteProveedor/CtaCtedetalleDetalle/',
        data: {
            IdProveedor: filtros.IdProveedor,
            fechaDesde: filtros.fechaDesde
        },
        cache: false,
        dataType: 'json',
        success: function (r) {
            if (r.data != "null" && r.result) {
                bindInforme(r.data, 'tblInforme');
            } else {
                $('.clock-pacientes').addClass('hide');
                toastr.error('No se encontraron datos...')
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            toastr.error('Error - ' + errorThrown);
        }
    });

}

function bindInforme(datosJson, oTable) {
    datosJson = JSON.parse(datosJson);

    var th_ini = "";
    var tr_ini = "";
    var thjson = "";

    if ((typeof (datosJson) !== 'undefined') && (datosJson.length > 0)) {

        // CABECERA DE TABLA
        for (h in datosJson[0]) { thjson += "<th> " + h + " </th>"; }
        th_ini += "<tr>" + thjson + "</tr>";
        var valor = 0;
        // CUERPO DE TABLA
        for (x in datosJson) {
            tr_ini += "<tr>";
            for (y in datosJson[x]) {
                valor = datosJson[x][y];
                tr_ini += "<td> " + valor + " </td>";
            }
            tr_ini += " </tr>";
        }


        if ($.fn.dataTable.isDataTable('#' + oTable)) {
            table = $('#' + oTable).DataTable();
            table.destroy();
            $('#' + oTable + ' thead tr').remove();
            $('#' + oTable + ' thead').append(th_ini);
            $('#' + oTable + ' tbody tr').remove();
            $('#' + oTable + ' tbody').append(tr_ini);
            tablaAd(oTable);
        } else {
            $('#' + oTable + ' thead').append(th_ini);
            $('#' + oTable + ' tbody').append(tr_ini);
            tablaAd(oTable);
        }

        var valores = '';
        $('#' + oTable + ' thead tr').each(function (index) {
            $(this).children("th").each(function (index2) {
                switch (index2) {
                    case 0: valores = $(this).text();
                        break;
                    default:
                        valores = valores + ',' + $(this).text();
                }
            })
        })
        $("#infomeColum").val('' + valores + '');

    } else {
        toastr.warning('No se encontraron datos...');
    }
};

function tablaAd(oTable) {

    var table = $('#' + oTable).dataTable({
        dom: 'Bfrtip',
        buttons: [          
            //-------margin: [left, top, right, bottom]//https://www.javascripting.com/view/pdfmake
            {
                extend: 'pdf',
                orientation: 'landscape',
                pageSize: 'legal',
                title: 'Cuenta Corriente de Proveedor Detalles',
                download: 'download',
                customize: function (doc) {
                    //doc.pageMargins = [10, 10, 10, 10];  
                    // margenes de contenido en hoja                   
                    doc['header'] = [
                        {
                            columns: [
                                { text: '', width: 'auto', bold: true, fontSize: 12, alignment: 'left', margin: [20, 10, 0, 0] },                              
                            ]
                        },                        
                    ];
                    var cols = [];
                    cols[0] = { text: ' ', alignment: 'left', margin: [20] };
                    var f = moment(new Date()).format("DD/MM/YYYY HH:mm:ss");
                    cols[1] = { text: 'Impreso: ' + f, alignment: 'right', margin: [0, 0, 20] };
                    var objFooter = {};
                    objFooter['columns'] = cols;
                    doc['footer'] = objFooter;

                },

                exportOptions: {
                    columns: ':visible'
                }


            },
            ],
        scrollX: true,
        "language": { "url": "../Content/assets/plugins/datatables/es.txt" },
        "initComplete": function (settings, json) {

        }
       });
}
//-------------------------------------------------------

var valores = '';
$('#tblInforme thead tr').each(function (index) {
    $(this).children("th").each(function (index2) {
        switch (index2) {
            case 0: valores = $(this).text();
                break;
            default:
                valores = valores + ',' + $(this).text();
        }
    })
})
$("#infomeColum").val('' + valores + '');

//*------------------------------------------------------
function insertToTable(oJson, oTable) {

    $('#' + oTable).DataTable({
        autoWidth: false,
        searching: false,
        bLengthChange: false,
        bInfo: false,
        bPaginate: false,
        bSort: false,
        language: {
            emptyTable: "No hay datos para mostrar."
        },
        columns: [
            { title: 'Nombre', data: 'nombre' },
            { title: 'Apellido', data: 'apellido' },
            { title: 'DNI', data: 'dni' }
        ],
    });
    $('#' + oTable).DataTable().row.add({
        "nombre": oJson.nombre,
        "apellido": oJson.apellido,
        "dni": oJson.documento

    }).draw();
}


$(document).ready(function () {

    $('#tblCtaCteProveedorDetalles').DataTable({
        "language": { "url": "../Content/assets/plugins/datatables/es.txt" },
        'paging': false,
        'lengthChange': false,
        'searching': true,
        'ordering': false,
        'info': false,
        'autoWidth': true,
        'scrollY': '350px',
        'scrollCollapse': true,
        'dom': 'Bfrtip',
        'buttons': [
            'pdf', 'excel'
        ]
    });
});


//todo esto es para validar el ingreso de fechas
//la segunda fecha se carga y valida en relacion a la primer fecha

//var getDate = function (input) {
//    return new Date(input.date.valueOf());
//}

//$('#inicio, #fin').datepicker({
//    format: "dd/mm/yyyy",
//    language: 'es',
//    autoclose: true,
//    todayHighlight: true
//});

//$('#fin').datepicker({
//    startDate: '+6d',
//    endDate: '+36d',
//});

//$('#inicio').datepicker({
//    startDate: '+5d',
//    endDate: '+35d',
//}).on('changeDate',
//    function (selected) {
//        $('#fin').datepicker('clearDates');
//        $('#fin').datepicker('setStartDate', getDate(selected));
//    });




