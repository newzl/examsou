"use strict";
function getnav(emp, callback) {
    var name = emp.name != null ? emp.name : '游客';
    var photo = emp.photo != null ? emp.photo : '/images/face/face0001.png';
    var nav =
    [{
        title: '在线考试'
    }, {
        title: '在线学习'
    }, {
        title: '管理中心'
    }, {
        title: '<img src="' + photo + '" class="layui-nav-img">' + name,
        children: [
            {
                title: '基本资料',
                href: '/profile/personal'
            }, {
                title: '个人中心',
                href: '/profile/core'
            }, {
                title: '安全退出',
                href: '/account/quitlogin'
            }
        ]
    }];
    callback(nav);
}