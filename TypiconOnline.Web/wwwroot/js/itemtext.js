function createItemTextTable(itemTextId, itemTextName, addbtnId = ".add-new") {
    $('[data-toggle="tooltip"]').tooltip();
    //var actions = $(itemTextId + " td:last-child").html();
    var actions = getActionsCell(itemTextName);
    // Append table with add row form on add new button click
    $(addbtnId).click(function () {
        $(this).attr("disabled", "disabled");
        var index = $(itemTextId + " tbody tr:last-child").index();
        var row = '<tr>' +
            '<td class="language"><input type="text" class="form-control" name="Language" id="language"></td>' +
            '<td class="text"><input type="text" class="form-control" name="Text" id="text"></td>' +
            '<td>' + actions + '</td>' +
            '</tr>';
        $(itemTextId).append(row);
        $(itemTextId + " tbody tr").eq(index + 1).find(".add, .edit").toggle();
        $(itemTextId + " tbody tr:last-child input.text").attr("name", itemTextName + ".Items[" + (index + 1) + "].Text");
        $(itemTextId + " tbody tr:last-child input.language").attr("name", itemTextName + ".Items[" + (index + 1) + "].Language");
        $('[data-toggle="tooltip"]').tooltip();
    });
    // Add row on add button click
    $(itemTextId).on("click", ".add", function () {
        var empty = false;
        var input = $(this).parents("tr").find('input[type="text"]');
        input.each(function () {
            if (!$(this).val()) {
                $(this).addClass("error");
                empty = true;
            } else {
                $(this).removeClass("error");
            }
        });
        $(this).parents("tr").find(".error").first().focus();
        if (!empty) {
            input.each(function () {
                $(this).parent("td").parent("tr").find("td input." + $(this).attr("id")).val($(this).val());
                $(this).parent("td").html($(this).val());
            });
            $(this).parents("tr").find(".add, .edit").toggle();
            $(addbtnId).removeAttr("disabled");
        }
    });
    // Edit row on edit button click
    $(itemTextId).on("click", ".edit", function () {
        $(this).parents("tr").find("td:not(:last-child)").each(function () {
            $(this).html('<input type="text" class="form-control" value="' + $(this).text() + '" id="' + $(this).attr("class") + '">');
        });
        $(this).parents("tr").find(".add, .edit").toggle();
        $(addbtnId).attr("disabled", "disabled");
    });
    // Delete row on delete button click
    $(itemTextId).on("click", ".delete", function () {
        $(this).parents("tr").remove();
        $(addbtnId).removeAttr("disabled");

        $(itemTextId + " tbody tr").each(function (i) {
            $("td:last-child input.text").attr("name", itemTextName + ".Items[" + i + "].Text");
            $("td:last-child input.language").attr("name", itemTextName + ".Items[" + i + "].Language");
        })
    });
}

function getActionsCell(itemTextName) {
    return '<input class="language" type="hidden" asp-for="' + itemTextName + '.Items[i].Language" />'
        + '<input class="text" type = "hidden" asp -for= "' + itemTextName + '.Items[i].Text" />'
        + '<a class="add" title="Add" data-toggle="tooltip"><i class="material-icons">&#xE03B;</i></a>'
        + '<a class="edit" title="Edit" data-toggle="tooltip"><i class="material-icons">&#xE254;</i></a>'
        + '<a class="delete" title="Delete" data-toggle="tooltip"><i class="material-icons">&#xE872;</i></a>';
}