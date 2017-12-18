"use strict";
layui.use(['layer', 'form'], function () {
    var layer = layui.layer,
    form = layui.form;
    $(document).ready(function () {
        init.accCookie();
        event.changeCode();
    });
    var init = {
        accCookie: function () {
            var acc = $.cookie('ZXXXADMINACCOUNT');
            if (acc != null) $("#account").val(acc);
        }
    };
    var event = {
        changeCode: function () {
            $('#vCodeImg').off().on('click', function () {
                changeVCode();
            });
        }
    };
    form.verify({
        account: function (value) {
            if (value.length < 1) {
                return '用户名不能为空，请输入正确的账号。';
            }
        },
        pwd: function (v) {
            if (v.length < 1) {
                return '密码不能为空，请输入正确的密码。'
            }
        },
        code: [/^[0-9]{4}$/, '您的验证码可能输入有误']
    });
    form.on('submit(loginForm)', function (fd) {
        $.ajax({
            url: "/admin/account/loginform?r=" + Math.random(),
            type: "POST", dataType: 'JSON', cache: false,
            data: fd.field,
            beforeSend: function () { layer.load(1); },
            success: function (res) {
                if (res.state == -1) {
                    layer.msg('验证码错误', { icon: 5, anim: 6 });
                    changeVCode();
                }
                else if (res.state == 0) {
                    layer.msg('用户名或密码错误', { icon: 5, anim: 6 });
                    changeVCode();
                }
                else if (res.state == 1) {
                    if (fd.field.remember != undefined) {
                        $.cookie('ZXXXADMINACCOUNT', fd.field.account, { expires: 360 });
                    }
                    if (res.type === 1) {
                        window.location.replace('/root?r=' + Math.random());
                    }
                    else if (res.type === 2) {
                        window.location.replace('/admin?r=' + Math.random());
                    }
                }
                else {
                    layer.msg('服务器异常！' + res);
                    changeVCode();
                }
            },
            complete: function () { layer.closeAll('loading'); },
            error: function (res) { layer.msg('网络错误，请与管理员联系。'); console.log(res.responseText); }
        });
        return false;
    });
    function changeVCode() {
        $('#vCodeImg').attr('src', '/account/verification?r=' + Math.random());
        $('#code').val('');
    }
});