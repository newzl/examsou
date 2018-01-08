layui.define('jquery', function (exports) {
    "use strict";
    var $ = layui.$,
        checks = {
            subjectClass: function (pid, dom) {
                this.ajaxselect('/handler/root/sele_subjectclass.ashx', { pid: pid }, dom);
            },
            ajaxselect: function (url, data, dom) {
                $.ajax({
                    url: url, type: 'get', dataType: 'json',
                    cache: false, async: false, data: data,
                    success: function (res) {
                        var html = '';
                        if (res !== null) {
                            dom.empty();
                            $.each(res, function (i, o) {
                                html = html + "<input type='checkbox' name='like' title='" + o.text + "' value='" + o.val + "' >";
                            });
                        } else {
                            dom.empty();
                            html = html + "<div class='layui-form-mid layui-word-aux'>当前练习试题集中没有题库</div>";
                        }
                        dom.append(html);
                    },
                    error: function (msg) { alert('ajaxError:' + msg.responseText); console.log(msg); }
                });
            }
        };
    exports('checks', checks);
});