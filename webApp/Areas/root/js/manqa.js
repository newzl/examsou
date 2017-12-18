layui.config({ base: '/areas/root/js/' }).use(['form', 'table', 'glevent'], function () {
    var form = layui.form, table = layui.table, glevent = layui.glevent;
    $(function () {
        glevent.bindFind();
        glevent.bindCommon();
        glevent.bindEditor();
        startImport();
    });
    form.verify({
        versid: function (va) {
            if (parseInt(va) <= 0) { return '请选择题库类型'; }
        }
    });
    //tab切换事件
    element.on('tab(tabView)', function (d) {
        if (d.index === 1) {
            $('#kid,#sid').val(0);
            $('#subnav').text('请选择题库类型');
            $('#resetForm').click();
            glevent.bindKey(true);
        }
        else {
            glevent.bindKey(false);
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
            sid: parseInt(ds.sid),
            title: ds.title,
            subs: ds.subs,
            result: ds.result,
            explain: ds.explain.length > 0 ? ds.explain : null,
            instate: ds.instate === 'on' ? true : false
        }, function (res) {
            if (parseInt(ds.id) <= 0) {
                if (res <= 0) layer.msg('该题目在题库类型中已经存在', { icon: 2 });
                else {
                    layer.msg('保存成功', { icon: 1 });
                    $('#resetForm').click();
                    $('#title').focus();
                }
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
    //开始导入
    function startImport() {
        $('#importForm').off().on('click', function () {
            var condom = $('#content'), subjson = condom.val(), isid = parseInt($('#isid').val());
            if (isid > 0 && subjson.length > 0) {
                var obj = $.parseJSON(subjson), wcd;
                layer.open({
                    type: 1, title: false, closeBtn: false,
                    area: '300px;', resize: false, shade: 0.8, scrollbar: false,
                    content: '<div style="padding:30px;line-height:22px;">正在导入试题……<br/><br/>完成度【' + obj.length + '/<span id="wcd">0</span>】</div>',
                    success: function () { wcd = $('#wcd'); }
                });
                //模拟loading
                var i = 0, timer = setInterval(function () {
                    if (i < obj.length) {
                        save({
                            id: 0,
                            sid: isid,
                            title: obj[i].tit,
                            subs: obj[i].tit,
                            explain: obj[i].exp,
                            result: obj[i].ans,
                            instate: true
                        }, function (res) {
                            if (res > 0) {
                                wcd.text(i + 1);
                                i++;
                            }
                            else {
                                clearInterval(timer);
                                layer.alert('该题目在题库类型中已经存在', { icon: 2 }, function () {
                                    layer.closeAll();
                                    return;
                                });
                            }
                        });
                    }
                    else {
                        clearInterval(timer);
                        condom.val('');
                        layer.closeAll();
                        layer.msg('导入成功', { icon: 1 });
                        return;
                    }
                }, 200);
            }
            else {
                layer.msg('必填项不能为空', { icon: 5, anim: 6 });
            }
        });
    }
    form.on('checkbox(stateView)', function (obj) {
        var kid = parseInt(this.value);
        $.ajax({
            url: '/root/manqa/instate',
            type: 'post', dataType: 'json', cache: false,
            data: {
                id: kid,
                state: obj.elem.checked
            },
            beforeSend: function () { layer.load(2); },
            success: function () {
                layer.msg('更新成功', { icon: 1 });
            },
            complete: function () { layer.closeAll('loading'); },
            error: function (msg) { alert('ajaxError:' + msg.responseText); }
        });
    });
    table.render({
        elem: '#tableDom', url: '/root/manqa/listdata',
        page: {
            hash: 'fenye',
            curr: location.hash.replace('#!fenye=', '') || 1
        }, limits: [10],
        cols: [[
            { field: 'rows', title: '序号', width: 80, align: 'center', unresize: true },
            { field: 'sname', title: '类型', width: 200 },
            { field: 'title', title: '题目', width: 500 },
            { field: 'result', title: '答案', width: 600 },
            { field: 'date', title: '添加日期', width: 110, unresize: true },
            { field: 'state', title: '是否入库', width: 110, templet: '#stateTpl', unresize: true },
            { fixed: 'right', width: 120, align: 'center', toolbar: '#operate' }
        ]]
    });
    table.on('tool(tableView)', function (obj) {
        var data = obj.data;
        if (obj.event === 'detail') { //查看
            openDetail(data.id);
        }
        else if (obj.event === 'edit') {
            initentity(data.id)
        }
    });
    function save(d, callback) {
        $.ajax({
            url: '/root/manqa/save',
            type: 'post', dataType: 'json', cache: false, async: false, data: d,
            success: function (res) {
                callback(res);
            },
            error: function (msg) { alert('ajaxError:' + msg.responseText); }
        });
    }
    function openDetail(id) {
        layer.open({
            id: 'subDetail', type: 2,
            title: '查看试题', time: 20000,
            area: ['900px', '80%'],
            shadeClose: true,
            content: '/root/manqa/detail/' + id
        });
    }
    function initentity(did) {
        element.tabChange('tabView', 'edit');
        $.ajax({
            url: '/root/manqa/getentity',
            type: 'get', dataType: 'json', cache: false,
            data: { id: did },
            beforeSend: function () { layer.load(2); },
            success: function (res) {
                $('#kid').val(res.id);
                $('#subnav').text(res.snav);
                $('#sid').val(res.sid);
                $('#title').val(res.title);
                $('#subs').val(res.subs);
                $('#result').val(res.result);
                if (res.explain !== null) $('#explain').val(res.explain);
                $('#instate').attr('checked', res.instate);
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