layui.define('jquery', function (exports) {
    var $ = layui.$;
    exports('exameven', {
        wscroll: function () {
            var sNav = $('.nav-content');
            $(window).scroll(function () {//滚动时
                if ($(window).scrollTop() >= 60) { sNav.addClass('nav-fix'); }
                else { sNav.removeClass('nav-fix'); }
            });
        },
        icheck: function () {
            $('.sub-wrap input').iCheck({
                checkboxClass: 'icheckbox_square-green',
                radioClass: 'iradio_square-green',
                increaseArea: '20%'
            });
        },
        //答题更新答题卡和选择答题卡滑到试题
        card: function () {
            //点击导航滑到题目
            $('#subCard').off().on('click', 'dd', function () {
                var h = $('#t' + $(this).text()).offset().top;
                $('body,html').animate({ "scrollTop": (h - 15) }, 600);
            });
            //选择选项后更新导航
            $('#subView input').on('ifClicked', function (dom) {
                var par = $(this).parent().parent().parent('div'),
                    nav = $('#' + par.attr('id').replace(/t/gm, 'n'));
                if (dom.target.type === 'checkbox') {
                    $(this).iCheck('toggle');
                    if (par.find('.checked').length > 0) nav.addClass('yd');
                    else nav.removeClass('yd');
                }
                else nav.addClass('yd');
            });
        },
        //禁止所有按键
        noAllKey: function () {
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
    });
});