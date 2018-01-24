layui.use(['element', 'layer', 'laytpl'], function () {
    var element = layui.element,
        layer = layui.layer,
        laytpl = layui.laytpl;
    $.cookie('COURSETIME', null, { path: '/', expires: -1 });//删除正在学习cookie
    $.ajax({
        url: '/api/eduindex',
        type: 'get', dataType: 'json', cache: false,
        beforeSend: function () { layer.load(2); },
        success: function (res) {
            if (res) {
                $('#itemName').text(res.name);
                var kj = res.inkjid <= 0 ? res.list[0].id : res.inkjid;
                $('#continue').attr('href', '/course/learn/' + res.miid + '?rid=' + Math.random().toString(36).substr(2) + '#!kj=' + kj);
                $('#chapter').attr('href', '/learn/chapter/'+res.scid);
                progress({ text: '学习进度', percent: res.xxjd, elem: '#xxjd' });
                progress({ text: '必修课', percent: res.bxk, elem: '#bxk', color: '#f4665b' });
                progress({ text: '选修课', percent: res.xxk, elem: '#xxk', color: '#00A854' });
                laytpl(kjTpl.innerHTML).render(res, function (htm) {
                    $('#kjView').html(htm);
                });
                element.render('progress');
            }
            else {
                $('.member-content').empty();
                layer.alert('您还没有学习任何项目，请在“继教项目”里添加项目学习。', { icon: 5, closeBtn: 0, offset: '100px' }, function () {
                    window.location.replace('/eduitem/search');
                });
            }
        },
        complete: function () { layer.closeAll('loading'); },
        error: function (msg) { alert('ajaxError:' + msg.responseText); }
    });
    function progress(options) {
        var _cr = options.color || '#108ee9',
            _ps = (options.percent / 100) * 222;
        var htm = '<svg viewBox="0 0 100 100">';
        htm += '<path d="M 50,50 m 0,47 a 47,47 0 1 1 0,-94 a 47,47 0 1 1 0,94" stroke="#e2e2e2" stroke-width="6" fill-opacity="0" style="stroke-dasharray: 222px, 295.31px; stroke-dashoffset: -37.5px; transition: stroke-dashoffset 0.3s ease 0s, stroke-dasharray 0.3s ease 0s, stroke 0.3s;"></path>';
        htm += '<path d="M 50,50 m 0,47 a 47,47 0 1 1 0,-94 a 47,47 0 1 1 0,94" stroke="' + _cr + '" stroke-width="6" fill-opacity="0" style="stroke-dasharray: ' + _ps + 'px, 295.31px; stroke-dashoffset: -37.5px; transition: stroke-dashoffset 0.3s ease 0s, stroke-dasharray 0.3s ease 0s, stroke 0.3s;"></path>';
        htm += '</svg><span style="color:' + _cr + '">' + options.percent + '%</span>';
        if (options.text != undefined && options.text.length > 0) htm += '<p>' + options.text + '</p>';
        $(options.elem).empty().append(htm);
    }
});