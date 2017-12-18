//全局事件
"use strict";
layui.define('form', function (exports) {
    var form = layui.form;
    var even = {
        //打开窗口查看用户详细信息
        employeeDetail: function (id) {
            layer.open({
                type: 2,
                title: '用户详细信息',
                area: ['820px', '500px'],
                shadeClose: true, btn: '关闭',
                yes: function (i, l) { layer.close(i); },
                content: '/admin/employeemsg/detail/' + id + '?r=' + Math.random()
            });
        },
        //初始化部门 isall:是否显示全部
        loadbm: function (isall) {
            even.retbmks(0, isall || true, function (htm) {
                $('#bm').empty().append(htm);
                form.render('select');
            });
        },
        //加载部门科室select
        loadbmks: function () {
            even.retbmks(0, true, function (htm) {
                $('#bm').empty().append(htm);
                form.render('select');
                form.on('select(bm)', function (d) {
                    if (parseInt(d.value) === -1) {
                        $('#ks').empty().append('<option value="-1">全部</option>'); form.render('select');
                    }
                    else {
                        even.retbmks(d.value, true, function (htm) {
                            $('#ks').empty().append(htm); form.render('select');
                        });
                    }
                    
                });
            });
        },
        retbmks: function (pid, isall, callback) {
            even.ajaxselect('/handler/sele_bmks.ashx', pid, function (res) {
                var htm = '';
                if (isall != undefined && isall === true) htm += '<option value="-1">全部</option>';
                else htm += '<option value="">请选择</option>';
                $.each(res, function (i, item) {
                    htm += '<option value="' + item.val + '">' + item.text + '</option>';
                });
                callback(htm);
            });
        },
        loadjb: function (isall) {
            even.retjbzc(0, isall || true, function (htm) {
                $('#jb').empty().append(htm);
                form.render('select');
            });
        },
        //加载级别职称select
        loadjbzc: function () {
            even.retjbzc(0, true, function (htm) {
                $('#jb').empty().append(htm);
                form.render('select');
                form.on('select(jb)', function (d) {
                    if (parseInt(d.value) === -1) {
                        $('#zc').empty().append('<option value="-1">全部</option>'); form.render('select');
                    }
                    else {
                        even.retjbzc(d.value, true, function (htm) {
                            $('#zc').empty().append(htm); form.render('select');
                        });
                    }
                });
            });
        },
        retjbzc: function (pid, isall, callback) {
            even.ajaxselect('/handler/sele_jbzc.ashx', pid, function (res) {
                var htm = '';
                if (isall != undefined && isall === true) htm += '<option value="-1">全部</option>';
                else htm += '<option value="">请选择</option>';
                $.each(res, function (i, item) {
                    htm += '<option value="' + item.val + '">' + item.text + '</option>';
                });
                callback(htm);
            });
        },
        ajaxselect: function (url, pid, callback) {
            $.ajax({
                url: url,
                type: 'get', dataType: 'json', cache: false,
                data: { pid: pid },
                beforeSend: function () { layer.load(2); },
                success: function (res) {
                    if (res.state !== undefined && res.state === 4001) {
                        layer.msg(res.msg, { icon: 2, time: 1500, end: function () { window.location.replace("/admin/account/login?rid=" + Math.random()); } });
                    }
                    else {
                        callback(res);
                    }
                },
                complete: function () { layer.closeAll('loading'); },
                error: function (msg) { alert('ajaxError:' + msg.responseText); }
            });
        }
    };
    exports('glevent', even);
});