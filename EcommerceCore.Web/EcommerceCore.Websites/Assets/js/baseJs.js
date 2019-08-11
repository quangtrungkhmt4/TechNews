//ucfirst for string function
function centerModals($element) {
    var $modals;
    if ($element.length) {
        $modals = $element;
    } else {
        $modals = $('.modal:visible');
    }
    //$modals.each(function (i) {
    //    var $clone = $(this).clone().css('display', 'block').appendTo('body');
    //    var top = Math.round(($clone.height() - $clone.find('.modal-content').height()) / 2);
    //    top = top > 0 ? top : 0;
    //    $clone.remove();
    //    $(this).find('.modal-content').css("margin-top", top);
    //});
    $modals.each(function (i) {
        var $clone = $(this).clone().css('display', 'block').appendTo('body');
        var top = Math.round(($clone.height() - $clone.find('.modal-content').height()) / 2);
        top = top > 0 ? top : 0;
        $clone.remove();
        $(this).find('.modal-content').animate({ 'margin-top': '' + top + '' });
    });
}
$('.modal').on('show.bs.modal', function (e) {
    centerModals($(this));
});
$(window).on('resize', centerModals);
String.prototype.capitalize = function () {
    return this.charAt(0).toUpperCase() + this.slice(1);
};

var baseJs = baseJs || {};
// Common 
$.extend(true, baseJs, {
    init: function () {
        var t = this;
        $(document)
            //Modal close alert
            .on("click", "div.noty_modal", function () {
                $.noty.closeAll();
            }).on({
                // When ajaxStart is fired, add 'loading' to body class
                ajaxStart: function (event) {
                    $("#loading-ajax").removeClass("hide");
                    //hide a div after 10 seconds
                    setTimeout(function () {
                        $("#loading-ajax").addClass("hide");
                    }, 1000);
                },
                // When ajaxStop is fired, rmeove 'loading' from body class
                ajaxStop: function () {
                    $("#loading-ajax").addClass("hide");
                    //$.scroll.resize();
                },
                ajaxError: function (event, request, settings) {
                    $("#loading-ajax").addClass("hide");
                    //your error handling code here
                },
                ajaxSend: function (event, request, settings) {
                    var urls = [];
                    urls.push("/Controller/Action");
                    $.each(urls, function (k, url) {
                        if (settings.url.substr(0, url.length) == url) {
                            $("#loading-ajax").addClass("hide");
                            return false;
                        }
                    });

                },
                ajaxComplete: function (event, request, settings) {

                }
            });
    },
    Status: {
        Administrator: -10,
        Delete: -2,
        Inactive: 0,
        Active: 1,
        Open: 2,
        Pending: 3,
        Approved: 4,
        Closed: 5,
        Reject: 6,
        Cancelled: 7,
        Submited: 8,
        Draft: 9,
        Done: 10,
        Close: 11,
        Started: 12,
        Completed: 13
    },
    ActionType: {
        Add: 0,
        Edit: 1,
        Delete: 2,
        Status: 3,
        View: 4
    },
    PaymentMethod: {
        Master: 1,
        Discover: 2,
        Visa: 3,
        AMEX: 4,
        JCB: 5,
        Dinner: 6
    },
    FieldType: {
        Text: 1,
        DateTime: 2,
        Number: 3,
    },
    TypeCopy: {
        AllowConflicts: 0,
        OverwriteConflicts: 1,
        AvoidConflicts: 2
    },
    DateTimeType: {
        hhmmtt: 1,
        MMddyyyy: 2,
        MMddyyyyhhmmtt: 3,
    },
    EnumTypeUpdateStaff: {
        SaveAllFutureShifts: 0,
        SaveOnlyThisShift: 1
    },
    PayrollPeriodStatus: {
        Open: 0,
        CloseOut: 1,
        Reopen: 2,
        Finalize: 3
    },
    RoleName: {
        Super_Administrator: 1,
        Administrator: 2,
        Manager: 3,
        Employee: 4
    },
    PayTrackingStatus:
   {
       EmployeeEdit: 1,
       AdminEdit: 2
   },
    RequestTimeOffStatus: {
        Pending: 1,
        Accepted: 2,
        Denied: 3,
        Deleted: 4,
        Expired: 5

    },
    RequestTimeOffType:{
       Unpaid: 1,
       Paid: 2,
       Sick: 3,
       Holiday: 4,
       VacationNoPaid: 5,
       Vacation: 6,

    },
    RequestTimeOffTypeDay: {
        AllDay: 1,
        PartialDay: 2
    },
    checkPaymentType: function (ccnum) {
        //(Visa: length 16, prefix 4, dashes optional)
        var type = -1;
        var re = /^4\d{3}-?\d{4}-?\d{4}-?\d{4}$/;
        if (!re.test(ccnum)) {
            //(Mastercard: length 16, prefix 51-55, dashes optional)
            re = /^5[1-5]\d{2}-?\d{4}-?\d{4}-?\d{4}$/;
            if (!re.test(ccnum)) {
                //(Discover: length 16, prefix 6011, dashes optional)
                re = /^6011-?\d{4}-?\d{4}-?\d{4}$/;
                if (!re.test(ccnum)) {
                    //(American Express: length 15, prefix 34 or 37)"
                    re = /^3[4,7]\d{13}$/;
                    if (!re.test(ccnum)) {
                        //(JCB: length 16, prefix 3088,3096,3112,3158,3337,3528)
                        re = /(^(352)[8-9](\d{11}$|\d{12}$))|(^(35)[3-8](\d{12}$|\d{13}$))/;
                        if (!re.test(ccnum)) {
                            //(Diners: length 14, prefix 30, 36, or 38)
                            re = /^3[0,6,8]\d{12}$/;
                            if (!re.test(ccnum)) {
                                return -1;
                            }
                            else {
                                type = this.PaymentMethod.Dinner;
                            }
                        }
                        else {
                            type = this.PaymentMethod.JCB;
                        }
                    }
                    else {
                        type = this.PaymentMethod.AMEX;
                    }
                }
                else {
                    type = this.PaymentMethod.Discover;
                }
            }
            else {
                type = this.PaymentMethod.Master;
            }
        }
        else {
            type = this.PaymentMethod.Visa;
        }

        ccnum = ccnum.split("-").join("");
        var checksum = 0;
        for (var i = (2 - (ccnum.length % 2)) ; i <= ccnum.length; i += 2) {
            checksum += parseInt(ccnum.charAt(i - 1));
        }
        for (var i = (ccnum.length % 2) + 1; i < ccnum.length; i += 2) {
            var digit = parseInt(ccnum.charAt(i - 1)) * 2;
            if (digit < 10) { checksum += digit; } else { checksum += (digit - 9); }
        }
        if ((checksum % 10) == 0) {
            return type;
        }
        else {
            return -1;
        }
    },
    win: {
        initWindowByTemplate: function (template, viewModel) {
            // Create a placeholder element.
            var window = $(document.createElement('div'));

            // Apply template to the placeholder element, and bind the viewmodel.
            var templateHtml = $(template).html();
            window.html(kendo.template(templateHtml)(viewModel));
            kendo.bind(window, viewModel);
            // Add window placeholder to the body.
            $('body').append(window);
            // Turn placeholder into a Window widget.
            window.kendoWindow({
                width: 600,
                title: "",
                resizable: false,
                modal: true,
                draggable: true,
                close: function () {
                    // When the window is closed, remove the element from the document.
                    window.parents(".k-window").remove();
                }
            });

            // Center and show the Window.
            window.data("kendoWindow").center();
            window.data("kendoWindow").open();
        },
        initKendoWindowByHtml: function (options, beforeLoad) {
            var defaultOptions = {
                title: "",
                height: "100px",
                width: "100px",
                isHasFocus: false,
                idFocus: "",
                window: null,
            };
            var settings = $.extend(defaultOptions, options);
            var obj = settings.that;
            var win = settings.window.kendoWindow({
                minWidth: "100px",
                minHeight: "100px",
                height: settings.height,
                width: settings.width,
                draggable: true,
                modal: true,
                resizable: true,
                actions: ["Close"],
                title: settings.title,
                activate: function () {
                    //$('#').focus();
                },
                //deactivate: function () {
                //    //$(win).data("kendoWindow").close();
                //    //$(win).data("kendoWindow").destroy();
                //},
                //close: function () {
                //    $(win).fadeOut(250, function () {
                //        settings.window.parents(".k-window").remove();
                //        //$(win).data("kendoWindow").close();
                //        //$(win).data("kendoWindow").destroy();
                //    });
                //},
                refresh: beforeLoad
            });

            return win;
        },
        initKendoWindowByUrl: function (options, beforeLoad) {
            var defaultOptions = {
                title: "",
                url: "",
                data: {},
                height: "680px",
                width: "560px",
                id: "popupid",
                isHasFocus: false,
                idFocus: "",
                type:""
            };
            var settings = $.extend(defaultOptions, options);
            var obj = settings.that;
            var win = $("<div class='popup-content' id='" + settings.id + "'>").kendoWindow({
                minWidth: "180px",
                minHeight: "160px",
                height: settings.height,
                width: settings.width,
                draggable: true,
                modal: true,
                //visible:true,
                resizable: false,
                actions: ["Close"],
                title: settings.title,
                activate: function () {
                    //$('#').focus();
                },
                deactivate: function () {
                    $(win).data("kendoWindow").content("");
                    $(win).data("kendoWindow").destroy();
                },
                content: {
                    url: settings.url,
                    cache: false,
                    dataType: "html",
                    type: !(settings.type) ? "GET" : settings.type,
                    data: settings.data
                },
                resize: function () {
                    var pn = $("#" + settings.id + "").find(".popup-container").first();
                    pn.width("100%");
                    pn.height("100%");
                    // pn.getNiceScroll().resize();
                },
                close: function () {
                    //$(win).fadeOut(250, function () {
                    $(win).data("kendoWindow").destroy();
                    window.mapCanvas.tempPositon = {};
                    //});
                },
                refresh: beforeLoad
            });
            $(win).delegate(".btn-close", "click", function (e) {
                e.preventDefault();
                $(win).data("kendoWindow").destroy();
                //obj.close(win);
            });
            $(win).parent().attr("id", "popup-" + settings.id);
            return win;
        },
        initKendoWindowByUrlPost: function (options, beforeLoad) {
            var defaultOptions = {
                title: "",
                url: "",
                data: {},
                height: "680px",
                width: "560px",
                id: "popupid",
                isHasFocus: false,
                idFocus: ""
            };
            var settings = $.extend(defaultOptions, options);
            var obj = settings.that;
            var win = $("<div class='popup-content' id='" + settings.id + "'>").kendoWindow({
                minWidth: "180px",
                minHeight: "160px",
                height: settings.height,
                width: settings.width,
                draggable: true,
                modal: true,
                //visible:true,
                resizable: true,
                actions: ["Close"],
                title: settings.title,
                activate: function () {
                    //$('#').focus();
                },
                deactivate: function () {
                    $(win).data("kendoWindow").content("");
                    $(win).data("kendoWindow").destroy();
                },
                content: {
                    url: settings.url,
                    cache: false,
                    dataType: "html",
                    type: "POST",
                    data: settings.data
                },
                resize: function () {
                    var pn = $("#" + settings.id + "").find(".popup-container").first();
                    pn.width("100%");
                    pn.height("100%");
                    // pn.getNiceScroll().resize();
                },
                close: function () {
                    //$(win).fadeOut(250, function () {
                    $(win).data("kendoWindow").destroy();
                    //});
                },
                refresh: beforeLoad
            });
            $(win).delegate(".btn-close", "click", function (e) {
                e.preventDefault();
                $(win).data("kendoWindow").destroy();
                //obj.close(win);
            });
            return win;
        },
        close: function (win) {
            // Custom close event handler
            $(win).data("kendoWindow").close();
        }
    },
    validation: {
        show: function (name, msg, form) {
            var obj = this;
            baseJs.get.errorForField(name, form).html(msg[0]);
            var iEc = baseJs.get.field(name, form);
            var cbo = iEc.filter('[data-role="combobox"],[data-role="dropdownlist"]');
            if (cbo.length == 1) {
                cbo.on("change", function () {
                    cbo.attr('data-role') == 'combobox' ? cbo.parent().children('span').removeClass("input-validation-error") : cbo.prev().removeClass("input-validation-error");
                    form.find('[data-valmsg-for="' + name + '"]').html('');
                });
                // cbo.attr('data-role') == 'combobox' ? cbo.parent().children('span').addClass("input-validation-error") : cbo.prev().addClass("input-validation-error");
            } else
                iEc.addClass("input-validation-error");
        },
        hide: function (name, form) {
            var obj = this;
            baseJs.get.errorForField(name, form).html("");
            var iEc = baseJs.get.field(name, form);
            var cbo = iEc.filter('[data-role="combobox"],[data-role="dropdownlist"]');
            if (cbo.length == 1)
                cbo.attr('data-role') == 'combobox' ? cbo.parent().children('span').removeClass("input-validation-error") : cbo.prev().removeClass("input-validation-error");
            else
                iEc.removeClass("input-validation-error");
        },
        clear: function (form) {
            var obj = this;
            form.find("span.error, span.field-validation-valid").empty();
            form.find("input, textarea, selectbox, .k-dropdown-wrap.input-validation-error").removeClass("input-validation-error");
        },
        all: function (errors, form) {
            var obj = this;
            $.each(errors, function (k, v) {
                baseJs.validation.show(v.field, v.msgs, form);
            });
        },
    },
    get: {
        field: function (name, form) {
            var obj = this;
            return form.find("[name='" + name + "'], [name='" + name + "_input']").on("keypress", function () {
                $(this).removeClass("input-validation-error");
                form.find('[data-valmsg-for="' + name + '"]').html('');
            });
        },
        errorForField: function (name, form) {
            var obj = this;
            return form.find("span[data-valmsg-for='" + name + "']");
        }
    },
    checkPrototype: function () {
        for (var prop in this) {
            log(prop);
        }
    },
    loadExtTemplate: function (path) {
        //http://docs.kendoui.com/howto/load-templates-external-files
        //Use jQuery Ajax to fetch the template file
        var tmplLoader = $.get(path)
            .success(function (result) {
                //On success, Add templates to DOM (assumes file only has template definitions)
                $("body").append(result);
            })
            .error(function (result) {
                alert("Error Loading Templates -- TODO: Better Error Handling");
            });

        tmplLoader.complete(function () {
            //Publish an event that indicates when a template is done loading
            $(host).trigger("TEMPLATE_LOADED", [path]);
        });
    },
    //--------------Message alert--------------------
    alert: function (options) {
        var titleHeader = "";
        if (typeof options.titleHeader != undefined) {
            titleHeader = options.titleHeader;
        }
        var defaultOptions = {
            layout: 'center',
            theme: 'defaultTheme',
            type: 'alert',
            text: '',
            dismissQueue: true,
            template: '<div class="noty_message"><div class="noty-header">' + titleHeader + '</div><div class="noty_text"></div><div class="noty_close"></div></div>',
            animation: {
                open: { height: 'toggle' },
                close: { height: 'toggle' },
                easing: 'swing',
                speed: 300
            },
            timeout: 2000, //1s
            force: false,
            modal: true,
            maxVisible: 5, // max item display
            closeWith: ['button'], /// ['click', 'button', 'hover']
            //css: {
            //    display: 'none',
            //    width: '510px'
            //},
            callback: {
                onShow: function () {
                },
                afterShow: function () {
                    var that = this;
                    $.each(that.options.buttons, function (i, v) {
                        if (v.focus != undefined && v.focus == true) {
                            $(that.$buttons).find("button")[i].focus();
                            return false;
                        }
                    });
                },
                onClose: function () {
                },
                afterClose: function () {
                },
                onCloseClick: function () {
                }
            },
            buttons: false
        };
        /* merge options into defaultOptions, recursively */
        $.extend(true, defaultOptions, options);


        if (defaultOptions.type == 'success') {
            defaultOptions.callback.onClose.call();
            // this.log(defaultOptions.text);
        } else {
            if (defaultOptions.type == 'showsuccess') {
                defaultOptions.type = 'success';
            }

            return noty(defaultOptions);
        }
        //return noty(defaultOptions);
    },
    msgAlert: function (options) {
        var settings = $.extend({}, { type: 'alert' }, options);
        this.alert(settings);
    },
    msgSuccess: function (options) {
        var settings = $.extend({}, { type: 'success' }, options);
        this.alert(settings);
    },
    msgError: function (options, input) {
        var settings = {
            type: 'error',
            buttons: [{
                //addClass: 'btn btn-primary',
                addClass: 'btn btn-grey btn-gt btn-ok',
                text: 'OK',
                onClick: function ($noty) {
                    // this = button element
                    // $noty = $noty element
                    if (input != undefined) {
                        input.siblings("input:visible").focus();
                        // return false;
                    }
                    $noty.close();
                },
                focus: true
            }],
            modal: true,
            titleHeader: 'Error'
        };
        $.extend(true, settings, options);
        this.alert(settings);
    },
    msgWarning: function (options) {
        var settings = {
            type: 'warning',
            buttons: [{
                //addClass: 'btn btn-primary',
                addClass: 'btn btn-grey btn-gt btn-ok',
                text: 'OK',
                onClick: function ($noty) {
                    // this = button element
                    // $noty = $noty element
                    $noty.close();
                },
                focus: true
            }],
            modal: true,
            titleHeader: 'Warning'
        };
        $.extend(true, settings, options);
        this.alert(settings);
    },
    msgInfor: function (options) {
        var settings = $.extend({}, { type: 'information' }, options);
        this.alert(settings);
    },
    msgShowSuccess: function (options) {
        var settings = {
            type: 'showsuccess',
            titleHeader: 'Success'
        };
        $.extend(true, settings, options);
        this.alert(settings);
    },
    msgConfirm: function (options) {
        var settings = {
            type: 'confirm',
            buttons: [{
                //addClass: 'btn btn-primary',
                addClass: 'btn btn-grey btn-gt btn-ok',
                text: 'Yes',
                onClick: function ($noty) {
                    // this = button element
                    // $noty = $noty element
                    $noty.close();
                },
                focus: false

            }, {
                //addClass: 'btn btn-danger',
                addClass: 'btn btn-grey btn-gt btn-cancel',
                text: 'No',
                onClick: function ($noty) {
                    $noty.close();
                },
                focus: true
            }],
            modal: true,
            titleHeader: 'Confirmation'
        };
        $.extend(true, settings, options);
        this.alert(settings);
    },
    msgConfirmAllOrOnly: function (options) {
        var settings = {
            type: 'confirm',
            buttons: [{
                //addClass: 'btn btn-primary',
                addClass: 'btn btn-grey btn-gt btn-delete-shift-only',
                text: 'Option 1',
                onClick: function ($noty) {
                    // this = button element
                    // $noty = $noty element
                    $noty.close();
                },
                focus: false
            }, {
                //addClass: 'btn btn-primary',
                addClass: 'btn btn-grey btn-gt btn-ok',
                text: 'Option 2',
                onClick: function ($noty) {
                    // this = button element
                    // $noty = $noty element
                    $noty.close();
                },
                focus: false
            }, {
                //addClass: 'btn btn-danger',
                addClass: 'btn btn-grey btn-gt btn-cancel',
                text: 'No',
                onClick: function ($noty) {
                    $noty.close();
                },
                focus: true
            }],
            modal: true,
            titleHeader: 'Confirmation'
        };
        $.extend(true, settings, options);
        this.alert(settings);
    },
    msgWarningWithAbort: function (options) {
        var settings = {
            type: 'confirm',
            buttons: [{
                addClass: 'btn btn-grey btn-gt btn-cancel',
                text: 'Abort',
                onClick: function ($noty) {
                    $noty.close();
                }
            }],
            modal: true
        };
        $.extend(true, settings, options);
        this.alert(settings);
    },

    //--------------End Message alert--------------------

    //--------------Validate Date--------------------
    //validate date with format
    isDate: function (txtDate, formatDate) {
        if (formatDate == undefined) formatDate = 'mm/dd/yyyy';
        formatDate = formatDate.toLowerCase();
        var currVal = txtDate;
        if (currVal == '')
            return false;
        var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/; //Declare Regex
        var dtArray = currVal.match(rxDatePattern); // is format OK?
        if (dtArray == null)
            return false;
        //Checks for format.
        switch (formatDate) {
            case "mm/dd/yyyy":
                dtMonth = dtArray[1];
                dtDay = dtArray[3];
                dtYear = dtArray[5];
                break;
            case "dd/mm/yyyy":
                dtMonth = dtArray[3];
                dtDay = dtArray[1];
                dtYear = dtArray[5];
                break;
            case "yyyy/mm/dd":
                dtMonth = dtArray[3];
                dtDay = dtArray[5];
                dtYear = dtArray[1];
                break;
        }
        if (dtMonth < 1 || dtMonth > 12)
            return false;
        else if (dtDay < 1 || dtDay > 31)
            return false;
        else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
            return false;
        else if (dtMonth == 2) {
            var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
            if (dtDay > 29 || (dtDay == 29 && !isleap))
                return false;
        }
        return true;
    },
    ValidateDate: function (date) {
        //only format mm/DD/YYYY
        var isValidDate = false;
        try {
            var arr1 = date.split('/');
            var year = 0;
            var month = 0;
            var day = 0;
            if (arr1.length == 3) {
                year = parseInt(arr1[2], 10);
                month = parseInt(arr1[0], 10);
                day = parseInt(arr1[1], 10);

                if (isNaN(year) || isNaN(month) || isNaN(day))
                    return false;

                var isLeapYear = false;
                if (year % 4 == 0)
                    isLeapYear = true;

                if ((month == 4 || month == 6 || month == 9 || month == 11) && (day >= 0 && day <= 30))
                    isValidDate = true;
                else if ((month != 2) && (day >= 0 && day <= 31))
                    isValidDate = true;

                if (!isValidDate) {
                    if (isLeapYear) {
                        if (month == 2 && (day >= 0 && day <= 29))
                            isValidDate = true;
                    } else {
                        if (month == 2 && (day >= 0 && day <= 28))
                            isValidDate = true;
                    }
                }

                if (year <= 0 || (month <= 0 || month > 12) || day <= 0) {
                    isValidDate = false;
                }

            }
        } catch (err) {
            isValidDate = false;
        }
        return isValidDate;
    },
    //--------------End Validate Date--------------------

    //--------------Grid List--------------------
    kendoUITreeList: function (options) {
        var defaultOptions = {
            objKendoTreeList: null,
            dataSource: [],
            columns: [],
            toolbar: false
        };
        var settings = $.extend(defaultOptions, options);

        return $(settings.objKendoTreeList).kendoTreeList({
            autoBind: true,
            rowTemplate: null,
            altRowTemplate: null,
            height: null,
            detailtemplate: null,
            columnresizehandlewidth: 3,
            toolbar: settings.toolbar,
            selectable: true,
            navigatable: false,
            groupable: false,
            editable: false,
            columnMenu: true,
            scrollable: true,
            resizable: true,
            reorderable: true,
            filterable: true,
            sortable: true,
            //sortable: { allowUnsort: true, mode: "multiple" },
            pageable: true,
            //pageable: {
            //    pageSize: 1,
            //    previousNext: true,
            //    numeric: true,
            //    buttonCount: 3,
            //    input: false,
            //    pageSizes: [1, 5, 10, 25, 50],
            //    refresh: true,
            //    info: true,
            //},
            dataSource: settings.dataSource,
            columns: settings.columns,
        }).data("kendoTreeList");
    },
    kendoTreeListDataSource: function (options) {
        var defaultOptions = {};
        var settings = $.extend(defaultOptions, options);

        return new kendo.data.TreeListDataSource({
            autoSync: false,
            batch: false,
            //page: 1,
            pageSize: settings.pageSize,
            total: 0,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: true,
            serverGrouping: false,
            serverAggregates: false,
            //type: "aspnetmvc-ajax",
            transport: {
                prefix: "",
                read: {
                    url: settings.url,
                    dataType: "json"
                }
            },
            schema: {
                data: function (response) { return response; },
                total: function (response) { return response.Total; },
                errors: function (response) { return response.Errors; },
                model: {
                    id: settings.id,
                    expanded: true,
                    fields: settings.fields
                },
                parse: function (response) { return response; }
            }
        });
    },
    kendoGridDataSource: function (options) {
        var defaultOptions = {
            pageSize: 10,
            fields: [],
            id: null,
            field: null,
            dir: null,
            url: "",
            type: "POST",
            jsonParameterMap: {},
            parse: function (response) {
                return response;
            }
        };
        var settings = $.extend(defaultOptions, options);
        var obj = this;
        var dataSource = new kendo.data.DataSource({
            autoSync: false,
            batch: false,
            page: 1,
            pageSize: settings.pageSize == undefined ? 5 : settings.pageSize,
            total: 0,
            serverPaging: true,
            serverFiltering: true,
            serverSorting: true,
            serverGrouping: false,
            serverAggregates: false,
            type: "json",
            //filter: false,
            sort: {
                field: settings.field,
                dir: settings.dir
            },
            schema: {
                data: function (response) {
                    return response.Data;
                },
                total: function (response) { return response.Total; },
                errors: function (response) { return response.Errors; },
                model: {
                    fields: settings.fields,
                    // id:settings.id,
                },
                parse: settings.parse
            },
            transport: {
                prefix: "",
                read: {
                    url: settings.url,
                    type: settings.type,
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: {
                    }
                },
                parameterMap: function (data, type) {
                    if (type === "read") {
                        var listFilter = new Array();
                        window.baseJs.convertListFilterToArray(data.filter, listFilter);
                        var _sortDir = null;
                        if (data.sort != undefined && data.sort.length > 0) {
                            _sortDir = data.sort[0].dir;
                        }
                        var _sortField = null;
                        if (data.sort != undefined && data.sort.length > 0) {
                            _sortField = data.sort[0].field;
                        }
                        var dataJson = {
                            page: data.page,
                            pageSize: data.pageSize,
                            sortField: _sortField,
                            sortDir: _sortDir,
                            Filters: listFilter
                        };
                        jQuery.each(settings.jsonParameterMap, function (i, val) {
                            dataJson["" + i + ""] = val;
                        });
                        return JSON.stringify(dataJson);
                    }
                    //if (type == "read") {
                    //    var listFilter = [];
                    //    window.baseJs.filterToArray(data.filter, listFilter);
                    //    var _sortDir = null;
                    //    if (data.sort != undefined && data.sort.length > 0) {
                    //        _sortDir = data.sort[0].dir;
                    //    }
                    //    var _sortField = null;
                    //    if (data.sort != undefined && data.sort.length > 0) {
                    //        _sortField = data.sort[0].field;
                    //    }
                    //    var dataJson = {
                    //        page: data.page,
                    //        pageSize: data.pageSize,
                    //        sortField: _sortField,
                    //        sortDir: _sortDir,
                    //        Filters: listFilter,
                    //    };
                    //    jQuery.each(settings.jsonParameterMap, function (i, val) {
                    //        dataJson["" + i + ""] = val;
                    //    });
                    //    return JSON.stringify(dataJson);
                }
            },
            error: function (e) {
                window.baseJs.msgError({ text: e.errors });
            }
        });
        return dataSource;
    },
    filterToArray: function (dataFilter, list) {
        if (dataFilter != undefined) {
            var itemList = window.baseJs.filterToArrayLoop(dataFilter.filters, list);
            var filter = {
                logic: dataFilter.logic,
                filters: itemList,
            };
            list.push(filter);
        }
    },
    filterToArrayLoop: function (filter, list) {
        var itemList = Array();
        if (filter != undefined) {

            for (var j = 0; j < filter.length; j++) {
                if (filter[j].filters != undefined) {
                    window.baseJs.filterToArray(filter[j], list);
                } else {
                    var zeroFilter = {
                        field: filter[j].field,
                        operator: filter[j].operator,
                        value: filter[j].value
                    };
                    itemList.push(zeroFilter);
                }
            }
        }
        return itemList;
    },
    convertListFilterToArray: function ($dataFilter, $list) {
        if ($dataFilter != undefined) {
            var itemList = window.baseJs.convertListFilterToArrayLoop($dataFilter.filters, $list);
            var historyFilter = {
                logic: $dataFilter.logic,
                filters: itemList,
            };
            $list.push(historyFilter);
        }
    },
    convertListFilterToArrayLoop: function ($filter, $list) {
        var itemList = Array();
        if ($filter != undefined) {
            for (var j = 0; j < $filter.length; j++) {
                if ($filter[j].filters != undefined) {
                    window.baseJs.convertListFilterToArray($filter[j], $list);
                } else {
                    var zeroFilter = {
                        field: $filter[j].field,
                        operator: $filter[j].operator,
                        value: $filter[j].value
                    };
                    itemList.push(zeroFilter);
                }
            }
        }
        return itemList;
    },
    kendoUIGrid: function (options) {
        var defaultOptions = {
            pageSize: 10,
            dataSource: [],
            columns: [],
            columnMenu: false,
            objGrid: null,
            height: 550,
            filterable: false,
            selectable: true,
            toolbar: false,
            reorderable: true,
            change: null,
            width: null,
            //pageable: {
            //    pageSize: options.pageSize,
            //    previousNext: true,
            //    numeric: true,
            //    buttonCount: 3,
            //    input: true,
            //    pageSizes: [5, 10, 20, 50, 100],
            //    refresh: true,
            //    info: true
            //},
            dataBound: null,
        };
        var settings = $.extend(defaultOptions, options);
        var obj = this;
        var columnMenu;
        if (settings.columnMenu) {
            columnMenu = {
                sortable: false
            };
        } else {
            columnMenu = false;
        }
        var grid = settings.objGrid.kendoGrid({
            autoBind: false,
            rowTemplate: null,
            altRowTemplate: null,
            height: settings.height,
            detailTemplate: null,
            columnResizeHandleWidth: 3,
            toolbar: settings.toolbar,
            selectable: settings.selectable,
            navigatable: false,
            groupable: false,
            editable: false,
            columnMenu: settings.columnMenu,
            change: settings.change,
            //columnMenu: columnMenu,
            width: settings.width,
            // groupable: true,
            resizable: true,
            reorderable: settings.reorderable,
            filterable: settings.filterable,
            sortable: true,
            //scrollable: {
            //    virtual: true
            //},
            //sscrollable: false,
            dataSource: settings.dataSource,
            //pageable: true,
            // pageable: settings.pageable,
            pageable: {
                pageSize: settings.pageSize,
                previousNext: true,
                numeric: true,
                buttonCount: 3,
                input: true,
                pageSizes: [5, 10, 20, 50, 100],
                refresh: true,
                info: true
            },
            columns: settings.columns,
            dataBound: settings.dataBound,
        }).data("kendoGrid");
        // console.log(settings.width)
        return grid;
    },
    //--------------End Grid List--------------------

    //-------------- Kendo UI--------------------
    kendoUIComboBox: function (options) {
        var defaultOptions = {
            //headerTemplate: "<div class='header'><span>Results</span></div>",
            dataTextField: "Text",
            dataValueField: "Value",
            filter: "startswith",
            suggest: true,
            autoBind: true,
            placeholder: "Please select",
            minLength: 0,
            cascadeFrom: null,
            template: null,
            htmlAttributes: null,
            value: null,
            select: function (e) { },
            change: function (e) { },
            enable: true,
        };
        var settings = $.extend(defaultOptions, options);

        return $(settings.kendoComboBoxId).kendoComboBox({
            dataSource: settings.dataSource,
            headerTemplate: settings.headerTemplate,
            dataTextField: settings.dataTextField,
            dataValueField: settings.dataValueField,
            filter: settings.filter,
            suggest: settings.suggest,
            placeholder: settings.placeholder,
            autoBind: settings.autoBind,
            cascadeFrom: settings.cascadeFrom,
            template: settings.template,
            minLength: settings.minLength,
            htmlAttributes: settings.htmlAttributes,
            value: settings.value != null ? settings.value : '',
            select: settings.select,
            change: settings.change,
            enable: settings.enable,
        }).data("kendoComboBox");
    },
    kendoUIDropDownList: function (options) {
        var defaultOptions = {
            dataTextField: "Text",
            dataValueField: "Value",
            filter: "contains",
            placeholder: "Please select",
            htmlAttributes: null,
            value: null,
            select: function (e) { },
            change: function (e) { },
            enable: true,
            cascadeFrom: null,
        };
        var settings = $.extend(defaultOptions, options);

        return $(settings.kendoDropDownListId).kendoDropDownList({
            //optionLabel: "Select product...",
            dataSource: settings.dataSource,
            headerTemplate: settings.headerTemplate,
            dataTextField: settings.dataTextField,
            dataValueField: settings.dataValueField,
            filter: settings.filter,
            suggest: true,
            placeholder: settings.placeholder,
            htmlAttributes: settings.htmlAttributes,
            value: settings.value != null ? settings.value : '',
            select: settings.select,
            change: settings.change,
            enable: settings.enable,
            cascadeFrom: settings.cascadeFrom,
        }).data("kendoDropDownList");
    },
    kendoUINumericTextBox: function (options) {
        var defaultOptions = {
            format: "n0",
            //decimals:2,
            value: 0,
            min: 0,
            max: 999999999.99,
            objNumericTextBox: null,
        };
        var settings = $.extend(defaultOptions, options);
        var obj = settings.objNumericTextBox;
        return obj.kendoNumericTextBox({
            format: settings.format,
            spinners: false,
            value: settings.value,
            min: settings.min,
            max: settings.max,
            //decimals: settings.decimals,
            change: function () {
                var value = this.value();
                if (value == null) {
                    this.value(0);
                }
                else {
                    if (value < this.min() || value >= this.max()) {
                        this.value(this.oldvalue);
                    }
                }
            }
        });
    },
    kendoUIMaskedTextBox: function (options) {
        var defaultOptions = {
            mask: "000-000-0000?",
            objMaskedTextBox: null,
            value: '',
        };
        var settings = $.extend(defaultOptions, options);
        var obj = settings.objMaskedTextBox;
        return obj.kendoMaskedTextBox({
            mask: settings.mask,
            //mask: "~~~-~~~-~~~^^",
            //rules: {
            //    "~": /[0-9#]/,
            //    "^": /[0-9#]{1,2}/
            //},
            value: settings.value
        });
    },
    KendoUIDatePicker: function (options) {
        var defaultOptions = {
            format: "MM/dd/yyyy",
            parseFormats: ["MM/dd/yyyy"],
            objDatePicker: null,
            value: null,
            enable: true,
            change: function () {
                var thisDate = kendo.toString(kendo.parseDate(this.value()), settings.format);
                if (thisDate == null || thisDate == undefined || thisDate == '') {
                    var today = kendo.toString(kendo.parseDate(new Date()), settings.format);
                    this.value(kendo.toString(today, settings.format));
                }
                else if (window.baseJs.isDate(thisDate)) {
                    this.value(thisDate);
                }
            }
        };
        var settings = $.extend(defaultOptions, options);
        var obj = settings.objDatePicker;
        return obj.kendoDatePicker({
            format: settings.format,
            parseFormats: settings.parseFormats,
            value: settings.value,
            change: settings.change,
            enable: settings.enable,
        });
    },
    kendoDataSource: function (options) {
        var defaultOptions = {
            url: "",
            data: {},
            parameterMap: {},
            serverFiltering: true
        };
        var settings = $.extend(defaultOptions, options);

        return new kendo.data.DataSource({
            type: "json",
            transport: {
                read: {
                    async: false,
                    url: settings.url,
                    contentType: "application/json;",
                    dataType: "json",
                    data: settings.data,
                    parameterMap: settings.parameterMap

                }
            },
            serverFiltering: settings.serverFiltering,
        });
    },
    //--------------End Kendo UI--------------------

    LoadGridFullResize: function (options) {
        var defaultOptions = {
            objGrid: null,
        };
        var settings = $.extend(defaultOptions, options);
        var windowHeight = $(window).height();
        settings.objGrid.find(".k-grid-content").css('height', windowHeight - 265 + 'px');
    },
    setValueDefaultObject: function (obj) {
        if (typeof obj == "object") {
            for (var i in obj) {
                switch (typeof obj[i]) {
                    case "object":
                        if (obj[i] == null) {
                            obj[i] = "";
                        }
                        break;
                    case "string":
                        if (obj[i].length == 0) {
                            obj[i] = "";
                        }
                        break;
                    case "number":
                        if (obj[i].length == 0) {
                            obj[i] = 0;
                        }
                        break;
                }
            }
        }
        return obj;
    },
    successAndRedirect: function (val, result) {
        $("#loading-ajax").find("span").first().html("Redirecting...");
        setTimeout(function () {
            $("#loading-ajax").removeClass('hide');
        }, 10);

        switch (val) {
            case "save":
                // case "save-open":
                if (typeof result.returnUrl != 'undefined' && result.returnUrl != '') {
                    window.location.href = result.returnUrl;
                    return;
                }
                //location.reload(true);
                break;
        }
    },
    ViewMorePopover: function (element, title, content, placement, container) {
        if (placement == undefined) placement = function (tip, source) {
            var $element, above, actualHeight, actualWidth, below, boundBottom, boundLeft, boundRight, boundTop, elementAbove, elementBelow, elementLeft, elementRight, isWithinBounds, left, pos, right;
            isWithinBounds = function (elementPosition) {
                return boundTop < elementPosition.top && boundLeft < elementPosition.left && boundRight > (elementPosition.left + actualWidth) && boundBottom > (elementPosition.top + actualHeight);
            };
            $element = $(source);
            pos = $.extend({}, $element.offset(), {
                width: element.offsetWidth,
                height: element.offsetHeightsss
            });
            actualWidth = 280;
            actualHeight = 180;
            boundTop = $(document).scrollTop();
            boundLeft = $(document).scrollLeft();
            boundRight = boundLeft + $(window).width();
            boundBottom = boundTop + $(window).height();
            elementAbove = {
                top: pos.top - actualHeight,
                left: pos.left + pos.width / 2 - actualWidth / 2
            };
            elementBelow = {
                top: pos.top + pos.height,
                left: pos.left + pos.width / 2 - actualWidth / 2
            };
            elementLeft = {
                top: pos.top + pos.height / 2 - actualHeight / 2,
                left: pos.left - actualWidth
            };
            elementRight = {
                top: pos.top + pos.height / 2 - actualHeight / 2,
                left: pos.left + pos.width
            };
            above = isWithinBounds(elementAbove);
            below = isWithinBounds(elementBelow);
            left = isWithinBounds(elementLeft);
            right = isWithinBounds(elementRight);
            if (right) return 'right';
            else if (left) return 'left';
            else if (above) return 'top';
            else if (below) return 'bottom';
            else return 'left';
        };
        if (container == undefined) container = 'body';
        var popup = $(element);
        popup.popover({
            html: true,
            trigger: 'manual',
            placement: placement,
            title: title,
            // container: container,
            content: content
        }).popover('show')
            .on("click", function () {
                var _this = this;
                $(this).popover("show");
                $(".popover").on("mouseleave", function () {
                    $(_this).popover('hide');
                });
            }).on("mouseleave", function () {
                var _this = this;
                setTimeout(function () {
                    if (!$(".popover:hover").length) {
                        $(_this).popover("hide");
                    }
                }, 100);
            }).addClass('init-popover');

        if ($('html').hasClass('k-ie')) {
            $('.popover').remove();
            popup.popover("show");
            $('.popover-custom').parent('.popover-content').css({ "max-height": "140px" }).niceScroll();
            setTimeout(function () {
                popup.popover("hide");
            }, 2000);
        } else {
            setTimeout(function () {
                $('.popover-custom').parent('.popover-content').css({ "max-height": "140px" }).niceScroll();
            }, 50);
        }
    },
    CommingSoonAlert: function (options) {
        var defaultOptions = {
            msg: "Coming soon!",
            titleHeader: ""
        };
        var settings = $.extend(defaultOptions, options);
        return window.baseJs.msgShowSuccess({ text: settings.msg, titleHeader: settings.titleHeader });
    },

});

$.fn.KendoNumericTextBox = function (options) {
    var defaultOptions = {
        decimals: 0,
        format: "n2",
    };
    var settings = $.extend(defaultOptions, options);
    return this.each(function () {
        var numericTextBox = window.baseJs.kendoUINumericTextBox({
            objNumericTextBox: $(this),
            format: settings.format,
            //decimals: settings.decimals,
            value: settings.value == '' ? 0 : settings.value,
        });
        numericTextBox.on("focusin", function (e) {
            var $this = $(this);
            var me = $this.data("kendoNumericTextBox");
            me.oldvalue = $this.val();
        });
        // numericTextBox
        //.find(".k-select").remove();
    });
}
$.fn.KendoMaskedTextBox = function (options) {
    var defaultOptions = {
        value: '',
    };
    var settings = $.extend(defaultOptions, options);
    return this.each(function () {
        var maskedTextBox = window.baseJs.kendoUIMaskedTextBox({
            objMaskedTextBox: $(this),
            value: settings.value == '' ? 0 : settings.value,
        });
    });
}
$.fn.KendoDatePicker = function (options) {
    var defaultOptions = {
        format: "MM/dd/yyyy",
        parseFormats: ["MM/dd/yyyy"],
        value: '',
        enable: true,
        change: function () {

        }
    };
    var settings = $.extend(defaultOptions, options);
    return this.each(function () {
        // alert(settings.value)
        var datePicker = window.baseJs.KendoUIDatePicker({
            objDatePicker: $(this),
            format: settings.format,
            parseFormats: settings.parseFormats,
            value: settings.value === '' ? kendo.toString(kendo.parseDate(new Date()), settings.format) : settings.value,
            enable: settings.enable,
            change: settings.change,

        });
        //datePicker.trigger("change");
    });
}
//View more Popover
/* Fix kendo Comboxbox */
var kendoComboboxFix = {
    changeAllowEmpty: function (e) {
        var valid = kendoComboboxFix.change(e, this.value());
        if (!valid) {
            e.sender.value(null);
        }
    },
    dataBoundAllowEmpty: function (e) {
        var valid = kendoComboboxFix.dataBound(e, this.value());
        if (!valid) {
            e.sender.value(null);
        }
    },
    change: function (e) {
        var value;
        if (arguments.length == 1) {
            value = this.value();
        } else {
            value = arguments[1];
        }

        var valid = false;
        var dataSource = e.sender.dataSource.data();
        if (dataSource.length == 0) {
            e.sender.value(null);
            return;
        }

        if (value !== "") {
            $.each(dataSource, function (k, v) {
                if (v.Text == value || v.Value == value) {
                    e.sender.value(v.Value);
                    valid = true;
                    return false;
                }
            });
        }

        if (!valid) {
            $.each(dataSource, function (k, v) {
                if ((typeof (v.Selected) == "boolean" && v.Selected == true) || (typeof (v.Selected) == "string" && v.Selected.toLowerCase() == "true")) {
                    e.sender.value(v.Value);
                    valid = true;
                    return false;
                }
            });
        }

        if (!valid) {
            e.sender.value(dataSource[0].Value);
        }
        return valid;
    },
    dataBound: function (e) {
        var value;
        if (arguments.length == 1) {
            value = this.value();
        } else {
            value = arguments[1];
        }
        var valid = false;
        var dataSource = e.sender.dataSource.data();

        if (dataSource.length == 0) {
            e.sender.value(null);
            return;
        }

        if (value !== "") {
            $.each(dataSource, function (k, v) {
                if (v.Text == value || v.Value == value) {
                    e.sender.value(v.Value);
                    valid = true;
                    return false;
                }
            });
        }
        if (!valid) {
            $.each(dataSource, function (k, v) {
                if ((typeof (v.Selected) == "boolean" && v.Selected == true) || (typeof (v.Selected) == "string" && v.Selected.toLowerCase() == "true")) {
                    e.sender.value(v.Value);
                    valid = true;
                    return false;
                }
            });
        }

        if (!valid) {
            e.sender.value(dataSource[0].Value);
        }
        return valid;
    },

    setDefaultValue: function (e) {
        var value = this.value();
        var valid = false;

        if (e.sender.dataSource.data().length == 0) {
            e.sender.value(null);
            return;
        }

        $.each(e.sender.dataSource.data(), function (k, v) {
            if ((typeof (v.Selected) == "boolean" && v.Selected == true) || (typeof (v.Selected) == "string" && v.Selected.toLowerCase() == "true")) {
                e.sender.value(v.Value);
                valid = true;
                return false;
            }
        });

        if (!valid) {
            e.sender.value(e.sender.dataSource.data()[0].Value);
        }
    }
};
var showGridfixArrowKey = function (e) {
    var obj = $(e.sender.wrapper);

    fixKendoGrid.apply(this, [e.sender]);

    $(obj).find(".k-grid-content").niceScroll();

    if (obj.is(":visible")) {
    } else {
        obj.removeClass("hide");
        $("#loading").addClass("hide");
    }

    if (!$(obj).hasClass("fixed")) {
        $(obj).find(".k-grid-content").height($("body").height() - 250);
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
//Init baseJs
window.baseJs.init();
