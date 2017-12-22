layui.define('jquery', function (exports) {
    "use strict";
    var $ = layui.$,
        selectr = {
            cbm: function (typeid, dom, first) {
                this.ajaxselect('/handler/root/sele_cbm.ashx', { type: typeid }, dom, first || 1);
            },
            subjectClass: function (pid, dom, first) {
                this.ajaxselect('/handler/root/sele_subjectclass.ashx', { pid: pid }, dom, first || 1);
            },
            eduitem: function (dom, first) {
                this.ajaxselect('/handler/root/sele_eduitemName.ashx', 0, dom, first || 1)
            },
            eduteacher: function (dom, first) {
                this.ajaxselect('/handler/root/sele_eduteacher.ashx', 0, dom, first || 1)
            },
            ajaxselect: function (url, data, dom, first) {
                $.ajax({
                    url: url, type: 'get', dataType: 'json',
                    cache: false, async: false, data: data,
                    success: function (res) {
                        if (res !== null) {
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
                        else dom.empty();
                    },
                    error: function (msg) { alert('ajaxError:' + msg.responseText); console.log(msg); }
                });
            }
        };
    exports('selectr', selectr);
});