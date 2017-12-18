layui.define(function (exports) {
    "use strict";
    var validator = {
        //验证数字(double类型) [可以包含负号和小数点]  
        isNumber: function (val) {
            if (val.match(/^-?\d+$|^(-?\d+)(\.\d+)?$/)) return true;
            else return false;
        },
        //验证正整数  
        isInt: function (val) {
            if (val.match(/^[0-9]*[1-9][0-9]*$/)) return true;
            else return false;
        },
        //验证整数  
        isInteger: function (val) {
            if (val.match(/^-?\d+$/)) return true;
            else return false;
        },
        //验证非负整数  
        isIntNN: function (val) {
            if (val.match(/^\d+$/)) return true;
            else return false;
        },
        //验证小数  
        isDecimal: function (val) {
            if (val.match(/^([-+]?[1-9]\d*\.\d+|-?0\.\d*[1-9]\d*)$/)) return true;
            else return false;
        },
        //验证手机号码 
        isPhone: function (input) {
            if (input.match(/^((\+)?86|((\+)?86)?)0?1[34578]\d{9}$/)) return true;
            else return false;
        },
        //验证电子邮箱
        isEmail: function (val) {
            if (val.match(/^([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\_|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/)) return true;
            else return false;
        },
        //验证身份证号 [可验证一代或二代身份证]  
        isIDCard: function (input) {
            input = input.toUpperCase();
            //验证身份证号码格式 [一代身份证号码为15位的数字；二代身份证号码为18位的数字或17位的数字加字母X]  
            if (!(/(^\d{15}$)|(^\d{17}([0-9]|X)$)/i.test(input))) {
                return false;
            }
            //验证省份  
            var arrCity = {
                11: '北京', 12: '天津', 13: '河北', 14: '山西', 15: '内蒙古', 21: '辽宁', 22: '吉林', 23: '黑龙江 ', 31: '上海', 32: '江苏', 33: '浙江',
                34: '安徽', 35: '福建', 36: '江西', 37: '山东', 41: '河南', 42: '湖北', 43: '湖南', 44: '广东', 45: '广西', 46: '海南', 50: '重庆', 51: '四川',
                52: '贵州', 53: '云南', 54: '西藏', 61: '陕西', 62: '甘肃', 63: '青海', 64: '宁夏', 65: '新疆', 71: '台湾', 81: '香港', 82: '澳门', 91: '国外'
            };
            if (arrCity[parseInt(input.substr(0, 2))] == null) {
                return false;
            }
            //验证出生日期  
            var regBirth, birthSplit, birth;
            var len = input.length;
            if (len == 15) {
                regBirth = new RegExp(/^(\d{6})(\d{2})(\d{2})(\d{2})(\d{3})$/);
                birthSplit = input.match(regBirth);
                birth = new Date('19' + birthSplit[2] + '/' + birthSplit[3] + '/' + birthSplit[4]);
                if (!(birth.getYear() == Number(birthSplit[2]) && (birth.getMonth() + 1) == Number(birthSplit[3]) && birth.getDate() == Number(birthSplit[4]))) {
                    return false;
                }
                return true;
            } else if (len == 18) {
                regBirth = new RegExp(/^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9]|X)$/i);
                birthSplit = input.match(regBirth);
                birth = new Date(birthSplit[2] + '/' + birthSplit[3] + '/' + birthSplit[4]);
                if (!(birth.getFullYear() == Number(birthSplit[2]) && (birth.getMonth() + 1) == Number(birthSplit[3]) && birth.getDate() == Number(birthSplit[4]))) {
                    return false;
                }
                //验证校验码  
                var valnum;
                var arrInt = new Array(7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2);
                var arrCh = new Array('1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2');
                var nTemp = 0,
					i;
                for (i = 0; i < 17; i++) {
                    nTemp += input.substr(i, 1) * arrInt[i];
                }
                valnum = arrCh[nTemp % 11];
                if (valnum != input.substr(17, 1)) {
                    return false;
                }
                return true;
            }
            return false;
        }
    };
    exports('validator', validator);
});