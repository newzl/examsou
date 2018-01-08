layui.config({
    base: '/areas/root/js/'
}).use(['form', 'table', 'glevent', 'selectr'], function () {
    var form = layui.form,
        table = layui.table,
        glevent = layui.glevent,
        selectr = layui.selectr;
    $(function () {
        glevent.bindEditor();
        selectr.eduitem($('#eduItem'), 1);
        selectr.eduteacher($('#teacher'), 1)
        selectr.cbm(12, $('#contTyp'), 2);
        selectr.cbm(12, $('#edit_contTyp'), 3);
        form.render('select');
        eve.inputr();
    });
    var eve = {
        //输入学时时长计算需学分钟
        inputr: function () {
            var xs = $('.xueshi'), xm = $('#xueMinute');
            xs.off().on('input', function () {
                if ($.isNumeric(xs[0].value) && $.isNumeric(xs[1].value)) {
                    xm.text(xs[0].value * xs[1].value);
                }
            });
            if ($.isNumeric(xs[0].value) && $.isNumeric(xs[1].value)) {
                xm.text(xs[0].value * xs[1].value);
            }
        }
    };
    //tab切换事件
    element.on('tab(tabView)', function (d) {
        if (d.index === 1) {
            $('#kid').val(0);
            $('#kcurl').val(0);
            $('#resetForm').click();
            eve.inputr();
        }
        else {
            table.reload('tableDom', {
                page: {
                    hash: 'fenye',
                    curr: location.hash.replace('#!fenye=', '') || 1
                }
            });
        }
    });
    form.verify({
        xue: [/^(([0-9]+[\.]?[0-9]{1,1})|[1-9])$/, '学时只能是正整数或小数，并且小数点后面只能有一位数。'],
        minute: [/^[1-9]\d*$/, '学时时长只能是正整数']
    });
    //查询
    form.on('submit(findForm)', function (d) {
        table.reload('tableDom', {
            page: { hash: 'fenye', curr: 1 },
            where: { wherejson: JSON.stringify(d.field) }
        });
        return false;
    });
    //保存
    form.on('submit(saveForm)', function (d) {
        if (d.field.teacher.length == 0 && d.field.author.length == 0) {
            layer.msg('至少选填一个授课老师或作者', { icon: 2, anim: 6 });
            return false;
        }
        else {
            var ds = d.field;
            save({
                id: parseInt(ds.id),
                title: ds.title,
                itid: ds.itid,
                teacher: ds.teacher,
                author: ds.author,
                xueshi_minute: parseInt(ds.minute),
                xueshi: parseFloat(ds.xue),
                typ: parseInt(ds.typ),
                cont_typ: parseInt(ds.contTyp),
                cont: ds.cont,
                curl: ds.curl
            }, function (res) {
                if (parseInt(ds.id) == 0) {
                    layer.msg('保存成功', { icon: 1 });
                    $('#resetForm').click();
                    eve.inputr();
                }
                else {
                    layer.msg('修改成功', {
                        icon: 1, time: 500, end: function () {
                            table.reload('tableDom', {
                                page: {
                                    hash: 'fenye',
                                    curr: location.hash.replace('#!fenye=', '') || 1
                                }
                            });
                            element.tabChange('tabView', 'list');
                        }
                    });
                }
            });
        };
        return false;
    });
    table.render({
        elem: '#tableDom', url: '/root/kejian/listdata',
        page: {
            hash: 'fenye',
            curr: location.hash.replace('#!fenye=', '') || 1
        }, limits: [10, 15],
        cols: [[
            { field: 'rows', title: '序号', width: 90, align: 'center', unresize: true },
            { field: 'xxlx', title: '课件类型', width: 120 },
            { field: 'name', title: '继教项目', width: 210 },
            { field: 'teachers', title: '授课老师', width: 120 },
            { field: 'title', title: '标题', width: 250 },
            { field: 'author', title: '作者', width: 100 },
            { field: 'xueshi', title: '学时', width: 100 },
            { field: 'xueshi_minute', title: '学时时长', width: 100 },
            { fixed: 'right', width: 200, align: 'center', toolbar: '#operate' }
        ]]
    });
    table.on('tool(tableView)', function (obj) {
        var data = obj.data;
        if (obj.event === 'detail') { //查看
            layer.open({
                id: 'subDetail', type: 2,
                title: '查看课件', time: 20000,
                area: ['700px', '400px'],
                shadeClose: true,
                content: '/root/kejian/detail/' + data.id
            });
        }
        else if (obj.event === 'edit') {
            initentity(data.id)
        }
        else if (obj.event === 'del') {
            layer.confirm('确认删除', { icon: 3 }, function () {
                $.ajax({
                    url: '/root/kejian/del',
                    type: 'post', dataType: 'json', cache: false,
                    data: { id: data.id, curl: data.curl },
                    success: function (res) {
                        obj.del(); //删除对应行（tr）的DOM结构
                        layer.msg('删除成功', { icon: 1 });
                    },
                    error: function (msg) { alert('ajaxError:' + msg.responseText); }
                });
            });

        }
    });
    //提交
    function save(d, callback) {
        $.ajax({
            url: '/root/kejian/save',
            type: 'post', dataType: 'json', cache: false, async: false, data: d,
            success: function (res) {
                callback(res);
            },
            error: function (msg) { alert('ajaxError:' + msg.responseText); }
        });
    }
    function initentity(did) {
        console.log(did);
        element.tabChange('tabView', 'edit');
        $.ajax({
            url: '/root/kejian/getentity',
            type: 'get', dataType: 'json', cache: false,
            data: { id: did },
            beforeSend: function () { layer.load(2); },
            success: function (res) {
                $('#kid').val(res.id);
                $('#eduItem').val(res.itid);
                $('#title').val(res.title);
                $('#teacher').val(res.teacher);
                $('#author').val(res.author);
                $('#minute').val(res.xueshi_minute);
                $('#xue').val(res.xueshi);
                $('#edit_contTyp').val(res.cont_typ);
                $('#edit_typ').val(res.typ);
                $('#cont').val(res.cont);
                $('#kcurl').val(res.curl);
                eve.inputr()
                form.render();
            },
            complete: function () { layer.closeAll('loading'); },
            error: function (msg) { alert('ajaxError:' + msg.responseText); }
        });

    }
});


function minDateEve() {
    var maxD = $dp.$('maxdate');
    WdatePicker({
        onpicked: function () {
            maxD.focus();
        },
        maxDate: '#F{$dp.$D(\'maxdate\')}'
    })
}
function maxDateEve() {
    WdatePicker({
        minDate: '#F{$dp.$D(\'mindate\')}'
    })
}
