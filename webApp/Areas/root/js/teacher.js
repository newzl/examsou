layui.config({
    base: '/areas/modules/'
}).use(['form', 'table', 'uploadpic'], function () {
    var form = layui.form,
        table = layui.table,
        uploadpic = layui.uploadpic;
    //tab切换事件
    element.on('tab(tabView)', function (d) {
        if (d.index === 1) {
            $('#resetForm').click();
            uploadpic.init({ elem: '#upPic' });
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
        var ds = d.field;
        save({
            id: parseInt(ds.id),
            pic: ds.picurl,
            name: ds.name,
            zc: ds.zc,
            isHome: ds.open === 'on' ? true : false,
            valid: ds.opens === 'on' ? true : false,
            detail: ds.detail
        }, function (res) {
            if (parseInt(ds.id) == 0) {
                layer.msg('保存成功', { icon: 1 });
                $('#resetForm').click();
                $('#title').focus();
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
        return false;
    });
    table.render({
        elem: '#tableDom', url: '/root/teacher/listdata',
        page: {
            hash: 'fenye',
            curr: location.hash.replace('#!fenye=', '') || 1
        }, limits: [10, 15],
        cols: [[
            { field: 'rows', title: '序号', width: 70, align: 'center', unresize: true },
            { field: 'name', title: '姓名', width: 120 },
            { field: 'zc', title: '职称', width: 400 },
            { field: 'home', title: '首页显示', width: 120 },
            { field: 'va', title: '状态', width: 120 },
            { fixed: 'right', width: 200, align: 'center', toolbar: '#operate' }
        ]]
    });
    table.on('tool(tableView)', function (obj) {
        var data = obj.data;
        if (obj.event === 'detail') { //查看
            layer.open({
                id: 'subDetail', type: 2,
                title: '查看老师', time: 20000,
                area: ['700px', '300px'],
                shadeClose: true,
                content: '/root/teacher/detail/' + data.id
            });
        }
        else if (obj.event === 'edit') {
            initentity(data.id);
        }
        else if (obj.event === 'del') {
            layer.confirm('确认删除', { icon: 3 }, function () {
                $.ajax({
                    url: '/root/teacher/Del',
                    type: 'post', dataType: 'json', cache: false,
                    data: { id: data.id },
                    success: function (res) {
                        obj.del();
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
            url: '/root/teacher/save',
            type: 'post', dataType: 'json', cache: false, async: false, data: d,
            success: function (res) {
                callback(res);
            },
            error: function (msg) { alert('ajaxError:' + msg.responseText); }
        });
    }
    function initentity(did) {
        element.tabChange('tabView', 'edit');
        $.ajax({
            url: '/root/teacher/getEntity',
            type: 'get', dataType: 'json', cache: false,
            data: { id: did },
            beforeSend: function () { layer.load(2); },
            success: function (res) {
                $('#kid').val(res.id);
                uploadpic.init({ elem: '#upPic',oldpic:res.pic });
                $('#name').val(res.name);
                $('#zc').val(res.zc);
                $('#detail').val(res.detail);
                document.getElementById('open').checked = res.isHome;
                document.getElementById('opens').checked = res.valid;
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
