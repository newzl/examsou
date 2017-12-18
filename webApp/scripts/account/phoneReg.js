layui.config({ base: '/scripts/' }).use(['layer', 'form', 'validator'], function () {
    var layer = layui.layer, form = layui.form, validator = layui.validator;
    form.verify({
        verPhone: function (val) { if (!validator.isPhone(val)) return '请输入正确的手机号码'; },
        verCode: [/^[0-9]{6}$/, '您输入的动态码可能有误'],
        verPwd: [/^.{6,16}$/, '密码为6到16个字符'],
        verPwdc: function (val) { if (val !== $('#pwd').val()) return '两次密码输入不一致'; }
    });
    $(function () {
        event.bindGetCode();
    });
    form.on('submit(regForm)', function (Data) {
        $.ajax({
            url: "/account/regphone?r=" + Math.random(),
            type: "POST", dataType: 'JSON', cache: false,
            data: Data.field,
            beforeSend: function () { layer.load(2); },
            success: function (res) {
                switch (res) {
                    case 0:
                        layer.alert('注册失败', { icon: 5 });
                        break;
                    case 1:
                        layer.alert('注册成功', { icon: 1, closeBtn: 0 }, function () {
                            window.location.replace("/account/login?ranid=" + Math.random());
                        });
                        break;
                    default:
                        layer.msg('服务器异常！' + res);
                        break;
                }
            },
            complete: function () { layer.closeAll('loading'); },
            error: function (msg) { layer.alert('ajaxError' + msg.responseText); }
        });
        return false;
    });
    var event = {
        bindGetCode: function () {
            $('#getCode').off().on('click', function () {
                var phone = $('#phone'), then = $(this);
                if (validator.isPhone(phone.val())) {
                    $.ajax({
                        url: "/account/getphonecode?r=" + Math.random(),
                        type: "POST", dataType: 'JSON', cache: false,
                        data: { phone: phone.val() },
                        beforeSend: function () { layer.load(2); },
                        success: function (res) {
                            switch (res) {
                                case -1:
                                    layer.msg('该手机号码已经注册，请登录', { icon: 2 });
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
                    phone.focus().addClass('layui-form-danger');
                    layer.msg('请输入正确的手机号码', { icon: 2, anim: 6 });
                }
            });
        }
    };
});