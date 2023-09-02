$(document).ready(function () {

    $.validator.unobtrusive.parse(document);

    // Selects2
    $("#OperationId").select2({
        placeholder: "Seleccionar operación",
        language: {
            noResults: function (params) {
                return 'No se encontraron resultados';
            }
        },
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        },
        sorter: data => data.sort((a, b) => a.text.localeCompare(b.text))
    });

    $("#AccessoryProduct").select2({
        placeholder: "Seleccionar accesorio del producto",
        language: {
            noResults: function (params) {
                return 'No se encontraron resultados';
            }
        },
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        },
        sorter: data => data.sort((a, b) => a.text.localeCompare(b.text))
    });

    $("#UnitMeasureId").select2({
        placeholder: "Seleccionar unidad de medida",
        language: {
            noResults: function (params) {
                return 'No se encontraron resultados';
            }
        },
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        },
        sorter: data => data.sort((a, b) => a.text.localeCompare(b.text))
    });

    $("#RawMaterial").select2({
        placeholder: "Seleccionar materia prima",
        language: {
            noResults: function (params) {
                return 'No se encontraron resultados';
            }
        },
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        },
        sorter: data => data.sort((a, b) => a.text.localeCompare(b.text))
    });

    $("#RelProductStation").select2({
        placeholder: "Seleccionar puesto",
        language: {
            noResults: function (params) {
                return 'No se encontraron resultados';
            }
        },
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        },
        sorter: data => data.sort((a, b) => a.text.localeCompare(b.text))
    });

    $("#SubCategoryId").select2({
        placeholder: "Seleccionar Sub-Categoría",
        language: {
            noResults: function (params) {
                return 'No se encontraron resultados';
            }
        },
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        },
        sorter: data => data.sort((a, b) => a.text.localeCompare(b.text))
    });

    $("#SubCategory_CategoryId").select2({
        placeholder: "Seleccionar Categoría",
        language: {
            noResults: function (params) {
                return 'No se encontraron resultados';
            }
        },
        theme: "bootstrap4",
        escapeMarkup: function (m) {
            return m;
        },
        sorter: data => data.sort((a, b) => a.text.localeCompare(b.text))
    });

    //$("#LocationStationId").select2({
    //    placeholder: "Seleccionar un puesto",
    //    language: {
    //        noResults: function (params) {
    //            return 'No se encontraron resultados';
    //        }
    //    },
    //    theme: "bootstrap4",
    //    escapeMarkup: function (m) {
    //        return m;
    //    },
    //    sorter: data => data.sort((a, b) => a.text.localeCompare(b.text))
    //});

    //#region Stock

    //$("#StockUnitMeasures").select2({
    //    placeholder: "Seleccionar una unidad",
    //    language: {
    //        noResults: function (params) {
    //            return 'No se encontraron resultados';
    //        }
    //    },
    //    theme: "bootstrap4",
    //    escapeMarkup: function (m) {
    //        return m;
    //    },
    //    sorter: data => data.sort((a, b) => a.text.localeCompare(b.text))
    //});

    //#endregion

    ///*carga de categorias en sidebar*/
    //function GetCategoriesForSidebar() {
    //    $.ajax({
    //        async: true,
    //        type: "GET",
    //        url: '/productmod/product/GetCategoriesForSideBar',
    //        success: function (result) {
    //            if (result.isValid) {
    //                result.categories.forEach(function (category) {
    //                    $("ul#Simplemak").append("<li class='nav-item'><a is-active-route='' href='/productmod/product/index?categoryId=" + category.id + "&categoryDescription=" + category.description + "&Smak=true' class='nav-link'><p> &nbsp;&nbsp; " + category.description + "</p></a></li>");
    //                    $("ul#Importados").append("<li class='nav-item'><a is-active-route='' href='/productmod/product/index?categoryId=" + category.id + "&categoryDescription=" + category.description + "&Smak=false' class='nav-link'><p> &nbsp;&nbsp; " + category.description + "</p></a></li>");
    //                });
    //                $("ul#Simplemak").append("<li class='nav-item'><a is-active-route='' href='/productmod/product/index?categoryId=0&categoryDescription=Todos los productos &Smak=true' class='nav-link'><p> &nbsp;&nbsp; Todos Simplemak</p></a></li>");
    //                $("ul#Importados").append("<li class='nav-item'><a is-active-route='' href='/productmod/product/index?categoryId=0&categoryDescription=Todos los productos &Smak=false' class='nav-link'><p> &nbsp;&nbsp; Todos importados</p></a></li>");

    //            }
    //        }
    //    })
    //}
    //GetCategoriesForSidebar();

    // Inicializar en Editar
    if ($('#isEdit').is(':checked')) {

        // Totales Roadmap
        roadmapTotals();
        $("#SubCategoryId").prop("disabled", true);
        select2RawMaterial();

        selectRawMaterialValue();
    }

    $("#SubCategory_CategoryId").prop("disabled", true);

    // CategorySelect2 "change"
    $('#SubCategory_CategoryId').on('select2:select', function () {
        var data = $(this).select2('data');
        if (data[0] != undefined) {
            if (data[0].id == "") {
                //Solo en Alta limpiamos todo y ocultamos
                if (!$('#isEdit').is(':checked')) {
                    updateFormByCategory("");
                }
            } else {
                updateFormByCategory(data[0].text);

            }
        }
    });

    //SubCategorySelect2 "change", TODO Se ejecuta tambien en codegenerator
    $('#SubCategoryId').on('select2:select', function (e) {
        var data = $(this).select2('data');
        $.ajax({
            async: true,
            type: "GET",
            url: '/productmod/codegenerator/GetCodeAndSubCategoryById?subcategoryId=' + data[0].id + '',
            success: function (result) {
                if (result.subcategory != null) {
                    $('#SubCategory_CategoryId').val(result.subcategory.categoryId).trigger("change").trigger('select2:select');
                    if (!$('#isEdit').is(':checked')) {
                        $('#CodeGeneratorInput').val(result.lastCode)
                    }

                }
            }
        })

    });

    // RawMaterial "change" asignamos valores a los hidden codigo y descripcion materia prima
    $('#RawMaterial').on('select2:select', function () {
        var data = $(this).select2('data');

        if (data[0] != undefined) {
            if (data[0].text != "") {
                var textos = data[0].text.split('-');
                var codigo = textos[0];
                var descripcion = textos[1];
                $("#ProductFeature_RawMaterialCode").val(codigo);
                $("#ProductFeature_RawMaterialDescription").val(descripcion);
            }
        }
    });

    //SubCategorySelect2 trigger "change" on document ready
    $('#SubCategoryId').trigger('select2:select');

    //CategorySelect2 trigger "change" on document ready
    $('#SubCategory_CategoryId').trigger('select2:select');

    //#region Stock

    //// Stock
    //if (!$('#ProductFeature_StoredStock').is(':checked')) {
    //    $("#divStock").hide();
    //    $("#div-stock-measures").hide();
    //} else {
    //    $("#divStock").show();
    //    if ($("#StockUnitMeasures").val() == 32 || $("#StockUnitMeasures").val() == 22 || $("#StockUnitMeasures").val() == 23 || $("#StockUnitMeasures").val() == 31 || $("#StockUnitMeasures").val() == 33 || $("#StockUnitMeasures").val() == 36 || $("#StockUnitMeasures").val() == 37) {
    //        $("#div-stock-measures").show();
    //        getUnitAbbreviation();
    //    } else {
    //        $("#div-stock-measures").hide();
    //    }
    //}

    //// Hide/show divStock
    //$(document).on("change", '#ProductFeature_StoredStock', function () {
    //    if ($(this).is(':checked')) {
    //        $("#divStock").show("slow");
    //        if ($("#UnitMeasureId").val() == 19) {
    //            $("#StockUnitMeasures").prop("disabled", true);
    //        } else {
    //            $("#StockUnitMeasures").prop("disabled", false);
    //        }

    //    } else {
    //        cleanDiv($("#divStock"))
    //        $("#divStock").hide("slow");
    //    }
    //});

    //// Show div-stock-measures when Stock unit (Barra, Rollo, Bobina, Hoja o Caja)
    //$('#StockUnitMeasures').on('select2:select', function (e) {
    //    let unitMeasure = e.params.data.id;
    //    if (unitMeasure == 32 || unitMeasure == 22 || unitMeasure == 23 || unitMeasure == 31 || unitMeasure == 33) {
    //        $("#div-stock-measures").show();
    //    } else {
    //        cleanDiv($("#div-stock-measures"))
    //        $("#div-stock-measures").hide("slow");
    //    }

    //});

    //#endregion

    $('#UnitMeasureId').on('select2:select', function (e) {
        if ($("#UnitMeasureId").val() == 19) {
            $("#StockUnitMeasures").prop("disabled", true);
            cleanDiv($("#div-stock-measures"))
            $("#div-stock-measures").hide("slow");
            $("#StockUnitMeasures").val("-1").trigger("change");
        } else {
            $("#StockUnitMeasures").prop("disabled", false);
        }
        getUnitAbbreviation();
    });

    // .DeleteButton delete row
    $(document).on("click", ".DeleteButton", function () {
        $(this).parent().parent().remove();
    });


    // RoadMap-Station
    $(document).on("click", ".DeleteButtonRoadmap", function () {
        $(this).parent().parent().remove();
        roadmapTotals();
    });
    $("#btnAddStation").on('click', function () {
        var code = $("#RelProductStation").find(":selected").data("code");
        var stationId = $("#RelProductStation").val();// Gets the selected value of the dropdown
        if (stationId == "") {
            alert("Debe seleccionar una estación de trabajo")
        }
        else {
            var max = 0; //Buscamos el Maximo, para q no se repita el index de agrupacion.
            if ($('#RelProductStationsTable tbody tr').length > 0) {
                max = Math.max.apply(Math, $('.getMax').map(function () {
                    return $(this).val()
                }))
                max++;
            }
            var orden = max + 1;
            $("#RelProductStationsTable tbody").append('<tr> <td class="d-none">' +
                '<input type="hidden" class="getMax" name="RelProductStations.Index" value="' + max + '" />' +
                '<input type="hidden" name="RelProductStations[' + max + '].StationId" readonly value="' + stationId + '" /></td>' +
                '<input type="hidden" name="RelProductStations[' + max + '].Station.Id" readonly value="' + stationId + '" />' +// no se si hace falta, Ver en RoadMap.cshtml
                '<td><input type="number"  min="1" class="form-control order" name="RelProductStations[' + max + '].Order" value="' + orden + '" /></td>' +
                '<td><input type="text" class="form-control" name="RelProductStations[' + max + '].Station.Abbreviation" readonly value="' + code + ' " /></td>' +
                '<td><input type="text" class="form-control" name="RelProductStations[' + max + '].Station.Description" readonly value="' + $("#RelProductStation option:selected").text() + ' " /></td>' +
                '<td><input type="text" class="form-control time" name="RelProductStations[' + max + '].Time"  value="" /></td>' +
                '<td><input type="text"  class="form-control cost" name="RelProductStations[' + max + '].Cost" value="" /></td>' +
                '<td><button type="button" class="btn btn-outline-danger d-block m-auto DeleteButtonRoadmap"><img class="action-img-icon" src="../../../images/Eliminar.svg" width="20" height="20" /></button></td></tr>');

            //Limpiamos combo cuando agrega 1 estación
            $("#RelProductStation").val(null).trigger("change");
            $("#RelProductStation").select2("open");
        }
    });

    //Code Generator
    $("#btnGeneratesCodes").click(function () {
        var quantity = $("#CodeGeneratorQuantityInput").val();
        var subCategoryVal = $('#SubCategoryId').val();
        if (subCategoryVal && quantity) {
            $.ajax({
                async: true,
                type: "GET",
                url: '/productmod/codegenerator/OnGetGenerateCodeGenerator?subCategoryId=' + subCategoryVal + '&quantity=' + quantity,
                success: function (result) {
                    $("#ProductsCodeGeneratorTable tbody").html(result);
                }
            })
        }
        else {
            alert('Debe completar los campos');
        }



    });

    //evento change en inputs de columnas 3 y 4 RelProductStationsTable
    $(document).on("change", "#RelProductStationsTable tbody input", function () {
        if ($(this).hasClass('time') || $(this).hasClass('cost')) {
            roadmapTotals();
        }
    });

    function selectRawMaterialValue() {
        $.ajax({
            async: true,
            type: "GET",
            url: '/productmod/product/GetProductIdOfRawMaterialCode?rawMaterialCode=' + $("#ProductFeature_RawMaterialCode").val() + '',
            success: function (result) {
                if (result.isValid) {
                    $("#RawMaterial").val(result.productId);
                    $('#RawMaterial').trigger('change.select2');
                }
            }
        });
    }

    //Asignamos la materia prima seleccionada al combo usando los datos de los hidden codigo y descripcion materia prima
    function select2RawMaterial() {
        let code = $("#ProductFeature_RawMaterialCode").val();
        let descripcion = $("#ProductFeature_RawMaterialDescription").val();
        let codeAndDescription = code + "-" + descripcion;
        let value = $("#RawMaterial").find("option:contains('" + codeAndDescription + "')").val();

        $("#RawMaterial").val(value);
        $('#RawMaterial').trigger('change.select2');
    }

    //Calculo de totales tabla RelProductStationsTable
    function roadmapTotals() {
        if ($('#RelProductStationsTable tbody td .time').length > 0) {
            var totalTime = 0;
            var totalCost = 0;
            $("#RelProductStationsTable tbody td .time").each(function () {
                if ($(this).val()) {
                    var time = parseInt($(this).val());
                    if (!isNaN(time)) {
                        totalTime += parseInt($(this).val());
                    }

                }

            });
            $("#RelProductStationsTable tbody td .cost").each(function () {
                if ($(this).val()) {
                    var cost = parseInt($(this).val());
                    if (!isNaN(cost)) {
                        totalCost += parseInt($(this).val());
                    }

                }
            });
            if ($('#RelProductStationsTable tr.totals').length > 0) {
                $('#RelProductStationsTable td.totalTime').html(totalTime);
                $('#RelProductStationsTable td.totalCost').html(totalCost);
            } else {
                $('#RelProductStationsTable tfoot').append('<tr style="font-weight: bold;" class="totals border-top"><td>Totales:</td><td colspan="2"></td><td class="pl-4 totalTime">' + totalTime + '</td><td class="pl-4 totalCost">' + totalCost + '</td><</tr>');
            }

        } else {
            $('#RelProductStationsTable tr.totals').remove();
        }

    }

    // AccessoryProductsTab
    $("#AddAccessoryProductButton").click(function () {
        var AccessoryProductId = $("#AccessoryProduct").val();// Gets the selected value of the dropdown
        var code = $("#AccessoryProduct").find(":selected").data("code");
        if (AccessoryProductId == "") {
            alert("Debe seleccionar un producto")
        }
        else {
            var add = true;
            $("#AccessoryProductsTable").find("td.d-none").each(function () {
                if ($(this).find("input").val() == AccessoryProductId) {
                    add = false;
                }

            });
            if (add) {
                $("#AccessoryProductsTable tbody").append(' <tr><td class= "d-none" ><input type="hidden" name="AccessoryProducts.Index" value="' + AccessoryProductId + '" />' +
                    '<input type="hidden" name="AccessoryProducts[' + AccessoryProductId + '].IdProductAccessory" readonly value="' + AccessoryProductId + '" />' +
                    '<input type="hidden" name="AccessoryProducts[' + AccessoryProductId + '].ProductAccesory.Id" readonly value="' + AccessoryProductId + '" /></td>' +
                    '<td><input type="text" class="form-control" name="AccessoryProducts[' + AccessoryProductId + '].ProductAccessory.Code" readonly value="' + code + ' " /></td>' +
                    '<td><input type="text" class="form-control" name="AccessoryProducts[' + AccessoryProductId + '].ProductAccessory.Description" readonly value="' + $("#AccessoryProduct option:selected").text() + '" /></td> ' +
                    ' <td><button type="button" class="btn btn-outline-danger d-block m-auto DeleteButtonRoadmap"><img class="action-img-icon" src="../../../images/Eliminar.svg" width="20" height="20" /></button></td>');
            } else {
                alert("El Producto seleccionado ya se encuentra en la lista")
            }
        }
    });

    /* colapse-decolapse*/
    $(document).on("click", "#colapseButton", function () {
        /* if para evitar colapsar cuando estan hiddens*/
        if ($("#roadmapTab").is(":visible")) {
            if ($(this).find("i").hasClass("fa-compress-arrows-alt")) {
                colapseAll(true);
            } else {
                colapseAll(false)
            }
        }
    });

    function colapseAll(colapse) {
        if (colapse) {

            $("#colapseButton").find("i").removeClass("fa-compress-arrows-alt");
            $("#colapseButton").find("i").addClass("fa-expand-arrows-alt");

            $("div.colapsableTab").each(function () {
                $(this).removeClass("show");
            });
        } else {
            $("#colapseButton").find("i").removeClass("fa-expand-arrows-alt");
            $("#colapseButton").find("i").addClass("fa-compress-arrows-alt");
            $("div.colapsableTab").each(function () {
                $(this).addClass("show");

            });
        }
    }

    /* Form flexible*/
    function updateFormByCategory(category) {
        switch (category) {
            case "Maquinas y Accesorios":
                updateForm(true);
                var subcategory = $("#SubCategoryId").select2('data');
                updateFormBySubCategory(subcategory[0].text);
                colapseAll(true);
                break;

            case "Piezas Individuales":

                updateForm(false);

                /*productionTab*/
                $("#productionTab").show("slow");

                colapseAll(true);
                break;

            case "Conjuntos":
                updateForm(false);

                /*productionTab*/
                cleanDiv("#productionTab");
                $("#productionTab").hide("slow");

                colapseAll(true);
                break;

            case "Componentes Comprados":
                updateForm(false);

                /*productionTab*/
                cleanDiv("#productionTab");
                $("#productionTab").hide("slow");

                colapseAll(true);
                break;


            default:
                /*detailsTab*/
                cleanDiv("#operationAppreciation");
                $("#operationAppreciation").hide("slow");

                /*otherData*/
                cleanDiv("#otherData");
                $("#otherData").hide();
                colapseAll(true);
                break;

        }
    }

    function updateFormBySubCategory(subcategory) {
        switch (subcategory) {

            case "Cortadoras":

                $("#divOtros").show("slow");

                /*slicersDiv*/
                $("#slicersDiv").show("slow");

                /*printersDiv*/
                cleanDiv("#printersDiv");
                $("#printersDiv").hide("slow");

                /*pressesDiv*/
                cleanDiv("#pressesDiv");
                $("#pressesDiv").hide("slow");

                /*conversionsDiv*/
                cleanDiv("#conversionsDiv");
                $("#conversionsDiv").hide("slow");

                break;

            case "Impresoras":

                $("#divOtros").show("slow");

                /*slicersDiv*/
                cleanDiv("#slicersDiv");
                $("#slicersDiv").hide("slow");

                /*printersDiv*/
                $("#printersDiv").show("slow");

                /*pressesDiv*/
                cleanDiv("#pressesDiv");
                $("#pressesDiv").hide("slow");

                /*conversionsDiv*/
                cleanDiv("#conversionsDiv");
                $("#conversionsDiv").hide("slow");

                break;

            case "Prensas":

                $("#divOtros").show("slow");

                /*slicersDiv*/
                cleanDiv("#slicersDiv");
                $("#slicersDiv").hide("slow");

                /*printersDiv*/
                cleanDiv("#printersDiv");
                $("#printersDiv").hide("slow");

                /*pressesDiv*/
                $("#pressesDiv").show("slow");

                /*conversionsDiv*/
                cleanDiv("#conversionsDiv");
                $("#conversionsDiv").hide("slow");

                break;

            case "De Conversión":

                $("#divOtros").show("slow");

                /*slicersDiv*/
                cleanDiv("#slicersDiv");
                $("#slicersDiv").hide("slow");

                /*printersDiv*/
                cleanDiv("#printersDiv");
                $("#printersDiv").hide("slow");

                /*pressesDiv*/
                cleanDiv("#pressesDiv");
                $("#pressesDiv").hide("slow");

                /*conversionsDiv*/
                $("#conversionsDiv").show("slow");

                break;
            case "AllShow":
                $("#productionTab").show("slow");
                $("#conversionsDiv").show("slow");
                $("#pressesDiv").show("slow");
                $("#printersDiv").show("slow");
                $("#slicersDiv").show("slow");
                $("#divOtros").show("slow");
                $("#otherData").show();
                $("#operationAppreciation").show("slow");
                $("#specifications").show("slow");
                $("#accessoryProductsTab").show("slow");
                break;
            default:
                $("#divOtros").hide("slow");

        }

    }
    function updateForm(isMachine) {
        if (isMachine) {
            /*detailsTab*/
            $("#operationAppreciation").show("slow");
            $("#subcategoryLabel").html("Familia");

            /*specificationTab*/
            $("#specifications").show("slow");

            /*accessoryProductsTab*/
            $("#accessoryProductsTab").show("slow");

            /*productionTab*/
            cleanDiv("#productionTab");
            $("#productionTab").hide("slow");

            /*otherDataTabs*/
            $("#otherData").show("slow");
        } else {

            /*detailsTab*/
            cleanDiv("#operationAppreciation");
            $("#operationAppreciation").hide("slow");
            $("#subcategoryLabel").html("Rubro");

            /*specificationTab*/
            cleanDiv("#specifications");
            $("#specifications").hide("slow");

            /*accessoryProductsTab*/
            cleanDiv("#accessoryProductsTab");
            $("#accessoryProductsTab").hide("slow");

            /*otherDataTabs*/
            $("#otherData").show("slow");

        }
    }

    function cleanDiv(divId) {

        /*Inputs*/
        $(':input', divId)
            .not(':button, :submit, :reset, :hidden')
            .val('')
            .prop('checked', false)
            .prop('selected', false);

        /*Select2*/
        $('select.select2-hidden-accessible', divId).val(null).trigger("change");

        /*Tables*/
        $("tbody tr", divId).remove();


    }

    //#region Stock

    //function getUnitAbbreviation() {
    //    if ($("#UnitMeasureId").val() == 25 || $("#UnitMeasureId").val() == 20) {
    //        $("#StockWidthUnitLabel").text("cm.");
    //        $("#StockLengthUnitLabel").text("cm.");
    //        $("#StockHeightUnitLabel").text("cm.");
    //    }
    //    else if ($("#UnitMeasureId").val() == 24) {
    //        $("#StockWidthUnitLabel").text("g.");
    //        $("#StockLengthUnitLabel").text("g.");
    //        $("#StockHeightUnitLabel").text("g.");
    //    }
    //    else if ($("#UnitMeasureId").val() == 1) {
    //        $("#StockWidthUnitLabel").text("kg.");
    //        $("#StockLengthUnitLabel").text("kg.");
    //        $("#StockHeightUnitLabel").text("kg.");
    //    }
    //    else if ($("#UnitMeasureId").val() == 18) {
    //        $("#StockWidthUnitLabel").text("l.");
    //        $("#StockLengthUnitLabel").text("l.");
    //        $("#StockHeightUnitLabel").text("l.");
    //    }
    //    else if ($("#UnitMeasureId").val() == 2 || $("#UnitMeasureId").val() == 2 || $("#UnitMeasureId").val() == 2) {
    //        $("#StockWidthUnitLabel").text("m.");
    //        $("#StockLengthUnitLabel").text("m.");
    //        $("#StockHeightUnitLabel").text("m.");
    //    }
    //    else if ($("#UnitMeasureId").val() == 26) {
    //        $("#StockWidthUnitLabel").text("ml.");
    //        $("#StockLengthUnitLabel").text("ml.");
    //        $("#StockHeightUnitLabel").text("ml.");
    //    }
    //    else if ($("#UnitMeasureId").val() == 30 || $("#UnitMeasureId").val() == 30) {
    //        $("#StockWidthUnitLabel").text("mm.");
    //        $("#StockLengthUnitLabel").text("mm.");
    //        $("#StockHeightUnitLabel").text("mm.");
    //    }
    //}

    //let stockWidth = document.getElementById('StockWidth');
    //if (stockWidth != null) {
    //    stockWidth.addEventListener('input', (e) => {
    //        var entrada = e.target.value.split(','),
    //            parteEntera = entrada[0].replace(/\./g, ''),
    //            parteDecimal = entrada[1],
    //            salida = parteEntera.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.");

    //        e.target.value = salida + (parteDecimal !== undefined ? ',' + parteDecimal : '');
    //    }, false);
    //}


    //let stockLength = document.getElementById('StockLength');
    //if (stockLength != null) {
    //    stockLength.addEventListener('input', (e) => {
    //        var entrada = e.target.value.split(','),
    //            parteEntera = entrada[0].replace(/\./g, ''),
    //            parteDecimal = entrada[1],
    //            salida = parteEntera.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.");

    //        e.target.value = salida + (parteDecimal !== undefined ? ',' + parteDecimal : '');
    //    }, false);
    //}

    //let stockHeight = document.getElementById('StockHeight');
    //if (stockHeight != null) {
    //    stockHeight.addEventListener('input', (e) => {
    //        var entrada = e.target.value.split(','),
    //            parteEntera = entrada[0].replace(/\./g, ''),
    //            parteDecimal = entrada[1],
    //            salida = parteEntera.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.");

    //        e.target.value = salida + (parteDecimal !== undefined ? ',' + parteDecimal : '');
    //    }, false);
    //}

    //let stockWeight = document.getElementById('StockWeight');
    //if (stockWeight != null) {
    //    stockWeight.addEventListener('input', (e) => {
    //        var entrada = e.target.value.split(','),
    //            parteEntera = entrada[0].replace(/\./g, ''),
    //            parteDecimal = entrada[1],
    //            salida = parteEntera.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.");

    //        e.target.value = salida + (parteDecimal !== undefined ? ',' + parteDecimal : '');
    //    }, false);
    //}

    //let existence = document.getElementById('Existence');
    //if (existence != null) {
    //    existence.addEventListener('input', (e) => {
    //        var entrada = e.target.value.split(','),
    //            parteEntera = entrada[0].replace(/\./g, ''),
    //            parteDecimal = entrada[1],
    //            salida = parteEntera.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.");

    //        e.target.value = salida + (parteDecimal !== undefined ? ',' + parteDecimal : '');
    //    }, false);
    //}

    //let minimum = document.getElementById('Minimum');
    //if (minimum != null) {
    //    minimum.addEventListener('input', (e) => {
    //        var entrada = e.target.value.split(','),
    //            parteEntera = entrada[0].replace(/\./g, ''),
    //            parteDecimal = entrada[1],
    //            salida = parteEntera.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.");

    //        e.target.value = salida + (parteDecimal !== undefined ? ',' + parteDecimal : '');
    //    }, false);
    //}

    //let quantityToOrder = document.getElementById('QuantityToOrder');
    //if (quantityToOrder != null) {
    //    quantityToOrder.addEventListener('input', (e) => {
    //        var entrada = e.target.value.split(','),
    //            parteEntera = entrada[0].replace(/\./g, ''),
    //            parteDecimal = entrada[1],
    //            salida = parteEntera.replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.");

    //        e.target.value = salida + (parteDecimal !== undefined ? ',' + parteDecimal : '');
    //    }, false);
    //}

    //#endregion






    

  
});