layui.config({
    base: '/scripts/modules/'
}).use(['layer', 'timer'], function () {
    "use strict";
    var layer = layui.layer,
        timer = layui.timer,
        service = $.connection.learnService,
        inter = null,
        isCook = true,//关闭或刷新是否保存cookie
        timeCook = 'COURSETIME',
        tielem = $('#timer'),
        mdom = $('#maincontent'),

        init = {
            winHeig: function () {
                $('.cou-heig').css('height', $(window).height() - 104);
                mdom.css('height', $(window).height() - 144);
            },
            //初始化设置
            initSet: function () {
                var c_str = 'COURSESETTINGCOOKIE', cook = $.cookie(c_str), obj = {};
                if (cook !== undefined && cook !== null) obj = $.parseJSON(cook);
                else obj = { skin: 0, font: 0 }
                _loadSet();
                //打开设置
                $('.setting').off().on('click', function () {
                    even.hideChapter();
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
                    layer.tips(htm, '.setting', {
                        time: 3000,
                        tips: [1, '#999'],
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
                    $.cookie(c_str, JSON.stringify(obj), { expires: 360, path: '/' });
                    _loadSet(); layer.closeAll();
                }
                function _loadSet() {
                    var dom = $('#maincontent');
                    if (obj.skin === 1) $('body').addClass('black');//皮肤设置
                    else $('body').removeClass('black');
                    switch (obj.font) {
                        case 0: dom.removeClass('font18 font20').addClass('font16'); break;
                        case 1: dom.removeClass('font16 font20').addClass('font18'); break;
                        case 2: dom.removeClass('font16 font18').addClass('font20'); break;
                    }
                }
            },
            //初始化提示
            tips: function () {
                layer.tips('请点击“退出学习”按钮暂停或结束学习，否则不计学时哟！', '#quit', { time: 10000, tips: [4, '#2F4056'] });
                $('.layui-layer-tips').click(function () {
                    layer.closeAll('tips');
                });
            },
            //初始化课件
            keJian: function () {
                getData(function (d) {
                    requ.getlist(d.miid, d.kjid, function (k) {
                        var cook = $.cookie(timeCook), time = null;
                        if (cook !== undefined && cook !== null) time = timer.minuteAgo(cook) > timer.minuteAgo(k.timer) ? cook : k.timer;
                        else time = k.timer;

                        inter = timer.counts(tielem, time);//计时
                        $('#title').text(k.title);
                        $('head title').text('正在学习-' + k.title);
                        mdom.load('/upload/171212/105123.html', function () {//k.cont
                            mdom.scrollTop(k.position);
                        });
                    });
                });
            },
            //即时通信
            hub: function () {
                //监听被停止
                service.client.learnStop = function () {
                    service.server.learnRemove();
                    layer.alert('检测到另外开始学习，当前学习页面在3秒后退出', {
                        icon: 2, closeBtn: 0, offset: '100px', time: 3000, end: function () {
                            even.skip();
                        }
                    });
                }
                //启动连接
                $.connection.hub.start().done(function () {
                    service.server.learnStart();//开始学习，通知其他正在学习的页面停止
                });
            }
        },

        even = {
            //展开章节目录
            openkj: function () {
                var sh = $('.cou-shade'),
                    ch = $('.cou-chapter'),
                    tool = $('.chapter-tool');
                tool.off().on('click', function () {
                    layer.closeAll('tips');
                    if (ch.width() <= 0) {
                        sh.show(); ch.children('ul').show()
                        ch.animate({ width: 200 }, 100);
                    }
                    else {
                        sh.hide(); ch.children('ul').hide()
                        ch.animate({ width: 0 }, 100);
                    }
                });
                sh.off().on('click', function () {
                    $(this).hide();
                    ch.animate({ width: 0 }, 100);
                });
            },
            //上一章和下一章
            prev_next: function () {
                var that = this,
                    prev = $('.cou-prev'),
                    next = $('.cou-next');
                prev.off().on('click', function () {
                    var ins = $('.inthis'),
                        pli = ins.parent('li').prev();
                    if (pli.length > 0) {
                        requ.save(function () {
                            that.hideChapter();
                            window.location.hash = '#!kj=' + pli.children('a').data('kjid');//改变hash
                            init.keJian();//重新初始化课件
                        });
                    }
                    else {
                        layer.msg('已经是第一章了');
                    }
                });
                next.off().on('click', function () {
                    var ins = $('.inthis'),
                        nli = ins.parent('li').next();
                    if (nli.length > 0) {
                        requ.save(function () {
                            that.hideChapter();
                            window.location.hash = '#!kj=' + nli.children('a').data('kjid');//改变hash
                            init.keJian();//重新初始化课件
                        });
                    }
                    else {
                        layer.msg('已经是最后一章了');
                    }
                });
            },
            //退出学习
            quit: function () {
                var that = this;
                $('#quit').off().on('click', function () {
                    requ.save(function () {
                        that.skip();
                    });
                });
            },
            //浏览器关闭或刷新事件
            browser: function () {
                window.onbeforeunload = function (b) {
                    if (isCook) $.cookie(timeCook, tielem.text(), { expires: 5, path: '/' });
                }
            },
            //点击目录里-绑定事件
            chapter: function (dom) {
                var self = this;
                dom.off().on('click', 'a', function () {
                    var that = $(this);
                    if (!that.hasClass("inthis")) {
                        requ.save(function () {
                            self.hideChapter();
                            window.location.hash = '#!kj=' + that.data('kjid');//改变hash
                            init.keJian();//重新初始化课件
                        });
                    }
                });
            },
            //隐藏目录
            hideChapter: function () {
                var sh = $('.cou-shade'), ch = $('.cou-chapter');
                sh.hide(); ch.children('ul').hide();
                ch.animate({ width: 0 }, 100);
            },
            //删除cookie
            delCookie: function () {
                $.cookie(timeCook, null, { path: '/', expires: -1 });
            },
            //跳转到我的学习页面
            skip: function () {
                isCook = false;//不用存cookie
                this.delCookie();//删除cookie
                window.clearInterval(inter);//停止倒计时
                service.server.learnRemove();//删除即时通信
                window.location.replace('/myitem');
            }
        },

        requ = {
            //保存记录，保存成功执行callback()回调
            save: function (callback) {
                var tt = tielem.text();
                if (tt.length > 0) {
                    getData(function (d) {
                        $.ajax({
                            url: '/api/record',
                            type: 'POST', dataType: 'json', cache: false,
                            data: {
                                miid: d.miid,
                                kjid: d.kjid,
                                minut: timer.minuteAgo(tt),
                                timer: tt,
                                position: mdom.scrollTop()
                            },
                            beforeSend: function () { layer.load(2); },
                            success: function () {
                                window.clearInterval(inter);
                                even.delCookie();
                                tielem.text('');
                                callback();
                            },
                            complete: function () { layer.closeAll('loading'); },
                            error: function (msg) { alert('ajaxError:' + msg.responseText); }
                        });
                    });
                }
                else {
                    layer.msg('时间还未加载完成……');
                }
            },
            //callback:回调参数是kjid（正在学习）的item
            getlist: function (miid, kjid, callback) {
                $.ajax({
                    url: '/api/record',
                    type: 'GET', dataType: 'json', cache: false,
                    data: { miid: miid },
                    beforeSend: function () { layer.load(2); },
                    success: function (res) {
                        var dom = $('#chapter'), inkj = {};
                        dom.empty();
                        $.each(res, function (i, item) {
                            var li = $('<li/>', { 'class': 'layui-elip' }),
                                a = $('<a/>', { 'data-kjid': item.id, text: item.title, title: item.title });
                            if (item.id === kjid) {
                                a.addClass('inthis');
                                inkj = item;
                            }
                            dom.append(li.append(a));
                        });
                        even.chapter(dom);
                        callback(inkj);
                    },
                    complete: function () { layer.closeAll('loading'); },
                    error: function (msg) { alert('ajaxError:' + msg.responseText); }
                });
            }
        };
    $(function () {
        init.winHeig();
        init.initSet();
        init.keJian();
        init.tips();
        init.hub();

        even.openkj();
        even.prev_next();
        even.quit();
        even.browser();
    });
    function getData(cb) {
        var h_kj = parseInt(location.hash.replace('#!kj=', ''));
        if ($.isNumeric(h_kj) && h_kj > 0) {
            cb({
                miid: parseInt($('#miid').val()),
                kjid: h_kj
            });
        }
        else {
            even.skip();
        }
    }
});