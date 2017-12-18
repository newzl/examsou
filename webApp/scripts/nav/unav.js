"use strict";
function getnav(emp, callback) {
    var name = emp.name != null ? emp.name : '游客';
    var photo = emp.photo != null ? emp.photo : '/images/face/face0001.png';
    var nav =
    [{
        title: '在线考试',
        children: [
            {
                title: '考试列表',
                href: '/member/examlist'
            }, {
                title: '考试记录',
                href: '/member/examrecord'
            }, {
                title: '模拟考试',
                href: '/member/simulate'
            }, {
                title: '模拟记录',
                href: '/member/simulaterecord'
            }
        ]
    }, {
        title: '在线学习',
        children: [
            {
                title: '我的学习',
                href: '/learn'
            }, {
                title: '章节练习',
                href: '/learn/chapter'
            }, {
                title: '学习记录',
                href: '/learn/record'
            }, {
                title: '我的题库',
                href: '/subject/learnsubject'
            }
        ]
    }, {
        title: '管理中心',
        children: [
            {
                title: '我的错题',
                href: '/member/myerror'
            }, {
                title: '我的笔记',
                href: '/member/mynote'
            }, {
                title: '我的收藏',
                href: '/member/mycollect'
            }, {
                title: '查找试题',
                href: '/member/findsubject'
            }
        ]
    }, {
        title: '<img src="' + photo + '" class="layui-nav-img">' + name,
        children: [
            {
                title: '我的消息',
                href: '/profile/messages'
            }, {
                title: '基本资料',
                href: '/profile/personal'
            }, {
                title: '个人中心',
                href: '/profile/core'
            }, {
                title: '选择题库',
                href: '/subject'
            }, {
                title: '意见反馈',
                href: '/feedback'
            }, {
                title: '安全退出',
                href: '/account/quitlogin'
            }
        ]
    }];
    callback(nav);
}