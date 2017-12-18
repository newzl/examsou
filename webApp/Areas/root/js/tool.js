var repl = [], appName = '';
$.ajax({
    url: '/root/tool/getreplace',
    type: 'get', dataType: 'json', cache: false,
    data: null,
    success: function (res) {
        $.each(res, function (i, item) {
            repl.push({ r: eval(item.r), n: item.n })
        });
    },
    error: function (msg) { alert('ajaxError:' + msg.responseText); }
});
//替换gif
function replg(str) {
    $.each(repl, function (i, ri) {
        str = str.replace(ri.r, ri.n);
    });
    return str;
}
function _setImg(str) {//暂时没用
    var isrc = 'http://t.api.ksbao.com/tk_img/ImgDir_' + appName + '/';
    str = str.replace(/[%[]/g, '<img src="' + isrc);
    str = str.replace(/]/g, '"/>');
    return str;
}
$('#netParse').click(function () {
    var nva = $('#netData').val();
    if (nva.length > 0) {
        serialize($.parseJSON(nva));
    }
    else {
        alert('没有任何数据');
    }
});
function serialize(nd) {
    var cdom = $('.layui-collapse'), num = 0;
    cdom.empty();
    appName = nd.data.test.APPEName;
    $('.appname').text(appName);
    $.each(nd.data.test.StyleItems, function (i, obj) {
        num += obj.TestCount;
        var citem = $('<div/>', { 'class': 'layui-colla-item' });
        $('<div class="layui-colla-title">【类型：' + obj.Style + '】&ensp;【数量：' + obj.TestCount + '】</div>').appendTo(citem);
        var content = $('<div/>', { 'class': 'layui-colla-content' });
        //--------
        if (obj.Type === 'ATEST') {//单选题
            var subject = [];
            $.each(obj.TestItems, function (i, item) {
                load.ABX({
                    tit: item.Title,
                    ans: item.Answer,
                    exp: item.Explain,
                    sel: item.SelectedItems,
                }, function (res) {
                    subject.push(res);
                });
            });
            word.ABX(subject, content, obj.Style);
            textareas(subject, content);
            table.ABX(subject, content);
        }
        else if (obj.Type === 'BTEST') {
            var subject = [];
            $.each(obj.TestItems, function (i, item) {
                if (item.BTestItems !== undefined) {
                    $.each(item.BTestItems, function (i, bitem) {
                        load.ABX({
                            tit: bitem.Title,
                            ans: bitem.Answer,
                            exp: bitem.Explain,
                            sel: item.SelectedItems,
                        }, function (res) {
                            subject.push(res);
                        });
                    });
                }
                else {
                    load.ABX({
                        tit: item.Title,
                        ans: item.Answer,
                        exp: item.Explain,
                        sel: item.SelectedItems,
                    }, function (res) {
                        subject.push(res);
                    });
                }
            });
            word.ABX(subject, content, obj.Style);
            textareas(subject, content);
            table.ABX(subject, content);
        }
        else if (obj.Type === 'XTEST') {//多选题
            var subject = [];
            $.each(obj.TestItems, function (i, item) {
                load.ABX({
                    tit: item.Title,
                    ans: item.Answer.split('').join(),
                    exp: item.Explain,
                    sel: item.SelectedItems,
                }, function (res) {
                    subject.push(res);
                });
            });
            word.ABX(subject, content, obj.Style);
            textareas(subject, content);
            table.ABX(subject, content, true);
        }
        else if (obj.Type === 'PDTEST') {//判断题
            var subject = [];
            $.each(obj.TestItems, function (i, item) {
                load.PD({
                    tit: item.Title,
                    ans: item.Answer,
                    exp: item.Explain
                }, function (res) {
                    subject.push(res);
                });
            });
            word.PD(subject, content, obj.Style);
            textareas(subject, content);
            table.PD(subject, content);
        }
        else if (obj.Type === 'JDTEST') {//简答题
            var subject = [];
            $.each(obj.TestItems, function (i, item) {
                load.JDTK({
                    tit: item.Title,
                    ans: item.Answer,
                    exp: item.Explain
                }, function (res) {
                    subject.push(res);
                });
            });
            word.JDTK(subject, content, obj.Style);
            textareas(subject, content);
            table.JDTK(subject, content);
        }
        else if (obj.Type === 'TKTEST') {//填空题
            var subject = [];
            $.each(obj.TestItems, function (i, item) {
                load.JDTK({
                    tit: item.Title,
                    ans: item.Answer.join('；'),
                    exp: item.Explain
                }, function (res) {
                    subject.push(res);
                });
            });
            word.JDTK(subject, content, obj.Style);
            textareas(subject, content);
            table.JDTK(subject, content);
        }
        else if (obj.Type === 'A3TEST') {
            var subject = [];
            $.each(obj.TestItems, function (i, item) {
                $.each(item.A3TestItems, function (i, its) {
                    load.A3({
                        tit: replg(aesDecrypt(item.FrontTitle)) + '<br/>' + replg(aesDecrypt(its.Title)),
                        ans: its.Answer,
                        exp: its.Explain,
                        sel: its.SelectedItems,
                    }, function (ress) {
                        subject.push(ress);
                    });
                });
            });
            word.ABX(subject, content, obj.Style);
            textareas(subject, content);
            table.ABX(subject, content);
        }
        else {
            alert('未知类型');
        }
        //-------
        citem.append(content);
        cdom.append(citem);
    });
    $('.appnum').text(num);
    element.render('collapse');
}
var load = {
    ABX: function (obj, callback) {
        var sitem = {};
        sitem['tit'] = replg(aesDecrypt(obj.tit));
        sitem['ans'] = $.trim(obj.ans);
        sitem['exp'] = obj.exp.length > 0 ? replg(obj.exp) : null;
        $.each(obj.sel, function (i, opt) {
            sitem[opt.ItemName] = replg(opt.Content);
        });
        callback(sitem);
    },
    A3: function (obj, callback) {
        var sitem = {};
        sitem['tit'] = obj.tit;
        sitem['ans'] = $.trim(obj.ans);
        sitem['exp'] = obj.exp.length > 0 ? replg(obj.exp) : null;
        $.each(obj.sel, function (i, opt) {
            sitem[opt.ItemName] = replg(opt.Content);
        });
        callback(sitem);
    },
    PD: function (obj, callback) {
        var sitem = {};
        sitem['tit'] = replg(aesDecrypt(obj.tit));
        sitem['ans'] = obj.ans === 'A' ? true : false;
        sitem['exp'] = obj.exp.length > 0 ? replg(obj.exp) : null;
        callback(sitem);
    },
    JDTK: function (obj, callback) {
        var sitem = {};
        sitem['tit'] = replg(aesDecrypt(obj.tit));
        sitem['ans'] = replg(obj.ans);
        sitem['exp'] = obj.exp.length > 0 ? replg(obj.exp) : null;
        callback(sitem);
    }
};
var word = {
    ABX: function (obj, elem, filename) {
        var btn = $('<button/>', { 'class': 'layui-btn layui-btn-xs', text: '导出word' });
        var word = $('<div class="exportWord" style="display:none"></div>');
        $.each(obj, function (i, sub) {
            var htm = '<div>' + (i + 1) + '、' + sub.tit + '</div>';
            htm += '<div>A、' + sub.A + '</div>';
            htm += '<div>B、' + sub.B + '</div>';
            htm += '<div>C、' + sub.C + '</div>';
            htm += '<div>D、' + sub.D + '</div>';
            if (sub.E !== undefined) if (sub.E.length > 0) htm += '<div>E、' + sub.E + '</div>';
            if (sub.F !== undefined) if (sub.F.length > 0) htm += '<div>F、' + sub.F + '</div>';
            htm += '<div>【答案】' + sub.ans + '</div>';
            var ex = sub.exp !== null ? sub.exp : '';
            htm += '<div>【解析】' + ex + '</div><br />';
            word.append(htm);
        });
        word.append('<div>' + JSON.stringify(obj) + '</div>');
        elem.append(btn).append(word);
        btn.click(function () {
            $(this).next('.exportWord').wordExport(filename);
        });
    },
    PD: function (obj, elem, filename) {
        var btn = $('<button/>', { 'class': 'layui-btn layui-btn-xs', text: '导出word' });
        var word = $('<div class="exportWord" style="display:none"></div>');
        $.each(obj, function (i, sub) {
            var htm = '<div>' + (i + 1) + '、' + sub.tit + '</div>';
            var anss = sub.ans ? '正确' : '错误';
            htm += '<div>【答案】' + anss + '</div>';
            var ex = sub.exp !== null ? sub.exp : '';
            htm += '<div>【解析】' + ex + '</div><br />';
            word.append(htm);
        });
        word.append('<div>' + JSON.stringify(obj) + '</div>');
        elem.append(btn).append(word);
        btn.click(function () {
            $(this).next('.exportWord').wordExport(filename);
        });
    },
    JDTK: function (obj, elem, filename) {
        var btn = $('<button/>', { 'class': 'layui-btn layui-btn-xs', text: '导出word' });
        var word = $('<div class="exportWord" style="display:none"></div>');
        $.each(obj, function (i, sub) {
            var htm = '<div>' + (i + 1) + '、' + sub.tit + '</div>';
            htm += '<div>【答案】' + sub.ans + '</div>';
            var ex = sub.exp !== null ? sub.exp : '';
            htm += '<div>【解析】' + ex + '</div><br />';
            word.append(htm);
        });
        word.append('<div>' + JSON.stringify(obj) + '</div>');
        elem.append(btn).append(word);
        btn.click(function () {
            $(this).next('.exportWord').wordExport(filename);
        });
    }
};
var table = {
    ABX: function (obj, elem, isF) {
        var table = $('<table/>');
        var thea = '<thead><tr><th class="miwid">序号</th><th>题目</th><th class="miwid">答案</th><th>A</th><th>B</th><th>C</th><th>D</th><th>E</th>';
        if (isF) thea += '<th>F</th>';
        thea += '<th class="miwid">解析</th></tr></thead>';
        table.append(thea);
        var tbody = $('<tbody/>');
        $.each(obj, function (i, sub) {
            var htm = '<tr><td>' + (i + 1) + '</td><td>' + sub.tit + '</td><td>' + sub.ans + '</td><td>' + sub.A + '</td><td>' + sub.B + '</td><td>' + sub.C + '</td><td>' + sub.D + '</td>';
            if (sub.E !== undefined) {
                if (sub.E.length > 0) htm += '<td>' + sub.E + '</td>';
                else htm += '<td></td>';
            }
            else {
                htm += '<td></td>';
            }
            if (isF) {
                if (sub.F !== undefined) {
                    if (sub.F.length > 0) htm += '<td>' + sub.F + '</td>';
                    else htm += '<td></td>';
                }
                else {
                    htm += '<td></td>';
                }
            }
            var ex = sub.exp !== null ? sub.exp : '';
            htm += '<td>' + ex + '</td></tr>';
            tbody.append(htm);
        });
        table.append(tbody);
        elem.append(table);
    },
    PD: function (obj, elem) {
        var table = $('<table/>');
        table.append('<thead><tr><th class="miwid">序号</th><th>题目</th><th class="miwid">答案</th><th class="miwid">解析</th></tr></thead>');
        var tbody = $('<tbody/>');
        $.each(obj, function (i, sub) {
            var anss = sub.ans ? '正确' : '错误';
            var htm = '<tr><td>' + (i + 1) + '</td><td>' + sub.tit + '</td><td>' + anss + '</td>';
            var ex = sub.exp !== null ? sub.exp : '';
            htm += '<td>' + ex + '</td></tr>';
            tbody.append(htm);
        });
        table.append(tbody);
        elem.append(table);
    },
    JDTK: function (obj, elem) {
        var table = $('<table/>');
        table.append('<thead><tr><th class="miwid">序号</th><th>题目</th><th class="miwid">答案</th><th class="miwid">解析</th></tr></thead>');
        var tbody = $('<tbody/>');
        $.each(obj, function (i, sub) {
            var htm = '<tr><td>' + (i + 1) + '</td><td>' + sub.tit + '</td><td>' + sub.ans + '</td>';
            var ex = sub.exp !== null ? sub.exp : '';
            htm += '<td>' + ex + '</td></tr>';
            tbody.append(htm);
        });
        table.append(tbody);
        elem.append(table);
    }
};
function textareas(obj, elem) {
    var t = $('<textarea style="width:100%;height:100px;"></textarea>')
    t.val(JSON.stringify(obj));
    elem.append(t);
}
//解密
function aesDecrypt(message) {
    var keyHex = CryptoJS.enc.Utf8.parse("5QTtRO3vQMaYnPajQqc4d7eaF6BNS2dG");
    var ivHex = CryptoJS.enc.Utf8.parse("5QTtRO3vQMaYnPajQqc4d7eaF6BNS2dG");
    var decrypted = CryptoJS.DES.decrypt({
        ciphertext: CryptoJS.enc.Hex.parse(message)
    }, keyHex, {
        iv: ivHex,
        mode: CryptoJS.mode.CBC,
        padding: CryptoJS.pad.Pkcs7
    });
    if (true) {
        return decrypted.toString(CryptoJS.enc.Utf8);
    } else {
        return message;
    }
}