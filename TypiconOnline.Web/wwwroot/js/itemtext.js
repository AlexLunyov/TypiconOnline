function createItemTextTable(itemTextId, itemTextName) {
    $('[data-toggle="tooltip"]').tooltip();
    var actions = $(itemTextId + " td:last-child").html();
    // Append table with add row form on add new button click
    $(".add-new").click(function () {
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
    $(document).on("click", ".add", function () {
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
            $(".add-new").removeAttr("disabled");
        }
    });
    // Edit row on edit button click
    $(document).on("click", ".edit", function () {
        $(this).parents("tr").find("td:not(:last-child)").each(function () {
            $(this).html('<input type="text" class="form-control" value="' + $(this).text() + '" id="' + $(this).attr("class") + '">');
        });
        $(this).parents("tr").find(".add, .edit").toggle();
        $(".add-new").attr("disabled", "disabled");
    });
    // Delete row on delete button click
    $(document).on("click", ".delete", function () {
        $(this).parents("tr").remove();
        $(".add-new").removeAttr("disabled");

        $(itemTextId + " tbody tr").each(function (i) {
            $("td:last-child input.text").attr("name", itemTextName + ".Items[" + i + "].Text");
            $("td:last-child input.language").attr("name", itemTextName + ".Items[" + i + "].Language");
        })
    });
}