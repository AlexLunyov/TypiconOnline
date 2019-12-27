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

            const dayNameDiv = scheduleDiv.appendChild(document.createElement('h4'))
            dayNameDiv.setAttribute('id', 'sched_day_name')
            dayNameDiv.setAttribute('class', day.Name.Language)
            dayNameDiv.innerHTML = day.Name.Text

            const daySignDiv = scheduleDiv.appendChild(document.createElement('label'))
            daySignDiv.setAttribute('id', 'sched_day_sign')
            daySignDiv.setAttribute('class', day.SignName.Language)
            daySignDiv.innerHTML = 'Знак службы: ' + day.SignName.Text

            const tbl = scheduleDiv.appendChild(document.createElement('table'))

            day.Worships.forEach(function (worship) {
                const tr = tbl.appendChild(document.createElement('tr'))

                var container = tr.appendChild(document.createElement('td'))
                container.innerHTML = worship.Time + "&nbsp;"

                container = tr.appendChild(document.createElement('td'))

                if (worship.HasSequence) {
                    container = container.appendChild(document.createElement('a'))
                    const url = location.protocol + "//" + location.host + "/schedule/sequence/" + $("#typId").val() + "/" + worship.Id
                    container.setAttribute('href', url)
                }

                var text = container.appendChild(document.createElement('span'))
                var cls = worship.Name.Text.Language
                
                if (worship.Name.Style != null) {
                    if (worship.Name.Style.IsRed) {
                        cls = cls + " red"
                    }
                    if (worship.Name.Style.IsBold) {
                        cls = cls + " bold"
                    }
                    if (worship.Name.Style.IsItalic) {
                        cls = cls + " italic"
                    }
                    if (worship.Name.Style.Header != "0") {
                        cls = cls + " h" + worship.Name.Style.Header
                    }
                }
                text.setAttribute('class', cls)
                text.innerHTML = worship.Name.Text.Text

                if (worship.AdditionalName != null) {
                    text = container.appendChild(document.createElement('span'))
                    text.setAttribute('class', worship.AdditionalName.Language)
                    text.innerHTML = worship.AdditionalName.Text
                }
            });
        })
    }
    
}

