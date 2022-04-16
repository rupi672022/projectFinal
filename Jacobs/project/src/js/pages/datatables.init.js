
/**
* Theme:  Moltran - Responsive Bootstrap 4 Admin & Dashboard
* Author: Coderthemes
* File:   Datatable
*/

$(document).ready(function() {

    // Default Datatable
    $('#datatable').DataTable();

    //Buttons examples
    var table = $('#datatable-buttons').DataTable({
        lengthChange: false,
        buttons: ['copy', 'csv', 'excel', 'pdf', 'print'],
        buttons: [
            { extend: 'copy', className: 'btn-sm' },
            { extend: 'csv', className: 'btn-sm' },
            { extend: 'excel', className: 'btn-sm' },
            { extend: 'pdf', className: 'btn-sm' },
            { extend: 'print', className: 'btn-sm' }
        ]
    });

    // fixed-header

    $('#fixed-header-datatable').DataTable({
        fixedHeader: true
    });

    // Key Tables

    $('#keytable-datatable').DataTable({
        keys: true
    });

    // Responsive Datatable
    $('#responsive-datatable').DataTable();

    // scroller Datatable
    $('#scroller-datatable').DataTable({
        ajax: "../assets/data/scroller-demo.json", 
        deferRender: true, 
        scrollY: 380, 
        scrollCollapse: true, 
        scroller: true
    });

    table.buttons().container()
            .appendTo('#datatable-buttons_wrapper .col-md-6:eq(0)');
} );