
layui.config({ base: '/areas/root/js/' }).use(['form', 'table', 'selectr'], function () {
    var form = layui.form, table = layui.table, selectr = layui.selectr;
    $(function () {
        selectr.cbm(14, $('#typ'));
        selectr.subjectClass(2, $('#scidt'));
        form.render('select');//从新渲染select不然显示不出来    
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
    form.on('select(scidt)', function (sd) {
        if (sd.value !== '') selectr.subjectClass(sd.value, $('#scid'));
        else $("#scid").empty();
        form.render('select');
    });
    //验证
    form.verify({
        xue: [/^(([0-9]+[\.]?[0-9]+)|[1-9])$/, '学分只能是正整数或正浮点数'],
       //bh: [/^[1-9]\d*$/, '项目编号只能是正整数']
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
        console.log(ds.data);
        save({
            id: parseInt(ds.id),
            bh: ds.bh,
            name: ds.name ,
            typ: ds.typ,
            xf: ds.xf,
            fzr: ds.fzr,
            fzdw: ds.fzdw,
            pic: '',
            scid: ds.scid,
            detail:ds.detail,
            isHome: ds.open != null ? true : false,
            valid: ds.opens != null ? true : false
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
        elem: '#tableDom', url: '/root/edu_item/listdata',
        page: {
            hash: 'fenye',
            curr: location.hash.replace('#!fenye=', '') || 1
        }, limits: [10, 15],
        cols: [[
            { field: 'rows', title: '序号', width: 90, align: 'center', unresize: true },
            { field: 'bh', title: '编号', width: 120 },
            { field: 'name', title: '名称', width: 200 },
            { field: 'xf', title: '学分', width: 100 },
            { field: 'fzr', title: '负责人', width: 120 },
            { field: 'fzdw', title: '负责单位', width: 280 },
            { field: 'home', title: '显示', width: 100 },
            { field: 'va', title: '状态', width: 100 },
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
                    url: '/root/edu_item/del',
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
        //alert(1);
        $.ajax({
            url: '/root/edu_item/save',
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
            url: '/root/edu_item/GetList',
            type: 'get', dataType: 'json', cache: false,
            data: { id: did },
            beforeSend: function () { layer.load(2); },
            success: function (res) {
                $('#kid').val(res.id);
                $('#bh').val(res.bh);
                $('#name').val(res.name);
                //$('#title').val(res.title);
                $('#typ').val(res.typ);
                $('#xf').val(res.xf);
                $('#fzr').val(res.fzr);
                $('#fzdw').val(res.fzdw);
                $('#detail').val(res.detail);
                $('#scidt').val(res.pid);
                selectr.subjectClass(res.pid, $('#scid'));
                $('#scid').val(res.scid);
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
