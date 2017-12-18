var loginacc = 'ZXXXLOGINCCOUNT', employee = 'ZXXXEMPLOYEECOOKIE', cookieAcc = $.cookie(loginacc);
if (cookieAcc !== null) $('#account').val(cookieAcc);
layui.use(['layer', 'form'], function () {
    var layer = layui.layer,
    form = layui.form;
    $(function () {
        if ($.cookie(employee) !== null) $.cookie(employee, null, { path: '/', expires: -1 });
        $("#codeImg").on('click', function () { changeVCode(); });
    });
    form.verify({
        account: function (value) {
            if (value.length < 1) {
                return '用户名不能为空';
            }
        },
        pwd: function (v) {
            if (v.length < 1) {
                return '密码不能为空'
            }
        },
        code: [/^[0-9]{4}$/, '您输入的验证码可能有误']
    });
    form.on('submit(loginForm)', function (d) {
        var fd = d.field;
        $.ajax({
            url: "/account/loginform",
            type: "POST", dataType: 'JSON', cache: false,
            data: fd,
            beforeSend: function () { layer.load(2); },
            success: function (res) {
                switch (res.state) {
                    case -1:
                        layer.msg('验证码错误！', { icon: 5, anim: 6 });
                        changeVCode();
                        break;
                    case 0:
                        layer.msg('账号或密码错误！', { icon: 5, anim: 6 });
                        changeVCode();
                        break;
                    case 1:
                        $.cookie(employee, JSON.stringify(res), { path: '/' });
                        window.location.replace("/learn?rid=" + Math.random().toString(36).substr(2));
                        break;
                    default:
                        layer.msg('服务器异常！' + res);
                        changeVCode();
                        break;
                }
                if (fd.isSave == 'on') {
                    $.cookie(loginacc, fd.account, { expires: 360, path: '/' });
                }
            },
            complete: function () { layer.closeAll('loading'); },
            error: function (msg) { layer.alert('ajaxError' + msg.responseText); }
        });
        return false;
    });
    function changeVCode() {//换验证码
        $("#codeImg").attr("src", '/account/verification?r=' + Math.random());
        $('#code').val('');
    }
});