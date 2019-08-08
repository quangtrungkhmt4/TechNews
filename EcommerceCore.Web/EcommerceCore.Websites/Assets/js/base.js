$.fn.hasAttr = function (name) {
    return this.attr(name) !== undefined;
};

(function ($, window, document, undefined) {
    
    function getInternetExplorerVersion()
        // Returns the version of Internet Explorer or a -1
        // (indicating the use of another browser).
    {
        var rv = -1; // Default value assumes failure.
        var ua = navigator.userAgent;

        // If user agent string contains "MSIE x.y", assume
        // Internet Explorer and use "x.y" to determine the
        // version.

        var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
        if (re.exec(ua) != null)
            rv = parseFloat(RegExp.$1);
        return rv;

    }

    $(document).ready(function () {
        var version = getInternetExplorerVersion();
        if (version != -1 && version < 8) {
            alert("Your browser version is not supported. For better view, please upgrade IE version 8 above.");
        }
        $("body").removeClass("hide");
        
        // select all text when clicked
        $(':text').focus(function () {
            $(this).one('mouseup', function (event) {
                event.preventDefault();
            }).select();
        });
    });
})(jQuery, window, document);

//add new property for Js Common
if (!String.prototype.format) {
    String.prototype.format = function () {
        var args = arguments;
        return this.replace(/{(\d+)}/g, function (match, number) {
            return typeof args[number] != 'undefined'
              ? args[number]
              : match
            ;
        });
    };
}

if (!String.prototype.trim) {
    //code for trim
    String.prototype.trim = function () { return this.replace(/^\s+|\s+$/g, ''); };
    String.prototype.ltrim = function () { return this.replace(/^\s+/, ''); };
    String.prototype.rtrim = function () { return this.replace(/\s+$/, ''); };
    String.prototype.fulltrim = function () { return this.replace(/(?:(?:^|\n)\s+|\s+(?:$|\n))/g, '').replace(/\s+/g, ' '); };
}
if (!Date.prototype.addMinutes) {
    Date.prototype.addHours = function (h) {
        var copiedDate = new Date(this.getTime());
        copiedDate.setMinutes(copiedDate.getMinutes() + h);
        return copiedDate;
    }
    Date.prototype.subMinutes = function (h) {
        var copiedDate = new Date(this.getTime());
        copiedDate.setMinutes(copiedDate.getMinutes() - h);
        return copiedDate;
    }
    String.prototype.substringEclipse = function (maxLength) {
        return this.length > maxLength ? this.substr(this, maxLength) + '...' : this;
    }
}

//ucfirst for string function
String.prototype.capitalize = function () {
    return this.charAt(0).toUpperCase() + this.slice(1);
};

// Need comment out:
// fix display facebook widgets error

//Object.prototype.hasOwnProperty = function (property) {
//    return typeof this[property] !== 'undefined';
//};


function SubStr(input, count) {
    try {
        var flg = true;
        var arr = input.split(' ');
        var strSub = "";
        $.each(arr, function () {
            if ((strSub.length + this.length) < count) {
                strSub += this;
                strSub += " ";
            } else {
                if (flg) {
                    strSub += "...";
                    flg = false;
                }
            }
        });
    }
    catch (e) {
    }
    return strSub;
}

function ListAllGridAdditionalData() {
    return {
        keyword: $("#inputSearchGrid").val()
    };
}

var fixKendoGrid = function (obj) {
    if (obj.dataSource.data().length == 0) {

        $(obj.wrapper)
            .find(".k-grid-content").first()
            .find(".k-grid-content-empty")
            .remove();

        var msgDisplay = $("<div>").addClass("k-grid-content-empty").html(setaJs.l("Grid_NoRecordMessage"));

        $(obj.wrapper)
            .find(".k-grid-content").first()
            .append(msgDisplay);

        $(obj.wrapper)
            .find("ul.k-pager-numbers").first()
            .find("li").first()
            .children("span").html("1");

        $(obj.wrapper)
            .find(".k-grid-pager").first()
            .addClass("hide");
    } else {
        $(obj.wrapper)
            .find(".k-grid-content").first()
            .find(".k-grid-content-empty")
            .remove();
        $(obj.wrapper)
            .find(".k-grid-pager").first()
            .removeClass("hide");
    }
}

$.unserialize = function (serializedString) {
    qryString = decodeURI(serializedString);
    qryString = qryString.substring(1);
    var qryStringArr = qryString.split('&');
    var jsonobj = {}, paramvalue = '';
    for (i = 0; i < qryStringArr.length; i++) {
        paramvalue = qryStringArr[i].split('=');
        jsonobj[paramvalue[0]] = paramvalue[1];
    }
    return jsonobj;
}
/* Ext hidden field array*/
jQuery.fn.extend({
    addToHiddenArray: function (value) {
        return this.filter(":input").val(function (i, v) {
            var arr = v.split(',');
            arr.push(value);
            return arr.join(',');
        }).end();
    },
    removeFromHiddenArray: function (value) {
        return this.filter(":input").val(function (i, v) {
            return $.grep(v.split(','), function (val) {
                return val != value;
            }).join(',');
        }).end();
    },
    getHiddenArray: function () {
        return $.grep(this.val().split(','), function (val) {
            return val != null && val.length > 0;
        });
    },
    inHiddenArray: function (value) {
        var index = -1;
        $.grep(this.val().split(','), function (val, i) {
            if (val == value)
                index = i;
            return val == value;
        });
        return index;
    }
});
Array.prototype.getArrayDiff = function (a) {
    return this.filter(function (i) { return a.indexOf(i) < 0; });
};
var showGrid = function (e) {
    var obj = $(e.sender.wrapper);

    fixKendoGrid.apply(this, [e.sender]);

    obj.find(".k-grid-content").css('overflow', 'hidden');
    obj.find(".k-pager-numbers").remove();
    obj.find(".k-grid-header").addClass("k-grid-header-fixed");
    //filter by alphabeta
    if (obj.find("ul.left-filter-by-charactor").length > 0) {
        obj.find("ul.left-filter-by-charactor").removeClass('hide');
        obj.find("th.k-header > .k-header-column-menu").removeClass("k-highlight");
        obj.find("ul.left-filter-by-charactor li.title").removeClass("key-filter-active");
        var ft = e.sender.dataSource.filter();
        if (ft != null) {
            var fields = $.gridState.lookdeepFieldName(ft.filters);
            $.each(fields, function(k, item) {
                if (item.field == "KeyFilter") {
                    var keyFilter = obj.find("ul.left-filter-by-charactor li.title");
                    $.each(keyFilter, function(i, key) {
                        var $key = $(key);
                        if ($key.text().toLowerCase() == item.value) {
                            $key.addClass("key-filter-active");
                            return false;
                        }
                    });
                } else {
                    obj.find("th.k-header[data-field='" + item.field + "']").first().children(".k-header-column-menu").addClass("k-highlight");
                }
            });
        }
    } else { //mark highlight fields in filter in KeyFilter does not exist
        obj.find("th.k-header > .k-header-column-menu").removeClass("k-highlight");
        var ft = e.sender.dataSource.filter();
        if (ft != null) {
            var fields = $.gridState.lookdeepFieldName(ft.filters);
            $.each(fields, function (k, item) {
                if (item.field != "KeyFilter") {
                    obj.find("th.k-header[data-field='" + item.field + "']").first().children(".k-header-column-menu").addClass("k-highlight");
                }
            });
        }
    }
    
    if (obj.is(":visible")) {
    } else {
        obj.removeClass("hide");
        $("#loading").addClass("hide");
    }

    if (!obj.hasClass("fixed")) {
        var gridHeight = 0;
        if (obj.attr('gridMargin') > 0) {
            gridHeight = $("body").height() - $('#header').outerHeight(true) - obj.attr('gridMargin');
        } else {
            gridHeight = $("body").height() - (obj.find('.k-grid-toolbar').outerHeight(true) + obj.find('.k-grid-header').outerHeight(true) + 178);    
        }
        obj.find(".k-grid-content").height(gridHeight);
    }
    
    if (!obj.attr("keyboard")) {
        obj.attr("keyboard", true);
        obj.attr("tabindex", 0);

        obj.keydown(function (ke) {
            var kGrid, curRow, newRow;

            kGrid = $(this).closest('.k-grid').data("kendoGrid");

            curRow = kGrid.select();
            if (!curRow.length)
                return;

            var kContent = $(obj).children(".k-grid-content");
            var contentHeight = kContent.height();

            if (ke.which == 38) {
                newRow = curRow.prev();
                var row = $(curRow).closest("tr");

                $(kContent).getNiceScroll().each(function (key, val) {
                    var self = this;
                    var top = self.getScrollTop();
                    var rowTop = row.position().top;

                    if (rowTop < 0) {
                        self.setScrollTop(top + (rowTop - row.height()));
                    } else if (rowTop > contentHeight) {
                        self.setScrollTop(top + (rowTop - contentHeight));
                    } else if (rowTop <= row.height() && top > 0) {
                        self.setScrollTop(top - row.height());
                    }
                });
            } else if (ke.which == 40) {
                newRow = curRow.next();
                var row = $(curRow).closest("tr");

                $(kContent).getNiceScroll().each(function (key, val) {
                    var self = this;
                    var top = self.getScrollTop();
                    var rowTop = row.position().top;

                    if (rowTop < 0) {
                        self.setScrollTop(top + (rowTop + row.height()));
                    } else if (rowTop > contentHeight) {
                        self.setScrollTop(top + (rowTop - contentHeight + 2 * row.height()));
                    } else if (rowTop >= contentHeight - 2 * row.height()) {
                        self.setScrollTop(top + row.height());
                    }
                });
            } else {
                return;
            }

            if (!newRow.length)
                return;

            kGrid.select(newRow);
        });
    }
};

function kendoNullToString(value) {
    return (value == null) ? '' : value;
}
//format number: c2: currency, 2 precision| n2: number, 2 precision
//format datetime: yyyy/MM/dd hh:mm:ss tt
function kendoToStringFormat(value, format) {
    return value == null ? '' : kendo.toString(value, format); //using kendo format
}

function kendoSearchFocus(e) {
    var ft = e.sender.dataSource.filter();
    $(e.sender.wrapper).find("th.k-header > .k-header-column-menu").removeClass("k-highlight");
    if (ft != null) {
        $.each(ft.filters, function (k, filter) {
            $(e.sender.wrapper).find("th.k-header[data-field='" + filter.field + "']").first().children(".k-header-column-menu").addClass("k-highlight");
        });
    }
}