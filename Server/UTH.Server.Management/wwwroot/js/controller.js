; !(function (win) {

    " use strict ";

    var layer = layui.layer, form = layui.form, paging = layui.laypage;
    var root = win;

    var listObj = function (options) {
        var that = this;

        var _headerItemCallback = function (_type, _field, _title, _width, _align) {
            var $item = $("<th class=\"txt-" + _align + "\"></th>");
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
        var _bodyItemCallback = function (_type, _data, _field, _value, _width, _align) {
            var $item = $("<td class=\"txt-" + _align + "\"></td>");
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
                    align: 'left',
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
            var _align = app.util.getProperty(that.cols[i]["align"]) || "center";

            $collection.append(that.config.header.format(_type, _field, _title, _width, _align));
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
                var _align = app.util.getProperty(that.cols[j]["align"]) || "center";
                var _title = app.util.getProperty(that.cols[j]["title"]) || "";

                if (app.util.isFunction(that.cols[j]["template"])) {
                    _value = that.cols[j]["template"](rows[i]);
                }

                $collection.append(that.config.item.format(_type, rows[i], _field, _value, _width, _align));
            }
            $body.append($collection);
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


    var fileObj = function (options) {
        var that = this;
        that.guid = app.util.getId(), that.__ = window.top, that.__$ = window.top.$, that.__layer = window.top.layer, that.__WebUploader = window.top.WebUploader;
        that.obj = null, that.container = null, that.uploader = null;

        that.config = $.extend({}, {
            id: null,
            files: []
        }, options);

        that.render();
    }

    fileObj.prototype.render = function (options) {
        var that = this;
        that.obj = null;
        that.container = null;
        that.uploader = null;

        if (options) {
            that.config = $.extend({}, that.config, options);
        };

        that.obj = $("<div class='files-list'></div><div class='files-select'><a href='#'>选择</a></div>");
        $(that.config.id).html(that.obj);

        $(that.config.id).find('.files-select').unbind("click").on("click", function () {
            that.open();
        });
    }

    fileObj.prototype.open = function () {
        var that = this;
        var selectId = "btn-" + that.guid + "-select";

        var html = "";
        html += "<div class=\"assets\" data-id=\"" + that.guid + "\">";
        html += "    <div class=\"assets-navs\">";
        html += "        <div class=\"tabs\">";
        html += "            <span class=\"active\">上传</span>";
        html += "            <span>管理</span>";
        html += "        </div>";
        html += "        <div class=\"links\">";
        html += "            <a href=\"javascript:;\" id=\"" + selectId + "\">选择</a>";
        html += "        </div>";
        html += "    </div>";
        html += "    <div class=\"assets-main\">"
        html += "       <div class=\"assets-items\">";
        html += "       </div>";
        html += "       <div class=\"assets-manage\">";
        html += "       </div>";
        html += "    </div>";
        html += "    <div class=\"assets-footer\">"
        html += "           <a href='javascript:;' class='layui-btn layui-btn-sm btn-upload'>上传</a>";
        html += "    </div>";
        html += "</div>";

        that.__layer.open({
            type: 1,
            title: '资源管理器',
            content: html,
            area: ['540px', '340px'],
        });

        that.container = that.__$("div[data-id='" + that.guid + "']");

        that.uploader = that.__WebUploader.create({
            swf: '/js/Uploader.swf',
            server: '/common/upload',
            pick: "#" + selectId,
            resize: false,
            duplicate: true,
            threads: 3,
            chunked: true,
            chunkSize: 5242880,  //5M
            chunkRetry: 5,
        });

        that.uploader.on('beforeFileQueued', function (file) {
            if (that.container.find(".assets-up-file[data-name='" + file.name + "'][data-size='" + file.size + "']").length > 0) {
                return false;
            }
            return true;
        });

        that.uploader.on('fileQueued', function (file) {
            that.uploader.md5File(file).progress(function (percentage) {
                //console.log('Percentage:', percentage);
            }).then(function (val) {
                var _html = "";
                _html += "<div class='assets-up-file' data-id='" + file.id + "' data-name='" + file.name + "' data-size='" + file.size + "' data-md5='" + val + "'>";
                _html += "  <span>" + file.name + "</span>";
                _html += "  <span class='info'><em class='status'>0%</em><em class='close'>x</em></span>";
                _html += "</div>";
                that.container.find(".assets-items").append(_html);
                syncFiles();
            });
        });

        that.uploader.on('uploadBeforeSend', function (obj, data, headers) {
            var md5 = that.container.find(".assets-up-file[data-id='" + data['id'] + "']").attr("data-md5");
            data["md5"] = md5 || "";
            data["autoMerge"] = false;
        })

        that.uploader.on('uploadProgress', function (file, percentage) {
            that.container.find(".assets-up-file[data-id='" + file['id'] + "']").find(".status").html(percentage);
        });

        that.uploader.on('uploadSuccess', function (file, response) {
            var md5 = (that.container.find(".assets-up-file[data-id='" + file['id'] + "']").attr("data-md5")) || "";
            var action = encodeURIComponent("/app/merge?md5=" + md5 + "&name=" + file.name);
            app.request("/common/do?action=" + action, { }, function (data) {

            }, function (err) {

            })
        });

        that.uploader.on('uploadError', function (file, reason) {
            ///console.log("uploadError:", file, reason);
        });

        that.uploader.on('error', function (type) {
            //console.log(type);
        });

        that.container.on("click", "em.close", function () {
            var _id = $(this).parents(".assets-up-file").data("id");
            that.uploader.removeFile(_id, true);
            $(this).parents(".assets-up-file").remove();
            syncFiles();
        });

        that.container.on("click", ".btn-upload", function () {
            that.uploader.upload();
        });

        function syncFiles() {
            var uploadBtn = that.container.find(".btn-upload");
            var files = that.uploader.getFiles();
            uploadBtn.addClass("layui-btn-disabled");
            if (files.length > 0) {
                uploadBtn.removeClass("layui-btn-disabled");
            }
            console.log(files);
        }
        syncFiles();
    }
    
    var control = {
        list: function (opt) { return new listObj(opt); },
        paging: function (opt) { return new pagingObj(opt); },
        file: function (opt) { return new fileObj(opt); }
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