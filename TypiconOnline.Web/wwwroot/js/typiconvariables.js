$(document).ready(function () {
    const id = $("#varGrid").attr("data-id");
    var table = $("#varGrid").DataTable(
        {
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
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
                { "data": "Type", "name": "Type", "autoWidth": true },
                { "data": "Count", "name": "Count", "autoWidth": true },
                {
                    data: null, render: function (data, type, row) {
                        return '<a class="edit" href="#" title="Редактировать описание" data-toggle="tooltip"><i class="material-icons">chat_bubble</i></a> ' +
                            '<a class="define" href="#" title="Задать значение Переменной" data-toggle="tooltip"><i class="material-icons">low_priority</i></a></a>';
                    },
                    orderable: false
                },
            ]
        }
    );

    table.on('click', 'tbody tr td .edit', function () {
        var row = table.row($(this).parents('tr'));
        var data = row.data();

        $("#modalWindow").load("/TypiconVariables/Edit/" + data.Id);

        $(".editvar-modal").modal('show');
    });

    table.on('click', 'tbody tr td .define', function () {
        var row = table.row($(this).parents('tr'));
        var data = row.data();

        $("#modalWindow").load("/TypiconVariables/Define/" + data.Id);

        $(".editvar-modal").modal('show');
    });

    table.on('draw', function () {
        //redraw VariablesCount
        var count = table.page.info().recordsTotal;
        var span = $('#varCountSpan');
        if (!count || count == 0) {
            span.hide();
        }
        else {
            span.html(count);
            span.show();
        }
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