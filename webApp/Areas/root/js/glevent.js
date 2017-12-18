//全局事件
"use strict";
layui.define('form', function (exports) {
    var form = layui.form;
    var extend = {
        //绑定查询打开题库类型
        bindFind: function () {
            $('#findsname').off().on('click', function () {
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
            $('#findreset').off().on('click', function () {
                $('#findsname').val('');
                $('#findsid').val(0);
            });
        },
        //绑定快捷键
        bindKey: function (isbind) {
            if (isbind) {
                $(document).unbind("keydown").bind("keydown", function (e) {
                    e = window.event || e;
                    if (e.altKey && e.keyCode === 83) {
                        $('#saveForm').click();
                    }
                });
            }
            else {
                $(document).unbind("keydown");
            }
        },
        //绑定公用事件
        bindCommon: function () {
            extend.bindSel();
            $("#title").bind('input propertychange', function () {
                $('#subs').val($(this).val());
            });
        },
        bindSel: function () {
            $('.selClass').off().on('click', function () {
                var self = $(this);
                layer.open({
                    type: 2, title: '选择题型', shadeClose: true,
                    shade: 0.3, area: ['400px', '600px'],
                    content: '/root/common/seltree',
                    btn: ['确定', '取消'],
                    yes: function (i, lo) {
                        var iframeWin = window[lo.find('iframe')[0]['name']];
                        iframeWin.getNode(function (res) {
                            if (res !== null) {
                                if (res.leved === 4) {
                                    self.prev('.subnav').text(res.nav);
                                    self.next('.sid').val(res.id);
                                    layer.closeAll();
                                }
                                else {
                                    layer.msg('请选择4级题库类型');
                                }
                            }
                            else {
                                layer.msg('没有选择任何题型');
                            }
                        });
                    }
                });
            });
        },
        //绑定点击打开编辑器
        bindEditor: function () {
            var time = null;
            $('.editor').click(function () {
                clearTimeout(time);
                var nex = $(this).parent('label').next('div').children('textarea');
                time = setTimeout(function () {
                    extend.openEdit({
                        url: '/root/common/edit_xheditor',
                        elem: nex
                    });
                }, 200);
            }).dblclick(function () {
                clearTimeout(time);
                var nex = $(this).parent('label').next('div').children('textarea');
                extend.openEdit({
                    url: '/root/common/edit_kindeditor',
                    elem: nex
                });
            });
        },
        //弹出编辑器{url:,elem}
        openEdit: function (options) {
            layer.open({
                type: 2, title: '编辑器',
                shade: 0.3, area: ['750px', '95%'],
                content: options.url,
                btn: ['确定', '取消'],
                success: function (lo, i) {
                    var iframeWin = window[lo.find('iframe')[0]['name']];
                    if (options.elem.val().length > 0) {
                        iframeWin.set(options.elem.val());
                    }
                },
                yes: function (i, lo) {
                    var iframeWin = window[lo.find('iframe')[0]['name']];
                    iframeWin.get(function (res) {
                        if (res !== null) {
                            options.elem.val(res);
                            layer.closeAll();
                        }
                        else {
                            options.elem.val('');
                            layer.msg('没有编辑任何内容', {
                                time: 600,
                                end: function () {
                                    layer.closeAll();
                                }
                            });
                        }
                    });
                }
            });
        }
    };
    exports('glevent', extend);
});