var getclosestdayType = {
    attrs: {
        dayofweek: ["понедельник", "вторник", "среда", "четверг", "пятница", "суббота", "воскресенье"],
        weekcount: null
    },
    children: ["date"]
};

var valueType = {
    children: ["daysfromeaster", "date", "int", "getclosestday", "getdayofweek"],
    daysfromeaster: {
        children: ["date"]
    },
    getclosestday: getclosestdayType,
    getdayofweek: {
        attrs: {
            name: null
        },
        children: ["date", "getclosestday"],
        getclosestday: getclosestdayType
    }
};

var switchType = {
    attrs: {},
    children: ["expression", "case", "default"],
    expression: valueType,
    case: {
        children: ["values", "action"],
        values: valueType,
        action: executableType
    }
};

var executableType = {
    children: ["switch", "worship"],
    switch: switchType
}

var tags = {
    "!top": ["rule"],
    rule: executableType,
};

function completeAfter(cm, pred) {
    var cur = cm.getCursor();
    if (!pred || pred()) setTimeout(function () {
        if (!cm.state.completionActive)
            cm.showHint({ completeSingle: false });
    }, 100);
    return CodeMirror.Pass;
}

function completeIfAfterLt(cm) {
    return completeAfter(cm, function () {
        var cur = cm.getCursor();
        return cm.getRange(CodeMirror.Pos(cur.line, cur.ch - 1), cur) == "<";
    });
}

function completeIfInTag(cm) {
    return completeAfter(cm, function () {
        var tok = cm.getTokenAt(cm.getCursor());
        if (tok.type == "string" && (!/['"]/.test(tok.string.charAt(tok.string.length - 1)) || tok.string.length == 1)) return false;
        var inner = CodeMirror.innerMode(cm.getMode(), tok.state).state;
        return inner.tagName;
    });
}

function createTextArea(elementId, elementValue) {
    var e = CodeMirror.fromTextArea(document.getElementById(elementId), {
        mode: "xml",
        lineNumbers: true,
        lineWrapping: true,
        scrollbarStyle: "native",
        extraKeys: {
            "'<'": completeAfter,
            "'/'": completeIfAfterLt,
            "' '": completeIfInTag,
            "'='": completeIfInTag,
            "Ctrl-Space": "autocomplete"
        },
        hintOptions: { schemaInfo: tags },
        value: elementValue
    });
    e.setSize(null, 500);
}

