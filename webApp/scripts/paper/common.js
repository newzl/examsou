//获取url中的参数
function getUrlParam(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
    var r = window.location.search.substr(1).match(reg); //匹配目标参数
    if (r != null) return unescape(r[2]); return null; //返回参数值
}
function initiCheck() {
    $('.subContent input').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
        increaseArea: '20%'
    });
}
//禁止所有按键
function noAllKey() {
    try {
        //禁止缩放
        var scrollFunc = function (e) {
            e = e || window.event;
            if (e.wheelDelta && event.ctrlKey) {//IE/Opera/Chrome
                event.returnValue = false;
            } else if (e.detail) {
                event.returnValue = false;
            }
        }
        if (document.addEventListener) {
            document.addEventListener('DOMMouseScroll', scrollFunc, false);
        }
        window.onmousewheel = document.onmousewheel = scrollFunc;
        //禁止按F5F12等
        $(document).unbind("keydown").bind("keydown", function (e) {
            e = window.event || e;
            if (e.keyCode == 116 || e.keyCode == 123) {
                e.keyCode = 0;
                return false;
            }
        });
        //禁止右击复制
        $(document).unbind("contextmenu").bind("contextmenu", function (e) {
            return false;
        });
        with (document.body) {
            oncontextmenu = function () { return false }
            ondragstart = function () { return false }
            onselectstart = function () { return false }
            onbeforecopy = function () { return false }
            onselect = function () { document.selection.empty() }
            oncopy = function () { document.selection.empty() }
        }
    } catch (e) {
        alert(e.message);
    }
}