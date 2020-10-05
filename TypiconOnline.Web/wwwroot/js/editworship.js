//$(".add-worship").on('click', function () {
$(document).ready(function () {
    $(function () {
        $('#time').datetimepicker({
            format: 'HH.mm'
        });
    });

    FillTable();

    createItemTextTable("#wNameTable", "Name", "#addwNameBtn");
    createItemTextTable("#wAddNameTable", "AdditionalName", "#addwAddNameBtn");

    $('.add-worship').on('click', function () {
        ClearForm();

        $("#wNameTable tbody").append(DefaultItemTextValue('Name'));

        $(".edit-modal").modal('show');
    }); 

    $("#saveWorshipForm").submit(function (e) {
        $("<input />").attr("type", "hidden")
            .attr("name", "Value")
            .attr("value", JSON.stringify(worshipsData))
            .appendTo("#saveWorshipForm");

        return true;
    });

    //сохранение значения
    $("#editWorship").submit(function (e) {
        e.preventDefault();

        //var form = $(this).closest("form");
        //var form = document.getElementById("editWorship");
        //$(this).removeData("validator")    // Added by jQuery Validation
        //    .removeData("unobtrusiveValidation");   // Added by jQuery Unobtrusive Validation
        //$.validator.unobtrusive.parse(this);

        if (!$(this).valid()) {
            return false;
        }

        var sData = new Object();

        sData.Time = $("#wTime").val();
        sData.Mode = parseInt($("#wMode").val());

        sData.Name = GetNameData("#wName");
        sData.AdditionalName = GetNameData("#wAddName");

        const index = $("#editWorship").attr("worship-id");

        if (index != null) {
            worshipsData[index] = sData;
        }
        else {
            worshipsData.push(sData);
        }

        FillTable();

        $(".edit-modal").modal('hide');
    });
});

function FillTable() {
    $("#worshipsGrid tbody").empty();

    if (worshipsData != null
        && worshipsData.length > 0) {
        $.each(worshipsData, function (index) {
            this.Id = index;
            var tr = "<tr>"
                + "<td>" + this.Time + "</td>"
                + "<td>" + GetMode(this.Mode) + "</td>"
                + '<td class="' + GetClassOfName(this.Name) + '">' + GetText(this.Name) + "</td>"
                + '<td class="' + GetClassOfName(this.AdditionalName) + '">' + GetText(this.AdditionalName) + "</td>"
                + '<td>'
                + '<a class="icon-btn-edit edit-worship" data-toggle="tooltip" data-original-title="Редактировать"><i class="fas fa-pen"></i></a> '
                + getUdDownDeleteBtns()
                //+ '<a class="icon-btn-danger delete-worship" data-toggle="tooltip" data-original-title="Удалить"><i class="fas fa-trash"></i></a>'
                + '</td>' +
                + "</tr>";

            $("#worshipsGrid tbody").append(tr);
        });
    }
    else {
        var tr = "<tr>"
            + '<td colspan="5" class="text-center">Данные отсутствуют.</td>'
            + "</tr>";

        $("#worshipsGrid tbody").append(tr);
    }

    calcBtns("#worshipsGrid");
}

function GetText(text) {
    if (text == null) {
        return "";
    }
    return text.Items[0].Text;
}

function GetClassOfName(text) {
    var str = "";

    if (text != null) {
        if (text.IsRed) {
            str += "red "
        }
        if (text.IsBold) {
            str += "bold "
        }
        if (text.IsItalic) {
            str += "italic "
        }
    }

    return str;
}

function GetMode(mode) {
    switch (mode) {
        case 0:
            return "Накануне";
        case 1:
            return "В самый день";
        default:
            return "В самый день";
    }
}

$(document).on("click", ".up", function () {
    MoveRow(this, -1);
});

$(document).on("click", ".down", function () {
    MoveRow(this, 1);
});

function MoveRow(element, index) {
    const tri = $(element).parents("tr").index();
    //раздираемся с данными
    move(worshipsData, tri, index);
    //теперь с отображением в таблице
    $(element).parents("tr").find("td:not(:last-child)").each(function (i, td1) {
        //находим следующее значение
        var v1 = $(td1).html();
        var style1 = $(td1).attr("class");
        var td2 = $("#worshipsGrid tbody tr").eq(tri + index).children().eq($(td1).index());
        //меняем все значения в td
        $(td1).html(td2.html());
        td2.html(v1);
        $(td1).attr("class", td2.attr("class"));
        td2.attr("class", style1);
    });
}

$(document).on("click", ".delete", function () {
    const i = $(this).parents("tr").index();
    $(this).parents("tr").remove();

    worshipsData.splice(i, 1);

    FillTable();
});

$(document).on("click", ".edit-worship", function () {
    ClearForm();
    FillForm($(this).parents("tr").index());

    $(".edit-modal").modal('show');
});

function ClearForm() {
    ClearTable("#wName");
    ClearTable("#wAddName");

    $("#editWorship").removeAttr("worship-id");
}

function ClearTable(idPrefix) {
    $(idPrefix + "Table tbody").empty();

    $(idPrefix + "IsRed").attr('checked', false);
    $(idPrefix + "IsBold").attr('checked', false);
    $(idPrefix + "IsItalic").attr('checked', false);
}

function DefaultItemTextValue(name) {
    return "<tr>"
            + '<td class="language">cs-ru</td>'
            + '<td class="text">Время богослужения</td>'
            + '<td class="text">' + getActionsCell(name) + '</td>'
            + "</tr>";
}

function FillForm(index) {

    const sData = worshipsData[index];

    $("#wTime").val(sData.Time);
    $("#wMode").val(sData.Mode);

    SetNameData("#wName", sData.Name);
    SetNameData("#wAddName", sData.AdditionalName);

    $("#editWorship").attr("worship-id", index);
}

function GetNameData(idPrefix) {
    if ($(idPrefix + "Table tbody tr").length > 0) {
        var d = new Object();

        d.Items = [];

        $.each($(idPrefix + "Table tbody tr"), function () {
            var item = new Object();
            item.Language = $(this).children().eq(0).html();
            item.Text = $(this).children().eq(1).html();
            d.Items.push(item);
        });
        d.IsRed = $(idPrefix + "IsRed").is(":checked");
        d.IsBold = $(idPrefix + "IsBold").is(":checked");
        d.IsItalic = $(idPrefix + "IsItalic").is(":checked");

        return d;
    }
    
    return null;
}

function SetNameData(idPrefix, sData) {
    if (sData != null) {
        $.each(sData.Items, function (i) {
            var tr = "<tr>"
                + '<td class="language">'+ this.Language+'</td>'
                + '<td class="text">' + this.Text + '</td>'
                + '<td class="text">' + getActionsCell(idPrefix) + '</td>'
                + "</tr>";

            $(idPrefix + "Table tbody").append(tr);
        });

        $(idPrefix + "IsRed")[0].checked = sData.IsRed;
        $(idPrefix + "IsBold")[0].checked = sData.IsBold;
        $(idPrefix + "IsItalic")[0].checked = sData.IsItalic;
    }
}

