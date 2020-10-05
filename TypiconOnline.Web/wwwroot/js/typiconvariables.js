$(document).ready(function () {
    const id = $("#varGrid").attr("data-id");
    var table = $("#varGrid").DataTable(
        {
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "language": {
                "url": "/lib/datatables/russian.json"
            },
            "ajax": {
                "url": "/TypiconVariables/LoadData/" + id,
                "type": "POST",
                "datatype": "json"
            },
            "columnDefs":
                [{
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                }],
            "columns": [
                { "data": "Id", "name": "Id", "autoWidth": true },
                { "data": "Name", "name": "Name", "autoWidth": true },
                {
                    "data": "Type", "name": "Type", "autoWidth": true,
                    "render": function (data, type, row) {
                        switch (data) {
                            case 0:
                                return "Дата";
                            case 1:
                                return "Время";
                            case 2:
                                return "Целочисленное";
                            case 3:
                                return "Строка";
                            case 4:
                                return "Многоязыковая строка";
                            case 5:
                                return "Службы";
                            default:
                                return "Переменная";
                        }
                    }
                },
                { "data": "Count", "name": "Count", "autoWidth": true },
                {
                    "data": "HasValue", "name": "HasValue", "autoWidth": true,
                    "render": function (data, type, row) {
                        if (data) {
                            return '<i class="fas fa-check icon-btn-success"></i>';
                        }
                        else {
                            return '<i class="fas fa-times icon-btn-danger"></i>';
                        }
                    }
                },
                {
                    data: null, render: function (data, type, row) {
                        return '<a class="btn btn-primary" title="Редактировать" href="/TypiconVariables/Edit/' + row['Id'] + '">Редактировать</a>';
                    },
                    orderable: false
                },
            ]
        }
    );

    //table.on('click', 'tbody tr td .edit', function () {
    //    var row = table.row($(this).parents('tr'));
    //    var data = row.data();

    //    $("#modalWindow").load("/TypiconVariables/Edit/" + data.Id);

    //    $(".editvar-modal").modal('show');
    //});

    //table.on('click', 'tbody tr td .define', function () {
    //    var row = table.row($(this).parents('tr'));
    //    var data = row.data();

    //    $("#modalWindow").load("/TypiconVariables/Define/" + data.Id);

    //    $(".editvar-modal").modal('show');
    //});

    table.on('draw', function () {
        //redraw VariablesCount
        //var count = table.page.info().recordsTotal;
        //var span = $('#varCountSpan');
        //if (!count || count == 0) {
        //    span.hide();
        //}
        //else {
        //    span.html(count);
        //    span.show();
        //}
    });
});

function submitForm(e) {
    e.preventDefault(); // avoid to execute the actual submit of the form.

    var form = $("#editVarForm");
    var url = form.attr("action");

    $.ajax({
        type: "POST",
        url: url,
        data: form.serialize(), // serializes the form's elements.
        success: function (data) {
            if (data == null) {
                $('#varGrid').DataTable().draw(false);
                $(".editvar-modal").modal('hide');
            }
            else {
                alert(data);
            }
        }
    });

    return false;
}