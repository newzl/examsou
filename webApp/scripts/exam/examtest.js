layui.use(['layer', 'util'], function () {
    var layer = layui.layer, util = layui.util;
    util.fixbar({ top: true });
    $.ajax({
        url: '/attempt/getexamtest',
        type: 'GET', dataType: 'JSON', cache: false,
        data: { sid: $('#sid').val() },
        beforeSend: function () { layer.msg('正在组卷中...', { icon: 16, shade: 0.7 }); },
        success: function (res) { loadSubject(res); },
        complete: function () { noAllKey(); initiCheck(); bindEvent(); layer.closeAll(); },
        error: function (err) {
            alert('ajaxError' + err.responseText);
        }
    });
    function loadSubject(obj) {
        var subDom = $('#subContent'), navDom = $('#subNav');
        $.each(obj, function (i, json) {
            var sid = json.ID;
            var htm = '<ul id="' + sid + '" data-result="' + json.Result + '">';
            htm += '<li><span>第' + (i + 1) + '题</span>' + json.Subjects + '</li>';
            htm += '<li><input type="radio" name="ch' + sid + '" id="ch' + sid + 'A"/><label for="ch' + sid + 'A">A、' + json.A + '</label></li>';
            htm += '<li><input type="radio" name="ch' + sid + '" id="ch' + sid + 'B"/><label for="ch' + sid + 'B">B、' + json.B + '</label></li>';
            htm += '<li><input type="radio" name="ch' + sid + '" id="ch' + sid + 'C"/><label for="ch' + sid + 'C">C、' + json.C + '</label></li>';
            htm += '<li><input type="radio" name="ch' + sid + '" id="ch' + sid + 'D"/><label for="ch' + sid + 'D">D、' + json.D + '</label></li>';
            if (json.E != null && json.E != '') { htm += '<li><input type="radio" name="ch' + sid + '" id="ch' + sid + 'E"/><label for="ch' + sid + 'E">E、' + json.E + '</label></li>'; }
            htm += '</ul>';
            var nav = '<a href="javascript:;" id="nav' + sid + '" data-sid="' + sid + '">' + (i + 1) + '</a>'
            subDom.append(htm);
            navDom.append(nav);
        });
    }
    function bindEvent() {
        var sNav = $('#navContent');
        //滚动时选中
        $(window).scroll(function () {
            var iTop = $(window).scrollTop();//鼠标滚动的距离
            if (iTop >= 60) { sNav.addClass('nav-fix'); }
            else { sNav.removeClass('nav-fix'); }
        });
        //点击导航滑到题目
        $('#subNav a').on('click', function () {
            var h = $('#' + $(this).data('sid')).offset().top;
            $('body,html').animate({ "scrollTop": (h - 15) }, 500);
        });
        //选择选项后更新导航
        $('#subContent input').on('ifClicked', function () {
            $('#nav' + $(this).parent().parent().parent('ul').attr('id')).addClass('yd');
        });
        //批阅试题
        $('#marking').on('click', function () {
            $('#subContent ul').each(function () {
                $(this).find('.checked').next().css('color', 'red');
                $(this).find('label[for="ch' + $(this).attr('id') + $(this).data('result') + '"]').css('color', '#4bcc60');
            });
            $('body,html').animate({ "scrollTop": 0 }, 500);
            $(this).off().addClass('layui-btn-disabled').attr('disabled', true);
            $('#subContent input').iCheck('disable');
        });
    }
});