$(function () {


    $('#Nombre').autocomplete({
        source: function (request, response) {
            $.getJSON("/Banco/GetListClienteJson/", request, function (data) {
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
            // getProvedor(ui.item.id)
            $("#IdCliente").val(ui.item.id);


            this.value = ui.item.value;
            return false;
        }

    });

});