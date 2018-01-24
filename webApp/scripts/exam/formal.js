$.getScript('/content/icheck/icheck.min.js', function () {
    $.getScript('/scripts/exam/downCount.js', function () {
        layui.config({
            base: '/scripts/modules/'
        }).use(['layer', 'util', 'laytpl', 'exameven'], function () {
            var layer = layui.layer,
                util = layui.util,
                laytpl = layui.laytpl,
                exameven = layui.exameven,

                chIDarr, muIDarr, juIDarr,//id数组
                chPick, muPick, juPick,//用户选择
                startTime, //开始时间
                init = {
                    //初始化参数
                    param: function () {
                        chIDarr = new Array(); muIDarr = new Array(); juIDarr = new Array();
                        chPick = new Array(); muPick = new Array(); juPick = new Array();
                        startTime = new Date();
                    }
                },
                even = {
                    //批阅试卷
                    marking: function () {
                        $('.countdown').downCount(0, function (useTime) {
                            var s_elem = $('#subView');
                            if (chIDarr.length > 0) {//要有题库
                                s_elem.find('.dan').each(function () {
                                    var inputs = $(this).find('.checked input');
                                    if (inputs.length > 0) chPick.push(inputs.val());//答题了
                                    else chPick.push('');//未答题追加空
                                });
                            }
                            if (muIDarr.length > 0) {//要有题库
                                s_elem.find('.duo').each(function () {
                                    var c_elem = $(this).find('.checked');
                                    if (c_elem.length > 0) {//答题了
                                        var pickArr = new Array();
                                        c_elem.each(function () {
                                            pickArr.push($(this).find('input').val());
                                        });
                                        muPick.push(pickArr.join());
                                    }
                                    else muPick.push('');//未答题追加空
                                });
                            }
                            if (juIDarr.length > 0) {//要有题库
                                s_elem.find('.pan').each(function () {
                                    var inputs = $(this).find('.checked input');
                                    if (inputs.length > 0) juPick.push(inputs.val());//答题了
                                    else juPick.push('');//未答题追加空
                                });
                            }
                            getData(function (d) {
                                requ.save({
                                    miid: d.miid,
                                    chlist: chIDarr.join(),
                                    mulist: muIDarr.join(),
                                    julist: juIDarr.join(),
                                    chans: chPick.join(':'),
                                    muans: muPick.join(':'),
                                    juans: juPick.join(':'),
                                    useTime: useTime
                                });
                            });
                        });
                    },
                    //提交试卷事件
                    clickBtn: function () {
                        var that = this;
                        $('#submit').off().on('click', function () {
                            if ($('#subCard .yd').length > 0) {
                                if ((new Date() - startTime) > 60000 * 5) {
                                    layer.confirm('确定提交试卷？', { icon: 3 }, function () {
                                        that.marking();
                                    });
                                }
                                else layer.msg('答题时间不足5分钟，不能提交试卷', { icon: 2 });
                            }
                            else layer.msg('没有答题，不能提交试卷', { icon: 2 });
                        });
                    }
                },
                requ = {
                    paper: function (itid) {
                        $.ajax({
                            url: '/api/formalpaper',
                            type: 'GET', dataType: 'JSON', cache: false,
                            data: { itid: itid },
                            beforeSend: function () { init.param(); layer.msg('正在组卷中...', { icon: 16, shade: 0.7 }); },
                            success: function (res) {
                                if (res !== null) {
                                    if (res.code === 1) {
                                        //开始倒计时
                                        $('.countdown').downCount(res.useTime, function () {
                                            even.marking();
                                        });
                                        laytpl(subTpl.innerHTML).render(res, function (htm) {
                                            $('#subView').html(htm);
                                        });
                                        var counts = 1, card = $('#subCard');
                                        $.each(res.dan, function (i, item) {
                                            chIDarr.push(item.id);
                                            card.append('<dd id="n' + counts + '">' + counts + '</dd>');
                                            counts++;
                                        });
                                        $.each(res.duo, function (i, item) {
                                            muIDarr.push(item.id);
                                            card.append('<dd id="n' + counts + '">' + counts + '</dd>');
                                            counts++;
                                        });
                                        $.each(res.pan, function (i, item) {
                                            juIDarr.push(item.id);
                                            card.append('<dd id="n' + counts + '">' + counts + '</dd>');
                                            counts++;
                                        });
                                        exameven.icheck();
                                        exameven.card();
                                        exameven.noAllKey();
                                        even.clickBtn();
                                        layer.closeAll();
                                    }
                                    else {
                                        layer.closeAll();
                                        layer.msg('继教项目已过期', {
                                            icon: 2, end: function () {
                                                window.location.replace('/myitem/list');
                                            }
                                        });
                                    }
                                }
                                else {
                                    layer.closeAll();
                                    layer.msg('组卷失败', {
                                        icon: 2, end: function () {
                                            window.location.replace('/exam/list');
                                        }
                                    });
                                }
                            },
                            error: function (err) { layer.alert(err.responseText); }
                        });
                    },
                    save: function (data) {
                        $.ajax({
                            url: '/api/formalpaper',
                            type: 'POST', dataType: 'JSON', cache: false,
                            data: data,
                            beforeSend: function () { layer.load(2); },
                            success: function () {
                                layer.closeAll();
                                layer.msg('提交试卷成功', {
                                    icon: 1, time: 800, end: function () {
                                        window.location.replace('/exam/list');
                                    }
                                });
                            },
                            error: function (err) { layer.alert(err.responseText); }
                        });
                    }
                };
            $(function () {
                getData(function (d) {
                    util.fixbar({ top: true });
                    exameven.wscroll();
                    requ.paper(d.itid);
                });
            });
            function getData(cb) {
                cb(JSON.parse($('#data').val()));
            }
        });
    });
});