$(document).ready(function () {

    var script = {
        language: {
            "sProcessing": "Procesando su consulta…",
            "sLengthMenu": "Mostrar _MENU_ registros",
            "sZeroRecords": "No se encontraron resultados",
            "sEmptyTable": "No hay servicios para mostrar",
            "sInfo": "Mostrando del _START_ al _END_ de un total de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0",
            "sInfoFiltered": "(filtrado de un total de _MAX_ registros) ",
            "sInfoPostFix": "",
            "sSearch": "Buscar: ",
            "sUrl": "",
            "sInfoThousands": ", ",
            "sLoadingRecords": "Cargando…",
            "oPaginate": {
                "sFirst": "Primera",
                "sLast": "Última",
                "sNext": "Siguiente",
                "sPrevious": "Anterior"
            },
            "oAria": {
                "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
            }
        }
    };


    $('#contenedor2_tblNiveles').DataTable({
        "paging": false,
        "ordering": false,
        "searching": false,
        "info": false,
        //rowReorder: {
        //    selector: 'tr'
        //},
    });

    $('#contenedor2_tblPlanAccion').DataTable({
        "paging": false,
        "ordering": true,
        "searching": false,
        "info": false
    });

    $('#contenedor2_tblNivParam').DataTable({
        "paging": false,
        "ordering": false,
        "searching": false,
        "info": false
    });

    $('#contenedor2_tblPlanAccParam').DataTable({
        "paging": false,
        "ordering": false,
        "searching": false,
        "info": false
    });

    $('#contenedor2_tblMetas').DataTable({
        "paging": false,
        "ordering": false,
        "searching": false,
        "info": false
    });

    $('#contenedor2_tblArchivos').DataTable({
        "paging": false,
        "ordering": false,
        "searching": false,
        "info": false
    });

    $('#contenedor2_tblImagenes').DataTable({
        "paging": false,
        "ordering": false,
        "searching": false,
        "info": false
    });

    $('#contenedor2_tblPac').DataTable(script);
    //$('#contenedor2_tblPlanAccion').DataTable(script);

    
});