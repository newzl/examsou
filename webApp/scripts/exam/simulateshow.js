$.getScript('/content/icheck/icheck.min.js', function () {
    layui.config({
        base: '/scripts/modules/'
    }).use(['layer', 'util', 'element', 'laytpl', 'exameven'], function () {
        var layer = layui.layer,
            util = layui.util,
            element = layui.element,
            laytpl = layui.laytpl,
            exameven = layui.exameven,

            chIDarr, muIDarr, juIDarr,//id数组
            chAnswer, muAnswer, juAnswer,//答案数组
            ispy,//是否批阅试卷
            down = $('.countdown'),
            init = {
                //初始化参数
                param: function () {
                    chIDarr = new Array(), muIDarr = new Array(), juIDarr = new Array(),
                    chAnswer = new Array(), muAnswer = new Array(), juAnswer = new Array(),
                    ispy = false;
                }
            },
            even = {
                marking: function () {
                    var chYes = 0, muYes = 0, juYes = 0,
                        chNo = 0, muNo = 0, juNo = 0;
                    var s_elem = $('#subView');
                    s_elem.find('input').iCheck('disable');
                    if (chIDarr.length > 0) {//要有题库
                        s_elem.find('.dan').each(function (i) {
                            var inputs = $(this).find('.checked input');
                            if (inputs.length > 0) {//答题了
                                if (chAnswer[i] === inputs.val()) chYes++;//答对加一
                                else {
                                    chNo++;
                                    if (!ispy) {
                                        $('#' + $(this).attr('id').replace(/t/gm, 'n')).addClass('cd');//答题卡为错选class
                                        $(this).append('<div class="item" style="color: red;">答案：' + chAnswer[i] + '</div>');
                                    }
                                }
                            }
                        });
                    }
                    if (muIDarr.length > 0) {//要有题库
                        s_elem.find('.duo').each(function (i) {
                            var c_elem = $(this).find('.checked');
                            if (c_elem.length > 0) {//答题了
                                var pickArr = new Array();
                                c_elem.each(function () {
                                    pickArr.push($(this).find('input').val());
                                });
                                if (muAnswer[i] === pickArr.join()) muYes++;//答对加一
                                else {
                                    muNo++;
                                    if (!ispy) {
                                        $('#' + $(this).attr('id').replace(/t/gm, 'n')).addClass('cd');//答题卡为错选class
                                        $(this).append('<div class="item" style="color: red;">答案：' + muAnswer[i] + '</div>');
                                    }
                                }
                            }
                        });
                    }
                    if (juIDarr.length > 0) {//要有题库
                        s_elem.find('.pan').each(function (i) {
                            var inputs = $(this).find('.checked input');
                            if (inputs.length > 0) {//答题了
                                if (juAnswer[i] === parseInt(inputs.val())) juYes++;//答对加一
                                else {
                                    juNo++;
                                    if (!ispy) {
                                        $('#' + $(this).attr('id').replace(/t/gm, 'n')).addClass('cd');//答题卡为错选class
                                        var an = juAnswer[i] === 1 ? '正确' : '错误';
                                        $(this).append('<div class="item" style="color: red;">答案：' + an + '</div>');
                                    }
                                }
                            }
                        });
                    }
                    var dans = chIDarr.length, duos = muIDarr.length, pans = juIDarr.length; //单多判题数
                    var DDS = chYes + muYes + juYes;//答对数
                    var DCS = chNo + muNo + juNo;//答错数
                    var DTS = DDS + DCS;//答题数
                    var JD = DDS * 100.0 / DTS;//进度
                    var hinum = 1;
                    switch (true) {
                        case (JD < 10): hinum = 1; break;
                        case (JD >= 10 && JD < 20): hinum = 10; break;
                        case (JD >= 20 && JD < 30): hinum = 20; break;
                        case (JD >= 30 && JD < 40): hinum = 30; break;
                        case (JD >= 40 && JD < 50): hinum = 40; break;
                        case (JD >= 50 && JD < 60): hinum = 50; break;
                        case (JD >= 60 && JD < 70): hinum = 60; break;
                        case (JD >= 70 && JD < 80): hinum = 70; break;
                        case (JD >= 80 && JD < 90): hinum = 80; break;
                        case (JD >= 90): hinum = 90; break;
                        default: hinum = 1; break;
                    }
                    var htm = '<div class="ksdf">';
                    htm += '<div class="layui-progress layui-progress-big" lay-showpercent="t"><div class="layui-progress-bar" lay-percent="' + JD.toFixed(2) + '%"></div></div>';
                    htm += '<table class="layui-table"><thead><tr><th rowspan="2" style="width: 20%; background: #fff;text-align:center"><img src="/images/hi/' + hinum + '.gif" alt="进度图标"/></th><th colspan="4" style="text-align:center">本次考试用时：<span>' + down.text() + '</span></th></tr>';
                    htm += '<tr><th style="width: 20%">单选题/1分</th><th style="width: 20%">多选题/1分</th><th style="width: 20%">判断题/1分</th><th style="width: 20%">合计</th></tr></thead>';
                    htm += '<tbody>';
                    htm += '<tr><td>全部题数</td><td>' + dans + '</td><td>' + duos + '</td><td>' + pans + '</td><td>' + (dans + duos + pans) + '</td></tr>';
                    htm += '<tr><td>答对题数</td><td>' + chYes + '</td><td>' + muYes + '</td><td>' + juYes + '</td><td>' + DDS + '</td></tr>';
                    htm += '<tr><td>答错题数</td><td>' + chNo + '</td><td>' + muNo + '</td><td>' + juNo + '</td><td>' + DCS + '</td></tr>';
                    htm += '<tr><td>未答题数</td><td>' + (dans - (chYes + chNo)) + '</td><td>' + (duos - (muYes + muNo)) + '</td><td>' + (pans - (juYes + juNo)) + '</td><td>' + ((dans - (chYes + chNo)) + (duos - (muYes + muNo)) + (pans - (juYes + juNo))) + '</td></tr>';
                    htm += '<tr><td>获得分数</td><td>' + chYes + '分</td><td>' + muYes + '分</td><td>' + juYes + '分</td><td>' + (chYes + muYes + juYes) + '分</td></tr>';
                    htm += '</tbody></table></div>';
                    layer.closeAll();
                    layer.open({
                        type: 1,
                        title: ' 考试得分', resize: false,
                        area: '520px',
                        content: htm,
                        btn: '关闭',
                        success: function () { element.render('progress'); ispy = true; },
                        yes: function () {
                            layer.closeAll();
                        }
                    });
                },
                //选择答案  answer：用户选择
                checkAnswer: function (arr) {
                    var s_elem = $('#subView');
                    if (chIDarr.length > 0) {//要有题库
                        s_elem.find('.dan').each(function (i) {
                            var that = $(this),
                                usc = arr.charr[i],
                                card = $('#' + that.attr('id').replace(/t/gm, 'n'));
                            if (usc.length > 0) {//答题了
                                that.find('input[value="' + usc + '"]').iCheck('check');
                                card.addClass('yd');
                            }
                        });
                    }
                    if (muIDarr.length > 0) {//要有题库
                        s_elem.find('.duo').each(function (i) {
                            var that = $(this),
                                usc = arr.muarr[i],
                                card = $('#' + that.attr('id').replace(/t/gm, 'n'));
                            if (usc.length > 0) {//答题了
                                var uscArr = usc.split(',');
                                $.each(uscArr, function (j, item) {
                                    that.find('input[value="' + item + '"]').iCheck('check');
                                })
                                card.addClass('yd');
                            }
                        });
                    }
                    if (juIDarr.length > 0) {//要有题库
                        s_elem.find('.pan').each(function (i) {
                            var that = $(this),
                                usc = arr.juarr[i],
                                card = $('#' + that.attr('id').replace(/t/gm, 'n'));
                            if (usc.length > 0) {//答题了
                                that.find('input[value="' + usc + '"]').iCheck('check');
                                card.addClass('yd');
                            }
                        });
                    }
                    this.marking();
                },
                clickBtn: function () {
                    var that = this;
                    $('#marking').off().on('click', function () {
                        that.marking();
                    });
                }
            },
            requ = {
                paper: function (esid) {
                    $.ajax({
                        url: '/api/showsimulate',
                        type: 'GET', dataType: 'JSON', cache: false,
                        data: { esid: esid },
                        beforeSend: function () { init.param(); layer.msg('正在组卷中...', { icon: 16, shade: 0.7 }); },
                        success: function (res) {
                            if (res !== null) {
                                down.text(res.usetime);
                                laytpl(subTpl.innerHTML).render(res, function (htm) {
                                    $('#subView').html(htm);
                                });
                                var counts = 1, card = $('#subCard');
                                card.empty();
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
                                even.clickBtn();
                                $('body,html').animate({ "scrollTop": 0 }, 600);//滑到顶部
                            }
                            else {
                                layer.closeAll();
                                layer.msg('查看试卷失败', {
                                    icon: 2, end: function () {
                                        window.location.replace('/exam/simulate');
                                    }
                                });
                            }
                        },
                        error: function (err) { alert('ajaxError' + err.responseText); }
                    });
                }
            };
        $(function () {
            util.fixbar({ top: true });
            requ.paper(parseInt($('#data').val()));
        });
    });
});
