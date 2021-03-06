﻿/*
 Скрипт для внешнего использования расписания богослужений.
 Выводит расписание на указанную дату для указанного Устава
 */

function GetSchedule(id, weeks) {
    
}

const MONTHS_DAY = ['января', 'февраля', 'марта', 'апреля', 'мая', 'июня', 'июля', 'августа', 'сентября', 'октября',
    'ноября', 'декабря',]

const DAYS_DAY = ['ВОСКРЕСЕНЬЕ', 'ПОНЕДЕЛЬНИК', 'ВТОРНИК', 'СРЕДА', 'ЧЕТВЕРГ', 'ПЯТНИЦА', 'СУББОТА']

const SCHED_ERROR_DAY = "Ссылка на скрипт должна быть определена в теле документа, чтобы загрузить расписание"

const script = document.scripts[document.scripts.length - 1]
if (script.parentElement.localName == 'head') {
    console.error(SCHED_ERROR)
    alert(SCHED_ERROR)

    //break
}

args = getScriptArgs(script)

const id = args['id']
const date = args['date']

const url = 'https://typicon.online/Schedule/day?id=' + id + '&date=' + date

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
        var day = jsonResp.day

        const wDate = new Date(day.Date)

        const wDayOfWeek = DAYS_DAY[wDate.getDay()]

        const wDateFormatted = wDate.getDate() + " " + MONTHS_DAY[wDate.getMonth()] + " " + wDate.getFullYear() + " г."

        const dayDateDiv = scheduleDiv.appendChild(document.createElement('strong'))

        if (day.Header != null) {
            var nameCls = day.Header.Name.Language
            if (day.Header.IsRed == true) {
                nameCls = nameCls + " red"
            }

            dayDateDiv.setAttribute('class', nameCls)

            dayDateDiv.setAttribute('id', 'sched_date')
            dayDateDiv.innerHTML = wDayOfWeek + "<br/>" + wDateFormatted + "<br/>" + day.Header.Name.Text

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
