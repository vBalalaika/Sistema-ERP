$(document).ready(function () {

    /* Filter parameters for export to excel */
    let columnFilter_2 = "";
    let columnFilter_3 = "";
    let columnFilter_4 = "";
    let columnFilter_5 = "";
    let columnFilter_6 = "";
    let columnFilter_7 = "";
    let columnFilter_8 = "";
    let columnFilter_9 = "";
    let columnFilter_10 = "";

    let colIndexOrder = 0;
    let colOrderDirection = "";

    let editPermission = "disabled";
    let deletePermission = "disabled";

    if (editPermissionFromView == "True") {
        editPermission = "";
    }
    if (deletePermissionFromView == "True") {
        deletePermission = "";
    }

    $('#btn-create').on('click', function () {
        jQueryModalGet('/Logistics/Stock/_OnGetCreateOrEditExistence', stockInformation);
    });

    let existencesTable = $('#existencesTable').DataTable({
        "scrollY": '55vh',
        "scrollX": true,
        "scrollCollapse": true,
        "scroller": true,
        "oLanguage": languageDataTable,
        "sAjaxSource": "/Logistics/Stock/_LoadAllExistences",
        "bServerSide": true,
        "bProcessing": true,
        "bSearchable": true,
        "language": {
            "processing":
                '<div class="text-center"> <div id="spinnerDataTable" class="spinner-border text-primary" role="status"> <span class="sr-only"></span></div></div>'
        },
        "order": [
            [2, "asc"],
            [4, "asc"]
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

                    let iconSize = 22;

                    let html = '<div class="btn-group"><div class="d-flex animated--grow-in">';

                    // Icono editar.
                    html += '<a class="btn action-btn ' + editPermission + '" title="' + editStockProductTitle + '" onclick="editExistence(' + row.id + ')"><img class="action-img-icon" src="../../../images/Editar.svg" width="' + iconSize + '" height="' + iconSize + '" /></a>';

                    // Icono movimientos.
                    html += '<a class="btn action-btn" title="' + movementsTitle + '" onclick=""><img class="action-img-icon" src="../../../images/movimientoStock.svg" width="' + iconSize + '" height="' + iconSize + '" /></a>';

                    if (row.archives.length > 0) {

                        //for (let i = 0; i < row.archives.length; i++) {

                        //    let path = row.archives[i].pathUrl.toString().replace("\\\\172.16.50.240\\ARCHIVOS\\SIMPLEMAK\\OFICINA TECNICA", "/files");
                        //    // PLANOS
                        //    if (row.archives[i].archiveTypeId == 1) {
                        //        // Icono archivos para PLANOS.
                        //        html += '<a href="' + path + '" target="_blank" class="btn action-btn" title="' + archivesPlanosTitle + '"><img class="action-img-icon" src="../../../images/Plano.svg" width="' + iconSize + '" height="' + iconSize + '" /></a>';
                        //    }
                        //    // 3D
                        //    else if (row.archives[i].archiveTypeId == 3) {
                        //        // Icono archivos para 3D.
                        //        html += '<a href="' + path + '" target="_blank" class="btn action-btn" title="' + archives3DTitle + '"><img class="action-img-icon" src="../../../images/3D.svg" width="' + iconSize + '" height="' + iconSize + '" /></a>';
                        //    }
                        //}

                        let path = "jQueryModalGet('/productmod/product/GetFilesByProduct?id=" + row.id + "'" + ',' + "'" + row.codeAndDescription + "'" + ")";
                        html += '<a title="@localizer["Files"]" class="btn action-btn" onclick="' + path + '"><img class="action-img-icon" src="../../../images/Archivos.svg" width="20" height="20"/></a>';
                    }
                    else {
                        html += '<a title="@localizer["Files"]" class="btn action-btn disabled"><img class="action-img-icon" src="../../../images/Archivos.svg" width="20" height="20"/></a>';
                    }

                    // Icono eliminar (se desactiva el StoredStock).
                    html += '<a class="btn action-btn ' + deletePermission + '" title="' + deleteTitle + '" onclick="deleteExistence(' + row.id + ')" ><img class="action-img-icon" src="../../../images/Eliminar.svg" width="' + iconSize + '" height="' + iconSize + '" /></a>';

                    html += '</div></div>';
                    return html;
                },
                "visible": true,
                "width": "5%",
                "searchable": false
            },
            {
                "data": "subCategory",
                "render": function (value) {
                    if (value != null) {
                        return value.description;
                    } else {
                        return "";
                    }
                },
                "autoWidth": true,
                "searchable": true,
                "className": "text-center"
            },
            {
                "data": "code",
                "render": function (value) {
                    return value;
                },
                "autoWidth": true,
                "searchable": true,
                "className": "text-center"
            },
            {
                "data": "description",
                "render": function (value) {
                    return value;
                },
                "autoWidth": true,
                "searchable": true
            },
            // Cantidad disponible
            {
                "render": function () {
                    return 0;
                },
                "autoWidth": true,
                "searchable": true,
                "className": "text-center"
            },
            {
                "data": "id",
                "render": function (data, type, row, meta) {
                    if (row.existenceUnitMeasure != null) {
                        return $.fn.dataTable.render.number('.', ',', 2).display(row.existence) + " " + row.existenceUnitMeasure.abbreviation;
                    } else {
                        return $.fn.dataTable.render.number('.', ',', 2).display(row.existence);
                    }
                },
                "autoWidth": true,
                "searchable": true,
                "className": "text-center"
            },
            {
                "data": "id",
                "render": function (data, type, row, meta) {
                    if (row.minimumUnitMeasure != null) {
                        return $.fn.dataTable.render.number('.', ',', 2).display(row.minimum) + " " + row.minimumUnitMeasure.abbreviation;
                    } else {
                        return $.fn.dataTable.render.number('.', ',', 2).display(row.minimum);
                    }
                },
                "autoWidth": true,
                "searchable": true,
                "className": "text-center"
            },
            {
                "data": "id",
                "render": function (data, type, row, meta) {
                    if (row.quantityToOrderUnitMeasure != null) {
                        return $.fn.dataTable.render.number('.', ',', 2).display(row.quantityToOrder) + " " + row.quantityToOrderUnitMeasure.abbreviation;
                    } else {
                        return $.fn.dataTable.render.number('.', ',', 2).display(row.quantityToOrder);
                    }
                },
                "autoWidth": true,
                "searchable": true,
                "className": "text-center"
            },
            {
                "data": "storageUnitMeasure",
                "render": function (value) {
                    if (value != null) {
                        return value.description;
                    } else {
                        return "";
                    }
                },
                "autoWidth": true,
                "searchable": true,
                "className": "text-center"
            },
            {
                "data": "locationStation",
                "render": function (value) {
                    if (value != null) {
                        return value.description;
                    } else {
                        return "";
                    }
                },
                "autoWidth": true,
                "searchable": true,
                "className": "text-center"
            },
        ],
        initComplete: function () {
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

    colIndexOrder = existencesTable.order()[0][0];
    colOrderDirection = existencesTable.order()[0][1];

    // Get order (index and direction) columns for export to excel table
    $('div.dataTables_scrollHeadInner thead').on('click', 'th', function () {

        if ($(this).hasClass("sorting") && !$(this).hasClass("sorting_asc") && !$(this).hasClass("sorting_desc")) {
            colOrderDirection = "asc";
        }
        else if ($(this).hasClass("sorting_asc")) {
            colOrderDirection = "desc";
        }
        else if ($(this).hasClass("sorting_desc")) {
            colOrderDirection = "asc";
        }

        switch (existencesTable.column(this).index()) {
            case 2:
                colIndexOrder = 2;
                break;
            case 3:
                colIndexOrder = 3;
                break;
            case 4:
                colIndexOrder = 4;
                break;
            case 5:
                colIndexOrder = 5;
                break;
            case 6:
                colIndexOrder = 6;
                break;
            case 7:
                colIndexOrder = 7;
                break;
            case 8:
                colIndexOrder = 8;
                break;
            case 9:
                colIndexOrder = 9;
                break;
            case 10:
                colIndexOrder = 10;
                break;
        }
    });

    // For export to excel, setting filters to send to the server
    $('.search-control').keyup(function (e) {
        let colIndex = $(this).attr("data-index");
        let valueOfInput = $(this).val();

        colIndex++;

        if (colIndex == 2) {
            columnFilter_2 = valueOfInput;
        }
        else if (colIndex == 3) {
            columnFilter_3 = valueOfInput;
        }
        else if (colIndex == 4) {
            columnFilter_4 = valueOfInput;
        }
        else if (colIndex == 5) {
            columnFilter_5 = valueOfInput;
        }
        else if (colIndex == 6) {
            columnFilter_6 = valueOfInput;
        }
        else if (colIndex == 7) {
            columnFilter_7 = valueOfInput;
        }
        else if (colIndex == 8) {
            columnFilter_8 = valueOfInput;
        }
        else if (colIndex == 9) {
            columnFilter_9 = valueOfInput;
        }
        else if (colIndex == 10) {
            columnFilter_10 = valueOfInput;
        }
    }).keyup();

    // Btn click export excel
    $('#btn-export-excel').on('click', function () {

        window.location.href = "/Logistics/Stock/ExportToExcel" +
            "?columnFilter_2=" + columnFilter_2 +
            "&columnFilter_3=" + columnFilter_3 +
            "&columnFilter_4=" + columnFilter_4 +
            "&columnFilter_5=" + columnFilter_5 +
            "&columnFilter_6=" + columnFilter_6 +
            "&columnFilter_7=" + columnFilter_7 +
            "&columnFilter_8=" + columnFilter_8 +
            "&columnFilter_9=" + columnFilter_9 +
            "&columnFilter_10=" + columnFilter_10 +
            "&colIndexOrder=" + colIndexOrder +
            "&colOrderDirection=" + colOrderDirection;
    });

});

function editExistence(productId) {
    jQueryModalGet('/Logistics/Stock/_OnGetCreateOrEditExistence?productId=' + productId, stockInformation);
};

function deleteExistence(productId) {
    if (confirm(_areYouSure)) {
        let url = "/Logistics/Stock/_OnPostDelete?productId=" + productId;
        $.ajax({
            url: url,
            type: "POST",
            success: function (response) {
                setTimeout(function () { $('#existencesTable').DataTable().ajax.reload(null, false); }, 1000);
            }
        });
    }
};