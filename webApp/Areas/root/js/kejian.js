layui.config({
    base: '/areas/root/js/'
}).use(['form', 'table', 'glevent', 'selectr'], function () {
    var form = layui.form,
        table = layui.table,
        glevent = layui.glevent,
        selectr = layui.selectr;
    $(function () {
        //glevent.bindCommon();
        glevent.bindEditor();
        selectr.cbm(12, $('#contTyp'), true);
        selectr.cbm(12, $('#edit_contTyp'));
        var xs = $('.xueshi'), xm = $('#xueMinute');
        xs.off().on('input', function () {
            if ($.isNumeric(xs[0].value) && $.isNumeric(xs[1].value)) {
                xm.text(xs[0].value * xs[1].value);
            }
        });
        form.render('select');//从新渲染select不然显示不出来
    });
    //tab切换事件
    element.on('tab(tabView)', function (d) {
        if (d.index === 1) {
            selectr.cbm(12, $('#edit_contTyp'));
            $('#kid').val(0);
            $('#kcurl').val(0);
            $('#resetForm').show();
            $('#resetForm').click();
            $('#saveForm').show();
            glevent.bindKey(true);
        }
        else {
            //initSele.jbzc(0, $('#jibiec'));
            //glevent.bindKey(false);
            table.reload('tableDom', {
                page: {
                    hash: 'fenye',
                    curr: location.hash.replace('#!fenye=', '') || 1
                }
            });
        }
    });
    //验证
    form.verify({
        xue: [/^(([0-9]+[\.]?[0-9]+)|[1-9])$/, '学时只能是正整数或正浮点数'],
        minute: [/^[1-9]\d*$/, '学时时长只能是正整数']
    });
    //点击查询
    form.on('submit(findForm)', function (d) {
        table.reload('tableDom', {
            page: { hash: 'fenye', curr: 1 },
            where: { wherejson: JSON.stringify(d.field) }
        });
        return false;
    });
    //保存
    form.on('submit(saveForm)', function (d) {
        var ds = d.field,
            fd={
                id: parseInt(ds.id),
                title: ds.title,
                teacher: ds.teacher.length > 0 ? parseInt(ds.teacher) : null,
                author: ds.author.length > 0 ? ds.author : null,
                xueshi_minute: parseInt(ds.minute),
                xueshi: parseFloat(ds.xue),
                typ: parseInt(ds.typ),
                cont_typ: parseInt(ds.contTyp),
                cont: ds.cont,
                curl: ds.curl
            };
        if (fd.teacher!==null||fd.author!==null) {
            console.log(fd);

        }
        else {
            layer.msg('至少选填一个授课老师或作者', { icon: 2, anim: 6 });
        }
        //save(fd, function (res) {
        //    if (parseInt(ds.id) == 0) {
        //        layer.msg('保存成功', { icon: 1 });
        //        $('#resetForm').click();
        //        $('#title').focus();
        //    }
        //    else {
        //        layer.msg('修改成功', {
        //            icon: 1, time: 500, end: function () {
        //                table.reload('tableDom', {
        //                    page: {
        //                        hash: 'fenye',
        //                        curr: location.hash.replace('#!fenye=', '') || 1
        //                    }
        //                });
        //                element.tabChange('tabView', 'list');
        //            }
        //        });
        //    }
        //});
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
            { field: 'kmc', title: '课件类型', width: 120 },
            { field: 'title', title: '标题', width: 400 },
            { field: 'name', title: '级别', width: 120 },
            { field: 'jobName', title: '职称', width: 120 },
            { field: 'xueshi', title: '学时', width: 100 },
            { field: 'xueshi_minute', title: '学时时长', width: 100 },
            { fixed: 'right', width: 200, align: 'center', toolbar: '#operate' }
        ]]
    });
    table.on('tool(tableView)', function (obj) {
        var data = obj.data;
        if (obj.event === 'detail') { //查看
            initentity(data.id);
            $('#saveForm').hide();
            $('#resetForm').hide();
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
                $('#jibie').val(res.levelID);
                initSele.jbzc(res.levelID, $('#zhicheng'));
                $('#zhicheng').val(res.sid);
                $('#title').val(res.title);
                $('#subs').val(res.cont);
                $('#inputCont').val(res.cont_typ);
                $('#inputTyp').val(res.typ);
                $('#xue').val(res.xueshi);
                $('#minute').val(res.xueshi_minute);
                $('#kcurl').val(res.curl);
                form.render();
            },
            complete: function () { layer.closeAll('loading'); },
            error: function (msg) { alert('ajaxError:' + msg.responseText); }
        });

    }
    //动态获取下拉选项框中的值
    //var initSele = {

    //    jbzc: function (pidval, dom) {
    //        initSele.ajaxselect('/root/kejian/datajian', { pid: pidval }, dom);
    //    },
    //    ajaxselect: function (url, field, dom) {
    //        $.ajax({
    //            url: url,
    //            type: 'get', dataType: 'json', cache: false, async: false,
    //            data: field,
    //            success: function (res) {
    //                if (res !== null) {
    //                    dom.empty().append($('<option/>', { value: '', text: '' }));
    //                    $.each(res, function (i, o) {
    //                        dom.append($('<option/>', {
    //                            value: o.val,
    //                            text: o.text
    //                        }));
    //                    });
    //                }
    //                else {
    //                    dom.empty();
    //                }
    //            },
    //            error: function (msg) { alert('ajaxError:' + msg.responseText); console.log(msg); }
    //        });
    //    }
    //};
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
