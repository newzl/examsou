'use strict';
layui.config({
    base: '/scripts/learn/'
}).use(['element', 'laypage', 'laytpl', 'globalsx'], function () {
    var element = layui.element,
        laypage = layui.laypage,
        laytpl = layui.laytpl,
        globalsx = layui.globalsx;
    $(function () {
        getBase(function (bres) {
            globalsx.initStype($('#stype'), bres);//初始化题型
        });
        globalsx.initSet();//初始化设置
        globalsx.noAllKey();
        loadTable();
    });
    function loadTable(page) {
        getBase(function (bres) {
            globalsx.getlearnks({
                scid: bres.scid,
                stype: bres.stype,
                page: page || 1
            }, function (res) {
                laytpl(collaTpl.innerHTML).render(res.data, function (htm) {
                    $('#collaView').html(htm);
                    element.render('collapse');
                });
                if (res.count > 10) initpage(res.count, page);
            })
        });
    }
    function initpage(count, curr) {
        laypage.render({
            elem: 'pageView', count: count, curr: curr,
            layout: ['count', 'prev', 'page', 'next', 'skip'],
            jump: function (obj, first) {
                if (!first) loadTable(obj.curr);
            }
        });
    }
    function getBase(cb) {
        cb($.parseJSON($('#data').val()));
    }
});