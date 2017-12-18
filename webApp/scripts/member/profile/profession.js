layui.config({
    base: '/scripts/modules/'
}).use(['form', 'validator', 'selectr'], function () {
    var form = layui.form,
        validator = layui.validator,
        selectr = layui.selectr,
        county_xzbm = null;
    form.verify({
        verIdCard: function (val) {
            if (!validator.isIDCard(val)) return '请输入正确的身份证号';
        }
    });
    $(function () {
        layer.load(2);
        selectr.xzbm('520000000', $('#city'));
        selectr.jbzc(0, $('#jibie'));
        loadInfo();
        layer.closeAll('loading');
    })
    form.on('select(city)', function (sd) {
        var cdom = $('#county');
        if (sd.value.length > 0) selectr.xzbm(sd.value, cdom);//初始化县
        else cdom.empty();
        county_xzbm = null;
        form.render('select');
    });
    form.on('select(county)', function (sd) {
        county_xzbm = sd.value;
    });
    form.on('select(jibie)', function (sd) {
        var zdom = $('#zhicheng');
        if (sd.value.length > 0) selectr.jbzc(sd.value, zdom);
        else zdom.empty();
        form.render('select');
    });
    $('#seleCompany').click(function () {
        if (county_xzbm === null) layer.msg('请选择所在地区', { icon: 2, anim: 6 });
        else {
            layer.open({
                type: 1, title: '选择单位', offset: '100px', shadeClose: true,
                area: ['400px', '440px'],
                content: '<div class="layui-form" style="padding:10px"><select id="companyView" lay-filter="companyView" lay-search></select></div>',
                success: function () {
                    selectr.company(county_xzbm, $('#companyView'));
                    form.render('select');
                }
            });
        }
    });
    form.on('select(companyView)', function (sd) {
        var opt = sd.elem.options;
        $('#company').val(opt[opt.selectedIndex].text);
        layer.closeAll();
    });
    form.on('submit(saveForm)', function (fd) {
        var ob = fd.field;
        $.ajax({
            url: '/profile/saveprofession',
            type: 'post', dataType: 'json', cache: false,
            data: {
                county: ob.county,
                company: ob.company,
                jibie: ob.jibie,
                zhicheng: ob.zhicheng,
                idcard: ob.idcard,
                bz: ob.bz.length > 0 ? ob.bz : null
            },
            beforeSend: function () { layer.load(2); },
            success: function (res) {
                if (res > 0) {
                    layer.msg('保存成功', {
                        icon: 1, time: 1000, end: function () {
                            window.location.href = "/profile/core";
                        }
                    });
                }
                else if (res === 0) {
                    layer.msg('身份证号已经存在，不能重复', { icon: 2, anim: 6 });
                }
                else {
                    console.log(res);
                }
            },
            complete: function () { layer.closeAll('loading'); },
            error: function (msg) { alert('ajaxError:' + msg.responseText); }
        });
        return false;
    });
    //加载信息
    function loadInfo() {
        $.ajax({
            url: '/profile/professionentity', type: 'get',
            dataType: 'json', cache: false, async: false,
            data: null,
            success: function (res) {
                if (res.county !== null) {
                    selected('city', res.city);
                    selectr.xzbm(res.city, $('#county'));
                    selected('county', res.county);
                    county_xzbm = res.county;
                }
                if (res.zhicheng !== null) {
                    selected('jibie', res.jibie);
                    selectr.jbzc(res.jibie, $('#zhicheng'));
                    selected('zhicheng', res.zhicheng);
                }
                if (res.company !== null) $('#company').val(res.company);
                if (res.idcard !== null) $('#idcard').val(res.idcard);
                if (res.bz !== null) $('#bz').val(res.bz);
                form.render('select');
            },
            error: function (msg) { alert('ajaxError:' + msg); console.log(msg); }
        });
    }
});