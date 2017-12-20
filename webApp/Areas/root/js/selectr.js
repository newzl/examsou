﻿layui.define('jquery', function (exports) {
    "use strict";
    var $ = layui.$,
        selectr = {
            cbm: function (typeid, dom, isFind) {
                this.ajaxselect('/handler/root/sele_cbm.ashx', { type: typeid }, dom, isFind || false);
            },
            subjectClass: function (pid, dom, isFind) {
                this.ajaxselect('/handler/root/sele_subjectclass.ashx', { pid: pid }, dom, isFind || false);
            },
            ajaxselect: function (url, field, dom, isFind) {
                $.ajax({
                    url: url,
                    type: 'get', dataType: 'json', cache: false, async: false,
                    data: field,
                    success: function (res) {
                        if (res !== null) {
                            if (res.state !== undefined && res.state === 4001) {
                                layer.msg(res.msg, { icon: 2, time: 1000, end: function () { window.location.replace("/account/login?rid=" + Math.random()); } });
                            }
                            else {
                                dom.empty();
                                if (isFind) dom.append($('<option/>', { value: '-1', text: '全部' }));
                                else dom.append($('<option/>', { value: '', text: '' }));
                                $.each(res, function (i, o) {
                                    dom.append($('<option/>', {
                                        value: o.val,
                                        text: o.text
                                    }));
                                });
                            }
                        }
                        else {
                            dom.empty();
                        }
                    },
                    error: function (msg) { alert('ajaxError:' + msg.responseText); console.log(msg); }
                });
            }
        };
    exports('selectr', selectr);
});