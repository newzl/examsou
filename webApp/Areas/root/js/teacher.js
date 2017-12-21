layui.config({ base: '/areas/root/js/' }).use(['form', 'table'], function () {
    var form = layui.form, table = layui.table
    $(function () {
        ////glevent.bindCommon();
        //$("#s").value='on';
        //form.render('checkbox');
    });
    //tab切换事件
    element.on('tab(tabView)', function (d) {
        if (d.index === 1) {
            $('#kid').val(0);
            $('#kcurl').val(0);
            $('#resetForm').show();
            $('#resetForm').click();
            $('#saveForm').show();

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
        console.log(d);
        save({
            id: parseInt(ds.id),
            name: ds.name,
            zc: ds.zc,
            isHome: ds.open != null ? true : false,
            valid: ds.opens != null ? true : false,
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
            { field: 'rows', title: '序号', width: 90, align: 'center', unresize: true },
            { field: 'name', title: '姓名', width: 120 },
            { field: 'zc', title: '职称', width: 400 },
            { field: 'home', title: '显示', width: 120 },
            { field: 'va', title: '状态', width: 120 },
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
                    url: '/root/teacher/Del',
                    type: 'post', dataType: 'json', cache: false,
                    data: { id: data.id },
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
            url: '/root/teacher/save',
            type: 'post', dataType: 'json', cache: false, async: false, data: d,
            success: function (res) {
                callback(res);
            },
            error: function (msg) { alert('ajaxError:' + msg.responseText); }
        });
    }


    function initentity(did) {
        //console.log(did);
        element.tabChange('tabView', 'edit');
        $.ajax({
            url: '/root/teacher/getList',
            type: 'get', dataType: 'json', cache: false,
            data: { id: did },
            beforeSend: function () { layer.load(2); },
            success: function (res) {
                $('#kid').val(res.id);
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
