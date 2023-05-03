// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Doctor
$(document).ready(function () {
    $('.datepicker').datepicker({
        format: 'dd/mm/yyyy',
        language: 'pt-BR',
        autoclose: true,
        todayHighlight: true
    });

    $('.crm').mask('0000000');
    $('.phone').mask('(00) 00000-0000');
    $('.email').mask('A', {
        translation: {
            'A': {pattern: "/[\w@@\-.+]/", recursive: true}
        }
    });
});

// Appointment
$(document).ready(function () {
    $('.datepicker').datepicker({
        format: 'dd/mm/yyyy',
        language: 'pt-BR',
        autoclose: true,
        todayHighlight: true
    });

    $('.invoice').maskMoney({
        prefix: 'R$ ',
        allowNegative: false,
        thousands: '.',
        decimal: ',',
        affixesStay: false
    });
});

// Patient
$(document).ready(function () {
    $('.datepicker').datepicker({
        format: 'dd/mm/yyyy',
        language: 'pt-BR',
        autoclose: true,
        todayHighlight: true
    });

    $('.cpf').mask('000.000.000-00', { reverse: true });
    $('.phone').mask('(00) 00000-0000');
    $('.email').mask('A', {
        translation: {
            'A': { pattern: "/[\w@@\-.+]/", recursive: true }
        }
    });
});