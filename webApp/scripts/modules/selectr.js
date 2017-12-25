layui.define('jquery', function (exports) {
    "use strict";
    var $ = layui.$,
        selectr = {
            cbm: function (typeid, dom, first) {
                this.ajaxselect('/handler/sele_cbm.ashx', { type: typeid }, dom, first || 1);
            },
            subjectClass: function (pid, dom, first) {
                this.ajaxselect('/handler/sele_subjectclass.ashx', { pid: pid }, dom, first || 1);
            },
            xzbm: function (pval, dom, first) {
                this.ajaxselect('/handler/sele_xzbm.ashx', { pxzbm: pval }, dom, first || 1);
            },
            company: function (xzbm, dom, first) {
                this.ajaxselect('/handler/sele_company.ashx', { xzbm: xzbm }, dom, first || 1);
            },
            bmks: function (pid, dom, first) {
                initSele.ajaxselect('/handler/sele_bmks.ashx', { companyid: companyID, pid: pid }, dom, first || 1);
            },
            jbzc: function (pid, dom, first) {
                this.ajaxselect('/handler/sele_jb_zc.ashx', { pid: pid }, dom, first || 1);
            },
            ajaxselect: function (url, field, dom, first) {
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
                                switch (first) {
                                    case 1: dom.append($('<option/>', { value: '', text: '' }));
                                        break;
                                    case 2: dom.append($('<option/>', { value: '-1', text: '全部' }));
                                        break;
                                }
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