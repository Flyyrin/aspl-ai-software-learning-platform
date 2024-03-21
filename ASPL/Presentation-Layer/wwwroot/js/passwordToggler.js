$(document).ready(function () {
    $('.input-group-text, .passwordToggler').mousedown(function (e) {
        e.preventDefault();
        var input = $(this).closest('.input-group').find('.input-group-input');
        input.focus();
    });
    $('.passwordToggler').click(function (e) {
        e.preventDefault();
        var icon = $(".passwordToggler");
        icon.toggleClass('bi-eye-fill bi-eye-slash-fill');
        var input = $('.input-group-input');
        if (input.attr('type') === 'password') {
            input.attr('type', 'text');
        } else {
            input.attr('type', 'password');
        }
    });
});