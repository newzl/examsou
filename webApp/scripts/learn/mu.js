"use strict";
var sdata = {},
    ldata = {},//基本信息
    si = 0,//sdata索引
    auto = true;//自动下一题
layui.config({
    base: '/scripts/learn/'
}).use(['form', 'laytpl', 'globalsx'], function () {
    var form = layui.form,
        laytpl = layui.laytpl,
        globalsx = layui.globalsx;
    form.on('checkbox(auto)', function (d) {
        auto = d.elem.checked;
    });
    $(function () {
        ldata = $.parseJSON($('#data').val());
        firstInit(ldata.rows || null);
        globalsx.initStype($('#stype'), ldata);//初始化题型
        globalsx.initSet();//初始化设置
        globalsx.noAllKey();
        init.anscard(ldata.mus);//初始化答题卡
    });
    //首次加载
    function firstInit(rows) {
        if (rows !== null) {
            layer.open({
                type: 1, title: false, closeBtn: false, area: '300px;', shade: 0.8,
                btn: ['继续练习', '重新开始'],
                content: '<div style="padding: 40px; line-height: 22px; background-color: #393D49; color: #fff;">' + ldata.time + ' 练习到第' + rows + '题，是否继续？</div>',
                yes: function () {
                    init.sub(rows);
                },
                btn2: function () {
                    init.sub(1);
                }
            });
        }
        else {
            init.sub(1);
        }
    }
    var event = {
        exeNext: function () {//下一题
            if (si + 1 >= sdata.length) {
                var rows = parseInt(sdata[si].row);
                init.getsub(1, rows + 1, function (res) {
                    sdata = res, si = 0;
                    init.tpldata(sdata[si]);
                });
            }
            else {
                si += 1;
                init.tpldata(sdata[si]);
            }
        },
        exePrev: function () {//上一题
            if (si <= 0) {
                var rows = parseInt(sdata[0].row);
                init.getsub(2, rows - 1, function (res) {
                    sdata = res, si = res.length - 1;
                    init.tpldata(sdata[si]);
                });
            }
            else {
                si -= 1;
                init.tpldata(sdata[si]);
            }
        },
        bindToolBtn: function () {
            //确定选择
            $('.submit').off().on('click', function () {
                var self = $(this);
                var soption = [];//多选择题
                $('#subContent>.options .checked input').each(function () {
                    soption.push($(this).val());
                });
                if (soption.length > 0) {
                    var sans = soption.toString(),//我的选择
                        ress = sdata[si].res,//正确答案
                        crdom = $('#c' + sdata[si].row);//答题卡DOM
                    laytpl(resultTlp.innerHTML).render({
                        ans: sans,
                        res: ress,
                        isdt: true
                    }, function (htm) {
                        $('#resultView').html(htm);
                    });
                    //保存学习
                    globalsx.saveLearn({
                        miid: ldata.miid,
                        scid: ldata.scid,
                        stype: ldata.stype,
                        row: sdata[si].row,
                        kid: sdata[si].id,
                        answer: sans,
                        result: sans === ress
                    });
                    sdata[si]['ans'] = sans;
                    if (sans === ress) {
                        crdom.addClass('yes');
                        if (auto && sdata[si].row < ldata.mus) {
                            setTimeout(function () {
                                event.exeNext()
                            }, 600);
                        }
                    }
                    else {
                        crdom.addClass('no');
                        $('.ln-explain').show();
                    }
                    $('#subContent>.options input[type="checkbox"]').iCheck('disable');
                    self.hide();
                }
                else {
                    layer.msg('没有选择任何选项', { icon: 2, anim: 6 });
                }
            });
            //上一题
            $('.prev').off().on('click', function () {
                event.exePrev();
            });
            //下一题
            $('.next').off().on('click', function () {
                event.exeNext();
            });
            //收藏
            $('.star-btn').off().on('click', function () {
                var obj = {
                    coid: $(this).data('coid') || 0,
                    miid: ldata.miid,
                    stype: ldata.stype,
                    kid: sdata[si].id
                };
                globalsx.saveCollect(obj, function (res) {
                    if (obj.coid > 0) sdata[si]['col'] = null;
                    else sdata[si]['col'] = res;
                    init.tpldata(sdata[si]);
                });
            });
            //笔记
            $('.notes-btn').off().on('click', function () {
                $('.ln-notes').toggle('fast');
            });
            //详解
            $('.explain-btn').off().on('click', function () {
                $('.ln-explain').toggle('fast');
            });
            //答题卡
            $('.answer-btn').off().on('click', function () {
                layer.open({
                    id: 'answerLayer',
                    type: 1, title: false,
                    closeBtn: 0, time: 40000,
                    area: '1100px',
                    shadeClose: true,
                    content: $('#cardView')
                });
            });
        },
        bindInfoBtn: function () {
            globalsx.bindErrorBack({
                elem: $('.error-btn'),
                stype: ldata.stype,
                kid: sdata[si].id,
            });
            //绑定编辑笔记
            globalsx.bindEditNotes({
                elem: $('.edit-btn'),
                not: sdata[si].not,
                miid: ldata.miid,
                stype: ldata.stype,
                kid: sdata[si].id
            }, function (res) {
                sdata[si]['not'] = res;
                init.tpldata(sdata[si]);
            });
        },
    };
    var init = {
        sub: function (rows) {
            var self = this;
            self.getsub(1, rows, function (res) {
                sdata = res, si = 0;
                self.tpldata(sdata[si]);
                layer.closeAll();
            });
        },
        //获得题库
        getsub: function (fxs, rows, callback) {
            globalsx.getlearnsx({
                miid: ldata.miid,
                scid: ldata.scid,
                stype: ldata.stype,
                fx: fxs,
                row: rows
            }, function (res) {
                callback(res);
            });
        },
        //初始化试题
        tpldata: function (d) {
            var crdom = $('#c' + sdata[si].row),//答题卡DOM
                isdt = (crdom.hasClass('yes') || crdom.hasClass('no'));//是否答题
            $('#snav').text(d.snav);
            //试题
            laytpl(subTpl.innerHTML).render({
                row: d.row,
                id: d.id,
                sub: d.sub,
                A: d.A,
                B: d.B,
                C: d.C,
                D: d.D,
                E: d.E,
                F: d.F
            }, function (htm) {
                $('#subContent').html(htm);
                initiCheck();
            });
            //工具条
            laytpl(toolTpl.innerHTML).render({
                row: d.row,
                count: ldata.mus,
                au: auto,
                col: d.col,
                not: d.not,
                isdt: isdt
            }, function (htm) {
                $('#toolView').html(htm);
                form.render('checkbox');
                event.bindToolBtn();
            });
            //详解和笔记
            laytpl(infoTpl.innerHTML).render({
                tot: d.tot,
                yes: d.yes,
                exp: d.exp,
                not: d.not
            }, function (htm) {
                $('#infoView').html(htm);
                event.bindInfoBtn();
            });
            //答题结果
            laytpl(resultTlp.innerHTML).render({
                ans: d.ans,
                res: d.res,
                isdt: isdt
            }, function (htm) {
                $('#resultView').html(htm);
                if (d.ans !== null && isdt) {
                    var dom = $('#subContent>.options');
                    var arr = d.ans.split(',');
                    for (var i = 0; i < arr.length; i++) {
                        dom.find('input[value="' + arr[i] + '"]').iCheck('check');
                    }
                    dom.find('input[type="checkbox"]').iCheck('disable');
                    $('.ln-explain').show();
                }
            });
        },
        //初始化答题卡
        anscard: function (count) {
            var view = $('#cardView');
            laytpl(cardTlp.innerHTML).render({ count: count }, function (htm) {
                view.html(htm);
            });
            view.find('dd').off().on('click', function () {
                init.sub(parseInt($(this).text()));
            });
        }
    };
    function initiCheck() {
        var dom = $('#subContent>.options input[type="checkbox"]');
        dom.iCheck({
            checkboxClass: 'icheckbox_square-blue',
            increaseArea: '20%'
        });
    }
});