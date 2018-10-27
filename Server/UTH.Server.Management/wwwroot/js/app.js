﻿; Date.prototype.format = function (format) {
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

    var app = function (obj) {
        if (obj instanceof app) return obj;
        if (!(this instanceof app)) return new app(obj);
        //this._wrapped = obj;
    };

    if (typeof exports !== 'undefined') {
        if (typeof module !== 'undefined' && module.exports) {
            exports = module.exports = app;
        }
        exports.app = app;
    }
    else {
        root.app = app;
    }

    //underscore.string.js
    //http://epeli.github.io/underscore.string/#usage
    _.mixin(s.exports());

    var _util = root._;

    var plusRegex = /\+/g;
    var spaceRegex = /\%20/g;
    var bracketRegex = /(?:([^\[]+))|(?:\[(.*?)\])/g;

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

    app.util = _util;

    app.isEmpty = _util.isEmpty;

    app.request = function (url, opt, success, error) {
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
                        error(res);
                    } else {
                        app.tip(res.message);
                    }
                }
            };
        }

        $.ajax(options);
    };

    app.open = function (url, opt) {
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

    app.tip = function (msg, callback) {
        layer.alert(msg || "", { title: __language.sysTiShi }, function (index) {
            if (callback) {
                callback();
            }
            layer.close(layer.index);
        });
    };

    app.message = function (msg, callback) {
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

    app.confirm = function (msg, callback, cancel) {
        layer.confirm(msg, {
            title: __language.sysQueRen,
            btn: [__language.sysQueDing, __language.sysQuXiao] //按钮
        }, function (index) {
            if (!app.isEmpty(callback)) {
                callback();
            }
            layer.close(index);
        }, function (index) {
            if (!app.isEmpty(cancel)) {
                cancel();
            }
            layer.close(index);
        });
    };

    app.parentFunc = function (func, args) {
        var callWin = _util.getParent(window, func);
        if (callWin) {
            if (args) {
                eval("callWin." + callback + "(args)");
            } else {
                eval("callWin." + callback + "()");
            }
        }
    };

    app.tableEditTemplate = function (id) {
        return "<span class='dxm-table-actions' data-id='" + id + "'><a href='javascript:;' class='dxm-table-item-update'>" + __language.sysXiuGai + "</a><a href='javascript:;' class='dxm-table-item-delete'>" + __language.sysShanChu + "</a></span>";
    };

})();