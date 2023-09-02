$(document).ready(function () {

    let pageScrollPos = 0;

    $('.form-image').click(function () { $('#customFile').trigger('click'); });
    $(function () {
        $('.selectpicker').selectpicker();
    });
    setTimeout(function () {
        $('body').addClass('loaded');
    }, 200);

    jQueryModalGet = (url, title) => {
        try {
            $.ajax({
                type: 'GET',
                url: url,
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#form-modal .modal-header').html('<h5 class="modal-title">' + title + '</h5> <button type="button" class="btn btn-secondary" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>');
                    //$('#form-modal .modal-title').html(title);
                    $('#form-modal .modal-body').html(res.html);
                    $('#form-modal').modal('show');
                },
                error: function (err) {
                    console.log(err)
                },
                complete: function () {
                    hideSpinner();
                }
            })
            //to prevent default form submit event
            return false;
        } catch (ex) {
            console.log(ex)
        }
    }

    jQueryModalPost = form => {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.isValid) {

                        $('#form-modal #btn-save').attr('disabled', 'disabled');
                        $('#form-modal #btn-save').html('<i class="fa fa-spinner fa-spin"></i> ' + $('#form-modal #btn-save').text());

                        $('#viewAll').html(res.html)
                        $('#form-modal').modal('hide');

                    }
                },
                error: function (err) {
                    console.log(err)
                }
            })
            return false;
        } catch (ex) {
            console.log(ex)
        }
    }
    jQueryModalPostWithoutRefresh = form => {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.isValid) {

                        $('#form-modal #btn-save').attr('disabled', 'disabled');
                        $('#form-modal #btn-save').html('<i class="fa fa-spinner fa-spin"></i> ' + $('#form-modal #btn-save').text());

                        $('#form-modal').modal('hide');

                    }
                },
                error: function (err) {
                    console.log(err)
                }
            })
            return false;
        } catch (ex) {
            console.log(ex)
        }
    }

    jQueryModalPostWithRefresh = (form, tableInstance) => {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.isValid) {

                        $('#form-modal #btn-save').attr('disabled', 'disabled');
                        $('#form-modal #btn-save').html('<i class="fa fa-spinner fa-spin"></i> ' + $('#form-modal #btn-save').text());

                        refreshDataTableInstanceWithoutScrollOnTop(tableInstance);
                        $('#form-modal').modal('hide');
                    }
                },
                error: function (err) {
                    console.log(err)
                }
            })
            return false;
        } catch (ex) {
            console.log(ex)
        }
    }

    jQueryModalPostSaleAndProductionOrders = form => {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.isValid) {
                        if (res.renumberProductNumber) {
                            let text = "Algunos de los números de productos generados ya existen, ¿desea reenumerarlos?";
                            if (confirm(text) == true) {
                                let idNumberSM = function () {
                                    let tmp = null;
                                    $.ajax({
                                        async: false,
                                        url: '/Sales/Order/GetLastProductNumberForMachines',
                                        type: 'get',
                                        dataType: 'json',
                                        success: function (response) {
                                            tmp = response.maxProductNumber + 1;
                                        }
                                    });
                                    return tmp;
                                }();
                                let idNumberSA = function () {
                                    let tmp = null;
                                    $.ajax({
                                        async: false,
                                        url: '/Sales/Order/GetLastProductNumberForAccessories',
                                        type: 'get',
                                        dataType: 'json',
                                        success: function (response) {
                                            tmp = response.maxProductNumber + 1;
                                        }
                                    });
                                    return tmp;
                                }();
                                let idNumberSR = function () {
                                    let tmp = null;
                                    $.ajax({
                                        async: false,
                                        url: '/Sales/Order/GetLastProductNumberForSpare',
                                        type: 'get',
                                        dataType: 'json',
                                        success: function (response) {
                                            tmp = response.maxProductNumber + 1;
                                        }
                                    });
                                    return tmp;
                                }();
                                let idNumberIM = function () {
                                    let tmp = null;
                                    $.ajax({
                                        async: false,
                                        url: '/Sales/Order/GetLastProductNumberForMachinesImported',
                                        type: 'get',
                                        dataType: 'json',
                                        success: function (response) {
                                            tmp = response.maxProductNumber + 1;
                                        }
                                    });
                                    return tmp;
                                }();
                                let idNumberIA = function () {
                                    let tmp = null;
                                    $.ajax({
                                        async: false,
                                        url: '/Sales/Order/GetLastProductNumberForAccessoriesImported',
                                        type: 'get',
                                        dataType: 'json',
                                        success: function (response) {
                                            tmp = response.maxProductNumber + 1;
                                        }
                                    });
                                    return tmp;
                                }();
                                let idNumberIR = function () {
                                    let tmp = null;
                                    $.ajax({
                                        async: false,
                                        url: '/Sales/Order/GetLastProductNumberForSpareImported',
                                        type: 'get',
                                        dataType: 'json',
                                        success: function (response) {
                                            tmp = response.maxProductNumber + 1;
                                        }
                                    });
                                    return tmp;
                                }();
                                let idNumberSCS = function () {
                                    let tmp = null;
                                    $.ajax({
                                        async: false,
                                        url: '/Sales/Order/GetLastProductNumberForStockComponents',
                                        type: 'get',
                                        dataType: 'json',
                                        success: function (response) {
                                            tmp = response.maxProductNumber + 1;
                                        }
                                    });
                                    return tmp;
                                }();

                                $("#OrderDetailsTable tr").each(function () {
                                    if ($(this).find("input.productNumber").val() != undefined && !$(this).find("input.productNumber").hasClass("BelongsToAProductionOrder")) {

                                        $(this).find("input.productNumber").css("background-color", "#FFD680");

                                        let IdentificationNumber = parseInt($(this).find("input.productNumber").val().toString().slice(0, 2));

                                        if (IdentificationNumber == 1) {
                                            $(this).find("input.productNumber").val("01-" + ('00000' + idNumberSM).slice(-5));
                                            idNumberSM++;
                                        }

                                        if (IdentificationNumber == 11) {
                                            $(this).find("input.productNumber").val("11-" + ('00000' + idNumberIM).slice(-5));
                                            idNumberIM++;
                                        }

                                        if (IdentificationNumber == 2) {
                                            $(this).find("input.productNumber").val("02-" + ('00000' + idNumberSA).slice(-5));
                                            idNumberSA++;
                                        }

                                        if (IdentificationNumber == 12) {
                                            $(this).find("input.productNumber").val("12-" + ('00000' + idNumberIA).slice(-5));
                                            idNumberIA++;
                                        }

                                        if (IdentificationNumber == 3) {
                                            $(this).find("input.productNumber").val("03-" + ('00000' + idNumberSR).slice(-5));
                                        }

                                        if (IdentificationNumber == 13) {
                                            $(this).find("input.productNumber").val("13-" + ('00000' + idNumberIR).slice(-5));
                                        }

                                        if (IdentificationNumber == 4) {
                                            $(this).find("input.productNumber").val("04-" + ('00000' + idNumberSCS).slice(-5));
                                        }
                                    }
                                });
                            }
                        }

                        if (res.isSale) {
                            $('#form-modal').modal('hide');

                            setTimeout(function () {
                                $('#saleOrdersTable').DataTable().ajax.reload(null, false);

                                // Fran: 23/5/2023: Se agrega para bloquear la pagina y no permitir darle dos veces la boton de guardado.
                                hideSpinner();

                            }, 1000);

                        }
                        else {
                            $('#form-modal').modal('hide');

                            setTimeout(function () {
                                $('#productionOrdersTable').DataTable().ajax.reload(null, false);

                                // Fran: 23/5/2023: Se agrega para bloquear la pagina y no permitir darle dos veces la boton de guardado.
                                hideSpinner();

                                if (res.isGeneratedFromMissingProducts) {
                                    $('#missingProductsTable').DataTable().ajax.reload(null, false);
                                }

                            }, 1000);
                        }
                    }
                    else
                    {
                        if (res.isSale) {
                            setTimeout(function () {
                                // Fran: 23/5/2023: Se agrega para bloquear la pagina y no permitir darle dos veces la boton de guardado.
                                hideSpinner();

                            }, 1000);

                        }
                        else {
                            setTimeout(function () {
                                // Fran: 23/5/2023: Se agrega para bloquear la pagina y no permitir darle dos veces la boton de guardado.
                                hideSpinner();

                            }, 1000);
                        }
                    }
                },
                error: function (err) {
                    console.log(err)
                }
            })
            return false;
        } catch (ex) {
            console.log(ex)
        }
    }
    jQueryPost = form => {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    if (res.isValid) {
                        window.location.href = "/productmod/product/index?msg=" + res.html + "&productCreated=" + res.productCreated + "";
                    }
                },
                error: function (err) {
                    console.log(err)
                }
            })
            return false;
        } catch (ex) {
            console.log(ex)
        }
    }
    jQueryModalDelete = (form, title) => {
        if (confirm(title)) {
            try {
                $.ajax({
                    type: 'POST',
                    url: form.action,
                    data: new FormData(form),
                    contentType: false,
                    processData: false,
                    success: function (res) {
                        if (res.isValid) {
                            //$('#viewAll').html(res.html)
                            $(form).parents('tr').remove()
                        }
                    },
                    error: function (err) {
                        console.log(err)
                    }
                })
            } catch (ex) {
                console.log(ex)
            }
        }

        //prevent default form submit event
        return false;
    }
    // For refresh all DataTable
    jQueryModalDeleteModify = (form, title) => {
        if (confirm(title)) {
            try {
                $.ajax({
                    type: 'POST',
                    url: form.action,
                    data: new FormData(form),
                    contentType: false,
                    processData: false,
                    success: function (res) {
                        if (res.isValid) {
                            $('#viewAll').html(res.html)
                        }
                    },
                    error: function (err) {
                        console.log(err)
                    }
                })
            } catch (ex) {
                console.log(ex)
            }
        }

        //prevent default form submit event
        return false;
    }

    $.validator.methods.range = function (value, element, param) {
        var globalizedValue = value.replace(",", ".");
        return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
    }

    $.validator.methods.number = function (value, element) {
        return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
    }

    GetCategoriesForSidebar();

    //#region Refresh dataTable keeping scroll position on ajax reload
    refreshDatatableKeepingScrollPosition = (dataTableInstance) => {
        try
        {
            let scrollingContainer = $(dataTableInstance.table().node()).parent('div.dataTables_scrollBody');
            let scrollTop = scrollingContainer.scrollTop();

            dataTableInstance.ajax.reload(function () {
                scrollingContainer.scrollTop(scrollTop);
            }, false);
        }
        catch (ex)
        {
            console.log(ex);
        }
    }
    //#endregion

});

//#region Productos: carga de categorias en sidebar
function GetCategoriesForSidebar() {
    $.ajax({
        async: true,
        type: "GET",
        url: '/productmod/product/GetCategoriesForSideBar',
        success: function (result) {
            if (result.isValid) {
                if ($("ul#Simplemak").children("li").length == 0 && $("ul#Importados").children("li").length == 0) {
                    result.categories.forEach(function (category) {
                        if (category.description != "Insumos para Oficina" && category.description != "Desarrollos") {
                            $("ul#Simplemak").append("<li class='nav-item'><a is-active-route='' href='/productmod/product/index?categoryId=" + category.id + "&categoryDescription=" + category.description + "&Smak=true' class='nav-link'><p> &nbsp;&nbsp; " + category.description + "</p></a></li>");
                        }
                        if (category.description == "Maquinas y Accesorios" || category.description == "Componentes Comprados") {
                            $("ul#Importados").append("<li class='nav-item'><a is-active-route='' href='/productmod/product/index?categoryId=" + category.id + "&categoryDescription=" + category.description + "&Smak=false' class='nav-link'><p> &nbsp;&nbsp; " + category.description + "</p></a></li>");
                        }
                    });
                    $("ul#Simplemak").append("<li class='nav-item'><a is-active-route='' href='/productmod/product/index?categoryId=0&categoryDescription=Todos los productos &Smak=true' class='nav-link'><p> &nbsp;&nbsp; Todos Simplemak</p></a></li>");
                    $("ul#Importados").append("<li class='nav-item'><a is-active-route='' href='/productmod/product/index?categoryId=0&categoryDescription=Todos los productos &Smak=false' class='nav-link'><p> &nbsp;&nbsp; Todos importados</p></a></li>");
                }
            }
        }
    })
}
//#endregion

function validateIsNumber(evt) {
    // code is the decimal ASCII representation of the pressed key.
    var code = (evt.which) ? evt.which : evt.keyCode;

    if (code == 8) {
        return true;
    } else if (code == 44 || code == 46) {
        return true;
    } else if (code >= 48 && code <= 57) {
        return true;
    } else {
        return false;
    }
}

//#region To order by Date type column Datatable
$.fn.dataTable.ext.order['date-time'] = function (settings, col) {
    return this.api().column(col, { order: 'index' }).nodes().map(function (td, i) {
        var val = $(td).text().trim();    // Get datetime string from <td>
        return moment(val, "DD/MM/YYYY hh:mm:ss a").format("X");
    });
}
//#endregion

function refreshDataTableInstanceWithoutScrollOnTop(dataTable) {

    let scrollPos = $('.dataTables_scrollBody').scrollTop();

    dataTable.ajax.reload(function () {
        $('.dataTables_scrollBody').scrollTop(scrollPos);
    }, false);

}

//#region To validate input of decimal number with format: ###.###,##
function validateDecimalsNumbersOnInput(arrayIds) {

    $.each(arrayIds, function (index, value) {

        $("#" + value).on({
            "focus": function (event) {
                $(event.target).select();
            },
            "keyup": function (event) {
                $(event.target).val(function (index, value) {
                    return value.replace(/\D/g, "")
                        //.replace(/^0+/, '0')
                        .replace(/([0-9])([0-9]{2})$/, '$1,$2')
                        .replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ".");
                });
            },
            "change": function (event) {
                $(event.target).val(function (index, value) {
                    let valueAux = value;
                    if (!value.includes(",")) {
                        valueAux = value.concat(",00");
                    }
                    else {
                        valueAux = value;
                    }

                    if (value.charAt(0) == "0" && value.length == 1) {
                        valueAux = value.concat(",00");
                    }
                    else if (value.charAt(0) == "0" && value.charAt(1) != ",") {
                        if (value.charAt(1) == ".") {
                            valueAux = value.slice(2);
                        }
                        else if (value.charAt(2) == ".") {
                            valueAux = value.slice(1);
                        }
                        else {
                            valueAux = value.slice(1);
                        }
                    }

                    for (var i = 0; i < valueAux.length; i++) {
                        if (valueAux[i] != "0" && valueAux[i] != ".") {
                            let addZero = "";
                            if (valueAux[i] == ",") {
                                addZero = "0";
                            }
                            valueAux = addZero + valueAux.slice(i);
                            break;
                        }
                    }

                    return valueAux;
                });
            }
        });

        //document.getElementById(value).addEventListener('input', function () {

        //    if (this.value == "," && this.value.length == 1) {
        //        this.value = "0,";
        //    }

        //    var clean = this.value.replace(/[^0-9,]/g, "")
        //        .replace(/(,.*?),(.*,)?/, "$1");
        //    // don't move cursor to end if no change
        //    if (clean !== this.value) this.value = clean;

        //    var separador = (this.value.substr(this.value.length - 1, 1) === ',') ? ',' : '';
        //    var monto1 = this.value
        //        .replace(/[^\d,]/g, "")
        //        .replace(",", ".");

        //    this.value = Intl.NumberFormat('es-AR', {
        //        maximumFractionDigits: 2,
        //        minimumFractionDigits: 0
        //    }).format(monto1)
        //        + separador;

        //});

    });

};
//#endregion

function getPageScrollPos() {
    return this.pageScrollPos;
}

function setPageScrollPos(pageScrollPos) {
    this.pageScrollPos = pageScrollPos;
}

function zeroPadded(val) {
    if (val >= 10)
        return val;
    else
        return '0' + val;
}

//#region For decimal format export excel datatable.

function addCustomNumberFormat(xlsx, numberFormat) {
    // this adds a new custom number format to the Excel "styles" document:
    var numFmtsElement = xlsx.xl['styles.xml'].getElementsByTagName('numFmts')[0];
    // assume 6 custom number formats already exist, and next available ID is 176:
    var numFmtElement = '<numFmt numFmtId="176" formatCode="' + numberFormat + '"/>';
    $(numFmtsElement).append(numFmtElement);
    $(numFmtsElement).attr("count", "7"); // increment the count

    // now add a new "cellXfs" cell formatter, which uses our new number format (numFmt 176):
    var celXfsElement = xlsx.xl['styles.xml'].getElementsByTagName('cellXfs');
    var cellStyle = '<xf numFmtId="176" fontId="0" fillId="0" borderId="0" xfId="0" applyNumberFormat="1"'
        + ' applyFont="1" applyFill="1" applyBorder="1"/>';
    // this will be the 8th "xf" element - and will therefore have an index of "7", when we use it later:
    $(celXfsElement).append(cellStyle);
    $(celXfsElement).attr("count", "69"); // increment the count
}

function formatTargetColumn(xlsx, col) {
    var sheet = xlsx.xl.worksheets['sheet1.xml'];
    // select all the cells whose addresses start with the letter prvoided
    // in 'col', and add a style (s) attribute for style number 68:
    $('row c[r^="' + col + '"]', sheet).attr('s', '68');
}

//#endregion

function format_number_to_decimal(number, dec, dsep, tsep) {
    if (isNaN(number) || number == null) return '';

    number = parseFloat(number).toFixed(~~dec);
    tsep = typeof tsep == 'string' ? tsep : ',';

    var parts = number.split('.'),
        fnums = parts[0],
        decimals = parts[1] ? (dsep || '.') + parts[1] : '';

    return fnums.replace(/(\d)(?=(?:\d{3})+$)/g, '$1' + tsep) + decimals;
}

//#region Show spinner.
function showSpinner() {
    if ($('#layout_main_body').hasClass('loaded')) {
        $('#layout_main_body').removeClass('loaded');
    }
}
//#endregion

//#region Hide spinner.
function hideSpinner() {
    if (!$('#layout_main_body').hasClass('loaded')) {
        $('#layout_main_body').addClass('loaded');
    }
}
//#endregion