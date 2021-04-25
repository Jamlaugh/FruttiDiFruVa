(function ($) {

    $('.datepicker').datepicker({
        format: "dd.mm.yyyy",
        "setDate": new Date(),
        autoclose: true,
        language: 'de-DE',
        defaultViewDate: 'today'
    });
    $('.datepicker').datepicker('update', new Date());

})(window.jQuery);

function prepareModel() {
    $('.checkbox:checked').each(function () {
        var articleId = $(this).parent().parent().find("a").data("id");
        var amount = $(this).parent().parent().find("input[type=number]").val();

        $("#selected-order").val($("#selected-order").val() + articleId + ":" + amount + ",");
    });

    // remove last comma
    $("#selected-order").val($("#selected-order").val().replace(/,\s*$/, ""));
}

function checkboxChanged(checkbox) {

    var amountInput = $(checkbox).parent().parent().find("input[type=number]");

    if ($(checkbox).prop("checked")) {
        amountInput.prop("required", true);
    }
    else {
        amountInput.prop("required", false);
    }
}