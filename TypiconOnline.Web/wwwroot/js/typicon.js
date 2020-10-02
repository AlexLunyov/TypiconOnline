$(document).ready(function () {
    var table = $("#typiconGrid").DataTable(
        {
            language: {
                processing: "Загрузка данных...",
                zeroRecords: "Не найдено ни одного совпадения."
            },
            processing: false, // for show progress bar
            serverSide: true, // for process server side
            filter: true, // this is for disable filter (search box)
            orderMulti: false, // for disable multiple column at once
            ajax: {
                "url": "/Typicon/LoadData/",
                "type": "POST",
                "datatype": "json"
            },
            columns: [
                {
                    name: "Id",
                    data: "Id",
                    visible: false,
                    searchable: false
                },
                {
                    name: "Наименование",
                    data: "Name",
                },
                {
                    name: "Сист. имя",
                    data: "SystemName",
                },
                {
                    name: "Статус",
                    data: "Status",
                    render: function (data, type, row) {
                        switch (data) {
                            case "WatingForReview":
                                return 'Ожидает проверки';
                            case "InProcess":
                                return 'В процессе обработки... <span class="fa fa-spinner fa-spin"></span>';
                            case "Rejected":
                                return 'Отклонена';
                            case "WaitingApprovement":
                                return 'Ожидает утверждения';
                            case "Approving":
                                return 'Утверждается... <span class="fa fa-spinner fa-spin"></span>';
                            case "Draft":
                                return 'Черновик';
                            case "Validating":
                                return 'В процессе валидации... <span class="fa fa-spinner fa-spin"></span>';
                            case "Publishing":
                                return 'В процессе публикации... <span class="fa fa-spinner fa-spin"></span>';
                            case "Published":
                                return 'Опубликован';
                        }
                    }
                },
                {
                    name: "Шаблон",
                    data: "IsTemplate",
                    render: function (data, type, row) {
                        if (data == null) {
                            return "";
                        }
                        if (data) {
                            str = 'Да';
                        }
                        else {
                            str = 'Нет';
                        }
                        return str;
                    }
                },
                {
                    title: "",
                    render: function (data, type, row) {
                        var str = "";
                        if (row['Reviewable']) {
                            str = str + '&nbsp;<a class="btn btn-success" title="Утвердить Устав" href="/Typicon/Review/' + row['Id'] + '">Рассмотреть</a>';
                        }
                        if (row['Editable']) {
                            str = str + '&nbsp;<a class="btn btn-primary" title="Редактировать Устав" href="/Typicon/Edit/' + row['Id'] + '">Редактировать</a>';
                        }
                        if (row['DeleteLink'] != null) {
                            str = str + '&nbsp;<a href="#" class="btn btn-danger btn-delete" title="Удалить Устав">Удалить</a>';
                        }
                        if (row['Exportable']) {
                            str = str + '&nbsp;<a class="btn btn-success export" href="#" title="Экспортировать Устав" data-toggle="tooltip" class="btn btn-primary">Экспортировать</a>';
                        }
                        return str;
                    },
                    orderable: false
                }
            ]
        });

    setInterval(function () {
        table.processing = true;
        table.ajax.reload(null, false); // user paging is not reset on reload
    }, 3000);

    table.on('click', 'tbody tr td .export', function () {
        var row = table.row($(this).parents('tr'));
        var data = row.data();

        $("#modalWindow").load("/Typicon/ExportGet/" + data.Id);

        $(".typ-modal").modal('show');
    });

    $('#importModBtn').on('click', function () {
        $(".import-modal").modal('show');
    });

    $("#importPostBtn").on("click", function () {
        var postData = new FormData();
        var fileInp = $('#importFile');
        postData.append('file', fileInp[0].files[0]);

        var url = $("#importForm").attr("action");

        $("#importPostBtn").attr("disabled", "disabled");

        var importSpinner = $("#importSpinner");
        var imp = importSpinner.attr("class", "fa fa-spinner fa-spin");
        $("#prImpTxt").removeAttr("hidden");
        fileInp.attr("disabled", "disabled");
        $("#prImpTxt").text("Запущен процесс импортирования...");

        $.ajax({
            type: "POST",
            url: url,
            data: postData, // serializes the form's elements.
            processData: false,  // tell jQuery not to process the data
            contentType: false,  // tell jQuery not to set contentType
            success: function (data) {
                $("#importPostBtn").removeAttr("disabled");
                importSpinner.attr("class", imp);
                fileInp.removeAttr("disabled");

                if (data.success) {
                    $("#prImpTxt").attr("hidden", "hidden");
                    $(".import-modal").modal('hide');
                    table.ajax.reload(null, false); // user paging is not reset on reload
                }
                else {
                    $("#prImpTxt").text(data.msg);
                }
            }
        });
    });

    table.on('click', 'tbody tr td .btn-delete', function () {
        var row = table.row($(this).parents('tr'));
        var data = row.data();

        var link = "typicon/" + data.DeleteLink + "/" + data.Id;

        $("#frmDltTyp").attr("action", link);

        $(".delete-modal").modal('show');
    });
});
