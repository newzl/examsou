"use strict";
layui.use(['laytpl', 'layer'], function () {
    var laytpl = layui.laytpl;
    var layer = layui.layer;
    $(document).ready(function () {
        req.getHome();
        $.getScript('/scripts/log.js', function () {
            init.loadUpdateLog(log);//升级日志
        });
    });
    var init = {
        loadUpdateLog: function (obj) {
            var getTpl = updateLogDom.innerHTML;
            laytpl(getTpl).render(obj, function (html) {
                document.getElementById('updateLogView').innerHTML = html;
            });
        },
        loadSymbol: function (obj) {
            var getTpl = symbolDom.innerHTML;
            laytpl(getTpl).render(obj, function (html) {
                document.getElementById('symbolView').innerHTML = html;
            });
        },
        loadBackInfo: function (obj) {
            var getTpl = backinfoDom.innerHTML;
            laytpl(getTpl).render(obj, function (html) {
                document.getElementById('backinfoView').innerHTML = html;
            });
        }
    };
    var req = {
        getHome: function () {
            $.ajax({
                url: '/admin/home/ajaxhomeget?r=' + Math.random(),
                type: 'get', dataType: 'json', cache: false,
                data: null,
                beforeSend: function () { layer.load(2); },
                success: function (res) {
                    if (res !== null && res !== undefined) {
                        var json = res[0];
                        var symbol = [
                            { text: '用户总数', value: json.users, href: '/admin/employeemsg/list', icon: '&#xe613' },
                            { text: '试卷数量', value: json.plans, href: null, icon: '&#xe705' },
                            { text: '今日学习', value: json.todays, href: null, icon: '&#xe60e' }
                        ];
                        var backInfo = [
                            { title: '登录用户', value: json.name },
                            { title: '单位名称', value: json.companyName },
                            { title: '上次登录', value: json.lastLoginTime },
                            { title: '部门总数', value: json.departments },
                            { title: '题库总数', value: json.total },
                            { title: '系统地址', value: 'www.zxxx.co' },
                            { title: '系统全称', value: '新智联在线学习考试系统' },
                            { title: '联系公司', value: '0851-88515155 / 88515156' },
                            { title: '微信扫描', value: '<img src="/images/qrcode.jpg" height="100"/>' },
                        ];
                        init.loadSymbol(symbol);
                        init.loadBackInfo(backInfo);
                    }
                    else {
                        layer.alert('获得单位数据错误，请重新登录或联系管理员', { icon: 2 }, function (index) {
                            layer.close(index);
                            window.location.replace("/admin/account/quitlogin");
                        });
                    }
                },
                complete: function () { layer.closeAll('loading'); },
                error: function (msg) { alert('ajaxError:' + msg.responseText); }
            });
        }
    };
});