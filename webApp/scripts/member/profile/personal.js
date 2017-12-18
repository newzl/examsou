var emp_cook = 'ZXXXEMPLOYEECOOKIE';
layui.config({
    base: '/scripts/modules/'
}).use(['form', 'validator', 'selectr'], function () {
    var form = layui.form, validator = layui.validator, selectr = layui.selectr;
    form.verify({
        verPhone: function (val) {
            if (!validator.isPhone(val)) return '请输入正确的手机号码';
        },
        verEmail: function (val) {
            if (val !== '') {
                if (!validator.isEmail(val)) return '请输入正确的邮箱地址';
            }
        }
    });
    $(function () {
        layer.load(2);
        selectr.cbm(8, $('#zzmm'));
        selectr.cbm(3, $('#nation'));
        selectr.cbm(7, $('#degree'));
        loadPersonal();
        bindEvent();
        layer.closeAll('loading');
    });
    form.on('submit(formPersonal)', function (fd) {
        var ob = fd.field;
        $.ajax({
            url: '/profile/savepersonal',
            type: 'post', dataType: 'json', cache: false,
            data: {
                photo: ob.photo !== '' ? ob.photo : null,
                name: ob.name,
                sex: parseInt(ob.sex),
                phone: ob.phone !== '' ? ob.phone : null,
                email: ob.email !== '' ? ob.email : null,
                zzmm: ob.zzmm !== '' ? parseInt(ob.zzmm) : null,
                nation: ob.nation !== '' ? parseInt(ob.nation) : null,
                degreeid: ob.degreeid !== '' ? parseInt(ob.degreeid) : null
            },
            beforeSend: function () { layer.load(2); },
            success: function (res) {
                switch (res.msg) {
                    case 1:
                        $.cookie(emp_cook, JSON.stringify(res.info), { path: '/' });
                        layer.msg('保存成功，请完善工作信息。', {
                            icon: 1, time: 1500, end: function () {
                                window.location.href = "/profile/profession/";
                            }
                        });
                        break;
                    case 2: layer.msg('手机已经存在，不能重复', { icon: 2, anim: 6 });
                        break;
                    case 3: layer.msg('邮箱已经存在，不能重复', { icon: 2, anim: 6 });
                        break;
                    case 0: layer.msg('保存失败', { icon: 2, anim: 6 });
                        break;
                    default: console.log(res);
                        break;
                }
            },
            complete: function () { layer.closeAll('loading'); },
            error: function (msg) { alert('ajaxError:' + msg.responseText); }
        });
        return false;
    });
    //加载信息
    function loadPersonal() {
        $.ajax({
            url: '/profile/datapersonal', type: 'get',
            dataType: 'json', cache: false, async: false,
            data: null,
            success: function (res) {
                $('#account').text(res.account);
                if (res.photo !== null) {
                    $('#userFace').attr('src', res.photo);
                    $('#photo').val(res.photo);
                }
                if (res.name !== null) $('#ename').val(res.name);
                if (res.sex !== null) {
                    $("input[name=sex]").removeAttr('checked');
                    $("input[name=sex][value=" + res.sex + "]").attr('checked', 'checked');
                    form.render('radio');
                }
                if (res.phone !== null) $('#phone').val(res.phone);
                if (res.email !== null) $('#email').val(res.email);
                if (res.zzmm !== null) selected('zzmm', res.zzmm);
                if (res.nation !== null) selected('nation', res.nation);
                if (res.degreeid !== null) selected('degree', res.degreeid);
                form.render('select');
            },
            error: function (msg) { alert('ajaxError:' + msg); console.log(msg); }
        });
    }
    //绑定事件
    function bindEvent() {
        $('#userFace').off().on('click', function () {
            var that = $(this);
            var htm = '<div class="faceList"><ul>';
            for (var i = 1001; i < 1021; i++) {
                htm += '<li><img src="/images/face/face' + i + '.png" /></li>';
            }
            htm += '</ul></div>';
            layer.open({
                type: 1, title: '选择头像', offset: '100px', shadeClose: true,
                area: ['580px', '460px'],
                content: htm,
                success: function () {
                    $('.faceList ul li').off().on('click', function () {
                        var isr = $(this).children('img').attr('src');
                        that.attr('src', isr);
                        $('#photo').val(isr);
                        layer.closeAll();
                    });
                }
            });
        });
    }
});