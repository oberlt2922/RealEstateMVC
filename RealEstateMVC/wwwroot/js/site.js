// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var slideIndex = 0;
showSlides(slideIndex);

//next/previous buttons
function plusSlides(i) {
    showSlides(slideIndex += i);
}

function showSlides(i) {
    var x;
    var slides = document.getElementsByClassName("mySlides");
    if (i < 0) {
        slideIndex = slides.length - 1;
    }
    if (i > slides.length - 1) {
        slideIndex = 0;
    }
    for (x = 0; x < slides.length; x++) {
        slides[x].style.display = "none";
    }
    slides[slideIndex].style.display = "block";
}

function deleteImage() {
    var imageId = document.getElementById("imageId");
    var slides = document.getElementsByClassName("mySlides");
    imageId.value = slides[slideIndex].id;
}
