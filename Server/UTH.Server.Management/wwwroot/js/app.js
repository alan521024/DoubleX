; !(function () {

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

    app.util = root._;

    // 是否为空
    app.isEmpty = function (obj) {
        var that = this;
        try {
            if (obj == null)
                return true;

            var typeValue = typeof (obj);

            if (typeValue == 'undefined')
                return true;

            if (that.util.isArray(obj)) {
                if (obj.length == 0)
                    return true;
                return false;
            }

            if (typeValue == "object" && Object.prototype.isPrototypeOf(obj) && Object.keys(obj).length === 0) {
                return true;
            }


            if (typeValue == "string") {
                obj = that.util.trim(obj);
                if (obj.length == 0)
                    return true;
            }
        }
        catch (e) {
            return true;
        }
        return false;
        //return obj == undefined || obj == null || (util.trim(obj) == '');
    }

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
            jsonData = options.data();
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
                if (that.isEmpty(res))
                {
                    alert('结果为空');
                    return;
                }
                if (res.code == 0) {
                    success(res.obj);
                }
                else {
                    if (that.util.isFunction(error)) {
                        error(res)
                    }else{
                        alert(res.message);
                    }
                }
            }
        }
        $.ajax(options);
    }

})();