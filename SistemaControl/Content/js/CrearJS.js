//var dateToday = new Date();
//$(function () {
//    $("#datepicker").datepicker({
//        minDate: 2,
//        maxDate: dateToday,
//        dateFormat: 'yy-mm-dd'
//    });
//});

//$('#numeroDocumento').keydown(function (e) {
//    debugger;
//    var key = e.key;
//    var code = e.keyCode;
//    var doc = $('#numeroDocumento').val();
//    var comTotal = /MA-PSJ-[0-9]{4}-2018|EXP-MA-[0-9]{4}$/;
//    var pat = /MA-PSJ-[0-9]{4}$/;
//    var expreg = /MA-PSJ-[0-9]{5}$/;

//    if (comTotal.test(doc)&& code !=8 && code !=13)
//    {
//        event.preventDefault();
//    }
//    else {
//        if ((code > 47 && code < 58) || /* numeric (0-9)*/ (code > 64 && code < 91) || /* upper alpha (A-Z)*/ (code > 96 && code < 105))// lower alpha (a-z)
//        {
//            if (!doc.includes('MA-PSJ-')) {

//                $('#numeroDocumento').val('MA-PSJ-');
//            }
//            else {
//                $('#numeroDocumento').val(doc);
//            }
//        }
//        else if (code == 8) {
//            var del = /2018$/;
//            if (del.test(doc))
//            {
//                doc = doc.substring(0, doc.length - 5);
//                $('#numeroDocumento').val(doc);
//            }
//            else {
//                if (doc.length < 5) {

//                    if (!doc.includes('MA-PSJ-')) {
//                        $('#numeroDocumento').val('MA-PSJ-');
//                    }
//                    else if (doc.includes('MA-PSJ-') && key == 'Backspace') {
//                        $('#numeroDocumento').val('MA-PSJ-');
//                        event.preventDefault();
//                    }
//                }
//            }
//        }
//        if (pat.test(doc)) {
//            if ((code > 47 && code < 58) || (code > 96 && code < 105)) {
//                $('#numeroDocumento').val(doc + key + '-' + (new Date()).getFullYear());
//                event.preventDefault();
//            }
//        }
//        else if (expreg.test(doc)) {
//            if (code !=8) {
//                $('#numeroDocumento').val(doc + '-' + (new Date()).getFullYear());
//                event.preventDefault();
//            }
//        }
//        }
//}
//);