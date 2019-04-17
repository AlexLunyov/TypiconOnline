"use strict";

$(() => {
    if ($('#typiconGrid').length !== 0) {

        /*$('#typiconGrid thead tr:last th').each(function () {
            var label = $('#typiconGrid thead tr:first th:eq(' + $(this).index() + ')').html();
            $(this)
                .addClass('p-0')
                .html('<span class="sr-only">' + label + '</span><input type="search" class="form-control form-control-sm" aria-label="' + label + '" />');
        });*/

        var table = $('#typiconGrid').DataTable({
            language: {
                processing: "Загрузка данных...",
                zeroRecords: "Не найдено ни одного совпадения."
            },
            processing: true,
            serverSide: true,
            orderCellsTop: true,
            autoWidth: true,
            deferRender: true,
            paging: true,
            
            dom: '<tr>',
            ajax: {
                type: "POST",
                url: '/Typicons/LoadData/',
                contentType: "application/json; charset=utf-8",
                async: true,
                headers: {
                    "XSRF-TOKEN": document.querySelector('[name="__RequestVerificationToken"]').value
                },
                data: function (data) {
                    //let additionalValues = [];
                    //additionalValues[0] = "Additional Parameters 1";
                    //additionalValues[1] = "Additional Parameters 2";
                    //data.AdditionalValues = additionalValues;
                    return JSON.stringify(data);
                }
            },
            columns: [
                {
                    title: "Id",
                    data: "Id",
                    visible: false,
                    searchable: false
                },
                {
                    title: "Name",
                    data: "Name",
                    name: "co"
                },
                {
                    title: "Status",
                    data: "Status",
                    name: "co"
                }
                ,{
                    title: "Editable",
                    data: "Editable",
                    visible: false,
                    searchable: false
                }
                ,{
                    title: "Deletable",
                    data: "Deletable",
                    visible: false,
                    searchable: false
                }
                ,{
                    title: "Approvable",
                    data: "Approvable",
                    visible: false,
                    searchable: false
                }
                ,{
                    title: "",
                    render: function (data, type, row) {
                        var str = "";
                        if (row['Approvable']) {
                            str = str + '&nbsp;<a class="btn btn-success" href="/Typicons/Approve/' + row['Id'] + '">Утвердить</>';
                        }
                        if (row['Editable']) {
                            str = str + '&nbsp;<a class="btn btn-primary" href="/Typicons/Edit/' + row['Id'] + '">Редактировать</>';
                        }
                        if (row['Deletable']) {
                            str = str + '&nbsp;<a class="btn btn-danger" href="/Typicons/Delete/' + row['Id'] + '">Удалить</>';
                        }
                        return str;
                    }
                }
            ]
        });

        table.columns().every(function (index) {
            $('#typiconGrid thead tr:last th:eq(' + index + ') input')
                .on('keyup',
                    function (e) {
                        if (e.keyCode === 13) {
                            table.column($(this).parent().index() + ':visible').search(this.value).draw();
                        }
                    });
        });
    }
});