$(document).ready(function () {

    let checkOfWorkActivityIds = [];

    let allActivitiesShippingTable = $('#allActivitiesShippingTable').DataTable({
        "scrollY": '55vh',
        "scrollX": true,
        "scrollCollapse": true,
        "scroller": true,
        "oLanguage": languageDataTable,
        "sAjaxSource": "/Logistics/Shipping/_LoadAllActivities",
        "sServerMethod": "POST",
        "fnServerParams": function (aoData) {
            aoData.push(
                { "name": "activitiesIdsArray", "value": activitiesIdsArray }
            );
        },
        "bServerSide": true,
        "bProcessing": true,
        "bSearchable": true,
        "language": {
            "processing":
                '<div class="text-center"> <div id="spinnerDataTable" class="spinner-border text-primary" role="status"> <span class="sr-only"></span></div></div>'
        },
        "order": [
            [2, "asc"]
        ],
        "columns": [
            {
                "data": "id",
                "searchable": false,
                "visible": false
            },
            {
                "data": "id",
                "render": function (data, type, row, meta) {

                    let html = '<div class="btn-group"><div class="d-flex animated--grow-in">';

                    // Check.
                    html += '<a class="btn action-btn"><div class="custom-control custom-checkbox small"><input class="checkSelectedRow custom-control-input" type="checkbox" value="" id="checkSelectedRow_' + meta.row + '"><label class="custom-control-label" for="checkSelectedRow_' + meta.row + '"></label></div></a>'

                    html += '</div></div>';
                    return html;
                },
                "className": "text-center",
                "visible": true,
                "width": "5%",
                "searchable": false
            },
            {
                "data": "workOrderItem.product.code",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "workOrderItem.product.description",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "workOrderItem.quantity",
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "id",
                "render": function (value) {
                    return "";
                },
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "id",
                "render": function (value) {
                    return "";
                },
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "id",
                "render": function (value) {
                    return "";
                },
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "id",
                "render": function (value) {
                    return "";
                },
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "id",
                "render": function (value) {
                    return "";
                },
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "id",
                "render": function (value) {
                    return "";
                },
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "id",
                "render": function (value) {
                    return "";
                },
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "id",
                "render": function (value) {
                    return "";
                },
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "id",
                "render": function (value) {
                    return "";
                },
                "autoWidth": true,
                "searchable": true
            },
            {
                "data": "assignedUsersIds",
                "autoWidth": true,
                "searchable": true
            },
        ],
        "initComplete": function () {
            this.api().columns().every(function () {
                var that = this;
                $('input', this.footer()).on('keyup change clear', function () {
                    if (that.search() !== this.value) {
                        that
                            .search(this.value)
                            .draw();
                    }
                });
            });
        },
        "drawCallback": function () {
            //GetItemsChecked
            let rows = allActivitiesShippingTable.rows().data();
            for (let i = 0; i < rows.length; i++) {
                let checkSelectedRow = document.getElementById('checkSelectedRow_' + i);

                // Vuelvo a checkear los que estaban chequeados cuando se refresco la grilla
                if (checkOfWorkActivityIds.length > 0) {

                    for (let x = 0; x < checkOfWorkActivityIds.length; x++) {
                        if (checkOfWorkActivityIds[x] == rows[i].id && !checkSelectedRow.hasAttribute('checked')) {
                            checkSelectedRow.setAttribute('checked', 'checked');
                        }
                    }
                }

                checkSelectedRow.addEventListener('change', function () {
                    if (this.checked) {
                        checkOfWorkActivityIds.push(rows[i].id);

                    } else {
                        const index = checkOfWorkActivityIds.indexOf(rows[i].id);
                        if (index > -1) {
                            checkOfWorkActivityIds.splice(index, 1);
                        }
                    }
                });
            }
        }
    });

    $('.table tfoot th').each(function (i) {
        let title = $(this).text();
        if (title != actions) {
            $(this).html('<input type="text" class="search-control" placeholder="' + title + '" data-index="' + i + '" />');
        } else {
            $(this).html('');
        }
    });

    $('#btn-create-delivery-note').click(function () {
        if (checkOfWorkActivityIds.length > 0) {
            createDeliveryNoteByWorkActivityIds(checkOfWorkActivityIds);
        }
        else {
            alert("Usted debe seleccionar al menos un item para generar un remito.");
        }
    });

});

function createDeliveryNoteByWorkActivityIds(checkOfWorkActivityIds) {
    console.log(checkOfWorkActivityIds);
}