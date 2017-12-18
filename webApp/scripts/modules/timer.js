layui.define(function (exports) {
    "use strict";
    var timer = {
        //计时 start：开始时间，elem：显示DOM
        counts: function (elem, start) {
            var ba = (start || '0:0:0').split(':'),
                hm = (parseInt(ba[0]) * 3600000) + (parseInt(ba[1]) * 60000) + (parseInt(ba[2]) * 1000),
                inter = window.setInterval(function () {
                    if (hm <= 86400000) {//24小时以内
                        var ti = [
                          Math.floor(hm / (1000 * 60 * 60)) % 24 //时
                          , Math.floor(hm / (1000 * 60)) % 60 //分
                          , Math.floor(hm / 1000) % 60 //秒
                        ], ta = [
                            ti[0] < 10 ? '0' + ti[0] : ti[0],
                            ti[1] < 10 ? '0' + ti[1] : ti[1],
                            ti[2] < 10 ? '0' + ti[2] : ti[2]
                        ];
                        hm += 1000;
                        elem.text(ta.join(':'));
                    }
                    else {
                        window.clearInterval(inter);
                    }
                }, 1000);
            return inter;
        },
        //返回多少分钟
        minuteAgo: function (time) {
            var ba = time.split(':');
            return parseInt(ba[0]) * 60 + parseInt(ba[1]);
        }
    };
    exports('timer', timer);
});