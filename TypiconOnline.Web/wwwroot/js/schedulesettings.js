document.addEventListener("DOMContentLoaded", function (event) {

    var id = $("#editDays").attr("data-id")

    $("#editDays").on('click', function () {
        

        // AJAX request
        $.ajax({
            url: '/ScheduleSettings/EditDays',
            type: 'get',
            data: { id: id },
            success: function (response) {
                $("#modalWindow").html(response);

                $(".edit-modal").modal('show');

                $("#editDaysForm").submit(editDaysSubmit);
            }
        });
    });

    CreateSignTable(id);
    CreateMenologyTable(id);
    CreateTriodionTable(id);
    //CreateIncludeTable(id);
})

function editDaysSubmit(e) {
    e.preventDefault(); // avoid to execute the actual submit of the form.

    var form = $("#editDaysForm");
    var url = form.attr("action");

    $.ajax({
        type: "POST",
        url: url,
        data: form.serialize(), // serializes the form's elements.
        success: function (data) {
            $("#daysContainer").html(data);

            $(".edit-modal").modal('hide');
        }
    });

    return false;
}

function CreateSignTable(id) {
    var table = $("#signGrid").DataTable(
        {
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "language": {
                "url": "/lib/datatables/russian.json"
            },
            "ajax": {
                "url": "/ScheduleSettings/LoadSigns/",
                "type": "POST",
                "data": { id: id },
                "datatype": "json"
            },
            "columnDefs":
                [{
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                },
                {
                    targets: -1,
                    className: 'dt-align-right'
                }],
            "columns": [
                { "data": "Id", "name": "Id", "autoWidth": true },
                { "data": "Name", "name": "Name", "autoWidth": true },
                { "data": "TemplateName", "name": "TemplateName", "autoWidth": true },
                {
                    data: null, "width": "25%", render: function (data, type, row) {
                        return '<a href="/Sign/Edit/' + row.Id + '" target="_blank" class="btn btn-info" data-toggle="tooltip" data-original-title="Редактировать"><i class="fas fa-pen"></i> Редактировать</a>'
                            + ' <a href="#" class="btn btn-danger sign-delete" data-toggle="tooltip" data-original-title="Исключить"><i class="fas fa-trash"></i> Исключить</a>';
                    },
                    orderable: false
                },
            ]
        }
    );

    table.on('click', 'tbody tr td .sign-delete', function () {
        //var table = $('#signGrid').DataTable();
        var row = table.row($(this).parents('tr'));
        var data = row.data();

        if (confirm('Вы действительно хотите исключить Знак службы "' + data.Name + '"?')) {

            $.post("/ScheduleSettings/DeleteSign/" + data.Id, function (result) {
                if (result == "") {
                    row.remove();
                }
                else {
                    alert("Что-то пошло не так.");
                }
                table.draw(false);
            });
        }
        else {
            return false;
        }
    });

    $("#addSign").on('click', function () {
        // AJAX request
        $.ajax({
            url: '/ScheduleSettings/AddSign/' + id,
            type: 'get',
            success: function (response) {
                $("#modalWindow").html(response);

                $(".edit-modal").modal('show');

                $("#addSignGrid").DataTable(
                    {
                        "processing": true, // for show progress bar
                        "serverSide": true, // for process server side
                        "filter": true, // this is for disable filter (search box)
                        "orderMulti": false, // for disable multiple column at once
                        "language": {
                            "url": "/lib/datatables/russian.json"
                        },
                        "ajax": {
                            "url": "/ScheduleSettings/LoadAddSignGrid/",
                            "type": "POST",
                            "data": { id: id },
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
                                data: null, render: function (data, type, row) {
                                    return '<a href="#" class="btn-add" data-toggle="tooltip" data-original-title="Добавить"><i class="fas fa-plus-circle"></i></a>';
                                },
                                orderable: false
                            },
                        ]
                    }
                );

                $('#addSignGrid').on('click', 'tbody tr td .btn-add', function () {
                    var table = $('#addSignGrid').DataTable();
                    var row = table.row($(this).parents('tr'));
                    var data = row.data();

                    $.post("/ScheduleSettings/AddSign/" + id + "/" + data.Id, function (result) {
                        if (result == "") {
                            $("#signGrid").DataTable().draw(false);
                        }
                        else {
                            alert("Что-то пошло не так.");
                        }

                        $(".edit-modal").modal('hide');
                    });
                });
            }
        });
    });
}

function CreateMenologyTable(id) {
    var table = $("#menologyGrid").DataTable(
        {
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "language": {
                "url": "/lib/datatables/russian.json"
            },
            "ajax": {
                "url": "/ScheduleSettings/LoadMenology/",
                "type": "POST",
                "data": { id: id },
                "datatype": "json"
            },
            "columnDefs":
                [{
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                },
                {
                    targets: -1,
                    className: 'dt-align-right'
                }],
            "columns": [
                { "data": "Id", "name": "Id", "autoWidth": true },
                { "data": "Name", "name": "Name", "autoWidth": true },
                { "data": "TemplateName", "name": "TemplateName", "autoWidth": true },
                { "data": "Date", "name": "Date", "autoWidth": true },
                { "data": "LeapDate", "name": "LeapDate", "autoWidth": true },
                {
                    data: null, "width": "25%", render: function (data, type, row) {
                        return '<a href="/MenologyRule/Edit/' + row.Id + '" target="_blank" class="btn btn-info" data-toggle="tooltip" data-original-title="Редактировать"><i class="fas fa-pen"></i> Редактировать</a>'
                            + ' <a href="#" class="btn btn-danger menology-delete" data-toggle="tooltip" data-original-title="Исключить"><i class="fas fa-trash"></i> Исключить</a>';
                    },
                    orderable: false
                },
            ]
        }
    );

    table.on('click', 'tbody tr td .menology-delete', function () {
        //var table = $('#signGrid').DataTable();
        var row = table.row($(this).parents('tr'));
        var data = row.data();

        if (confirm('Вы действительно хотите исключить Минею "' + data.Name + '"?')) {

            $.post("/ScheduleSettings/DeleteMenology/" + data.Id, function (result) {
                if (result == "") {
                    row.remove();
                }
                else {
                    alert("Что-то пошло не так.");
                }
                table.draw(false);
            });
        }
        else {
            return false;
        }
    });

    $("#addMenology").on('click', function () {
        // AJAX request
        $.ajax({
            url: '/ScheduleSettings/AddMenology/' + id,
            type: 'get',
            success: function (response) {
                $("#modalWindow").html(response);

                $(".edit-modal").modal('show');

                var defaultDate = moment().set('year', 2016).format("YYYY-MM-DD");

                $('#datepicker').datetimepicker({
                    locale: 'ru',
                    dayViewHeaderFormat: "MMMM",
                    format: "DD MMMM",
                    viewMode: "days",
                    maxDate: "2016-12-31",
                    minDate: "2016-01-01",
                    useCurrent: false,
                    inline: true,
                    defaultDate: defaultDate
                })
                .on('dp.change', function (e) {

                    $('.nodate').removeClass('active');
                    $("#addMenologyGrid").DataTable().ajax.url('/ScheduleSettings/LoadMenologyToAddGrid/' + id + '/' + moment(e.date).format("YYYY-MM-DD")).load();
                })

                //кнопка для загрузки переходящих праздников
                $('#btnNoDate').on('click', function () {
                    $('#datepicker').data("DateTimePicker").clear();
                    $(this).addClass('active');

                    //задаем значение таблице
                    $("#addMenologyGrid").DataTable().ajax.url('/ScheduleSettings/LoadMenologyToAddGrid/' + id).load();
                });

                $("#addMenologyGrid").DataTable(
                    {
                        "processing": true, // for show progress bar
                        "serverSide": true, // for process server side
                        "filter": true, // this is for disable filter (search box)
                        "orderMulti": false, // for disable multiple column at once
                        "language": {
                            "url": "/lib/datatables/russian.json"
                        },
                        "ajax": {
                            "url": "/ScheduleSettings/LoadMenologyToAddGrid/" + id + "/" + defaultDate,
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
                            { "data": "TemplateName", "name": "TemplateName", "autoWidth": true },
                            { "data": "Date", "name": "Date", "autoWidth": true },
                            { "data": "LeapDate", "name": "LeapDate", "autoWidth": true },
                            {
                                data: null, render: function (data, type, row) {
                                    return '<a href="#" class="btn-add" data-toggle="tooltip" data-original-title="Добавить"><i class="fas fa-plus-circle"></i></a>';
                                },
                                orderable: false
                            },
                        ]
                    }
                );

                $('#addMenologyGrid').on('click', 'tbody tr td .btn-add', function () {
                    var table = $('#addMenologyGrid').DataTable();
                    var row = table.row($(this).parents('tr'));
                    var data = row.data();

                    $.post("/ScheduleSettings/AddMenology/" + id + "/" + data.Id, function (result) {
                        if (result == "") {
                            $("#menologyGrid").DataTable().draw(false);
                        }
                        else {
                            alert("Что-то пошло не так.");
                        }

                        $(".edit-modal").modal('hide');
                    });
                });
            }
        });
    });
}

function CreateTriodionTable(id) {
    var table = $("#triodionGrid").DataTable(
        {
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "language": {
                "url": "/lib/datatables/russian.json"
            },
            "ajax": {
                "url": "/ScheduleSettings/LoadTriodion/",
                "type": "POST",
                "data": { id: id },
                "datatype": "json"
            },
            "columnDefs":
                [{
                    "targets": [0],
                    "visible": false,
                    "searchable": false
                },
                {
                    targets: -1,
                    className: 'dt-align-right'
                }],
            "columns": [
                { "data": "Id", "name": "Id", "autoWidth": true },
                { "data": "Name", "name": "Name", "autoWidth": true },
                { "data": "TemplateName", "name": "TemplateName", "autoWidth": true },
                { "data": "DaysFromEaster", "name": "DaysFromEaster", "autoWidth": true },
                {
                    data: null, "width": "25%", render: function (data, type, row) {
                        return '<a href="/TriodionRule/Edit/' + row.Id + '" target="_blank" class="btn btn-info" data-toggle="tooltip" data-original-title="Редактировать"><i class="fas fa-pen"></i> Редактировать</a>'
                            + ' <a href="#" class="btn btn-danger triodion-delete" data-toggle="tooltip" data-original-title="Исключить"><i class="fas fa-trash"></i> Исключить</a>';
                    },
                    orderable: false
                },
            ]
        }
    );

    table.on('click', 'tbody tr td .triodion-delete', function () {
        var row = table.row($(this).parents('tr'));
        var data = row.data();

        if (confirm('Вы действительно хотите удалить Триодь "' + data.Name + '"?')) {

            $.post("/ScheduleSettings/DeleteTriodion/" + data.Id, function (result) {
                if (result == "") {
                    row.remove();
                }
                else {
                    alert("Что-то пошло не так.");
                }
                table.draw(false);
            });
        }
        else {
            return false;
        }
    });

    $("#addTriodion").on('click', function () {
        // AJAX request
        $.ajax({
            url: '/ScheduleSettings/AddTriodion/' + id,
            type: 'get',
            success: function (response) {
                $("#modalWindow").html(response);

                $(".edit-modal").modal('show');

                var days = $("#daysFE").val();

                $("#daysFE").change(function () {
                    var d = $(this).val();
                    $("#addTriodionGrid").DataTable().ajax.url('/ScheduleSettings/LoadTriodionToAddGrid/' + id + '/' + d).load();
                });

                //кнопка для загрузки всех дней Триоди
                $('#btnAllDays').on('click', function () {
                    $("#daysFE").val("");
                    $(this).addClass('active');

                    //задаем значение таблице
                    $("#addTriodionGrid").DataTable().ajax.url('/ScheduleSettings/LoadTriodionToAddGrid/' + id).load();
                });

                $("#addTriodionGrid").DataTable(
                    {
                        "processing": true, // for show progress bar
                        "serverSide": true, // for process server side
                        "filter": true, // this is for disable filter (search box)
                        "orderMulti": false, // for disable multiple column at once
                        "language": {
                            "url": "/lib/datatables/russian.json"
                        },
                        "ajax": {
                            "url": "/ScheduleSettings/LoadTriodionToAddGrid/" + id + "/" + days,
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
                            { "data": "TemplateName", "name": "TemplateName", "autoWidth": true },
                            { "data": "DaysFromEaster", "name": "DaysFromEaster", "autoWidth": true },
                            {
                                data: null, render: function (data, type, row) {
                                    return '<a href="#" class="btn-add" data-toggle="tooltip" data-original-title="Добавить"><i class="fas fa-plus-circle"></i></a>';
                                },
                                orderable: false
                            },
                        ]
                    }
                );

                $('#addTriodionGrid').on('click', 'tbody tr td .btn-add', function () {
                    var table = $('#addTriodionGrid').DataTable();
                    var row = table.row($(this).parents('tr'));
                    var data = row.data();

                    $.post("/ScheduleSettings/AddTriodion/" + id + "/" + data.Id, function (result) {
                        if (result == "") {
                            $("#triodionGrid").DataTable().draw(false);
                        }
                        else {
                            alert("Что-то пошло не так.");
                        }

                        $(".edit-modal").modal('hide');
                    });
                });
            }
        });
    });
}

function CreateIncludeTable(id) {
    var table = $("#includeGrid").DataTable(
        {
            "processing": true, // for show progress bar
            "serverSide": true, // for process server side
            "filter": true, // this is for disable filter (search box)
            "orderMulti": false, // for disable multiple column at once
            "language": {
                "url": "/lib/datatables/russian.json"
            },
            "data": includedDates,
            "columns": [
                {
                    "data": "Date", "name": "Дата", "autoWidth": true,
                    render: function (data, type, row) {
                        return moment(data).format("YYYY-MM-DD");
                    }
                },
                {
                    data: null, render: function (data, type, row) {
                        return '<a href="#" class="icon btn-delete" data-toggle="tooltip" data-original-title="Удалить"><i class="fas fa-trash"></i></a>';
                    },
                    orderable: false
                },
            ]
        }
    );

    table.on('click', 'tbody tr td .btn-delete', function () {
        var row = table.row($(this).parents('tr'));
        var data = row.data();

        if (confirm('Вы действительно хотите удалить Включенную дату "' + data.Date + '"?')) {

            $.post("/ScheduleSettings/DeleteIncludedDate/" + data.Date, function (result) {
                if (result == "") {
                    row.remove();
                }
                else {
                    alert("Что-то пошло не так.");
                }
                //table.draw(false);
            });
        }
        else {
            return false;
        }
    });

    $('#addInclude').datetimepicker({
        locale: 'ru',
        format: 'DD MMMM YYYY года',
        defaultDate: new Date()
    })
        .on('dp.change', function (e) {
            if (e.date != null) {
                $('#addInclude_date').attr("value", moment(e.date).format("YYYY-MM-DD"));
            }
            else {
                $('#addInclude_date').attr("value", "");
            }

        });

    $("#addInclude").on('click', function () {
        // AJAX request
        $.ajax({
            url: '/ScheduleSettings/AddIncludedDate/' + id,
            type: 'get',
            success: function (response) {
                $("#modalWindow").html(response);

                $(".edit-modal").modal('show');

                var defaultDate = moment().set('year', 2016).format("YYYY-MM-DD");

                $('#datepicker').datetimepicker({
                    locale: 'ru',
                    dayViewHeaderFormat: "MMMM",
                    format: "DD MMMM",
                    viewMode: "days",
                    maxDate: "2016-12-31",
                    minDate: "2016-01-01",
                    useCurrent: false,
                    inline: true,
                    defaultDate: defaultDate
                })
                    .on('dp.change', function (e) {

                        $('.nodate').removeClass('active');
                        $("#addMenologyGrid").DataTable().ajax.url('/ScheduleSettings/LoadMenologyToAddGrid/' + id + '/' + moment(e.date).format("YYYY-MM-DD")).load();
                    })

                //кнопка для загрузки переходящих праздников
                $('#btnNoDate').on('click', function () {
                    $('#datepicker').data("DateTimePicker").clear();
                    $(this).addClass('active');

                    //задаем значение таблице
                    $("#addMenologyGrid").DataTable().ajax.url('/ScheduleSettings/LoadMenologyToAddGrid/' + id).load();
                });

                $("#addMenologyGrid").DataTable(
                    {
                        "processing": true, // for show progress bar
                        "serverSide": true, // for process server side
                        "filter": true, // this is for disable filter (search box)
                        "orderMulti": false, // for disable multiple column at once
                        "ajax": {
                            "url": "/ScheduleSettings/LoadMenologyToAddGrid/" + id + "/" + defaultDate,
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
                            { "data": "TemplateName", "name": "TemplateName", "autoWidth": true },
                            { "data": "IsAddition", "name": "IsAddition", "autoWidth": true },
                            { "data": "Date", "name": "Date", "autoWidth": true },
                            { "data": "LeapDate", "name": "LeapDate", "autoWidth": true },
                            {
                                data: null, render: function (data, type, row) {
                                    return '<a href="#" class="btn-add" data-toggle="tooltip" data-original-title="Добавить"><i class="fas fa-plus-circle"></i></a>';
                                },
                                orderable: false
                            },
                        ]
                    }
                );

                $('#addMenologyGrid').on('click', 'tbody tr td .btn-add', function () {
                    var table = $('#addMenologyGrid').DataTable();
                    var row = table.row($(this).parents('tr'));
                    var data = row.data();

                    $.post("/ScheduleSettings/AddMenology/" + id + "/" + data.Id, function (result) {
                        if (result == "") {
                            $("#menologyGrid").DataTable().draw(false);
                        }
                        else {
                            alert("Что-то пошло не так.");
                        }

                        $(".edit-modal").modal('hide');
                    });
                });
            }
        });
    });
}