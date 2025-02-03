export function loadHighchart(id, json) {
    var obj = looseJsonParse(json);
    Highcharts.char(id, obj);
}

export function looseJsonParse(obj) {
    return Function('"use strict";return (' + obj + ')')();
}