

    
$(function(){
$('#showtoast').click(function () {
        toastr.options = {
        "debug": false,
        "onclick": null,
        "fadeIn": 300,
        "fadeOut": 100,
        "timeOut": 3000,
        "extendedTimeOut": 1000
        }
        
        var d= "MA-PSI-0001-2019";
    toastr["success"](d,"Oficio agregado exitosamente");  
});
});