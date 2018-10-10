(function ($) {
    "use strict";

    $('body').scrollspy({
        target: '.navbar-fixed-top',
        offset: 60
    });

    $('#topNav').affix({
        offset: {
            top: 200
        }
    });

    new WOW().init();

    $('a.page-scroll').bind('click', function (event) {
        var $ele = $(this);
        $('html, body').stop().animate({
            scrollTop: ($($ele.attr('href')).offset().top - 60)
        }, 1450, 'easeInOutExpo');
        event.preventDefault();
    });

    $('.navbar-collapse ul li a').click(function () {
        /* always close responsive nav after click */
        $('.navbar-toggle:visible').click();
    });

    $('#galleryModal').on('show.bs.modal', function (e) {
        $('#galleryImage').attr("src", $(e.relatedTarget).data("src"));
    });

})(jQuery);

function scrollDownFunction() {

    var elem = document.getElementById('scrollButtonLink');
    var img = document.getElementById('scrollButtonImg');

    // console.log(elem.href);
    // console.log(elem.href.toString());
    // console.log(elem.toString().indexOf('#first'));
    if (elem.href.toString().indexOf('#first') >= 0)
        elem.href = elem.href.toString().replace('#first', '#one');
    else if (elem.href.toString().indexOf('#one') >= 0)
        elem.href = elem.href.toString().replace('#one', '#two');
    else if (elem.href.toString().indexOf('#two') >= 0) {
        elem.href = elem.href.toString().replace('#two', '#three');
        img.src = './css/imgs/scroll_up_ic.png';
    }
    else if (elem.href.toString().indexOf('#three') >= 0) {
        elem.href = elem.href.toString().replace('#three', '#first');
        img.src = './css/imgs/scroll_down_ic.png';
    }

    console.log(elem.href);
}

// Carousel Auto-Cycle
$(document).ready(function () {
    $('.carousel').carousel({
        interval: 60000
    })
});


// Modal

// Get the modal
var modal = document.getElementById('myModal');

// Get the button that opens the modal
var btn = document.getElementById("myBtn");

// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

// When the user clicks on the button, open the modal 
btn.onclick = function () {
    modal.style.display = "block";
}

// When the user clicks on <span> (x), close the modal
span.onclick = function () {
    modal.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}

var video = document.getElementById('video-background');
video.addEventListener('click', function () {
    video.play();
}, false);

$(document).on('ready', function () {
    $(".slider").slick({
        dots: false,
        infinite: true,
        slidesToShow: 1,
        slidesToScroll: 1,
        adaptiveHeight: true,
        centerMode: false,
          variableWidth: false,
          focusOnSelect: true
    });

});

