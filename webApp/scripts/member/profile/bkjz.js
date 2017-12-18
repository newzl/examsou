var companyID = parseInt($('#companyID').val()), old_jibie = null;
layui.use(['layer', 'form'], function () {
    var layer = layui.layer, form = layui.form;
    $(function () {
        initSele.bmks(0, $('#bumen'));
        initSele.jbzc(0, $('#jibie'));
        loadbkjz();
        form.render('select');
    });
    form.on('select(bumen)', function (sd) {
        if (sd.value !== '') initSele.bmks(sd.value, $('#keshi'));
        else $("#keshi").empty();
        form.render('select');
    });
    form.on('select(jibie)', function (sd) {
        if (sd.value !== '') initSele.jbzc(sd.value, $('#zhicheng'));
        else $("#zhicheng").empty();
        form.render('select');
    });
    form.on('submit(formbkjz)', function (fd) {
        var ob = fd.field;
        //如果修改了级别
        if (old_jibie !== null && old_jibie !== parseInt(ob.jibie)) {
            layer.open({
                type: 1, title: false, closeBtn: false, area: '300px;', shade: 0.8,
                btn: ['确定修改', '取消修改'], moveType: 1,
                content: '<div style="padding: 50px; line-height: 22px; background-color: #393D49; color: #fff; font-weight: 300;">特别注意！<br/><br/>您正在修改个人资料里的级别...<br/>修改级别后将删除您的所有学习科目和学习记录。<br/><br/>您是否确定修改？</div>',
                yes: function () {
                    layer.closeAll();
                    save(ob);
                }
            });
        }
        else {
            save(ob);
        }
        return false;
    });
    var initSele = {
        bmks: function (pidval, dom) {
            initSele.ajaxselect('/handler/sele_bmks.ashx', { companyid: companyID, pid: pidval }, dom);
        },
        jbzc: function (pidval, dom) {
            initSele.ajaxselect('/handler/sele_jbzc.ashx', { companyid: companyID, pid: pidval }, dom);
        },
        ajaxselect: function (url, field, dom) {
            $.ajax({
                url: url,
                type: 'get', dataType: 'json', cache: false, async: false,
                data: field,
                success: function (res) {
                    if (res.state === 4001) {
                        layer.msg(res.msg, { icon: 2, time: 1000, end: function () { window.location.replace("/account/login?rid=" + Math.random()); } });
                    }
                    if (res !== null) {
                        dom.empty().append($('<option/>', { value: '', text: '' }));
                        $.each(res, function (i, o) {
                            dom.append($('<option/>', {
                                value: o.val,
                                text: o.text
                            }));
                        });
                    }
                    else {
                        dom.empty();
                    }
                },
                error: function (msg) { alert('ajaxError:' + msg.responseText); console.log(msg); }
            });
        }
    };
    function loadbkjz() {
        $.ajax({
            url: '/profile/databkjz',
            type: 'get', dataType: 'json', cache: false, async: false,
            data: null,
            success: function (res) {
                if (res.bumen !== null) {
                    selected('bumen', res.bumen);
                    initSele.bmks(res.bumen, $('#keshi'));
                    if (res.keshi !== null) selected('keshi', res.keshi);
                }
                if (res.jibie !== null) {
                    old_jibie = res.jibie;
                    selected('jibie', res.jibie);
                    initSele.jbzc(res.jibie, $('#zhicheng'));
                    if (res.zhicheng !== null) selected('zhicheng', res.zhicheng);
                }
            },
            error: function (msg) { alert('ajaxError:' + msg); console.log(msg); }
        });
    }
    function save(ob) {
        $.ajax({
            url: '/profile/savebkjz',
            type: 'post', dataType: 'json', cache: false,
            data: {
                bumen: ob.bumen !== '' ? parseInt(ob.bumen) : null,
                keshi: ob.keshi !== '' ? parseInt(ob.keshi) : null,
                jibie: ob.jibie !== '' ? parseInt(ob.jibie) : null,
                zhicheng: ob.zhicheng !== '' ? parseInt(ob.zhicheng) : null
            },
            beforeSend: function () { layer.load(2); },
            success: function (res) {
                if (res > 0) {
                    layer.msg('保存成功。', {
                        icon: 1, time: 1000, end: function () {
                            window.location.replace("/profile/core?rid=" + Math.random());
                        }
                    });
                }
                else {
                    layer.msg('保存失败', { icon: 2, anim: 6 });
                }
            },
            complete: function () { layer.closeAll('loading'); },
            error: function (msg) { alert('ajaxError:' + msg.responseText); }
        });
    }
});