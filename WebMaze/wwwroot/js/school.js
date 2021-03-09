$(document).ready(function () {
    var formInput = $('input[type=submit]');
    formInput.attr('disabled', 'disabled');

    if (document.getElementById('Login')){
        $('#Login').keyup(function () {
            validateForm();
        });
    }

    if (document.getElementById('Password')) {
        $('#Password').keyup(function () {
            validateForm();
        });
    }

    if (document.getElementById('PasswordConfirm')) {
        $('#PasswordConfirm').keyup(function () {
            validateForm();
        });
    }

    function validateForm() {
        var everythingCorrect = true;
        if (document.getElementById('Login')) {
            everythingCorrect &&= $('#Login').val().length > 0;
        }
        if (document.getElementById('Password')) {
            var passForm = $('#Password').val().length;
            everythingCorrect &&= passForm > 0;
            if (document.getElementById('PasswordConfirm')) {
                everythingCorrect &&= $('#PasswordConfirm').val().length > 0;
            }
        }
        if (everythingCorrect) {
            formInput.removeAttr('disabled');
        } else {
            formInput.attr('disabled', 'disabled');
        }
    }
});