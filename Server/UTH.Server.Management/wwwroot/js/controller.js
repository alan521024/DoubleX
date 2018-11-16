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

        that.guid = common.util.getId(), that.id = that.config.id, that.obj = null, that.cols = [], that.result = null;
        that.container = null, that.header = null;

        that.render();
    };
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
    };
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
    };
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
            var _field = common.util.getProperty(that.cols[i]["field"]);
            var _type = common.util.getProperty(that.cols[i]["type"]);
            var _title = common.util.getProperty(that.cols[i]["title"]);
            var _width = common.util.getProperty(that.cols[i]["width"]) || 0;
            var _align = common.util.getProperty(that.cols[i]["align"]) || "center";

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
    };
    listObj.prototype.setData = function (rows, par) {
        var that = this;
        rows = rows || [];

        var $body = $(that.config.item.wrapper);
        $body.attr("data-for", "_body");

        for (var i = 0; i < rows.length; i++) {
            var $collection = $(that.config.item.collection);

            for (var j = 0; j < that.cols.length; j++) {
                var _field = common.util.getProperty(that.cols[j]["field"]);
                var _type = common.util.getProperty(that.cols[j]["type"]);
                var _width = common.util.getProperty(that.cols[j]["width"]) || 0;
                var _value = rows[i][_field];
                var _align = common.util.getProperty(that.cols[j]["align"]) || "center";
                var _title = common.util.getProperty(that.cols[j]["title"]) || "";

                if (common.util.isFunction(that.cols[j]["template"])) {
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

        if (common.util.isFunction(that.config.resultCallback)) {
            that.config.resultCallback(that.result, par);
        }

        that.event();

        listObjLayUIRend(that.guid);
    };
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
            if (common.util.isFunction(that.config.param)) {
                _par = $.extend(true, {}, {}, that.config.param());
            }
            var setting = $.extend({}, {
                method: that.config.method,
                data: _par
            }, that.config.remote);

            common.request(that.config.url, setting, function (data) {
                that.result = data;
                if (common.util.isFunction(that.config.success)) {
                    data = that.config.success(data);
                }
                that.setData(data, _par);
            }, function () {
                if (common.util.isFunction(that.config.error)) {
                    that.config.error();
                }
                that.setData(that);
            });
        }
    };
    listObj.prototype.event = function () {
        var that = this;

        var chkAllFilter = "chk-all-" + that.guid, chkItemFilter = "chk-item-" + that.guid;

        form.on("checkbox(" + chkAllFilter + ")", function (data) {

            that.container.find("[lay-filter='" + chkItemFilter + "']").each(function (index, item) {
                item.checked = data.elem.checked;
            });
            if (common.util.isFunction(that.config.checkAllCallback)) {
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
            if (common.util.isFunction(that.config.checkItemCallback)) {
                that.config.checkItemCallback(this, data, 'item',
                    that.container.find("[lay-filter='" + chkItemFilter + "']"),
                    that.container.find("[lay-filter='" + chkItemFilter + "']:checked"));
            }
            listObjLayUIRend(that.guid);
        });

    };
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
    };
    listObj.prototype.getSelecteds = function () {
        var that = this;
        var chks = that.container.find("[lay-filter='chk-item-" + that.guid + "']:checked");
        var selecteds = [];
        for (var i = 0; i < chks.length; i++) {
            selecteds.push(chks[i].value);
        }
        return selecteds;
    };

    var listObjLayUIRend = function (filter) {
        form.render('checkbox', filter);
    };


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
    };
    pagingObj.prototype.render = function (options) {
        var that = this;

        if (options) {
            that.config = $.extend({}, that.config, options);
        };

        paging.render(that.config);
    };

    var itemsObj = function (options) {
        var that = this;
        that.config = $.extend({}, {
            id: null,
            className: '',
            btnText: '选择',
            placeholder: '请选择',
            items: [],
            field: { text: "name", value: "value" },
            multiple: true,
            select: null
        }, options);

        that.obj = itemsObjInit(that.config);
        that.list = that.obj.find(".list");
        that.data = that.config.items || [];
        that.render();
    };
    itemsObj.prototype.render = function (options) {
        var that = this;
        if (options) {
            that.config = $.extend({}, that.config, options);
        };

        if (!common.util.isEmpty(that.config.className)) {
            that.obj.removeClass(className).addClass("className");
        }

        that.obj.unbind("click")

        that.obj.on("click", ".select", that.config.select);

        that.obj.on("click", ".close", function () {
            var $item = $(this).parent("span");
            itemsObjDataSource(that, 'del', $item.data());
        });

        that.source(that.data);
    };
    itemsObj.prototype.source = function (items) {
        var that = this;
        itemsObjDataSource(that, 'init', items);
    };
    itemsObj.prototype.add = function (v) {
        var that = this;
        itemsObjDataSource(that, 'add', v);
    };
    itemsObj.prototype.remove = function (v) {
        var that = this;
        itemsObjDataSource(that, 'del', v);
    };
    itemsObj.prototype.clear = function () {

    };
    itemsObj.prototype.get = function (i) {
        var that = this;
        i = i || 0;
        if (that.data.length == 0)
            return null;
        return { text: that.data[i][that.config.field.text], value: that.data[i][that.config.field.value] };
    };
    itemsObj.prototype.set = function (text, value) {
        var that = this;
        if (!common.util.isEmpty(text) && !common.util.isEmpty(value)) {
            var item = {};
            item[that.config.field.text] = text;
            item[that.config.field.value] = value;
            itemsObjDataSource(that, 'add', item);
        }
    };

    var itemsObjInit = function (config) {
        var _obj = $("<div class='items-box'><div class='list'></div><div class='select'><a href='#'>选择</a></div></div>");
        $(config.id).html(_obj);
        return _obj;
    }
    var itemsObjDataSource = function (me, type, v) {
        var config = me.config;
        var items = [];
        if (common.util.isArray(v)) {
            items = v;
        }
        else {
            items = [v];
        }

        if (type == 'init') {
            if (!config.multiple) {
                if (items.length > 0) {
                    me.data = [common.util.last(items)];
                }
                else {
                    me.data = [];
                }
            }
            me.list.html("");
            $.map(me.data, function (item) {
                me.list.append(itemsObjHtmlCreate(me.config, item));
            });
        }
        else if (type == 'add') {
            if (!config.multiple) {
                if (items.length > 0) {
                    items = [common.util.last(items)];
                }
                else {
                    items = [];
                }
                if (items.length > 0) {
                    me.data = [];
                    me.list.html("");
                }
            }
            $.map(items, function (item) {
                if (common.util.filter(me.data, function (d) { return d[me.config.field.value] == item[me.config.field.value]; }).length == 0) {
                    me.data.push(item);
                    me.list.append(itemsObjHtmlCreate(config, item));
                }
            });
        }
        else if (type == 'del') {
            $(items).each(function (index, item) {
                var value = item[config.field.value];
                var newArr = [];
                $(me.data).each(function (i, v) {
                    if (v[config.field.value] != value) {
                        newArr.push(v);
                    }
                })
                me.data = newArr;
                me.list.find("span[data-value='" + value + "']").remove();
            });
        }

        //值去重
        //var source = common.util.uniq(items, function (item, index, arr) {
        //    if (common.util.filter(arr, function (v) { item[config.field.value] == v[config.field.value] }).length > 0) {
        //        return false;
        //    }
        //    return value;
        //})
        //console.log(me.data);
    }
    var itemsObjHtmlCreate = function (config, item) {
        item = item || {};
        var text = item[config.field.text] || "";
        var value = item[config.field.value] || "";
        var $obj = $("<span data-value='" + value + "'>" + text + "<i class=\"iconfont icon-tool3 close\"></i></span>");
        $obj.data(item);
        return $obj;
    }


    var assetsObj = function (options) {
        var that = this;
        that.guid = common.util.getId(), that.__ = window.top, that.__$ = window.top.$, that.__layer = window.top.layer, that.__WebUploader = window.top.WebUploader;
        that.obj = null;
        that.uploader = null;

        that.config = $.extend({}, {
            id: null,
            auto: true,
            maxNum: undefined,
            maxSize: undefined,
            fileSize: undefined,
            formData: undefined,
            fileData: undefined,     //TODO:暂未实现,
            confirm: null,
            assetsType: 0
        }, options);

        that.render();
    }
    assetsObj.prototype.render = function (options) {
        var that = this;
        if (options) {
            that.config = $.extend({}, that.config, options);
        };
    }
    assetsObj.prototype.open = function (options) {
        var that = this;
        if (options) {
            that.config = $.extend({}, that.config, options);
        };

        var selectId = "btn-" + that.guid + "-select";

        var viewHtml = "";
        viewHtml += "<div class=\"assets\" data-id=\"" + that.guid + "\">";
        //viewHtml += "    <div class=\"assets-navs\">";
        //viewHtml += "        <div class=\"tabs\">";
        //viewHtml += "            <span class=\"active\">上传</span>";
        //viewHtml += "            <span>管理</span>";
        //viewHtml += "        </div>";
        //viewHtml += "        <div class=\"links\">";
        //viewHtml += "            <a href=\"javascript:;\" id=\"" + selectId + "\">选择文件</a>";
        //viewHtml += "        </div>";
        //viewHtml += "    </div>";
        viewHtml += "    <div class=\"assets-main\">"
        viewHtml += "       <div class=\"assets-items\">";
        viewHtml += "       </div>";
        //viewHtml += "       <div class=\"assets-manage\">";
        //viewHtml += "       </div>";
        viewHtml += "    </div>";
        viewHtml += "    <div class=\"assets-footer\">";
        viewHtml += "       <div class='item'>";

        if (that.config.auto) {
            viewHtml += "           <a href='javascript:;' class='btn-upload' style='display:none'>上传</a>";
            viewHtml += "           <a href=\"javascript:;\" id=\"" + selectId + "\">选择文件</a>";
        } else {
            viewHtml += "           <a href='javascript:;' class='layui-btn layui-btn-sm btn-upload'>上传</a>";
            viewHtml += "           <a href=\"javascript:;\" id=\"" + selectId + "\">[选择文件]</a>";
        }

        viewHtml += "       </div>";
        if (that.config.confirm) {
            viewHtml += "       <div class='item right'>";
            viewHtml += "           <a href='javascript:;' class='layui-btn layui-btn-sm btn-confirm'>确定</a>";
            viewHtml += "       </div>";
        }
        viewHtml += "    </div>";
        viewHtml += "</div>";

        var dialog = that.__layer.open({
            type: 1,
            title: '文件上传',
            content: viewHtml,
            area: ['540px', '340px'],
        });

        that.obj = that.__$("div[data-id='" + that.guid + "']");

        var upOption = {
            swf: '/js/Uploader.swf',
            server: '/common/upload',
            pick: "#" + selectId,
            auto: false,
            resize: false,
            duplicate: true,
            threads: 3,
            chunked: true,
            chunkSize: 5242880,  //5M
            chunkRetry: 0,       //重试次数
        };

        //fileNumLimit { int } [可选][默认值：undefined]          //验证文件总数量, 超出则不允许加入队列。
        //fileSizeLimit { int } [可选][默认值：undefined]         //验证文件总大小是否超出限制, 超出则不允许加入队列。
        //fileSingleSizeLimit { int } [可选][默认值：undefined]   //验证单个文件大小是否超出限制, 超出则不允许加入队列。 

        if (that.config.maxNum) {
            upOption["fileNumLimit"] = that.config.maxNum;
        }
        if (that.config.maxSize) {
            upOption["fileSizeLimit"] = that.config.maxSize;

        }
        if (that.config.fileSize) {
            upOption["fileSingleSizeLimit"] = that.config.fileSize;

        }
        if (that.config.formData) {
            upOption["formData"] = that.config.formData;
        }


        that.uploader = that.__WebUploader.create(upOption);


        that.uploader.on('beforeFileQueued', function (file) {
            if (that.obj.find(".assets-up-file[data-name='" + file.name + "'][data-size='" + file.size + "']").length > 0) {
                return false;
            }
            return true;
        });

        that.uploader.on('fileQueued', function (file) {
            var _html = "";
            _html += "<div class='assets-up-file' data-id='" + file.id + "' data-name='" + file.name + "' data-size='" + file.size + "'>";
            _html += "  <span>" + file.name + "</span>";
            _html += "  <span class='info'><em class='status'>0%</em><em class='close'>x</em></span>";
            _html += "</div>";
            that.obj.find(".assets-items").append(_html);
            that.uploader.md5File(file).then(function (val) {
                that.__$(".assets-up-file[data-id='" + file.id + "']").attr("data-md5", val);
            });
            assetsObjSyncFiles(that);
        });

        that.uploader.on('filesQueued', function (files) {
            if (that.config.auto) {
                that.obj.find(".btn-upload").trigger("click");
            }
        });

        that.uploader.on('uploadBeforeSend', function (obj, data, headers) {
            var $item = that.obj.find(".assets-up-file[data-id='" + data['id'] + "']");
            data["md5"] = $item.attr("data-md5") || "";
            data["autoMerge"] = false;
            data["assetsType"] = that.config.assetsType;
            if (that.config.fileData) {
                //.......
            }
        })

        that.uploader.on('uploadProgress', function (file, percentage) {
            //多线程,其发送结果都100%，其中一返回结果错误
            var pro = percentage;
            if (pro == 1) {
                pro = 0.9999;
            }
            that.obj.find(".assets-up-file[data-id='" + file['id'] + "']")
                .find(".status").html(common.util.formatPercent(pro));
        });

        that.uploader.on('uploadSuccess', function (file, response) {
            var item = that.obj.find(".assets-up-file[data-id='" + file['id'] + "']");
            var md5 = item.attr("data-md5") || "";
            if (file.size > that.uploader.options.chunkSize) {
                var action = encodeURIComponent("/assets/merge?md5=" + md5 + "&name=" + file.name);
                common.request("/common/do?action=" + action, {}, function (data) {
                    item.find(".status").html("<a>成功<a>");
                    file["md5"] = md5;
                }, function (code, msg) {
                    that.uploader.cancelFile(file); //取消当前错误文件
                    item.find(".status").html("<a title='[" + code + "]" + msg + "' class='error'>错误<a>");
                })
            } else {
                item.find(".status").html("<a>成功<a>");
                file["md5"] = md5;
            }
        });

        that.uploader.on('uploadError', function (file, reason) {
            that.obj.find(".assets-up-file[data-id='" + file['id'] + "']")
                .find(".status").html("<a title='" + reason + "' class='error'>错误<a>");
        });

        that.uploader.on("uploadAccept", function (obj, ret) {
            if (ret.code != 0) {
                return false;
            }
        });

        that.uploader.on('error', function (type) {
            //Q_EXCEED_NUM_LIMIT 在设置了fileNumLimit且尝试给uploader添加的文件数量超出这个值时派送。
            //Q_EXCEED_SIZE_LIMIT 在设置了Q_EXCEED_SIZE_LIMIT且尝试给uploader添加的文件总大小超出这个值时派送。
            //Q_TYPE_DENIED 当文件类型不满足时触发。。
        });


        that.obj.on("click", "em.close", function () {
            var _id = $(this).parents(".assets-up-file").data("id");
            that.uploader.removeFile(_id, true);
            $(this).parents(".assets-up-file").remove();
            assetsObjSyncFiles(that);
        });

        that.obj.on("click", ".btn-upload", function () {
            assetsObjChekMd5(that, function () {
                //console.log('all done!', arguments);
                that.uploader.upload();
            });
        });

        that.obj.on("click", ".btn-confirm", function () {
            if (that.config.confirm && common.util.isFunction(that.config.confirm)) {
                //console.log(that.uploader.getFiles('complete'), that.uploader.getFiles());
                that.config.confirm(that.uploader.getFiles('complete'), that.uploader.getFiles(), that);
            }
            that.__layer.close(dialog);
        });

        assetsObjSyncFiles(that);
    }

    var assetsObjInit = function (config) {

    };
    var assetsObjSyncFiles = function (me) {
        var upBtn = me.obj.find(".btn-upload");
        upBtn.removeClass("layui-btn-disabled").addClass("layui-btn-disabled");
        var files = me.uploader.getFiles();
        if (files.length > 0) {
            upBtn.removeClass("layui-btn-disabled");
        }
    };
    var assetsObjChekMd5 = function (me, callback) {
        //校验所有异步的md5是否完成
        function checkMd5() {
            var dfd = me.__$.Deferred();
            var i = 0;
            var chkInterval = setInterval(function () {
                var items = common.util.filter(me.__$(".assets-up-file"), function (item) {
                    return (me.__$(item).attr("data-md5") || "") != "";
                });
                if (items.length == me.__$(".assets-up-file").length) {
                    clearInterval(chkInterval);
                    chkInterval = null;
                    dfd.resolve(i, items);
                }
                i++;
            }, 5);
            return dfd.promise();
        }
        me.__$.when(checkMd5()).done(callback);
    };


    var singleUploadObj = function (options) {
        var that = this;
        that.guid = common.util.getId(), that.__WebUploader = window.top.WebUploader;
        that.uploader = null;
        that.file = {};

        that.config = $.extend({}, {
            selectBtn: null,
            uploadBtn: null,
            auto: false,
            maxNum: undefined,
            maxSize: undefined,
            fileSize: undefined,
            formData: undefined,
            fileData: undefined,     //TODO:暂未实现,
            filesQueued: null,
            sendBefore: null,
            progress: null,
            success: null,
            error: null

        }, options);

        that.render();
    };
    singleUploadObj.prototype.render = function (options) {
        var that = this;
        rest();

        if (options) {
            that.config = $.extend({}, that.config, options);
        };

        var upOption = {
            swf: '/js/Uploader.swf',
            server: '/common/upload',
            pick: that.config.selectBtn,
            auto: false,
            resize: false,
            duplicate: true,
            threads: 3,
            chunked: true,
            chunkSize: 5242880,  //5M
            chunkRetry: 0,       //重试次数
        };

        //fileNumLimit { int } [可选][默认值：undefined]          //验证文件总数量, 超出则不允许加入队列。
        //fileSizeLimit { int } [可选][默认值：undefined]         //验证文件总大小是否超出限制, 超出则不允许加入队列。
        //fileSingleSizeLimit { int } [可选][默认值：undefined]   //验证单个文件大小是否超出限制, 超出则不允许加入队列。 

        if (that.config.maxNum) {
            upOption["fileNumLimit"] = that.config.maxNum;
        }
        if (that.config.maxSize) {
            upOption["fileSizeLimit"] = that.config.maxSize;

        }
        if (that.config.fileSize) {
            upOption["fileSingleSizeLimit"] = that.config.fileSize;

        }
        if (that.config.formData) {
            upOption["formData"] = that.config.formData;
        }

        that.uploader = that.__WebUploader.create(upOption);


        that.uploader.on('beforeFileQueued', function (file) {
            if (that.file["name"] == file.name && that.file["size"] == file.size) {
                return false;
            }
            that.file = file;
            return true;
        });

        that.uploader.on('fileQueued', function (file) {
            that.uploader.md5File(file).then(function (val) {
                that.file["md5"] = val;
            });
        });

        that.uploader.on('filesQueued', function (files) {
            if (common.util.isFunction(that.config.filesQueued)) {
                that.config.filesQueued(files[0]);
            }
            if (that.config.auto) {
                uploader();
            }
        });

        that.uploader.on('uploadBeforeSend', function (obj, data, headers) {
            if (common.util.isFunction(that.config.sendBefore)) {
                that.config.sendBefore(data);
            }
            data["md5"] = that.file["md5"] || "";
            data["autoMerge"] = false;
            if (that.config.fileData) {
                //.......
            }
        })

        that.uploader.on('uploadProgress', function (file, percentage) {
            //多线程,其发送结果都100%，其中一返回结果错误
            var pro = percentage;
            if (pro == 1) {
                pro = 0.9999;
            }
            if (common.util.isFunction(that.config.progress)) {
                that.config.progress(common.util.formatPercent(pro), percentage, file);
            }
        });

        that.uploader.on('uploadSuccess', function (file, response) {
            var data = response.code == 0 ? response.obj : {};

            var md5 = that.file["md5"] || "";
            if (file.size > that.uploader.options.chunkSize) {
                var action = encodeURIComponent("/assets/merge?md5=" + md5 + "&name=" + file.name);
                common.request("/common/do?action=" + action, {}, function (data) {
                    if (common.util.isFunction(that.config.success)) {
                        that.config.success(data);
                    }
                    rest();
                }, function (code, msg) {
                    that.uploader.cancelFile(file); //取消当前错误文件
                    if (common.util.isFunction(that.config.error)) {
                        that.config.error(code, msg, file);
                    }
                    rest();
                })
            }
            else {
                if (common.util.isFunction(that.config.success)) {
                    that.config.success(data);
                }
                rest();
            }
        });

        that.uploader.on('uploadError', function (file, reason) {
            if (common.util.isFunction(that.config.error)) {
                that.config.error(-2, reason, file);
            }
            rest();
        });

        that.uploader.on("uploadAccept", function (obj, ret) {
            if (ret.code != 0) {
                return false;
            }
        });

        that.uploader.on('error', function (type) {
            rest();
            //Q_EXCEED_NUM_LIMIT 在设置了fileNumLimit且尝试给uploader添加的文件数量超出这个值时派送。
            //Q_EXCEED_SIZE_LIMIT 在设置了Q_EXCEED_SIZE_LIMIT且尝试给uploader添加的文件总大小超出这个值时派送。
            //Q_TYPE_DENIED 当文件类型不满足时触发。。
        });

        $("body").on("click", that.config.uploadBtn, function () {
            uploader();
        });

        function uploader() {
            console.log('上传:', that.file);
            singleUploadObjChekMd5(that, function () {
                //console.log('all done!', arguments);
                that.uploader.upload();
            });
        }

        function rest() {
            that.file = {};
        }

    };

    var singleUploadObjChekMd5 = function (me, callback) {
        //校验所有异步的md5是否完成
        function checkMd5() {
            var dfd = $.Deferred();
            var i = 0;
            var chkInterval = setInterval(function () {
                if ((me.file["md5"] || "") != "") {
                    clearInterval(chkInterval);
                    chkInterval = null;
                    dfd.resolve(i, me.file);
                }
                i++;
            }, 5);
            return dfd.promise();
        }
        $.when(checkMd5()).done(callback);
    };


    var flowObj = function (options) {
        var that = this;
        that.obj = null;
        that.flowId = "";

        that.config = $.extend({}, {
            name: "",
            data: {},
            errors: 50,
            interval: 1000,
            expire: 1800000, //1000*60*30 30m
            created: null,
            intervaled: null,
            success: null,
            error: null
        }, options);

        that.render();
    };
    flowObj.prototype.render = function (options) {
        var that = this;
        if (options) {
            that.config = $.extend({}, that.config, options);
        };
    };
    flowObj.prototype.start = function (options) {
        var that = this;
        if (options) {
            that.config = $.extend({}, that.config, options);
        };
        that.flowId = "";

        var action = encodeURIComponent("/core/flow/create");
        var post = $.extend({}, { flowType: that.config.name, expire: that.config.expire, param: "" }, that.config.data);

        common.request("/common/do?action=" + action, { data: post },
            function (data) {
                if (!common.util.isEmpty(data)) {
                    that.flowId = data["id"] || "";
                }
                var isGo = true;
                if (common.util.isFunction(that.config.created)) {
                    var __ = that.config.created(data);
                    if (__ == false) {
                        isGo = false;
                    }
                }
                if (isGo) {
                    flowObjCreateProgress(that);
                }
            },
            function (code, msg) {
                if (common.util.isFunction(that.config.error)) {
                    that.config.error(code, msg);
                }
            })

    };
    var flowObjCreateProgress = function (me) {
        var that = me;
        var startTimeout = setTimeout(function () {
            clearTimeout(startTimeout);
            var progressTotal = 0;
            var progressInvert = setInterval(function () {
                if (progressTotal > that.config.errors) {
                    //返回为空或错误， 超出最多调用次数
                    clearInterval(progressInvert);
                    if (common.util.isFunction(that.config.error)) {
                        that.config.error(-1, "", progressTotal > that.config.errors);
                    }
                    return;
                }
                var action = encodeURIComponent("/core/flow/progress?flowId=" + that.flowId);
                common.request("/common/do?action=" + action, {},
                    function (data) {
                        if (common.util.isEmpty(data)) {
                            progressTotal++;
                            return;
                        }
                        if (common.util.isFunction(that.config.intervaled)) {
                            that.config.intervaled(data, 0, "");
                        }

                        if (data["isSuccess"] && data["isOver"]) {
                            clearInterval(progressInvert);
                            progressInvert = null;
                            if (common.util.isFunction(that.config.success)) {
                                that.config.success(data);
                            }
                        }
                        else {
                            progressTotal++;
                        }
                    },
                    function (code, msg) {
                        progressTotal++;
                        if (common.util.isFunction(that.config.error)) {
                            that.config.error(code, msg, progressTotal > that.config.errors);
                        }
                    });

            }, that.config.interval);
        }, 1000);
    }


    var control = {
        list: function (opt) { return new listObj(opt); },
        paging: function (opt) { return new pagingObj(opt); },
        items: function (opt) { return new itemsObj(opt); },
        assets: function (opt) { return new assetsObj(opt); },
        singleUpload: function (opt) { return new singleUploadObj(opt); },
        flow: function (opt) { return new flowObj(opt); }
    };

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