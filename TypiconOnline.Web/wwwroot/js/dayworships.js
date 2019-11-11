function createDayWorsipsTable(tableId, entityName, properties) {
    $('[data-toggle="tooltip"]').tooltip();
    //вычисляем кнопки вверх и вниз
    calcBtns(tableId);

    // Клик на кнопку "Переместить вниз"
    $(document).on("click", ".down", function () {
        var tri = $(this).parents("tr").index();
        $(this).parents("tr").find("td:not(:first-child, :last-child)").each(function (i) {
            //находим следующее значение
            var v1 = $(this).html();
            var td2 = $(tableId + " tbody tr").eq(tri + 1).children().eq($(this).index());
            //меняем все значения в td
            $(this).html(td2.html());
            td2.html(v1);
        });
        //меняем значения в input-hidden
        var id1 = $(this).parent().find(".WorshipId").val();
        var id2 = $(tableId + " tbody tr").eq(tri + 1).find("td input.WorshipId");
        $(this).parent().find(".WorshipId").val(id2.val());
        id2.val(id1);
    });

    // Клик на кнопку "Переместить вверх"
    $(document).on("click", ".up", function () {
        var tri = $(this).parents("tr").index();
        $(this).parents("tr").find("td:not(:first-child, :last-child)").each(function (i) {
            //находим следующее значение
            var v1 = $(this).html();
            var td2 = $(tableId + " tbody tr").eq(tri - 1).children().eq($(this).index());
            //меняем все значения в td
            $(this).html(td2.html());
            td2.html(v1);
        });
        //меняем значения в input-hidden
        var id1 = $(this).parent().find(".WorshipId").val();
        var id2 = $(tableId + " tbody tr").eq(tri - 1).find("td input.WorshipId");
        $(this).parent().find(".WorshipId").val(id2.val());
        id2.val(id1);
    });

    // Delete row on delete button click
    $(document).on("click", ".delete", function () {
        $(this).parents("tr").remove();


        calcBtns(tableId);
        calcAddBtn("#dayworships", ".add-worship");
        //вычисляем номер по порядку
        $(tableId + " tbody tr").each(function (i) {
            $(this).find("td:first-child").html(i + 1);
            var td = $(this).find("td:last-child");
            td.find("input.Order").val(i + 1);

            //hidden names
            $.each(properties, function (index, value) {
                td.find("input." + value).attr("name", entityName + "[" + i + "]." + value);
            });
        });
    });
}

//вычисляем кнопки вверх и вниз
function calcBtns(tableId) {
    $(tableId + " tbody tr:not(:first-child, :last-child)").each(function () {
        $(this).find("td:last-child a.up").show();
        $(this).find("td:last-child a.down").show();
        $(this).find("td:last-child a.delete").show();
    });

    if ($(tableId + " tbody tr").length > 1) {
        $(tableId + " tbody tr:first-child td:last-child a.up").hide();
        $(tableId + " tbody tr:first-child td:last-child a.down").show();

        $(tableId + " tbody tr:last-child td:last-child a.up").show();
        $(tableId + " tbody tr:last-child td:last-child a.down").hide();

        $(tableId + " tbody tr:first-child td:last-child a.delete").show();
        $(tableId + " tbody tr:last-child td:last-child a.delete").show();
    }
    else {
        $(tableId + " tbody tr:first-child td:last-child a.up").hide();
        $(tableId + " tbody tr:first-child td:last-child a.down").hide();
        //$(tableId + " tbody tr:last-child td:last-child a.delete").hide();
    }
}

function calcAddBtn(tableId, btn) {
    var rowCount = $(tableId + ' tbody tr').length;
    if (rowCount >= 3) {
        $(btn).attr("disabled", "disabled");
    }
    else {
        $(btn).removeAttr("disabled");
    }
}

function getWorshipActionsCell(name) {
    return '<input class="WorshipId" type="hidden" asp-for="' + name + '.DayWorships[i].WorshipId" />' +
            '<input class="Order" type = "hidden" asp -for= "' + name + '.DayWorships[i].Order" />' +
            '<a class="down" title="Переместить вниз" data-toggle="tooltip"><i class="material-icons">arrow_drop_down</i></a>' +
            '<a class="up" title="Переместить вверх" data-toggle="tooltip"><i class="material-icons">arrow_drop_up</i></a>' +
            '<a class="delete" title="Удалить" data-toggle="tooltip"><i class="material-icons">delete</i></a>';
}