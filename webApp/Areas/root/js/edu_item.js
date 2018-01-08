
layui.config({
    base: '/areas/root/js/'
}).use(['form', 'table', 'selectr', 'uploadpic', 'checks'], function () {
    var form = layui.form, table = layui.table, selectr = layui.selectr, uploadpic = layui.uploadpic, check = layui.checks;
    $(function () {
        $("#checs").hide();
        selectr.cbm(14, $('#typ'));
        selectr.subjectClass(2, $('#scidt'));
        form.render('select');//从新渲染select不然显示不出来 
        eve();
    });
    function eve() {
        var xs = $('#xsNumber'), xm = $('#xueMinute');
        xs.off().on('input', function () {
            if ($.isNumeric(xs[0].value)) {
                xm.text(xs[0].value);
            }
        });
        if ($.isNumeric(xs[0].value)) {
            xm.text(xs[0].value);
        }
    };
    //tab切换事件
    element.on('tab(tabView)', function (d) {
        if (d.index === 1) {
            $("#checs").hide();
            $('#kid').val(0);
            $('#resetForm').click();
            eve();
            uploadpic.init({ elem: '#upPic', hw: '300x180' });
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
        $("#checs").hide();
        form.render('select');
    });
    form.on('select(scid)', function (sd) {
        if (sd.value !== '') {
            $("#checs").show();
            check.subjectClass(sd.value, $('#chec'));
        } else {
            $("#chec").empty();
            $("#checs").hide()
        }
        form.render('checkbox');
    });
    //验证
    form.verify({
        xue: [/^(([0-9]+[\.]?[0-9]+)|[1-9])$/, '学分只能是正整数或正浮点数'],
        xsNumber: [/^[1-9]\d*$/, '学时记分只能是正整数']
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
        var checkbox_info = document.getElementsByName("like");
        var check_list = [];
        for (var key in checkbox_info) {
            if (checkbox_info[key].checked) {
                check_list = check_list + checkbox_info[key].value + ',';
            }
        }
        //alert(scidArrs);
        //console.log(ds);
        save({
            id: parseInt(ds.id),
            bh: ds.bh,
            name: ds.name,
            typ: ds.typ,
            xsNumber: ds.xsNumber,
            scidArr: check_list,
            xf: ds.xf,
            fzr: ds.fzr,
            fzdw: ds.fzdw,
            pic: ds.picurl,
            scid: ds.scid,
            detail: ds.detail,
            isHome: ds.open != null ? true : false,
            valid: ds.opens != null ? true : false
        }, function (res) {
            if (parseInt(ds.id) == 0) {
                layer.msg('保存成功', { icon: 1 });
                $('#resetForm').click();
                $("#checs").hide();
                eve();
                uploadpic.init({ elem: '#upPic', hw: '300x180' });
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
            layer.open({
                id: 'subDetail', type: 2,
                title: '查看项目', time: 20000,
                area: ['700px', '300px'],
                shadeClose: true,
                content: '/root/edu_item/detail/' + data.id
            });
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
        console.log(d)
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
                uploadpic.init({ elem: '#upPic', oldpic: res.pic });
                $('#typ').val(res.typ);
                $('#xsNumber').val(res.xsNumber);
                $('#xf').val(res.xf);
                $('#fzr').val(res.fzr);
                $('#fzdw').val(res.fzdw);
                $('#detail').val(res.detail);
                $('#scidt').val(res.pid);
                selectr.subjectClass(res.pid, $('#scid'));
                $('#scid').val(res.scid);
                checkse(res.scid, res.scidArr);
                document.getElementById('open').checked = res.isHome;
                document.getElementById('opens').checked = res.valid;
                eve();
                form.render();

            },
            complete: function () { layer.closeAll('loading'); },
            error: function (msg) { alert('ajaxError:' + msg.responseText); }
        });
    }
    //获取复选框、给已经选择的复选框加状态
    function checkse(id, re) {

        $("#checs").show();
        check.subjectClass(id, $('#chec'));
        if (re != null) {
            var scidArrs = re.split(",");
            var che = $("input[name='like']")
            for (var key in che) {
                $.each(scidArrs, function (index, value) {
                    if (value == che[key].value)
                        che[key].checked = true;
                })
            }
        }
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
