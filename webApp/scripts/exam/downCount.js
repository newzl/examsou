(function ($) {
    var inter, minuted;
    $.fn.downCount = function (minutes, callback) {//minutes:倒计时分钟；callback：倒计时结束后的回调
        var that = this;
        if (minutes > 0) {
            minuted = minutes;
            var hm = minutes * 60000;//毫秒
            if (hm <= 86400000) {//24小时以内
                inter = window.setInterval(function () {
                    if (hm < 0) {
                        window.clearInterval(inter);
                        if (callback && typeof callback === 'function') callback();
                        return;
                    }
                    var ti = [
                      Math.floor(hm / (1000 * 60 * 60)) % 24 //时
                      , Math.floor(hm / (1000 * 60)) % 60 //分
                      , Math.floor(hm / 1000) % 60 //秒
                    ],
                    ta = [
                        ti[0] < 10 ? '0' + ti[0] : ti[0],
                        ti[1] < 10 ? '0' + ti[1] : ti[1],
                        ti[2] < 10 ? '0' + ti[2] : ti[2]
                    ];
                    hm -= 1000;
                    that.text(ta.join(':'));
                }, 1000);
            }
            else {
                window.clearInterval(inter);
                $.error('倒计时只能在24小时以内');
            }
        }
        else {
            window.clearInterval(inter);
            var ba = that.text().split(':'),
                hmed = (parseInt(ba[0]) * 3600000) + (parseInt(ba[1]) * 60000) + (parseInt(ba[2]) * 1000),
                hm = (minuted * 60000) - hmed,
                ti = [
                      Math.floor(hm / (1000 * 60 * 60)) % 24 //时
                      , Math.floor(hm / (1000 * 60)) % 60 //分
                      , Math.floor(hm / 1000) % 60 //秒
                ],
                ta = [
                    ti[0] < 10 ? '0' + ti[0] : ti[0],
                    ti[1] < 10 ? '0' + ti[1] : ti[1],
                    ti[2] < 10 ? '0' + ti[2] : ti[2]
                ];
            if (callback && typeof callback === 'function') callback(ta.join(':'));
            return;
        }
    };
})(jQuery);