layui.config({ base: '/scripts/' }).use(['layer', 'form', 'validator'], function () {
    var layer = layui.layer, form = layui.form, validator = layui.validator;
    form.verify({
        verAccount: function (val) { if (!(validator.isPhone(val)) && !(validator.isEmail(val))) return '请输入正确的手机号码或邮箱地址'; },
        verCode: [/^[0-9]{6}$/, '您输入的动态码可能有误'],
        verPwd: [/^.{6,16}$/, '密码为6到16个字符'],
        verPwdc: function (val) { if (val !== $('#pwd').val()) return '两次密码输入不一致'; }
    });
    $(function () {
        event.bindGetCode();
    });
    form.on('submit(backpwdForm)', function (Data) {
        var conf = layer.confirm('您确定重置密码？', { icon: 3 }, function () {
            $.ajax({
                url: "/account/resetpwd?r=" + Math.random(),
                type: "POST", dataType: 'JSON', cache: false,
                data: Data.field,
                beforeSend: function () { layer.load(2); },
                success: function (res) {
                    switch (res) {
                        case 0:
                            layer.alert('重置密码失败', { icon: 5 });
                            break;
                        case 1:
                            layer.alert('重置密码成功', { icon: 1, closeBtn: 0 }, function () {
                                window.location.replace("/account/login?ranid=" + Math.random());
                            });
                            break;
                        default:
                            layer.msg('服务器异常！' + res);
                            break;
                    }
                },
                complete: function () { layer.closeAll('loading'); layer.close(conf) },
                error: function (msg) { layer.alert('ajaxError' + msg.responseText); }
            });
        });
        return false;
    });
    var event = {
        bindGetCode: function () {
            $('#getCode').on('click', function () {
                var account = $('#account'), accval = account.val(), then = $(this);
                if (validator.isPhone(accval) || validator.isEmail(accval)) {
                    $.ajax({
                        url: "/account/getbackpwdcode?r=" + Math.random(),
                        type: "POST", dataType: 'JSON', cache: false,
                        data: { account: accval },
                        beforeSend: function () { layer.load(2); },
                        success: function (res) {
                            switch (res) {
                                case -1:
                                    layer.msg('该手机号码或邮箱地址不存在', { icon: 2 });
                                    break;
                                case 0:
                                    layer.msg('获得动态码失败', { icon: 5 });
                                    break;
                                case 1:
                                    then.off().addClass('layui-btn-disabled').text('动态码已发');
                                    break;
                                default:
                                    layer.msg('服务器异常！' + res);
                                    break;
                            }
                        },
                        complete: function () { layer.closeAll('loading'); },
                        error: function (msg) { layer.alert('ajaxError' + msg.responseText); }
                    });
                }
                else {
                    account.focus().addClass('layui-form-danger');
                    layer.msg('请输入正确的手机号码或邮箱地址', { icon: 2, anim: 6 });
                }
            });
        }
    };
});