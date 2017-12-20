"use strict";
layui.use(['element', 'laytpl'], function () {
    var element = layui.element, laytpl = layui.laytpl;
    var cook = $.cookie('ZXXXEMPLOYEECOOKIE');
    if (cook !== undefined && cook !== null) {
        $.getScript('/scripts/nav/unav.js', function () {
            getnav($.parseJSON(cook), function (res) {
                var getTpl = navDom.innerHTML;
                laytpl(getTpl).render(res, function (html) {
                    document.getElementById('navView').innerHTML = html;
                });
                element.render();
            });
        });
    }
    else {
        window.location.replace('/account/login?rid=' + Math.random());
    }
});
function selected(id, val) {
    $("#" + id + " option[value='" + val + "']").attr("selected", true);
}