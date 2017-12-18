//全局事件
"use strict";
layui.define('form', function (exports) {
    var form = layui.form;
    var even = {
        bindCommon: function () {
            //移除tab事件
            $(document).off('click', '.layui-tab-title li');
            form.verify({
                isExplain: function (va) {
                    if (va.length > 300) return '说明内容不能超过300字符！';
                },
                min10: function (va) {
                    if (va < 10) return '答题时间不能低于10分钟！';
                },
                versid: function (va) {
                    if (parseInt(va) <= 0) { return '请选择题库类型'; }
                },
                isScore: [/^\d+(\.{0,1}\d+){0,1}$/, '请输入正确的分数'],
                isInteger: [/^\d+$/, '请输入非负整数']
            });
            //截止日期显示与隐藏
            form.on('checkbox(isLimit)', function (d) {
                var dom = $('#setDate');
                if (d.elem.checked) dom.show();
                else {
                    dom.hide();
                    dom.find('input').val('');
                }
            });
            //绑定上一步事件
            $('.previous').off().on('click', function () {
                even.tabPrev();
            });
        },
        bindFind: function (elem) {
            elem.off().on('click', function () {
                var self = $(this);
                layer.open({
                    type: 2, title: '选择题型', shadeClose: true,
                    shade: 0.3, area: ['400px', '600px'],
                    content: '/root/common/findtree',
                    btn: ['确定', '取消'],
                    yes: function (i, lo) {
                        var iframeWin = window[lo.find('iframe')[0]['name']];
                        iframeWin.getNode(function (res) {
                            if (res !== null) {
                                $('#findsid').val(res.id);
                                self.val(res.name);
                                layer.closeAll();
                            }
                            else {
                                layer.msg('没有选择任何题型');
                            }
                        });
                    }
                });
            });
        },
        tabPrev: function () {//上一步
            var ths = 'layui-this', shw = 'layui-show';
            $('.layui-tab-title .' + ths).removeClass(ths).prev().addClass(ths);
            $('.layui-tab-content .' + shw).removeClass(shw).prev().addClass(shw);
        },
        tabNext: function () {//下一步
            var ths = 'layui-this', shw = 'layui-show';
            $('.layui-tab-title .' + ths).removeClass(ths).next().addClass(ths);
            $('.layui-tab-content .' + shw).removeClass(shw).next().addClass(shw);
        },
        bindInput: function () {//数据变化事件
            $("#subNum").on('input', '.txtNum', function () {
                var othis = $(this), tr = othis.closest("tr"), thisVa = othis.val();
                var score = tr.find(".txtScore").val();
                var amount = 0
                if ($.isNumeric(thisVa) && $.isNumeric(score)) {
                    amount = thisVa * score;
                }
                tr.find(".txtAmount").val(amount.toFixed(2));
                even.calcSum();
            }).on('input', '.txtScore', function () {
                var othis = $(this), tr = othis.closest("tr"), thisVa = othis.val();
                var num = tr.find(".txtNum").val();
                var amount = 0
                if ($.isNumeric(num) && $.isNumeric(thisVa)) {
                    amount = num * thisVa;
                }
                tr.find(".txtAmount").val(amount.toFixed(2));
                even.calcSum();
            });
        },
        calcSum: function () {//计算总计
            var tsum = 0, setnum = $(".setNumber");
            $("#subNum .txtAmount").each(function () {
                var val = $(this).val();
                if ($.isNumeric(val)) {
                    tsum += parseFloat(val);
                }
            });
            if (tsum > 0) setnum.show();
            else setnum.hide();
            $(".subTotal").html(tsum.toFixed(2));
        },
        //参考对象
        ckdx_all: function () {
            form.on('checkbox(bumen)', function (data) {
                if (!data.elem.checked) {//取消选中-把子类也取消选中
                    var tarr = data.value.split(':');
                    $('.bm' + tarr[0]).each(function () {
                        $(this).children('td').eq(0).children('input[type=checkbox]')[0].checked = false;
                    });
                    form.render('checkbox');
                }
            });
            form.on('checkbox(keshi)', function (data) {
                if (!data.elem.checked) {//取消选中-把子类也取消选中
                    var tarr = data.value.split(':');
                    $('.bm' + tarr[0] + '.ks' + tarr[1]).each(function () {
                        $(this).children('td').eq(0).children('input[type=checkbox]')[0].checked = false;
                    });
                    form.render('checkbox');
                }
            });
            form.on('checkbox(jibie)', function (data) {
                if (!data.elem.checked) {//取消选中-把子类也取消选中
                    var tarr = data.value.split(':');
                    $('.bm' + tarr[0] + '.ks' + tarr[1] + '.jb' + tarr[2]).each(function () {
                        $(this).children('td').eq(0).children('input[type=checkbox]')[0].checked = false;
                    });
                    form.render('checkbox');
                }
            });
        },
        ckdx_bm: function () {
            $('.spreadBM').off().on('click', function () {
                var othis = $(this),
                    trDOM = othis.parent('td').parent('tr'),
                    bmid = othis.data('bmid');
                if (othis.hasClass('load')) {//判断科室是否加载
                    if (othis.hasClass('open')) {//是否已经展开-关闭
                        othis.children('i').html('&#xe623;');
                        $('.bm' + bmid).each(function () {
                            othis.find('td>a>i').html('&#xe623;');
                            othis.find('td>a').removeClass('open');
                        });
                        trDOM.nextAll('.bm' + bmid).hide();
                    }
                    else {//展开
                        othis.children('i').html('&#xe625;');
                        trDOM.nextAll('.tree1.bm' + bmid).show();
                    }
                    othis.toggleClass('open');
                }
                else {
                    othis.addClass('load open');
                    othis.children('i').html('&#xe625;');
                    even.spread_ks(bmid, trDOM);//展开科室
                }
            });
        },
        ckdx_ks: function () {
            $('.spreadKS').off().on('click', function () {
                var othis = $(this),
                    trDOM = othis.parent('td').parent('tr'),
                    bmid = othis.data('bmid'),
                    ksid = othis.data('ksid');
                if (othis.hasClass('load')) {//判断科室是否加载
                    if (othis.hasClass('open')) {//是否已经展开-关闭
                        othis.children('i').html('&#xe623;');
                        $('.bm' + bmid + '.ks' + ksid).each(function () {
                            othis.find('td>a>i').html('&#xe623;');
                            othis.find('td>a').removeClass('open');
                        });
                        trDOM.nextAll('.bm' + bmid + '.ks' + ksid).hide();
                    }
                    else {//展开
                        othis.children('i').html('&#xe625;');
                        trDOM.nextAll('.tree2.bm' + bmid + '.ks' + ksid).show();
                    }
                    othis.toggleClass('open');
                }
                else {
                    othis.addClass('load open');
                    othis.children('i').html('&#xe625;');
                    even.spread_jb(bmid, ksid, trDOM);//展开级别
                }
            });
        },
        ckdx_jb: function () {
            $('.spreadJB').off().on('click', function () {
                var othis = $(this),
                    trDOM = othis.parent('td').parent('tr'),
                    bmid = othis.data('bmid'),
                    ksid = othis.data('ksid'),
                    jbid = othis.data('jbid');
                if (othis.hasClass('load')) {//判断科室是否加载
                    if (othis.hasClass('open')) {//是否已经展开-关闭
                        othis.children('i').html('&#xe623;');
                        trDOM.nextAll('.bm' + bmid + '.ks' + ksid + '.jb' + jbid).hide();
                    }
                    else {//展开
                        othis.children('i').html('&#xe625;');
                        trDOM.nextAll('.tree3.bm' + bmid + '.ks' + ksid + '.jb' + jbid).show();
                    }
                    othis.toggleClass('open');
                }
                else {
                    othis.addClass('load open');
                    othis.children('i').html('&#xe625;');
                    even.spread_zc(bmid, ksid, jbid, trDOM);//展开职称
                }
            });
        },
        //初始化部门
        spread_bm: function () {
            $.ajax({
                url: '/handler/admin/zj_getbm.ashx',
                type: 'get', dataType: 'json', cache: false,
                data: null,
                beforeSend: function () { layer.load(2); },
                success: function (res) {
                    var dom = $('#BMgridView');
                    dom.empty();
                    if (res != null) {
                        $.each(res, function (i, json) {
                            var htm = '<tr>';
                            htm += '<td><a href="javascript:;" class="spreadBM" data-bmid="' + json.ID + '"><i class="layui-icon">&#xe623;</i></a><input type="checkbox" value="' + json.ID + ':0:0:0" title="' + json.name + '" lay-filter="bumen" lay-skin="primary"></td>';
                            htm += '<td>' + json.must + '</td>';
                            htm += '<td>' + json.number + '</td>';
                            htm += '</tr>';
                            dom.append(htm);
                        });
                    }
                    else {
                        dom.append('<tr><td colspan="3" style="color:#f60;text-align:center;">没有记录</td></tr>');
                    }
                },
                complete: function () {
                    layer.closeAll('loading');
                    form.render('checkbox');
                    even.ckdx_all();
                    even.ckdx_bm();
                },
                error: function (msg) { alert('ajaxError:' + msg.responseText); }
            });
        },
        //展开科室
        spread_ks: function (BMID, trDom) {//部门ID，tr元素
            $.ajax({
                url: '/handler/admin/zj_getks.ashx',
                type: 'get', dataType: 'json', cache: false,
                data: { pid: BMID },
                beforeSend: function () { layer.load(2); },
                success: function (res) {
                    if (res != null) {
                        var htm;
                        $.each(res, function (i, json) {
                            htm += '<tr class="tree1 bm' + BMID + '">';
                            htm += '<td><a href="javascript:;" class="spreadKS" data-ksid="' + json.ID + '" data-bmid="' + BMID + '"><i class="layui-icon">&#xe623;</i></a><input type="checkbox" value="' + BMID + ':' + json.ID + ':0:0" title="' + json.name + '" lay-filter="keshi" lay-skin="primary"></td>';
                            htm += '<td>' + json.must + '</td>';
                            htm += '<td>' + json.number + '</td>';
                            htm += '</tr>';
                        });
                        trDom.after(htm);
                    }
                    else {
                        alert('没有记录');
                    }
                },
                complete: function () {
                    layer.closeAll('loading');
                    form.render('checkbox');
                    even.ckdx_ks();
                },
                error: function (msg) { alert('ajaxError:' + msg.responseText); }
            });
        },
        //展开级别
        spread_jb: function (BMID, KSID, trDom) {
            $.ajax({
                url: '/handler/sele_jbzc.ashx',
                type: 'get', dataType: 'json', cache: false,
                data: { pid: 0 },
                beforeSend: function () { layer.load(2); },
                success: function (res) {
                    if (res != null) {
                        var htm;
                        $.each(res, function (i, json) {
                            htm += '<tr class="tree2 bm' + BMID + ' ks' + KSID + '">';
                            htm += '<td  colspan="3"><a href="javascript:;" class="spreadJB" data-jbid="' + json.val + '" data-ksid="' + KSID + '" data-bmid="' + BMID + '"><i class="layui-icon">&#xe623;</i></a><input type="checkbox" value="' + BMID + ':' + KSID + ':' + json.val + ':0" title="' + json.text + '" lay-filter="jibie" lay-skin="primary"></td>';
                            htm += '</tr>';
                        });
                        trDom.after(htm);
                    }
                    else {
                        alert('没有记录');
                    }
                },
                complete: function () {
                    layer.closeAll('loading');
                    form.render('checkbox');
                    even.ckdx_jb();
                },
                error: function (msg) { alert('ajaxError:' + msg.responseText); }
            });
        },
        //打开职称
        spread_zc: function (BMID, KSID, JBID, trDom) {
            $.ajax({
                url: '/handler/sele_jbzc.ashx',
                type: 'get', dataType: 'json', cache: false,
                data: { pid: JBID },
                beforeSend: function () { layer.load(2); },
                success: function (res) {
                    if (res != null) {
                        var htm;
                        $.each(res, function (i, json) {
                            htm += '<tr class="tree3 bm' + BMID + ' ks' + KSID + ' jb' + JBID + '">';
                            htm += '<td colspan="3"><input type="checkbox" value="' + BMID + ':' + KSID + ':' + JBID + ':' + json.val + '" title="' + json.text + '" lay-skin="primary"></td>';
                            htm += '</tr>';
                        });
                        trDom.after(htm);
                    }
                    else {
                        alert('没有记录');
                    }
                },
                complete: function () { layer.closeAll('loading'); form.render('checkbox'); },
                error: function (msg) { alert('ajaxError:' + msg.responseText); }
            });
        }
    };
    exports('zjevent', even);
});