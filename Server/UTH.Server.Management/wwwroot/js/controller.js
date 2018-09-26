; !(function (win) {

    " use strict ";

    var layer = layui.layer, form = layui.form;

    var root = win;

    var control = function (obj) {
        if (obj instanceof control) return obj;
        if (!(this instanceof control)) return new control(obj);
        //this._wrapped = obj;
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

    /*表格选择框*/
    function _listCheckEvent() {

        form.on('checkbox(chk-list-all)', function (data) {
            var tableId = $(data.elem).parents('table').attr("id");
            var $items = $(data.elem).parents('table').find("[lay-filter='chk-list-item']");
            $items.each(function (index, item) {
                item.checked = data.elem.checked;
            });
            listCheckRender(tableId);
        });

        form.on('checkbox(chk-list-item)', function (data) {
            var tableId = $(data.elem).parents('table').attr("id");
            var itemCount = $(data.elem).parents('table').find("[lay-filter='chk-list-item']").length;
            var chkCount = $(data.elem).parents('table').find("[lay-filter='chk-list-item']:checked").length;

            if (itemCount == chkCount) {
                $(data.elem).parents('table').find("[lay-filter='chk-list-all']").prop("checked", true);
            } else {
                $(data.elem).parents('table').find("[lay-filter='chk-list-all']").prop("checked", false);
            }
            listCheckRender(tableId);
        });

        $(".close[lay-filter=\"dxm-actions-close\"]").on("click", function (data) {
            var tableId = $(this).parents(".dxm-actions").attr("check-for");
            $("#" + tableId).find("[lay-filter='chk-list-all']").prop("checked", false);
            $("#" + tableId).find("[lay-filter='chk-list-item']").prop("checked", false);
            listCheckRender(tableId);
        });

        function listCheckRender(tableId) {
            var optionLay = $(".dxm-option[check-for=\"" + tableId + "\"]");
            var actionLay = $(".dxm-actions[check-for=\"" + tableId + "\"]");
            if (optionLay.length == 1 && actionLay.length == 1) {
                var chkCount = $("#" + tableId).find("[lay-filter='chk-list-item']:checked").length;
                if (chkCount > 0) {
                    optionLay.hide();
                    actionLay.show();
                } else {
                    optionLay.show();
                    actionLay.hide();
                }
                actionLay.find(".statices > em").html(chkCount);
            }
            form.render('checkbox');
        }
    }

    /*页面初始*/
    ; !(function () {
        _listCheckEvent();
    })();

})(window);