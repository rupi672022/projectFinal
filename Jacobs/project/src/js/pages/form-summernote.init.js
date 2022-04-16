/**
* Theme:  Moltran - Responsive Bootstrap 4 Admin & Dashboard
* Author: Coderthemes
* File:   Summernote
*/

$(document).ready(function(){

    $('#summernote-editor').summernote({
        height: 250,                 // set editor height
        minHeight: null,             // set minimum height of editor
        maxHeight: null,             // set maximum height of editor
        focus: false                 // set focus to editable area after initializing summernote
    });

    $('#summernote-inline-editor').summernote({
        airMode: true
    });

});
