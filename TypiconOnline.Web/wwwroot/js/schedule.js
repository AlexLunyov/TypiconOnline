var timerId = null;

const MONTHS = ['января', 'февраля', 'марта', 'апреля', 'мая', 'июня', 'июля', 'августа', 'сентября', 'октября',
    'ноября', 'декабря',]

const DAYS = ['ВОСКРЕСЕНЬЕ', 'ПОНЕДЕЛЬНИК', 'ВТОРНИК', 'СРЕДА', 'ЧЕТВЕРГ', 'ПЯТНИЦА', 'СУББОТА']

document.addEventListener("DOMContentLoaded", function (event) {

    var btnNext = $("#btnNext");
    var btnView = $("#btnView");
    var spanView = $("#spanView");
    var btnDownload = $("#btnDownload");

    $('#datepicker').datetimepicker({
        locale: 'ru',
        format: 'DD MMMM YYYY года',
        defaultDate: new Date()
    })
        .on('dp.change', function (e) {
            if (e.date != null) {
                $('#dtp_date').attr("value", moment(e.date).format("YYYY-MM-DD"));
            }
            else {
                $('#dtp_date').attr("value", "");
            }

            btnView.removeAttr("disabled");
            spanView.attr("class", "fas fa-search");
            btnDownload.attr("disabled", "disabled");
        });

    btnNext.on('click', function () {
        var date = $('#datepicker').data("DateTimePicker").date();
        date = date.add(7, 'days');
        $('#datepicker').data("DateTimePicker").date(date);

        btnView.removeAttr("disabled");
        spanView.attr("class", "fas fa-search");
        btnDownload.attr("disabled", "disabled");
    });

    $("#btnPrev").on('click', function () {
        var date = $('#datepicker').data("DateTimePicker").date();
        date = date.add(-7, 'days');
        $('#datepicker').data("DateTimePicker").date(date);

        btnView.removeAttr("disabled");
        spanView.attr("class", "fas fa-search");
        btnDownload.attr("disabled", "disabled");
    });

    btnView.on('click', function () {
        getSchedule();
    });

    getSchedule();
})

function getSchedule() {
    $("#btnView").attr("disabled", "disabled");
    $("#btnDownload").attr("disabled", "disabled");

    $("#spanView").attr("class", "fa fa-spinner fa-spin");

    var url = location.protocol + "//" + location.host + "/schedule/view?id=" + $("#typId").val() + "&date=" + $("#dtp_date").val() + "&language=" + $("#language").val();
    $.ajax({
        type: "GET",
        url: url,
        data: null, // serializes the form's elements.
        processData: false,  // tell jQuery not to process the data
        contentType: false,  // tell jQuery not to set contentType
        success: function (data) {
            if (data.err != null) {
                if (timerId == null) {
                    timerId = setInterval(() => {
                        getSchedule();
                    }, 2000);
                }
            }
            else {
                $("#spanView").attr("class", "fas fa-search");
                $("#btnDownload").removeAttr("disabled");
                getHtml(data);
                clearInterval(timerId);
                timerId = null;
            }

            getHtml(data);

            assignEvents();
        }
    });
}

function getHtml(jsonResp) {
    const scheduleDiv = document.getElementById("week")
    scheduleDiv.innerHTML = ""

    if (jsonResp.err != null) {
        const nameDiv = scheduleDiv.appendChild(document.createElement('div'))
        const h2nameDiv = nameDiv.appendChild(document.createElement('h3'))
        h2nameDiv.innerHTML = jsonResp.err
    }
    else {
        const nameDiv = scheduleDiv.appendChild(document.createElement('div'))
        nameDiv.setAttribute('id', 'sched_sedmica_name')
        const h2nameDiv = nameDiv.appendChild(document.createElement('h3'))
        h2nameDiv.setAttribute('class', jsonResp.schedule.Name.Language)
        h2nameDiv.innerHTML = jsonResp.schedule.Name.Text

        jsonResp.schedule.Days.forEach(function (day) {
            const wDate = new Date(day.Date)

            const wDayOfWeek = DAYS[wDate.getDay()]
            //const dayDayOfWeekDiv = scheduleDiv.appendChild(document.createElement('h4'))
            //dayDayOfWeekDiv.setAttribute('id', 'sched_dayofweek')
            //dayDayOfWeekDiv.innerHTML = wDayOfWeek

            const wDateFormatted = wDate.getDate() + " " + MONTHS[wDate.getMonth()] + " " + wDate.getFullYear() + " г."

            const dayDateDiv = scheduleDiv.appendChild(document.createElement('h4'))
            dayDateDiv.setAttribute('id', 'sched_date')
            dayDateDiv.innerHTML = wDayOfWeek + ", " + wDateFormatted

            if (day.Header != null) {
                const dayNameDiv = scheduleDiv.appendChild(document.createElement('h4'))
                dayNameDiv.setAttribute('id', 'sched_day_name')
                dayNameDiv.setAttribute('class', day.Header.Name.Language)
                dayNameDiv.innerHTML = withModified(jsonResp.isEditor, day.ModifiedDate, day.Header.Name.Text) + "&nbsp;"

                if (jsonResp.isEditor) {
                    var elem = dayNameDiv.appendChild(document.createElement('a'))
                    elem.setAttribute('class', 'btn-edit')
                    elem.setAttribute('data-toggle', 'tooltip')
                    elem.setAttribute('data-original-title', 'Редактировать наименование дня')
                    elem.setAttribute('href', '#')

                    elem = elem.appendChild(document.createElement('i'))
                    elem.setAttribute('class', 'fas fa-pen')

                    const dayIdHdn = dayNameDiv.appendChild(document.createElement('input'))
                    dayIdHdn.setAttribute('id', 'dayIdHdn')
                    dayIdHdn.setAttribute("type", "hidden")
                    dayIdHdn.setAttribute("value", day.Id);
                }

                const daySignDiv = scheduleDiv.appendChild(document.createElement('label'))
                daySignDiv.setAttribute('id', 'sched_day_sign')
                daySignDiv.innerHTML = 'Знак службы: ' + day.Header.SignName
            }
            
            const tbl = scheduleDiv.appendChild(document.createElement('table'))

            day.Worships.forEach(function (worship) {
                const tr = tbl.appendChild(document.createElement('tr'))

                var container = tr.appendChild(document.createElement('td'))
                container.innerHTML = withModified(jsonResp.isEditor, worship.ModifiedDate, worship.Time) + "&nbsp;"

                container = tr.appendChild(document.createElement('td'))

                const td = container

                if (worship.HasSequence) {
                    container = container.appendChild(document.createElement('a'))
                    const url = location.protocol + "//" + location.host + "/schedule/sequence/" + $("#typId").val() + "/" + worship.Id
                    container.setAttribute('href', url)
                }

                var text = container.appendChild(document.createElement('span'))

                text.setAttribute('class', getClass(worship.Name))
                text.innerHTML = withModified(jsonResp.isEditor, worship.ModifiedDate, worship.Name.Text.Text) + "&nbsp;"

                if (worship.AdditionalName.Text != null) {
                    text = container.appendChild(document.createElement('span'))
                    text.setAttribute('class', getClass(worship.AdditionalName))
                    text.innerHTML = withModified(jsonResp.isEditor, worship.ModifiedDate, worship.AdditionalName.Text.Text) + "&nbsp;"
                }

                if (jsonResp.isEditor) {
                    container = td.appendChild(document.createElement('a'))
                    container.setAttribute('class', 'btn-edit')
                    container.setAttribute('data-toggle', 'tooltip')
                    container.setAttribute('data-original-title', 'Редактировать службу')
                    container.setAttribute('href', '#')

                    container = container.appendChild(document.createElement('i'))
                    container.setAttribute('class', 'fas fa-pen')

                    container = container.appendChild(document.createElement('input'))
                    container.setAttribute('id', 'wIdHdn')
                    container.setAttribute("type", "hidden")
                    container.setAttribute("value", worship.Id)
                }
                
            })
        })
    }

    $('[data-toggle="tooltip"]').tooltip()
}

function getClass(name) {
    var cls = name.Text.Language

    if (name.Style != null) {
        if (name.Style.IsRed) {
            cls = cls + " red"
        }
        if (name.Style.IsBold) {
            cls = cls + " bold"
        }
        if (name.Style.IsItalic) {
            cls = cls + " italic"
        }
        if (name.Style.Header != "0") {
            cls = cls + " h" + name.Style.Header
        }
    }

    return cls
}

function assignEvents() {
    //var scheduleDiv = document.getElementById()

    //outputday
    $("#week").on('click', 'h4 .btn-edit', function () {
        var id = $(this).parent().find('#dayIdHdn').val()

        // AJAX request
        $.ajax({
            url: '/Schedule/EditDay',
            type: 'get',
            data: { id: id },
            success: function (response) {
                // Add response in Modal body
                $("#modalWindow").html(response);

                $.validator.unobtrusive.parse($("#editOutputForm"));

                // Display Modal
                $(".editvar-modal").modal('show');

                $("#editOutputForm").submit(submitForm);
            }
        });
    });

    //outputworship
    $("#week").on('click', 'td .btn-edit', function () {
        var id = $(this).parent().find('#wIdHdn').val()

        // AJAX request
        $.ajax({
            url: '/Schedule/EditWorship',
            type: 'get',
            data: { id: id },
            success: function (response) {
                // Add response in Modal body
                $("#modalWindow").html(response);

                $.validator.unobtrusive.parse($("#editOutputForm"));

                // Display Modal
                $(".editvar-modal").modal('show');

                $('#time').datetimepicker({
                    format: 'HH.mm'
                });

                $("#editOutputForm").submit( submitForm );
            }
        });
    });
}

function submitForm(e) {
    e.preventDefault(); // avoid to execute the actual submit of the form.

    if (!$(this).valid()) {
        return false;
    }

    var form = $("#editOutputForm");
    var url = form.attr("action");

    $.ajax({
        type: "POST",
        url: url,
        data: form.serialize(), // serializes the form's elements.
        success: function (data) {
            if (data == null) {

                getSchedule();

                $(".editvar-modal").modal('hide');
            }
            else {
                alert(data);
            }
        }
    });
}

function withModified(isEditor, date, text) {
    if (isEditor && date != null) {
        var mark = document.createElement('mark')
        mark.setAttribute('data-toggle', 'tooltip')
        mark.setAttribute('data-original-title', '<em>Последнее редактирование:</em> ' + moment(date).format("DD.MM.YYYY г. HH:mm:ss"))
        mark.setAttribute('data-html', 'true')
        mark.innerHTML = text

        text = mark.outerHTML
    }

    return '<text>' + text + '</text>'
}

