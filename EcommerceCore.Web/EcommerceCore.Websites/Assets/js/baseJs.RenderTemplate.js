if (typeof baseJs != 'undefined')
    baseJs.RenderTemplate = {
        common: {
            objectDefault: {
                position: {
                    PositionID: null,
                    IsSalaried: false,
                    PositionName: "",
                    ColorCode: "",
                    HourlyRate: ""
                },

                assignWorker: {
                    EmployeeID:null,
                    StartDate: null,
                    DueDate: null,
                    DeliveredDate: null,
                    PickupDate: null,
                    StatusID: null,
                    Title: null,
                    AssignID:0
                },

                job: {
                    ServiceRequestID: null,
                    ShiftID: null,
                    Name: "",
                    AssignID:null,
                },
                jobTemplate: {
                    TemplateJobID: null,
                    ShiftID: null,
                    Name: ""
                },

            },
            method: {
                dataPositionPost: function (selector) {
                    var positions = new Array();
                    $(selector).each(function () {
                        var currentNumber = $(this).attr('data-number');
                        var attr = $(this).attr('data-edit-id');
                        var oItem = {
                            PositionID: $("[data-edit-id='" + attr + "']").children().eq(0).val(),
                            IsSalaried: $('input[name="Positions[' + currentNumber + '].IsSalaried"]').val(),
                            PositionName: $('input[name="Positions[' + currentNumber + '].PositionName"]').val(),
                            ColorCode: $('input[name="Positions[' + currentNumber + '].ColorCode"]').val(),
                            HourlyRate: $("[data-edit-id='" + attr + "']").children().eq(4).val(),
                            DataNumber: currentNumber
                        };
                        positions.push(oItem);
                    });
                    return positions;
                },

                dataAssignWorker: function (selector) {
                    var assignWorkers = new Array();
                    $(selector).each(function () {
                        var currentNumber = $(this).attr('data-id');
                        var oItem = {
                            AssignID: $('input[name="AssignWorkers[' + currentNumber + '].AssignID"]').val() == undefined ? 0 : $('input[name="AssignWorkers[' + currentNumber + '].AssignID"]').val(),
                            EmployeeID: $('input[name="AssignWorkers[' + currentNumber + '].EmployeeID"]').val(),
                            Title: $('input[name="AssignWorkers[' + currentNumber + '].Title"]').val(),
                            StatusID: $('input[name="AssignWorkers[' + currentNumber + '].StatusID"]').val(),
                            StartDate: $('input[name="AssignWorkers[' + currentNumber + '].StartDate"]').val(),
                            DueDate: $('input[name="AssignWorkers[' + currentNumber + '].DueDate"]').val(),
                            DeliveredDate: $('input[name="AssignWorkers[' + currentNumber + '].DeliveredDate"]').val(),
                            PickupDate: $('input[name="AssignWorkers[' + currentNumber + '].PickupDate"]').val(),
                            DataNumber: currentNumber
                        };
                        assignWorkers.push(oItem);
                    });
                    return assignWorkers;
                },
                dataJobPost: function (selector) {
                    var jobs = new Array();
                    $(selector).each(function () {
                        var currentNumber = $(this).attr('data-number');
                        var attr = $(this).attr('data-edit-id');
                        var oItem = {
                            ServiceRequestID: $('input[name="Jobs[' + currentNumber + '].ServiceRequestID"]').val(),
                            AssignID: $('input[name="Jobs[' + currentNumber + '].AssignID"]').val(),
                            ShiftID: $('input[name="Jobs[' + currentNumber + '].ShiftID"]').val(),
                            Name: $('input[name="Jobs[' + currentNumber + '].Name"]').val(),
                            DataNumber: currentNumber
                        };
                        jobs.push(oItem);
                    });
                    //console.log(jobs)
                    return jobs;

                },
                dataJobTemplate: function (selector) {
                    var jobs = new Array();
                    $(selector).each(function () {
                        var currentNumber = $(this).attr('data-number');
                        var attr = $(this).attr('data-edit-id');
                        var oItem = {
                            TemplateJobID: $('input[name="ListTemplateJob[' + currentNumber + '].TemplateJobID"]').val(),
                            ShiftID: $('input[name="ListTemplateJob[' + currentNumber + '].ShiftID"]').val(),
                            Name: $('input[name="ListTemplateJob[' + currentNumber + '].Name"]').val(),
                            DataNumber: currentNumber
                        };
                        jobs.push(oItem);
                    });
                    return jobs;
                },
                handlerAddElement: function (control, maxElement, settings, selectorForm) {
                    var t = this;
                    var data = {};
                    switch (control) {
                        case "position":
                            data = window.baseJs.RenderTemplate.common.objectDefault.position;
                            break;
                        case "job":
                            data = window.baseJs.RenderTemplate.common.objectDefault.job;
                            break;
                    }
                    data.numberInc = maxElement;
                    t.renderElement(control, data, settings, selectorForm);
                },
                removeRow: function (elBtnDel, elForm) {
                    var el = $(elBtnDel).closest(".delete-row"); //find parent has class .delete-row
                    //console.log($(elBtnDel));
                    var rowData = $(elBtnDel).closest(".row-remove");//el.prev('.row-remove');
                    var elValid = "position";
                    switch (true) { // method hasClass() better method is() - Performance
                        case $(rowData).hasClass('row-position'):
                            elValid = "position";
                            break;
                    }
                    rowData.remove();
                    el.remove();
                    //$('input[name^="' + elValid + '"]').valid();
                    return;
                },
                duplicateRow: function (source, destination, selectorForm) {
                    var idSource = source.attr('data-number');
                    var idDest = destination.attr('data-number');
                    source.find('input').each(function (i) {
                        var nameDest = $(this).attr('name').replace('[' + idSource + ']', '[' + idDest + ']');
                        var attrTypeBind = $(this).attr('type-bind');
                        var elDest = 'input[name="{0}"]'.format(nameDest);
                        if (typeof (attrTypeBind) != 'undefined') {
                            switch (attrTypeBind) {
                                case "kendoDropDownList":
                                    $(elDest).data(attrTypeBind).value($(this).val());
                                    break;
                                case "kendoComboBox":
                                    $(elDest).data(attrTypeBind).value($(this).val());
                                    break;
                            }
                        } else {
                            $(elDest).val($(this).val());
                        }
                    });
                    //$.validator.unobtrusive.parseDynamicContent('#frm-create-site');
                    $.validator.unobtrusive.parseDynamicContent(selectorForm);
                },
                renderElement: function (control, data, settings, selectorForm) {
                    var t = this;
                    window.baseJs.setValueDefaultObject(data);
                    var templ = kendo.template($("#templ" + control.capitalize()).html(), { useWithBlock: false });
                    if (typeof templ == "function") {
                        var result = templ(data); //Template
                        $('#create-' + control).append(result); //Append the result
                        switch (control) {
                            case "position":
                                t.positionElement(data.numberInc, settings);
                                break;
                            case "job":
                                t.positionElement(data.numberInc, settings);
                                break;
                        }
                    }
                    //$.validator.unobtrusive.parseDynamicContent(selectorForm);
                    //$.validator.unobtrusive.parse(selectorForm);
                },
                positionElement: function (numberIncr, settings) {
                    var self = this;

                    return this;
                },
                checkElementExist: function (selector) {
                    if ($(selector).length > 0) return true;
                    else return false;
                },
            }
        }
    };