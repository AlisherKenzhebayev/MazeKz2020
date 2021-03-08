$(document).ready(function () {
    $('input[type=submit]').attr('disabled', 'disabled');

    $('#Login').keyup(function () {
        validateForm();
    });

    $('#Password').keyup(function () {
        validateForm();
    });

    function validateForm() {
        var loginCorrect = $('#Login').val().length > 0;
        var passwordCorrect = $('#Password').val().length > 0;
        if (loginCorrect && passwordCorrect) {
            $('input[type=submit]').removeAttr('disabled');
        } else {
            $('input[type=submit]').attr('disabled', 'disabled');
        }
    }
});