layui.use(['element', 'laytpl'], function () {
    var element = layui.element, laytpl = layui.laytpl;
    var cook = $.cookie('ZXXXEMPLOYEECOOKIE');
    if (cook != undefined && cook != null) {
        var emp = $.parseJSON(cook);
        if (emp.state === 1) {
            $.getScript('/scripts/nav/unav.js', function () {
                getnav(emp, function (res) {
                    loadnav(res);
                });
            });
        }
        else {
            $.getScript('/scripts/nav/nnav.js', function () {
                getnav(emp, function (res) {
                    loadnav(res);
                });
            });
        }
    }
    else {
        window.location.replace('/account/login?ranid=' + Math.random());
    }
    function loadnav(nav) {
        var getTpl = navDom.innerHTML;
        laytpl(getTpl).render(nav, function (html) {
            document.getElementById('navView').innerHTML = html;
        });
        element.render();
    }
});
function selected(id, val) {
    $("#" + id + " option[value='" + val + "']").attr("selected", true);
}