var sub = [{
    item: [
    {
        text: '主治医师题库',
        img: '/images/home-yxtk.jpg',
        list: [
            { text: '全科医学', sid: 12404 },
            { text: '康复医学', sid: 12315 },
            { text: '内科学', sid: 12407 },
            { text: '妇产科', sid: 12553 }
        ]
    }, {
        text: '执业医师题库',
        img: '/images/home-wszgtk.jpg',
        list: [
            { text: '临床执业医师', sid: 10006 },
            { text: '临床执业助理医师', sid: 10007 }
        ]
    }, {
        text: '护理考试题库',
        img: '/images/home-zyhstk.jpg',
        list: [
            { text: '执业护士', sid: 10008 },
            { text: '主管护师', sid: 10013 },
            { text: '儿科主管护师', sid: 10009 }
        ]
    }]
}, {
    item: [
    {
        text: '医技考试题库',
        img: '/images/home-zyystk.jpg',
        list: [
            { text: '超声波医学与技术', sid: 10011 }
        ]
    }, {
        text: '医学三基',
        img: '/images/home-tk.jpg',
        list: [
            { text: '医师', sid: 10100 },
            { text: '药师', sid: 10101 },
            { text: '医技', sid: 10102 },
            { text: '护士', sid: 10103 },
            { text: '医院管理', sid: 10104 }
        ]
    }, {
        text: '医学法律法规',
        img: '/images/home-zyys.jpg',
        list: [
            { text: '法律法规训练', sid: 11465 },
            { text: '模拟考试题库', sid: 11466 }
        ]
    }]
}];
layui.use(['element', 'laytpl', 'util'], function () {
    var element = layui.element, laytpl = layui.laytpl, util = layui.util;
    util.fixbar({ top: true });
    $(function () {
        initTopNav();
        load.learn(sub);
    });
    function initTopNav() {
        if ($('#islogin').val() === 'True') {
            var cook = $.cookie('ZXXXEMPLOYEECOOKIE');
            if (cook != undefined && cook != null) {
                var employee = $.parseJSON(cook);
                var list = [{ href: '/learn', text: '在线学习' },
                            { href: '/profile/personal', text: '基本资料' },
                            { href: '/profile/core', text: '个人中心' }];
                var listn = [{ href: '/profile/personal', text: '基本资料' },
                            { href: '/profile/core', text: '个人中心' }];
                var nav = {
                    name: employee.name != null ? employee.name : '游客',
                    photo: employee.photo != null ? employee.photo : '/images/face/face0001.png',
                    list: employee.state === 1 ? list : listn
                };
                load.topnav(nav);
                element.render();
                $('#noton').hide();
                $('#beon').show();
            }
        }
    }
    var load = {
        learn: function (obj) {
            var getTpl = learnDom.innerHTML;
            laytpl(getTpl).render(obj, function (html) {
                document.getElementById('learnView').innerHTML = html;
            });
        },
        topnav: function (obj) {
            var getTpl = topnavDom.innerHTML;
            laytpl(getTpl).render(obj, function (html) {
                document.getElementById('beon').innerHTML = html;
            });
        }
    };
});