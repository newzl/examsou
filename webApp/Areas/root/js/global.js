"use strict";
var element;
layui.use('element', function () {
    element = layui.element;
    $.getScript('/scripts/nav/rmenu.js', function () {
        var htm = '<ul class="layui-nav layui-nav-tree">';
        $.each(menu, function (i, item) {
            htm += '<li class="layui-nav-item">';
            if (item.href != undefined || item.href != null) htm += '<a href="' + item.href + '">';
            else htm += '<a href="javascript:;">';
            if (item.icon != undefined && item.icon.length > 0) htm += '<i class="layui-icon">' + item.icon + ';</i>';
            else htm += '<i class="layui-icon">&#xe7a0;</i>';
            htm += '<cite>' + item.title + '</cite></a>';
            if (item.children != undefined && item.children.length > 0) {
                htm += '<dl class="layui-nav-child">';
                $.each(item.children, function (j, obj) {
                    if (obj.href != undefined || obj.href != null) htm += '<dd><a href="' + obj.href + '">';
                    else htm += '<dd><a href="javascript:;">';
                    if (obj.icon != undefined && obj.icon.length > 0) htm += '<i class="layui-icon">' + obj.icon + ';</i>';
                    else htm += '<i class="layui-icon">&#xe61d;</i>';
                    htm += '<cite>' + obj.title + '</cite></a></dd>';
                })
                htm += '</dl>';
            }
            htm += '</li>';
        })
        htm += '</ul>';
        $('#my_side').append(htm);
        element.render();
    });
    $('.topbar').off().on('click', function () {
        var dom = $('#my_side');
        $('<div>', {
            'class': 'my-shade',
            'click': function () {
                dom.animate({ width: '0' }, 100);
                $(this).remove();
            }
        }).appendTo('body');
        dom.animate({ width: '200px' }, 100);
    });
});
function selected(id, val) {
    $('#' + id + ' option[value="' + val + '"]').attr('selected', true);
}