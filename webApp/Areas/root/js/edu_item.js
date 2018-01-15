
layui.config({
    base: '/areas/root/js/'
}).use(['form', 'table', 'selectr', 'uploadpic', 'checks'], function () {
    var form = layui.form, table = layui.table, selectr = layui.selectr, uploadpic = layui.uploadpic, check = layui.checks;
    $(function () {
        $("#checs").hide();
        selectr.cbm(14, $('#typ'));
        selectr.subjectClass(2, $('#scidt'));
        form.render('select');//从新渲染select不然显示不出来 
        eve.inputr();
        eve.chsScorest();
        eve.musScorest();
        eve.jusScorest();

    });
    //function eve() {
    //    var xs = $('#xsNumber'), xm = $('#xueMinute');
    //    xs.off().on('input', function () {
    //        if ($.isNumeric(xs[0].value)) {
    //            xm.text(xs[0].value);
    //        }
    //    });
    //    if ($.isNumeric(xs[0].value)) {
    //        xm.text(xs[0].value);
    //    }
    //};
    var eve = {
        //输入学时时长计算需学分钟
        inputr: function () {
            var xs = $('#xsNumber'), xm = $('#xueMinute');
            xs.off().on('input', function () {
                if ($.isNumeric(xs[0].value)) {
                    xm.text(xs[0].value);
                }
            });
            if ($.isNumeric(xs[0].value)) {
                xm.text(xs[0].value);
            }
        },
        //动态生成单项选择题总分
        chsScorest: function () {
            var xs = $('.chst'), xm = $('#chsScores'), zf = $('#dzxztzf');
            xs.off().on('input', function () {
                if ($.isNumeric(xs[0].value) && $.isNumeric(xs[1].value)) {
                    xm.text(xs[0].value * xs[1].value);
                    zf.val(xs[0].value * xs[1].value);
                    $('.dx').show();
                    Scoress();
                }
            });
        },
        //动态生成多项选择题总分
        musScorest: function () {
            var xs = $('.must'), xm = $('#musScores'), sc = $("#dxxztzf");
            xs.off().on('input', function () {
                if ($.isNumeric(xs[0].value) && $.isNumeric(xs[1].value)) {
                    $('.dxt').show();
                    xm.text(xs[0].value * xs[1].value);
                    sc.val(xs[0].value * xs[1].value);
                    Scoress();
                    //var sct = parseFloat($("#jusScores").val());
                    //var dxtl = xs[0].value * xs[1].value + sct;;
                    //sc.val(dxtl);
                }
            });
        },
        //动态生成判断题总分
        jusScorest: function () {
            var xs = $('.just'), xm = $('#jusScores'), sc = $("#pdtzf");
            xs.off().on('input', function () {
                if ($.isNumeric(xs[0].value) && $.isNumeric(xs[1].value)) {
                    $('.pd').show();
                    xm.text(xs[0].value * xs[1].value);
                    sc.val(xs[0].value * xs[1].value);
                    Scoress();
                    //var sct = parseFloat($("#jusScores").val());
                    //var dxtl = xs[0].value * xs[1].value + sct;;
                    //sc.val(dxtl);
                }
            });
        }
    };
    function Scoress() {
        var dxt = parseFloat($("#dzxztzf").val());
        var dxtf = parseFloat($("#dxxztzf").val());
        var pdt = parseFloat($("#pdtzf").val());
        $("#scores").val(dxt + dxtf + pdt);
    }
    //tab切换事件
    element.on('tab(tabView)', function (d) {
        if (d.index === 1) {
            $("#checs").hide();
            $('#kid').val(0);
            $('#resetForm').click();
            //eve();
            eve.inputr();
            $('.dx').hide();
            $('.dxt').hide();
            $('.pd').hide();
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
        xsNumber: [/^[1-9]\d*$/, '学时记分只能是正整数'],
        xue: [/^(([0-9]+[\.]?[0-9]+)|[1-9])$/, '学分只能是正整数或正浮点数'],
        passScore: [/^(([0-9]+[\.]?[0-9]+)|[1-9])$/, '及格分数只能是正整数或正浮点数'],
        chs: [/^[1-9]\d*$/, '单项选择题数量只能是正整数'],
        chvs: [/^(([0-9]+[\.]?[0-9]+)|[1-9])$/, '单项选择题分数只能是正整数或正浮点数'],
        mus: [/^[1-9]\d*$/, '多项选择题数量只能是正整数'],
        muvs: [/^(([0-9]+[\.]?[0-9]+)|[1-9])$/, '多项选择题分数只能是正整数或正浮点数'],
        jus: [/^[1-9]\d*$/, '判断题数量只能是正整数'],
        juvs: [/^(([0-9]+[\.]?[0-9]+)|[1-9])$/, '判断题分数只能是正整数或正浮点数'],

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
        if (d.field.startDate >= d.field.cutOffDate) {
            layer.msg('项目开始执行时间不能大于或等于项目停止时间', { icon: 2, anim: 6 });
            return false;
        }
        if (d.field.chs == '' && d.field.mus == '' && d.field.jus == '') {
            layer.msg('考题数量不能为空', { icon: 2, anim: 6 });
            return false;
        }
        if (d.field.chs != '' && d.field.chvs == '') {
            layer.msg('单项选择题的分数不能为空', { icon: 2, anim: 6 });
            return false;
        }
        if (d.field.mus != '' && d.field.muvs == '') {
            layer.msg('多项选择题的分数不能为空', { icon: 2, anim: 6 });
            return false;
        }
        if (d.field.jus != '' && d.field.juvs == '') {
            layer.msg('判断题的分数不能为空', { icon: 2, anim: 6 });
            return false;
        }
        if (d.field.passScore >= d.field.scores) {
            layer.msg('及格分数只能小于总分', { icon: 2, anim: 6 });
            return false;
        }
        //console.log(d.field.scores);
        save({
            id: parseInt(ds.id),
            bh: ds.bh,
            name: ds.name,
            typ: ds.typ,
            xsNumber: ds.xsNumber,
            scidArr: check_list,
            xf: ds.xf,
            scores: ds.scores,
            chs: ds.chs,
            chvs: ds.chvs,
            mus: ds.mus,
            muvs: ds.muvs,
            jus: ds.jus,
            juvs: ds.juvs,
            fzr: ds.fzr,
            fzdw: ds.fzdw,
            pic: ds.picurl,
            scid: ds.scid,
            detail: ds.detail,
            startDate: ds.startDate,
            passScore: ds.passScore,
            useTime: ds.useTime,
            endDate: ds.cutOffDate,
            isHome: ds.open != null ? true : false,
            valid: ds.opens != null ? true : false
        }, function (res) {
            if (parseInt(ds.id) == 0) {
                layer.msg('保存成功', { icon: 1 });
                $('#resetForm').click();
                $("#checs").hide();
                $('.dx').hide();
                $('.dxt').hide();
                $('.pd').hide();
                eve.inputr();
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
                        if (res == -1) {
                            layer.msg('当前项目已经被使用不能删除', { icon: 2, anim: 6 });
                        } else {
                            obj.del(); //删除对应行（tr）的DOM结构
                            layer.msg('删除成功', { icon: 1 });
                        }
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
                $('#scores').val(res.scores == 0 ? '' : res.scores);
                $('#chs').val(res.chs == 0 ? '' : res.chs);
                $('#chvs').val(res.chvs == 0 ? '' : res.chvs);
                $('#mus').val(res.mus == 0 ? '' : res.mus);
                $('#muvs').val(res.muvs == 0 ? '' : res.muvs);
                $('#jus').val(res.jus == 0 ? '' : res.jus);
                $('#juvs').val(res.juvs == 0 ? '' : res.juvs);
                $('#useTime').val(res.useTime);
                $('#passScore').val(res.passScore);
                $('#fzr').val(res.fzr);
                $('#fzdw').val(res.fzdw);
                $('#detail').val(res.detail);
                $('#scidt').val(res.pid);
                selectr.subjectClass(res.pid, $('#scid'));
                $('#scid').val(res.scid);
                checkse(res.scid, res.scidArr);
                document.getElementById('open').checked = res.isHome;
                document.getElementById('opens').checked = res.valid;
                var date = new Date(parseInt(res.startDate.slice(6)));   //获取到时间  年月日时分秒
                hour = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
                minute = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
                var result = date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate() + ' ' + hour + ':' + minute;   //获取年月日
                $('#startDate').val(result == '1-1-1' ? '' : result);
                var datec = new Date(parseInt(res.endDate.slice(6)));   //获取到时间  年月日时分秒
                hourc = datec.getHours() < 10 ? "0" + datec.getHours() : datec.getHours();
                minutec = datec.getMinutes() < 10 ? "0" + datec.getMinutes() : datec.getMinutes();
                var resultc = datec.getFullYear() + '-' + (datec.getMonth() + 1) + '-' + datec.getDate() + ' ' + hourc + ':' + minutec;   //获取年月日
                $('#cutOffDate').val(resultc == '1-1-1' ? '' : resultc);
                if ($('#chs').val != '') { $('#chsScores').text(res.chs * res.chvs); $('.dx').show(); }
                if ($('#mus').val != '') { $('#musScores').text(res.mus * res.muvs); $('.dxt').show(); }
                if ($('#jus').val != '') { $('#jusScores').text(res.jus * res.juvs); $('.pd').show(); }
                $("#dzxztzf").val(res.chs * res.chvs);
                $("#dxxztzf").val(res.mus * res.muvs);
                $("#pdtzf").val(res.jus * res.juvs);
                eve.inputr();
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
