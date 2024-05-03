
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//
/**/
// window.addEventListener("scroll", function () {
//     const navbar = document.querySelector(".navbar");
//     const _nav = this.document.querySelector('nav');
//     const navbarHeight = navbar.clientHeight;
//     const scrollY = window.scrollY;

//     if (scrollY > navbarHeight) {
//         navbar.classList.add("navbar-fixed");
//         _nav.classList.add("glass-box");
//         _nav.classList.add("nav-float");
//         _nav.classList.remove("z-depth-0");
//         _nav.classList.add("z-depth-2");
//     } else {
//         navbar.classList.remove("navbar-fixed");
//         _nav.classList.remove("glass-box");
//         _nav.classList.remove("nav-float");
//         _nav.classList.add("z-depth-0");
//         _nav.classList.remove("z-depth-2");
//     }
// });


$(document).ready(function () {

    $('.modal').modal();

    $('.dropdown-trigger').dropdown(
        {
            hover: false,
            constrainWidth: false,
            coverTrigger: true,
            closeOnClick: true
        });

    $('select').formSelect();

    $('.collapsible').collapsible();

    $('.sidenav').sidenav();

    $('.tabs').tabs();

    $('.datepicker').datepicker();

    $('.tooltipped').tooltip();

    $('.scrollspy').scrollSpy();

    M.updateTextFields();

    var toggler = document.getElementsByClassName("caret");
    var i;

    for (i = 0; i < toggler.length; i++) {
        toggler[i].addEventListener("click", function () {
            this.parentElement.querySelector(".nested").classList.toggle("active");
            this.classList.toggle("caret-down");
        });
    }
});

function CloseDropdown() {
    $('.dropdown-trigger').dropdown(
        {
            hover: false,
            constrainWidth: false,
            coverTrigger: true,
            closeOnClick: true
        });
};

$(window).on('load', function () {
    $("#loader").hide();
    $(".blackboxload").fadeOut("slow");
});

$(function () {
    $(".submit").click(function () {
        $("#startbtn").hide();
        $(".blackboxload").show();
        $("#loader").show();
        $(this).addClass('pre-active');
    });
});

/* Enviar Form jquery */
$(document).ready(function () {
    $('#select_zone').change(function () {
        $("#startbtn").hide();
        $(".blackboxload").show();
        $("#loader").show();
        $(this).addClass('pre-active');
        $('form').submit();
    });
});

/* Redirect */
$(document).ready(function () {
    $('#select_city').change(function () {
        const value = this.value;
        // Configura a URL da página interna
        const url = '/empresas/' + value;
        // Redireciona para a página interna
        location.href = url;
        //loader
        $(".blackboxload").show();
        $("#loader").show();
    });
});


