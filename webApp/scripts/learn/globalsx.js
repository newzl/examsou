//练习全局函数
layui.define(['form', 'layer'], function (exports) {
    var form = layui.form,
        layer = layui.layer,
        req = {
            //保存错题反馈
            _saveErrorBack: function (data) {
                $.ajax({
                    url: '/api/errorback',
                    type: 'post', dataType: 'json', cache: false,
                    data: data,
                    beforeSend: function () { layer.load(2); },
                    success: function (res) {
                        layer.closeAll();
                        layer.msg('错题反馈成功', { icon: 1 });
                    },
                    complete: function () { layer.closeAll('loading'); },
                    error: function (msg) { alert('ajaxError:' + msg.responseText); }
                });
            },
            //保存笔记
            _saveNotes: function (data) {
                $.ajax({
                    url: '/api/notes',
                    type: 'post', dataType: 'json', cache: false,
                    data: data,
                    beforeSend: function () { layer.load(2); },
                    success: function (res) {
                        layer.closeAll();
                        layer.msg('保存笔记成功', { icon: 1 });
                    },
                    complete: function () { layer.closeAll('loading'); },
                    error: function (msg) { alert('ajaxError:' + msg.responseText); }
                });
            },
            //ajax获得数据
            _getlearn: function (url, data, callback) {
                $.ajax({
                    url: url,
                    type: 'get', dataType: 'json', cache: false,
                    data: data,
                    beforeSend: function () { layer.load(2); },
                    success: function (res) {
                        if (res.state !== undefined) {
                            if (res.state === 4001) window.location.replace("/account/login?rid=" + Math.random());
                        }
                        else {
                            if (res !== null) callback(res);
                            else layer.msg('试题获取失败', { icon: 2, time: 1500, end: function () { window.location.replace("/myitem?rid=" + Math.random()); } });
                        }
                    },
                    complete: function () { layer.closeAll('loading'); },
                    error: function (msg) { alert('ajaxError:' + msg.responseText); }
                });
            }
        };
    exports('globalsx', {
        //获得试题
        getlearnsx: function (data, callback) {
            req._getlearn('/handler/ln_getlearnsx.ashx', data, function (res) {
                callback(res);
            });
        },
        //获得快速背题
        getlearnks: function (data, callback) {
            req._getlearn('/handler/ln_getlearnks.ashx', data, function (res) {
                callback(res);
            });
        },
        //初始化设置
        initSet: function () {
            var learnCookie = 'LEARNSETTINGCOOKIE', cook = $.cookie(learnCookie), obj = {};
            if (cook !== undefined && cook !== null) obj = $.parseJSON(cook);
            else obj = { skin: 0, font: 0 }
            _loadSet();
            //打开设置
            $('#setting').off().on('click', function () {
                var pst = "layui-btn-primary",
                    s0 = obj.skin == 0 ? '' : pst,
                    s1 = obj.skin == 1 ? '' : pst,
                    f0 = obj.font == 0 ? '' : pst,
                    f1 = obj.font == 1 ? '' : pst,
                    f2 = obj.font == 2 ? '' : pst;
                var htm = '<table><tr><td>皮肤：</td><td class="settskin">';
                htm += '<button value="0" class="layui-btn layui-btn-xs ' + s0 + '">正常</button>';
                htm += '<button value="1" class="layui-btn layui-btn-xs ' + s1 + '">夜间</button>';
                htm += '</td></tr><tr><td>字体：</td><td style="padding-top:5px;" class="settfont">';
                htm += '<button value="0" class="layui-btn layui-btn-xs ' + f0 + '">小</button>';
                htm += '<button value="1" class="layui-btn layui-btn-xs ' + f1 + '">中</button>';
                htm += '<button value="2" class="layui-btn layui-btn-xs ' + f2 + '">大</button>';
                htm += '</td></tr></table>';
                layer.tips(htm, '#setting', {
                    time: 3000,
                    tips: [4, '#999'],
                    success: function () {
                        $('.settskin .layui-btn-primary').off().on('click', function () {
                            obj['skin'] = parseInt($(this).val()); _saveCookie();
                        });
                        $('.settfont .layui-btn-primary').off().on('click', function () {
                            obj['font'] = parseInt($(this).val()); _saveCookie();
                        });
                    }
                });
            });
            function _saveCookie() {
                $.cookie(learnCookie, JSON.stringify(obj), { expires: 360, path: '/' });
                _loadSet(); layer.closeAll();
            }
            function _loadSet() {
                var dom = $('#subContent');
                if (obj.skin === 1) $('body').addClass('black');//皮肤设置
                else $('body').removeClass('black');
                switch (obj.font) {
                    case 0: dom.removeClass('font18 font20').addClass('font16'); break;
                    case 1: dom.removeClass('font16 font20').addClass('font18'); break;
                    case 2: dom.removeClass('font16 font18').addClass('font20'); break;
                }
            }
            //打开帮助
            $('#help').off().on('click', function () {
                layer.open({ type: 1, title: false, closeBtn: 0, area: '1000px', offset: '100px', time: 5000, shadeClose: true, content: '<img src="/images/point_pic.png">' });
            });
        },
        //初始化题型选择
        initStype: function (dom, obj) {
            var htm = '';
            if (obj.chs > 0) htm += '<option value="ch">单选题</option>';
            if (obj.mus > 0) htm += '<option value="mu">多选题</option>';
            if (obj.jus > 0) htm += '<option value="ju">判断题</option>';
            if (obj.fis > 0) htm += '<option value="fi">填空题</option>';
            if (obj.qas > 0) htm += '<option value="qa">问答题</option>';
            if (obj.mcs > 0) htm += '<option value="mc">名词解析</option>';
            dom.empty().append(htm);
            if (dom.children('option').length > 1) {
                dom.children('option[value="' + obj.stype + '"]').attr('selected', true).attr('disabled', true);
                form.render('select');
                //题型选择事件
                form.on('select(stype)', function (sd) {
                    window.location.href = '/practise/' + obj.miid + '/' + obj.scid + '/' + sd.value + '?rid=' + Math.random().toString(36).substr(2);
                });
            }
            else {
                dom.parent().hide()
            }
        },
        //绑定错题反馈 options:{elem/点击按钮,stype,kid}
        bindErrorBack: function (options) {
            options.elem.off().on('click', function () {
                layer.open({
                    type: 2, title: '错题反馈', resize: false,
                    shade: 0.3, area: ['700px', '300px'],
                    content: '/content/static/errorback.html',
                    btn: ['确定', '取消'],
                    yes: function (i, lo) {
                        var iframeWin = window[lo.find('iframe')[0]['name']];
                        iframeWin.getContent(function (res) {
                            if (res.content.length > 500) layer.msg('反馈内容限制500个字符以内', { icon: 2 });
                            else if (res.content.length <= 0) layer.msg('反馈内容不能为空', { icon: 2 });
                            else {
                                layer.confirm('确定提交错题反馈？', { icon: 3 }, function () {
                                    req._saveErrorBack({
                                        stype: options.stype,
                                        kid: options.kid,
                                        errType: res.errType,
                                        content: res.content
                                    });
                                });
                            }
                        });
                    }
                });
            });
        },
        //绑定编辑笔记 options:{elem/点击按钮,not/原有笔记内容,miid,stype,kid}
        bindEditNotes: function (options, callback) {
            options.elem.off().on('click', function () {
                layer.open({
                    type: 1, title: '编辑笔记', resize: false,
                    shade: 0.3, area: ['700px', '300px'],
                    content: '<textarea id="editNotes" class="layui-textarea" style="height:200px;" placeholder="请输入笔记内容（限500字）" autofocus></textarea>',
                    btn: ['确定', '取消'],
                    success: function () {
                        if (options.not !== null && options.not !== '') $('#editNotes').val(options.not);
                    },
                    yes: function (i, lo) {
                        var cval = $('#editNotes').val();
                        if (cval.length > 500) {
                            layer.msg('笔记内容限制500个字符以内', { icon: 2 });
                            return;
                        }
                        else if (cval.length <= 0) {
                            layer.msg('笔记内容不能为空', { icon: 2 });
                            return;
                        }
                        else {
                            req._saveNotes({
                                miid: options.miid,
                                stype: options.stype,
                                kid: options.kid,
                                content: cval
                            });
                            callback(cval);
                        }
                    }
                });
            });
        },
        //保存或取消收藏 coid：0保存 cid！=0取消
        saveCollect: function (data, callback) {
            $.ajax({
                url: '/api/collect',
                type: 'post', dataType: 'json', cache: false,
                data: data,
                beforeSend: function () { layer.load(2); },
                success: function (res) {
                    if (data.coid > 0) {
                        layer.msg('取消收藏成功', { icon: 1 });
                        callback(res);
                    }
                    else {
                        layer.msg('收藏成功', { icon: 1 });
                        callback(res);
                    }
                },
                complete: function () { layer.closeAll('loading'); },
                error: function (msg) { alert('ajaxError:' + msg.responseText); }
            });
        },
        //保存学习
        saveLearn: function (obj) {
            $.ajax({
                url: '/api/savelearn',
                type: 'post', dataType: 'json', cache: false,
                data: obj,
                beforeSend: function () { layer.load(2); },
                complete: function () { layer.closeAll('loading'); },
                error: function (msg) { alert('ajaxError:' + msg.responseText); }
            });
        },
        //禁止所有按键
        noAllKey: function () {
            try {
                //禁止缩放
                var scrollFunc = function (e) {
                    e = e || window.event;
                    if (e.wheelDelta && event.ctrlKey) {//IE/Opera/Chrome
                        event.returnValue = false;
                    } else if (e.detail) {
                        event.returnValue = false;
                    }
                }
                if (document.addEventListener) {
                    document.addEventListener('DOMMouseScroll', scrollFunc, false);
                }
                window.onmousewheel = document.onmousewheel = scrollFunc;
                //禁止按F5F12等
                $(document).unbind("keydown").bind("keydown", function (e) {
                    e = window.event || e;
                    if (e.keyCode == 116 || e.keyCode == 123) {
                        e.keyCode = 0;
                        return false;
                    }
                });
                //禁止右击复制
                $(document).unbind("contextmenu").bind("contextmenu", function (e) {
                    return false;
                });
                with (document.body) {
                    oncontextmenu = function () { return false }
                    ondragstart = function () { return false }
                    onselectstart = function () { return false }
                    onbeforecopy = function () { return false }
                    onselect = function () { document.selection.empty() }
                    oncopy = function () { document.selection.empty() }
                }
            } catch (e) {
                alert(e.message);
            }
        }
    });
});