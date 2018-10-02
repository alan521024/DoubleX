; !(function (win) {

    " use strict ";

    var layer = layui.layer, form = layui.form, paging = layui.laypage;
    var root = win;

    var listObj = function (options) {
        var that = this;

        var _headerItemCallback = function (_type, _field, _title, _width) {
            var $item = $("<th></th>");
            $item.attr("data-field", _field);

            if (_type === "checkbox") {
                $item.addClass("dxm-list-chk-item");
                if (!that.container.hasClass("dxm-list-chk")) {
                    that.container.addClass("dxm-list-chk");
                }
                $item.html("<input type=\"checkbox\" name=\"list-chk-all\" lay-skin=\"primary\" lay-filter=\"chk-all-" + that.guid + "\" />");
            }
            else {
                if (_width > 0) {
                    $item.css({ width: _width });
                }
                $item.html("<div class=\"layui-table-cell\"><span>" + _title + "</span></div>");
            }
            return $item;
        }
        var _bodyItemCallback = function (_type, _data, _field, _value, _width) {
            var $item = $("<td></td>");
            $item.attr("data-field", _field);
            if (_type == "checkbox") {
                $item.addClass("dxm-list-chk-item");
                if (!that.container.hasClass("dxm-list-chk")) {
                    that.container.addClass("dxm-list-chk");
                }
                $item.html("<input type=\"checkbox\" name=\"list-chk-item\" lay-skin=\"primary\" lay-filter=\"chk-item-" + that.guid + "\" value=\"" + _value + "\"  />");
            }
            else {
                $item.html(_value);
            }
            return $item;
        };

        that.config = $.extend({}, {
            id: null,
            columns: [[]],
            url: null,
            method: 'POST',
            param: {},
            remote: null,
            rows: [],
            success: null,
            error: null,
            container: { wrapper: "<table class=\"layui-table dxm-table\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"></table>" },
            header: { enable: true, wrapper: "<thead></thead>", collection: "<tr></tr>", format: _headerItemCallback },
            item: { wrapper: "<tbody></tbody>", collection: "<tr></tr>", format: _bodyItemCallback },
            checkAllCallback: null,
            checkItemCallback: null,
            resultCallback: null,    //所以执行完成后回调
        }, options);

        that.guid = app.util.getId(), that.id = that.config.id, that.obj = null, that.cols = [], that.result = null;
        that.container = null, that.header = null;

        that.render();
    }

    listObj.prototype.render = function (options) {
        var that = this;

        if (options) {
            that.config = $.extend({}, that.config, options);
        };

        that.cols = [];
        for (var i = 0; i < that.config.columns.length; i++) {
            for (var j = 0; j < that.config.columns[i].length; j++) {
                that.cols.push($.extend({}, {
                    field: null,
                    type: 'html',               //type: html(default),checkbox,
                    title: null,
                    width: 0,
                    align: 'center',
                    fixed: null,                //fixed: 'left','right'
                    template: null
                }, that.config.columns[i][j]));
            }
        }

        that.setContainer();

        that.setHeader();
    }

    listObj.prototype.setContainer = function (options) {
        var that = this;

        that.container = null;

        if (options) {
            that.config = $.extend({}, that.config, options);
        };

        if (!that.id) {
            throw ('options id is null');
        }

        if ($(that.id).length == 0) {
            throw ('options id is null');
        }

        that.container = $(that.config.container.wrapper);
        that.container.removeClass("layui-form").addClass("layui-form");
        that.container.attr("lay-filter", that.guid);
        $(that.id).html(that.container);
    }

    listObj.prototype.setHeader = function (options) {
        var that = this;

        that.header = null;

        if (options) {
            that.config = $.extend({}, that.config, options);
        };

        if (!that.container) {
            throw ('container is null');
        }

        if (!that.config.header.enable) {
            return;
        }

        var $head = $(that.config.header.wrapper);
        $head.attr("data-for", "_header");

        var $collection = $(that.config.header.collection);

        for (var i = 0; i < that.cols.length; i++) {
            var _field = app.util.getProperty(that.cols[i]["field"]);
            var _type = app.util.getProperty(that.cols[i]["type"]);
            var _title = app.util.getProperty(that.cols[i]["title"]);
            var _width = app.util.getProperty(that.cols[i]["width"]) || 0;

            $collection.append(that.config.header.format(_type, _field, _title, _width));
        }

        $head.append($collection);

        if (that.container.find("[data-for='_header']").length > 0) {
            $(that.container.find("[data-for='_header']")).replaceWith($head);
        }
        else {
            that.container.html($head);
        }
        that.header = $head;
    }

    listObj.prototype.setData = function (rows, par) {
        var that = this;
        rows = rows || [];

        var $body = $(that.config.item.wrapper);
        $body.attr("data-for", "_body");

        for (var i = 0; i < rows.length; i++) {
            var $collection = $(that.config.item.collection);
            for (var j = 0; j < that.cols.length; j++) {
                var _field = app.util.getProperty(that.cols[j]["field"]);
                var _type = app.util.getProperty(that.cols[j]["type"]);
                var _width = app.util.getProperty(that.cols[j]["width"]) || 0;
                var _value = rows[i][_field];
                if (app.util.isFunction(that.cols[j]["template"])) {
                    _value = that.cols[j]["template"](_value);
                }
                $collection.append(that.config.item.format(_type, rows[i], _field, _value, _width));
            }
            $body.append($collection)
        }
        if (that.container.find("[data-for='_body']").length > 0) {
            $(that.container.find("[data-for='_body']")).replaceWith($body);
        }
        else {
            that.container.append($body);
        }

        if (app.util.isFunction(that.config.resultCallback)) {
            that.config.resultCallback(that.result, par);
        }

        that.event();

        listObjLayUIRend(that.guid);
    }

    listObj.prototype.query = function (options) {
        var that = this;

        if (options) {
            that.config = $.extend({}, that.config, options);
        };

        if (!that.container)
            throw ('container is null');

        if (that.config.rows && that.config.rows.length > 0) {
            that.result = that.config.rows;
            that.setData(that.config.rows);
        }
        else if (that.config.url || that.config.remote) {

            var _par = that.config.param;
            if (app.util.isFunction(that.config.param)) {
                _par = $.extend(true, {}, {}, that.config.param());
            }
            var setting = $.extend({}, {
                method: that.config.method,
                data: _par
            }, that.config.remote);

            app.request(that.config.url, setting, function (data) {
                that.result = data;
                if (app.util.isFunction(that.config.success)) {
                    data = that.config.success(data);
                }
                that.setData(data, _par);
            }, function () {
                if (app.util.isFunction(that.config.error)) {
                    that.config.error();
                }
                that.setData(that);
            });
        }
    }

    listObj.prototype.event = function () {
        var that = this;

        var chkAllFilter = "chk-all-" + that.guid, chkItemFilter = "chk-item-" + that.guid;

        form.on("checkbox(" + chkAllFilter + ")", function (data) {

            that.container.find("[lay-filter='" + chkItemFilter + "']").each(function (index, item) {
                item.checked = data.elem.checked;
            });
            if (app.util.isFunction(that.config.checkAllCallback)) {
                that.config.checkAllCallback(this, data, 'all',
                    that.container.find("[lay-filter='" + chkItemFilter + "']"),
                    that.container.find("[lay-filter='" + chkItemFilter + "']:checked"));
            }
            listObjLayUIRend(that.guid);
        });

        form.on("checkbox(" + chkItemFilter + ")", function (data) {

            var items = that.container.find("[lay-filter='" + chkItemFilter + "']").length;
            var chks = that.container.find("[lay-filter='" + chkItemFilter + "']:checked").length;
            if (items == chks) {
                that.container.find("[lay-filter='" + chkAllFilter + "']").prop("checked", true);
            } else {
                that.container.find("[lay-filter='" + chkAllFilter + "']").prop("checked", false);
            }
            if (app.util.isFunction(that.config.checkItemCallback)) {
                that.config.checkItemCallback(this, data, 'item',
                    that.container.find("[lay-filter='" + chkItemFilter + "']"),
                    that.container.find("[lay-filter='" + chkItemFilter + "']:checked"));
            }
            listObjLayUIRend(that.guid);
        });

    }

    listObj.prototype.checked = function (values) {
        var that = this;

        values = values || [];

        var chkAllFilter = "chk-all-" + that.guid, chkItemFilter = "chk-item-" + that.guid;
        that.container.find("[lay-filter='" + chkItemFilter + "']").prop("checked", false);

        for (var i = 0; i < values.length; i++) {
            that.container.find("[lay-filter='" + chkAllFilter + "'][value='" + values[i] + "']").prop("checked", true);
        }

        var items = that.container.find("[lay-filter='" + chkItemFilter + "']").length;
        var chks = that.container.find("[lay-filter='" + chkItemFilter + "']:checked").length;
        if (items == chks) {
            that.container.find("[lay-filter='" + chkAllFilter + "']").prop("checked", true);
        } else {
            that.container.find("[lay-filter='" + chkAllFilter + "']").prop("checked", false);
        }

        listObjLayUIRend(that.guid);
    }

    listObj.prototype.getSelecteds = function () {
        var that = this;
        var chks = that.container.find("[lay-filter='chk-item-" + that.guid + "']:checked");
        var selecteds = [];
        for (var i = 0; i < chks.length; i++) {
            selecteds.push(chks[i].value);
        }
        return selecteds;
    }

    var listObjLayUIRend = function (filter) {
        form.render('checkbox', filter);
    }

    var pagingObj = function (options) {

        var that = this;

        that.config = $.extend({}, {
            elem: null,
            count: 0,
            limit: 10,
            limits: [5, 10, 20, 30, 40, 50],
            groups: 3,
            layout: ['prev', 'page', 'next', 'skip', 'count', 'limit'],
            prev: '<i class="layui-icon">&#xe603;</i>',
            next: '<i class="layui-icon">&#xe602;</i>',
            jump: function (obj, first) {
                if (!first) {
                }
            }
        }, options);

        that.render();
    }

    pagingObj.prototype.render = function (options) {
        var that = this;

        if (options) {
            that.config = $.extend({}, that.config, options);
        };

        paging.render(that.config);
    }

    var control = {
        list: function (opt) { return new listObj(opt); },
        paging: function (opt) { return new pagingObj(opt); }
    }

    if (typeof exports !== 'undefined') {
        if (typeof module !== 'undefined' && module.exports) {
            exports = module.exports = control;
        }
        exports.control = control;
    }
    else {
        root.control = control;
    }

})(window);