layui.define('jquery', function (exports) {
    "use strict";
    var $ = layui.$,
        selectr = {
            cbm: function (typeid, dom) {
                this.ajaxselect('/handler/sele_cbm.ashx', { type: typeid }, dom);
            },
            xzbm: function (pval, dom) {
                this.ajaxselect('/handler/sele_xzbm.ashx', { pxzbm: pval }, dom);
            },
            company: function (xzbm, dom) {
                this.ajaxselect('/handler/sele_company.ashx', { xzbm: xzbm }, dom);
            },
            bmks: function (pid, dom) {
                initSele.ajaxselect('/handler/sele_bmks.ashx', { companyid: companyID, pid: pid }, dom);
            },
            jbzc: function (pid, dom) {
                this.ajaxselect('/handler/sele_jb_zc.ashx', { pid: pid }, dom);
            },
            ajaxselect: function (url, field, dom) {
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
                                dom.empty().append($('<option/>', { value: '', text: '' }));
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