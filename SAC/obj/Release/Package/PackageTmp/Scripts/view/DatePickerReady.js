jQuery('.fechadatepicker').datepicker({
    language: 'es',
    autoclose: true,
   // format: 'dd/mm/yyyy',
    todayHighlight: true

}).datepicker();


$('.Calendario').datepicker({
    format: "dd/mm/yyyy",
    language: 'es',
    autoclose: true,
    todayHighlight: true
}).datepicker('setDate', new Date()); 
//'setDate', new Date()
