"use strict";
var sdata = {},
    ldata = {},//基本信息
    si = 0,//sdata索引
    auto = true;//自动下一题
layui.config({ base: '/scripts/learn/' }).use(['form', 'layer', 'laytpl', 'globalsx'], function () {
    var form = layui.form, layer = layui.layer, laytpl = layui.laytpl, globalsx = layui.globalsx;
    form.on('checkbox(auto)', function (d) {
        auto = d.elem.checked;
    });
    $(function () {
        ldata = $.parseJSON($('#data').val());
        firstInit(ldata.rows || null);
        globalsx.initStype($('#stype'), ldata);//初始化题型
        globalsx.initSet();//初始化设置
        globalsx.noAllKey();
        init.anscard(ldata.chs);//初始化答题卡
    });
    //首次加载
    function firstInit(rows) {
        if (rows !== null) {
            layer.open({
                type: 1, title: false, closeBtn: false, area: '300px;', shade: 0.8,
                btn: ['继续练习', '重新开始'],
                content: '<div style="padding: 40px; line-height: 22px; background-color: #393D49; color: #fff;">上次练习到第' + rows + '题，是否继续？</div>',
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
                    cid: $(this).data('cid') || 0,
                    lid: ldata.lid,
                    sid: ldata.sid,
                    stype: ldata.stype,
                    kid: sdata[si].id
                };
                globalsx.saveCollect(obj, function (res) {
                    if (obj.cid > 0) sdata[si]['col'] = null;
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
            globalsx.bindErrorBack($('.error-btn'), {
                stype: ldata.stype,
                kid: sdata[si].id,
            });
            //绑定编辑笔记
            globalsx.bindEditNotes($('.edit-btn'), {
                not: sdata[si].not,
                lid: ldata.lid,
                sid: ldata.sid,
                stype: ldata.stype,
                kid: sdata[si].id
            }, function (res) {
                sdata[si]['not'] = res;
                init.tpldata(sdata[si]);
            });
        },
        //答题卡
        answerCard: function () {
            $('#cardView dl dd').off().on('click', function () {
                init.sub(parseInt($(this).text()));
            });
        }
    };
    var init = {
        sub: function (rows) {
            init.getsub(1, rows, function (res) {
                sdata = res, si = 0;
                init.tpldata(sdata[si]);
                layer.closeAll();
            });
        },
        //获得题库
        getsub: function (fxs, rows, cb) {
            globalsx.getlearnsx({
                lid: ldata.lid,
                sid: ldata.sid,
                stype: ldata.stype,
                fx: fxs,
                row: rows
            }, function (res) {
                cb(res);
            });
        },
        //初始化试题
        tpldata: function (d) {
            //试题
            laytpl(subTpl.innerHTML).render({
                row: d.row,
                id: d.id,
                sub: d.sub,
                A: d.A,
                B: d.B,
                C: d.C,
                D: d.D,
                E: d.E
            }, function (htm) {
                document.getElementById('subContent').innerHTML = htm;
                initiCheck();
            });
            //工具条
            laytpl(toolTpl.innerHTML).render({
                row: d.row,
                count: ldata.chs,
                au: auto,
                col: d.col,
                not: d.not
            }, function (htm) {
                document.getElementById('toolView').innerHTML = htm;
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
                document.getElementById('infoView').innerHTML = htm;
                event.bindInfoBtn();
            });
            //答题结果
            var crdom = $('#c' + sdata[si].row),//答题卡DOM
                isdt = (crdom.hasClass('yes') || crdom.hasClass('no'));//是否答题
            laytpl(resultTlp.innerHTML).render({
                ans: d.ans,
                res: d.res,
                isdt: isdt
            }, function (htm) {
                document.getElementById('resultView').innerHTML = htm;
                if (d.ans !== null && isdt) {
                    $('#subContent>.options input[value="' + d.ans + '"]').iCheck('check');
                }
            });
        },
        //初始化答题卡
        anscard: function (count) {
            laytpl(cardTlp.innerHTML).render({ count: count }, function (htm) {
                document.getElementById('cardView').innerHTML = htm;
                event.answerCard();
            });
        }
    };
    function initiCheck() {
        var dom = $('#subContent>.options input[type="radio"]');
        dom.iCheck({
            radioClass: 'iradio_square-blue',
            increaseArea: '20%'
        });
        dom.off().on('ifChecked', function (e) {
            dom.iCheck('disable');
            var sans = e.target.defaultValue,//我的选择
                ress = sdata[si].res;//正确答案
            laytpl(resultTlp.innerHTML).render({
                ans: sans,
                res: ress,
                isdt: true
            }, function (htm) {
                document.getElementById('resultView').innerHTML = htm;
                var crdom = $('#c' + sdata[si].row);//答题卡DOM
                if (crdom.hasClass('yes') || crdom.hasClass('no')) {//已经答过题
                    $('.ln-explain').show();
                }
                else {
                    //保存学习
                    globalsx.saveLearn({
                        lid: ldata.lid,
                        sid: ldata.sid,
                        stype: ldata.stype,
                        row: sdata[si].row,
                        kid: sdata[si].id,
                        answer: sans,
                        result: sans === ress
                    });
                    sdata[si]['ans'] = sans;
                    if (sans === ress) {
                        crdom.addClass('yes');
                        if (auto) {
                            setTimeout(function () {
                                event.exeNext()
                            }, 600);
                        }
                    }
                    else {
                        crdom.addClass('no');
                        $('.ln-explain').show();
                    }
                }
            });
        });
    }
});