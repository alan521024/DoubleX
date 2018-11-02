; Date.prototype.format = function (format) {
    if (!format) {
        format = 'yyyy-MM-dd hh:mm'
    }
    var o = {
        "M+": this.getMonth() + 1, //month
        "d+": this.getDate(),    //day
        "h+": this.getHours(),   //hour
        "m+": this.getMinutes(), //minute
        "s+": this.getSeconds(), //second
        "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter
        "S": this.getMilliseconds() //millisecond
    }
    if (/(y+)/.test(format)) format = format.replace(RegExp.$1,
        (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o) if (new RegExp("(" + k + ")").test(format))
        format = format.replace(RegExp.$1,
            RegExp.$1.length == 1 ? o[k] :
                ("00" + o[k]).substr(("" + o[k]).length));
    return format;
};
String.prototype.date = function (format) {
    var date = new Date(this);
    return date.format(format);
}

!(function () {

    " use strict ";

    var root = this;

    var common = function (obj) {
        if (obj instanceof common) return obj;
        if (!(this instanceof common)) return new common(obj);
        //this._wrapped = obj;
    };

    if (typeof exports !== 'undefined') {
        if (typeof module !== 'undefined' && module.exports) {
            exports = module.exports = common;
        }
        exports.common = common;
    }
    else {
        root.common = common;
    }

    //underscore.string.js
    //http://epeli.github.io/underscore.string/#usage
    _.mixin(s.exports());

    var _util = root._;

    var plusRegex = /\+/g;
    var spaceRegex = /\%20/g;
    var bracketRegex = /(?:([^\[]+))|(?:\[(.*?)\])/g;

    /**
     * 格式化字符串.formatString("{0}-{1}","a","b");
     * */
    _util.formatString = function () {
        for (var i = 1; i < arguments.length; i++) {
            var exp = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
            arguments[0] = arguments[0].replace(exp, arguments[i]);
        }
        return arguments[0];
    };

    /**
     * 格式化时间显示方式* 用法:format="yyyy-MM-dd hh:mm:ss";
     * @param {any} v
     * @param {any} format
     */
    _util.formatDate = function (v, format) {
        if (!v) return "";
        if (!format) { format = 'yyyy-MM-dd hh:mm:ss'; }
        var d = v;
        if (typeof v === 'string') {
            if (v.indexOf("/Date(") > -1) {
                d = new Date(parseInt(v.replace("/Date(", "").replace(")/", ""), 10));
            }
            else {
                var newStr = v.replace(/-/g, "/").replace("T", " ");
                if (newStr.indexOf(".") > -1) {
                    newStr = newStr.split(".")[0];
                }
                if (newStr.indexOf("+") > -1) {
                    newStr = newStr.split("+")[0];
                }
                d = new Date(Date.parse(newStr));//.split(".")[0] 用来处理出现毫秒的情况，截取掉.xxx，否则会出错
            }
        }
        var o = {
            "M+": d.getMonth() + 1,  //month
            "d+": d.getDate(),       //day
            "h+": d.getHours(),      //hour
            "m+": d.getMinutes(),    //minute
            "s+": d.getSeconds(),    //second
            "q+": Math.floor((d.getMonth() + 3) / 3),  //quarter
            "S": d.getMilliseconds() //millisecond
        };
        if (/(y+)/.test(format)) {
            format = format.replace(RegExp.$1, (d.getFullYear() + "").substr(4 - RegExp.$1.length));
        }
        for (var k in o) {
            if (new RegExp("(" + k + ")").test(format)) {
                format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
            }
        }
        return format;
    };

    /**
     * 格式化数字显示方式  formatNumber(12345.999,'#,##0.00');  formatNumber(12345.999,'#,##0.##');  formatNumber(123,'000000');
     * @param {any} v
     * @param {any} pattern
     */
    _util.formatNumber = function (v, pattern) {
        if (v == null)
            return v;
        var strarr = v ? v.toString().split('.') : ['0'];
        var fmtarr = pattern ? pattern.split('.') : [''];
        var retstr = '';
        // 整数部分   
        var str = strarr[0];
        var fmt = fmtarr[0];
        var i = str.length - 1;
        var comma = false;
        for (var f = fmt.length - 1; f >= 0; f--) {
            switch (fmt.substr(f, 1)) {
                case '#':
                    if (i >= 0) retstr = str.substr(i--, 1) + retstr;
                    break;
                case '0':
                    if (i >= 0) retstr = str.substr(i--, 1) + retstr;
                    else retstr = '0' + retstr;
                    break;
                case ',':
                    comma = true;
                    retstr = ',' + retstr;
                    break;
            }
        }
        if (i >= 0) {
            if (comma) {
                var l = str.length;
                for (; i >= 0; i--) {
                    retstr = str.substr(i, 1) + retstr;
                    if (i > 0 && ((l - i) % 3) == 0) retstr = ',' + retstr;
                }
            }
            else retstr = str.substr(0, i + 1) + retstr;
        }
        retstr = retstr + '.';
        // 处理小数部分   
        str = strarr.length > 1 ? strarr[1] : '';
        fmt = fmtarr.length > 1 ? fmtarr[1] : '';
        i = 0;
        for (var f = 0; f < fmt.length; f++) {
            switch (fmt.substr(f, 1)) {
                case '#':
                    if (i < str.length) retstr += str.substr(i++, 1);
                    break;
                case '0':
                    if (i < str.length) retstr += str.substr(i++, 1);
                    else retstr += '0';
                    break;
            }
        }
        return retstr.replace(/^,+/, '').replace(/\.$/, '');
    };

    /**
     * 格式化金额 #,##0.00
     * @param {any} value
     */
    _util.formatMoney = function (value) {
        var sign = value < 0 ? '-' : '';
        return sign + utils.formatNumber(Math.abs(value), '#,##0.00');
    };

    /**
     * 格式化百分比 
     * @param {any} value
     */
    _util.formatPercent = function (value) {
        return (Math.round(value * 10000) / 100).toFixed(2) + '%';
    };

    /**
     * html转义
     * @param {any} str
     */
    _util.htmlEncode = function (str) {
        var s = "";
        if (str.length == 0) return "";
        s = str.replace(/&/g, "&gt;");
        s = s.replace(/</g, "&lt;");
        s = s.replace(/>/g, "&gt;");
        s = s.replace(/ /g, "&nbsp;");
        s = s.replace(/\'/g, "&#39;");
        s = s.replace(/\"/g, "&quot;");
        s = s.replace(/\n/g, "<br>");
        return s;
    }

    /**
     * html转义
     * @param {any} str
     */
    _util.htmlDecode = function (str) {
        var s = "";
        if (str.length == 0) return "";
        s = str.replace(/&gt;/g, "&");
        s = s.replace(/&lt;/g, "<");
        s = s.replace(/&gt;/g, ">");
        s = s.replace(/&nbsp;/g, " ");
        s = s.replace(/&#39;/g, "\'");
        s = s.replace(/&quot;/g, "\"");
        s = s.replace(/<br>/g, "\n");
        return s;
    }

    /**
     * 判断是否为空
     * @param {any} obj
     */
    _util.isEmpty = function (obj) {
        try {
            if (obj == null)
                return true;

            var typeValue = typeof (obj);

            if (typeValue == 'undefined')
                return true;

            if (_util.isArray(obj)) {
                if (obj.length == 0)
                    return true;
                return false;
            }

            if (typeValue == "object" && Object.prototype.isPrototypeOf(obj) && Object.keys(obj).length === 0) {
                return true;
            }

            if (typeValue == "string") {
                obj = _util.trim(obj);
                if (obj.length === 0)
                    return true;
                if (obj === "00000000-0000-0000-0000-000000000000")
                    return true;
            }
        }
        catch (e) {
            return true;
        }
        return false;
        //return obj == undefined || obj == null || (util.trim(obj) == '');
    };

    /**
     * 获取GUID
     * */
    _util.getId = function () {
        // rel:https://github.com/tufanbarisyildirim/
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    };

    _util.getProperty = function (pro, args) {
        if (!_util.isFunction(pro))
            return pro;
        return pro(args);
    };

    _util.urlEncode = function (s) {
        return encodeURIComponent(s).replace(spaceRegex, '+');
    };

    _util.urlDecode = function (s) {
        return decodeURIComponent(s.replace(plusRegex, '%20'));
    };

    _util.urlPath = function (url) {
        url = _util.urlDecode(url) || "";
        if (url.indexOf("?") > -1) {
            return url.substring(0, url.indexOf("?"));
        }
        return url;
    };

    _util.urlQuery = function (url) {
        url = _util.urlDecode(url) || "";
        if (url.indexOf("?") === -1 && url.indexOf('=') == -1)
            return "";
        var queryStr = url.substring(url.indexOf("?") + 1);

        if (queryStr.indexOf("=") === -1)
            queryStr = queryStr + "=";

        return queryStr;
    };

    _util.urlQueryAppend = function (url, obj) {
        var fixd = _util.urlPath(url), queryStr = _util.urlQuery(url);
        var queryObj = {};

        if (!_util.isEmpty(queryStr)) {
            queryObj = _util.fromQuery(queryStr);
        }
        queryStr = _util.toQuery(_util.merge(queryObj, obj));
        return fixd + "?" + queryStr;
    };

    _util.urlQueryDelete = function (url, keys) {
        var fixd = _util.urlPath(url), queryStr = _util.urlQuery(url);
        var queryObj = {};
        if (!_util.isEmpty(queryStr)) {
            queryObj = _util.fromQuery(queryStr);
        }
        queryStr = _util.toQuery(_util.omit(queryObj, keys));
        return fixd + "?" + queryStr;
    };

    _util.getParent = function (win, p) {
        if (p) {
            try {
                if (eval("win." + p)) {
                    return win;
                }
            } catch (e) { ; }
        }

        if (win === window.top) {
            if (p)
                return null;
            return win;
        }
        return _util.getParent(window.parent, p);
    };
    
    common.util = _util;

    common.isEmpty = _util.isEmpty;

    common.request = function (url, opt, success, error) {
        var that = this;
        var options = $.extend({}, {
            type: "POST",
            dataType: "JSON",
            async: true,
            contentType: "application/json; charset=utf-8",
            beforeSend: function (data) { },
            complete: function () { },
            success: function (result) { },
            error: function (error) { }
        }, opt);

        var jsonData = options.data;

        if (that.util.isFunction(options.data)) {
            jsonData = $.extend(true, {}, {}, options.data());
        }
        if (that.util.isObject(jsonData)) {
            jsonData = JSON.stringify(jsonData);
        }

        options.data = jsonData;

        if (that.util.isString(url)) {
            options.url = url;
        }

        if (that.util.isFunction(success)) {
            options.success = function (res) {
                if (that.isEmpty(res)) {
                    alert(__language.sysJieGuoWeiKong);
                    return;
                }
                if (res.code === 0) {
                    success(res.obj);
                }
                else {
                    if (that.util.isFunction(error)) {
                        error(res.code, res.message, res.data);
                    } else {
                        common.tip(res.message);
                    }
                }
            };
        }

        $.ajax(options);
    };

    common.open = function (url, opt) {
        url = url || "";
        url = _util.urlQueryAppend(url, { _layout: "master" });
        var setting = $.extend({}, {
            type: 2,
            title: __language.sysCaoZuo,
            shade: [0.3],
            area: ['740px', '540px'],
            maxmin: true, //开启最大化最小化按钮
            content: [url]
        }, opt);
        layer.open(setting);
    };

    common.tip = function (msg, callback) {
        layer.alert(msg || "", { title: __language.sysTiShi }, function (index) {
            if (callback) {
                callback();
            }
            layer.close(layer.index);
        });
    };

    common.message = function (msg, callback) {
        layer.alert(msg || "", { title: __language.sysTiShi }, function (index) {
            if (callback) {
                callback();
            }
            //关闭自身
            layer.close(index); //它获取的始终是最新弹出的某个层，值是由layer内部动态递增计算的

            //如果你想关闭最新弹出的层，直接获取layer.index即可
            //layer.close(layer.index);

            //当你在iframe页面关闭自身时
            try {
                var parentIndex = parent.layer.getFrameIndex(window.name);
                parent.layer.close(parentIndex);
            }
            catch (e) { ; }
        });
    };

    common.confirm = function (msg, callback, cancel) {
        layer.confirm(msg, {
            title: __language.sysQueRen,
            btn: [__language.sysQueDing, __language.sysQuXiao] //按钮
        }, function (index) {
            if (!common.isEmpty(callback)) {
                callback();
            }
            layer.close(index);
        }, function (index) {
            if (!common.isEmpty(cancel)) {
                cancel();
            }
            layer.close(index);
        });
    };

    common.parentFunc = function (func, args) {
        var callWin = _util.getParent(window, func);
        if (callWin) {
            if (args) {
                eval("callWin." + callback + "(args)");
            } else {
                eval("callWin." + callback + "()");
            }
        }
    };

    common.tableEditTemplate = function (id) {
        return "<span class='dxm-table-actions' data-id='" + id + "'><a href='javascript:;' class='dxm-table-item-update'>" + __language.sysXiuGai + "</a><a href='javascript:;' class='dxm-table-item-delete'>" + __language.sysShanChu + "</a></span>";
    };

})();