/*
 Скрипт для внешнего использования расписания богослужений.
 Выводит расписание на указанное количество недель для указанного Устава
 */

function GetSchedule(id, weeks) {
    
}

const MONTHS = ['января', 'февраля', 'марта', 'апреля', 'мая', 'июня', 'июля', 'августа', 'сентября', 'октября',
    'ноября', 'декабря',]

const DAYS = ['ВОСКРЕСЕНЬЕ', 'ПОНЕДЕЛЬНИК', 'ВТОРНИК', 'СРЕДА', 'ЧЕТВЕРГ', 'ПЯТНИЦА', 'СУББОТА']

const SCHED_ERROR = "Ссылка на скрипт должна быть определена в теле документа, чтобы загрузить расписание"

const script = document.scripts[document.scripts.length - 1]
if (script.parentElement.localName == 'head') {
    console.error(SCHED_ERROR)
    alert(SCHED_ERROR)

    //break
}

args = getScriptArgs(script)

const id = args['id']
const weeks = args['count']
const redcolor = args['redcolor']

if (weeks == null) {
    weeks = 2
}

const url = 'https://typicon.online/Schedule/week?id=' + id + '&weekscount=' + weeks

let scheduleDiv = document.createElement('div')
scheduleDiv.setAttribute('class', "schedule")
script.parentElement.insertBefore(scheduleDiv, script)

const procDiv = scheduleDiv.appendChild(document.createElement('div'))
const procHDiv = procDiv.appendChild(document.createElement('h4'))
procHDiv.innerHTML = "Идет загрузка расписания..."

const xhr = new XMLHttpRequest()

xhr.onreadystatechange = function () {
    if (xhr.readyState < 4) {
        return;
    }

    if (xhr.status !== 200) {
        alert("Ошибка: " + xhr.status + ' => ' + xhr.statusText)

        return;
    }

    if (xhr.readyState === 4) {
        success(xhr.responseText);
    }
};

success = function (response) {

    scheduleDiv.innerHTML = ""

    const jsonResp = JSON.parse(response);

    if (jsonResp.err != null) {
        const nameDiv = scheduleDiv.appendChild(document.createElement('div'))
        const h2nameDiv = nameDiv.appendChild(document.createElement('h3'))
        h2nameDiv.innerHTML = jsonResp.err
    }
    else {

        jsonResp.schedule.forEach(function (week) {
            const nameDiv = scheduleDiv.appendChild(document.createElement('div'))
            nameDiv.setAttribute('id', 'sched_sedmica_name')
            const h2nameDiv = nameDiv.appendChild(document.createElement('h2'))
            h2nameDiv.setAttribute('class', week.Name.Language)
            h2nameDiv.innerHTML = week.Name.Text

            week.Days.forEach(function (day) {
                const wDate = new Date(day.Date)

                const wDayOfWeek = DAYS[wDate.getDay()]

                const wDateFormatted = wDate.getDate() + " " + MONTHS[wDate.getMonth()] + " " + wDate.getFullYear() + " г."

                const dayDateDiv = scheduleDiv.appendChild(document.createElement('strong'))

                dayDateDiv.setAttribute('id', 'sched_date')

                dayDateDiv.innerHTML = wDayOfWeek + "<br/>" + wDateFormatted

                if (day.Header != null) {
                    var nameCls = day.Header.Name.Language
                    if (day.Header.IsRed == true) {
                        nameCls = nameCls + " red"
                    }

                    dayDateDiv.setAttribute('class', nameCls)
                    setStyle(dayDateDiv, day.Header)
                    
                    dayDateDiv.innerHTML += "<br/>" + day.Header.Name.Text

                    if (day.Header.SignNumber > 0 && day.Header.SignNumber < 6) {
                        const img = document.createElement('img')
                        img.setAttribute("src", "http://typicon.online/images/sign/" + day.Header.SignNumber + ".png")
                        dayDateDiv.innerHTML = img.outerHTML + " " + dayDateDiv.innerHTML
                    }
                }

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

                    text.setAttribute('class', getClass(worship.Name))
                    setStyle(text, worship.Name.Style)
                    text.innerHTML = worship.Name.Text.Text

                    if (worship.AdditionalName.Text != null) {
                        text = container.appendChild(document.createElement('span'))
                        text.setAttribute('class', getClass(worship.AdditionalName))
                        setStyle(text, worship.AdditionalName.Style)
                        text.innerHTML = " " + worship.AdditionalName.Text.Text
                    }
                });
            })
        })
    }
}

xhr.open('GET', url, true)
xhr.send()

//document.addEventListener("DOMContentLoaded", function (event) {

//    alert()

    
//})

function getScriptArgs(script_id) {
    //const script_id = document.getElementById('schedule_script_id')
    const query = script_id.src.replace(/^[^\?]+\??/, '')
    const vars = query.split("&")
    const args = {}
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=")
        args[pair[0]] = decodeURI(pair[1]).replace(/\+/g, ' ')
    }

    return args
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

function setStyle(text, elementStyle) {
    var stl = ""

    if (elementStyle != null) {
        if (elementStyle.IsRed
            && redcolor != null) {
            stl = stl + " color: #ff0000;"
        }
        //if (name.Style.IsBold) {
        //    cls = cls + " bold"
        //}
        //if (name.Style.IsItalic) {
        //    cls = cls + " italic"
        //}
        //if (name.Style.Header != "0") {
        //    cls = cls + " h" + name.Style.Header
        //}
    }

    if (stl != "") {
        text.setAttribute('style', stl)
    }
}
