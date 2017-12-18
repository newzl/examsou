var menu = [{
    title: '考试管理',
    icon: '&#xe705',
    children: [{
        title: '考试成绩',
        icon: '&#xe6b2',
        href: '/admin/exam/examscore'
    }, {
        title: '非正常考试',
        icon: '&#xe6b2',
        href: '/admin/exam/nullscore'
    }]
}, {
    title: '试卷管理',
    icon: '&#xe857',
    children: [{
        title: '试卷列表',
        icon: '&#xe62d',
        href: '/admin/papermsg/planlist'
    }, {
        title: '随机组卷',
        icon: '&#xe630',
        href: '/admin/sjzj'
    }, {
        title: '手工组卷',
        icon: '&#xe630',
        href: '/admin/sgzj'
    }]
}, {
    title: '用户管理',
    icon: '&#xe612',
    children: [{
        title: '用户认证',
        icon: '&#x1005',
        href: '/admin/employeemsg/authorizes'
    }, {
        title: '用户查询',
        icon: '&#xe615',
        href: '/admin/employeemsg/list'
    }]
}, {
    title: '统计',
    icon: '&#xe62c',
    children: [{
        title: '考试情况统计',
        href: '/admin/report/examreport'
    }, {
        title: '学习情况统计',
        href: '/admin/report/learnreport'
    }, {
        title: '注册情况统计',
        href: '/admin/report/regreport'
    }, {
        title: '职称注册统计',
        href: '/admin/report/jobreport'
    }]
}, {
    title: '维护',
    icon: '&#xe614',
    children: [{
        title: '职称维护',
        icon: '&#xe631',
        href: '/admin/jobinfo/list'
    }, {
        title: '部门科室',
        icon: '&#xe631',
        href: '/admin/department/list'
    }]
}];