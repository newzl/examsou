$.getScript('/content/icheck/icheck.min.js', function () {
    layui.config({
        base: '/scripts/modules/'
    }).use(['layer', 'util', 'laytpl', 'exameven'], function () {
        var layer = layui.layer,
            util = layui.util,
            laytpl = layui.laytpl,
            exameven = layui.exameven,

            chIDarr, muIDarr, juIDarr,//id数组
            chAnswer, muAnswer, juAnswer,//答案
            init = {
                //初始化参数
                param: function () {
                    chIDarr = new Array(); muIDarr = new Array(); juIDarr = new Array();
                    chAnswer = new Array(); muAnswer = new Array(); juAnswer = new Array();
                }
            },
            even = {
                //选择答案  answer：用户选择
                checkAnswer: function (arr) {
                    var s_elem = $('#subView');
                    if (chIDarr.length > 0) {//要有题库
                        s_elem.find('.dan').each(function (i) {
                            var that = $(this),
                                usc = arr.charr[i],
                                card = $('#' + that.attr('id').replace(/t/gm, 'n')),
                                da = $('<div class="item" style="color: red;">答案：' + chAnswer[i] + '</div>');
                            if (usc.length > 0) {//答题了
                                that.find('input[value="' + usc + '"]').iCheck('check');
                                if (usc === chAnswer[i]) card.addClass('yd');
                                else {//答错
                                    card.addClass('cd');
                                    that.append(da);
                                }
                            }
                            else that.append(da);
                        });
                    }
                    if (muIDarr.length > 0) {//要有题库
                        s_elem.find('.duo').each(function (i) {
                            var that = $(this),
                                usc = arr.muarr[i],
                                card = $('#' + that.attr('id').replace(/t/gm, 'n')),
                                da = $('<div class="item" style="color: red;">答案：' + muAnswer[i] + '</div>');
                            if (usc.length > 0) {//答题了
                                var uscArr = usc.split(',');
                                $.each(uscArr, function (j, item) {
                                    that.find('input[value="' + item + '"]').iCheck('check');
                                })
                                if (usc === muAnswer[i]) card.addClass('yd');
                                else {//答错
                                    card.addClass('cd');
                                    that.append(da);
                                }
                            }
                            else that.append(da);
                        });
                    }
                    if (juIDarr.length > 0) {//要有题库
                        s_elem.find('.pan').each(function (i) {
                            var that = $(this),
                                usc = arr.juarr[i],
                                card = $('#' + that.attr('id').replace(/t/gm, 'n')),
                                an = juAnswer[i] === 1 ? '正确' : '错误',
                                da = $('<div class="item" style="color: red;">答案：' + an + '</div>');
                            if (usc.length > 0) {//答题了
                                that.find('input[value="' + usc + '"]').iCheck('check');
                                if (parseInt(usc) === parseInt(juAnswer[i])) card.addClass('yd');
                                else {//答错
                                    card.addClass('cd');
                                    that.append(da);
                                }
                            }
                            else that.append(da);
                        });
                    }
                    s_elem.find('input').iCheck('disable');
                }
            },
            requ = {
                paper: function (ansid) {
                    $.ajax({
                        url: '/api/showformal',
                        type: 'GET', dataType: 'JSON', cache: false,
                        data: { ansid: ansid },
                        beforeSend: function () { init.param(); layer.msg('正在组卷中...', { icon: 16, shade: 0.7 }); },
                        success: function (res) {
                            if (res !== null) {
                                $('.countdown').text(res.usetime);
                                laytpl(subTpl.innerHTML).render(res, function (htm) {
                                    $('#subView').html(htm);
                                });
                                var counts = 1, card = $('#subCard');
                                $.each(res.dan, function (i, item) {
                                    chIDarr.push(item.id);
                                    chAnswer.push(item.ans);
                                    card.append('<dd id="n' + counts + '">' + counts + '</dd>');
                                    counts++;
                                });
                                $.each(res.duo, function (i, item) {
                                    muIDarr.push(item.id);
                                    muAnswer.push(item.ans);
                                    card.append('<dd id="n' + counts + '">' + counts + '</dd>');
                                    counts++;
                                });
                                $.each(res.pan, function (i, item) {
                                    juIDarr.push(item.id);
                                    juAnswer.push(item.ans);
                                    card.append('<dd id="n' + counts + '">' + counts + '</dd>');
                                    counts++;
                                });
                                exameven.icheck();
                                exameven.wscroll();
                                exameven.card();
                                exameven.noAllKey();
                                even.checkAnswer({
                                    charr: res.chanswer.split(':'),
                                    muarr: res.muanswer.split(':'),
                                    juarr: res.juanswer.split(':')
                                });
                                layer.closeAll();
                            }
                            else {
                                layer.closeAll();
                                layer.msg('查看试卷失败', {
                                    icon: 2, end: function () {
                                        window.location.replace('/exam/list');
                                    }
                                });
                            }
                        },
                        error: function (err) { layer.alert(err.responseText); }
                    });
                }
            };
        $(function () {
            util.fixbar({ top: true });
            requ.paper(parseInt($('#data').val()));
        });
    });
});