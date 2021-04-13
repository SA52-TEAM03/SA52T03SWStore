$(function () {
    $.expr[":"].contains = function (a, i, m) {
        return jQuery(a).text().toUpperCase()
            .indexOf(m[3].toUpperCase()) >= 0;
    };
    $('#filterName').keyup(function () {
        $('table tbody tr').hide()
            .filter(":contains('" + ($(this).val()) + "')").show();
    }).keyup();
});