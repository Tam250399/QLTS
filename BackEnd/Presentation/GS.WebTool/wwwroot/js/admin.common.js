
function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}
function setLocation(url) {
    window.location.href = url;
}

function OpenWindow(query, w, h, scroll) {
    var l = (screen.width - w) / 2;
    var t = (screen.height - h) / 2;

    winprops = 'resizable=1, height=' + h + ',width=' + w + ',top=' + t + ',left=' + l + 'w';
    if (scroll) winprops += ',scrollbars=1';
    var f = window.open(query, "_blank", winprops);
}

function showThrobber(message) {
    $('.throbber-header').html(message);
    window.setTimeout(function () {
        $(".throbber").show();
    }, 100);
}
function hideThrobber() {
    $('.throbber-header').html("");
    $(".throbber").hide();
}
$(document).ready(function () {
    $('.multi-store-override-option').each(function (k, v) {
        checkOverriddenStoreValue(v, $(v).attr('data-for-input-selector'));
    });
});

function checkAllOverriddenStoreValue(item) {
    $('.multi-store-override-option').each(function (k, v) {
        $(v).attr('checked', item.checked);
        checkOverriddenStoreValue(v, $(v).attr('data-for-input-selector'));
    });
}

function checkOverriddenStoreValue(obj, selector) {
    var elementsArray = selector.split(",");
    if (!$(obj).is(':checked')) {
        $(selector).attr('disabled', true);
        //Kendo UI elements are enabled/disabled some other way
        $.each(elementsArray, function (key, value) {
            var kenoduiElement = $(value).data("kendoNumericTextBox") || $(value).data("kendoMultiSelect");
            if (kenoduiElement !== undefined && kenoduiElement !== null) {
                kenoduiElement.enable(false);
            }
        });
    }
    else {
        $(selector).removeAttr('disabled');
        //Kendo UI elements are enabled/disabled some other way
        $.each(elementsArray, function (key, value) {
            var kenoduiElement = $(value).data("kendoNumericTextBox") || $(value).data("kendoMultiSelect");
            if (kenoduiElement !== undefined && kenoduiElement !== null) {
                kenoduiElement.enable();
            }
        });
    };
}

function bindBootstrapTabSelectEvent(tabsId, inputId) {
    $('#' + tabsId + ' > ul li a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        var tabName = $(e.target).attr("data-tab-name");
        $("#" + inputId).val(tabName);
    });
}

function display_kendoui_grid_error(e) {
    if (e.errors) {
        if ((typeof e.errors) == 'string') {
            //single error
            //display the message
            alert(e.errors);
        } else {
            //array of errors
            //source: http://docs.kendoui.com/getting-started/using-kendo-with/aspnet-mvc/helpers/grid/faq#how-do-i-display-model-state-errors?
            var message = "The following errors have occurred:";
            //create a message containing all errors.
            $.each(e.errors, function (key, value) {
                if (value.errors) {
                    message += "\n";
                    message += value.errors.join("\n");
                }
            });
            //display the message
            alert(message);
        }
        //ignore empty error
    } else if (e.errorThrown) {
        alert('Error happened');
    }
}

// CSRF (XSRF) security
function addAntiForgeryToken(data) {
    //if the object is undefined, create a new one.
    if (!data) {
        data = {};
    }
    //add token
    var tokenInput = $('input[name=__RequestVerificationToken]');
    if (tokenInput.length) {
        data.__RequestVerificationToken = tokenInput.val();
    }
    return data;
};

function saveUserPreferences(url, name, value) {
    var postData = {
        name: name,
        value: value
    };
    addAntiForgeryToken(postData);
    $.ajax({
        cache: false,
        url: url,
        type: 'post',
        data: postData,
        dataType: 'json',
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to save preferences.');
        }
    });
};

function warningValidation(validationUrl, warningElementName, passedParameters) {
    addAntiForgeryToken(passedParameters);
    var element = $('[name="' + warningElementName + '"]');

    var messageElement = element.siblings('.field-validation-custom');
    if (messageElement.length == 0) {
        messageElement = $(document.createElement("span"));
        messageElement.addClass('field-validation-custom');
        element.after(messageElement);
    }

    $.ajax({
        cache: false,
        url: validationUrl,
        type: 'post',
        dataType: "json",
        data: passedParameters,
        success: function (data) {
            if (data.Result) {
                messageElement.addClass("warning");
                messageElement.html(data.Result);
            } else {
                messageElement.removeClass("warning");
                messageElement.html('');
            }
        },
        error: function () {
            messageElement.removeClass("warning");
            messageElement.html('');
        }
    });
};

function toggleNestedSetting(parentSettingName, parentFormGroupId) {
    if ($('input[name="' + parentSettingName + '"]').is(':checked')) {
        $('#' + parentFormGroupId).addClass('opened');
    } else {
        $('#' + parentFormGroupId).removeClass('opened');
    }
}

function parentSettingClick(e) {
    toggleNestedSetting(e.data.parentSettingName, e.data.parentFormGroupId);
}

function initNestedSetting(parentSettingName, parentSettingId, nestedSettingId) {
    var parentFormGroup = $('input[name="' + parentSettingName + '"]').closest('.form-group');
    var parentFormGroupId = $(parentFormGroup).attr('id');
    if (!parentFormGroupId) {
        parentFormGroupId = parentSettingId;
    }
    $(parentFormGroup).addClass('parent-setting').attr('id', parentFormGroupId);
    if ($('#' + nestedSettingId + ' .form-group').length == $('#' + nestedSettingId + ' .form-group.advanced-setting').length) {
        $('#' + parentFormGroupId).addClass('parent-setting-advanced');
    }

    //$(document).on('click', 'input[name="' + parentSettingName + '"]', toggleNestedSetting(parentSettingName, parentFormGroupId));
    $('input[name="' + parentSettingName + '"]').click(
        { parentSettingName: parentSettingName, parentFormGroupId: parentFormGroupId }, parentSettingClick);
    toggleNestedSetting(parentSettingName, parentFormGroupId);
}

//scroll to top
(function ($) {
    $.fn.backTop = function () {
        var backBtn = this;

        var position = 1000;
        var speed = 900;

        $(document).scroll(function () {
            var pos = $(window).scrollTop();

            if (pos >= position) {
                backBtn.fadeIn(speed);
            } else {
                backBtn.fadeOut(speed);
            }
        });

        backBtn.click(function () {
            $("html, body").animate({ scrollTop: 0 }, 900);
        });
    }
}(jQuery));

// Ajax activity indicator bound to ajax start/stop document events
$(document).ajaxStart(function () {
    $('#ajaxBusy').show();
}).ajaxStop(function () {
    $('#ajaxBusy').hide();
});

//
function DeleteWorkFile(fileId) {
    var postData = { Id: fileId };
    $.ajax({
        cache: false,
        type: "POST",
        url: "/FileCongViec/DeleteWorkFile?Id=" + fileId,
        data: postData,
        complete: function (data) {

        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(thrownError);
        },
        traditional: true
    });
}
function ShowViewLoading(ctl) {
    $(ctl).html($("#partialViewLoading").html());
}
function CheckValidMessageReturn(data) {
    $("span").filter(function () {
        return $(this).attr('data-valmsg-for') != null;
    }).text('').removeClass("field-validation-error text-danger").addClass("field-validation-valid");
    if (data.Code != "00") {
        ShowErrorMessage("Dữ liệu chưa phù hợp, mời kiểm tra lại!!!");
        for (var i = 0; i < data.ObjectInfo.length; i++) {
            var previous = $('span').filter(function () {
                return $(this).data("valmsg-for") == data.ObjectInfo[i].Key
            });
            if (previous.length == 0) {
                previous = $('span').filter(function () {
                    return $(this).data("valmsg-for") == data.ObjectInfo[i].SubKey.Value
                });
            }
            previous.text(data.ObjectInfo[i].Errors[0].ErrorMessage);
            previous.removeClass("field-validation-valid").addClass("field-validation-error text-danger");
        }
        return false;
    }
    return true;
}
function ConvertToNumber(ctlId) {
    if ($(ctlId).val().length == 0)
        return 0;
    return kendo.parseFloat($(ctlId).val(), kendo.culture("vi-VN"))
}
function SetEmptyModal(ctlId) {
    $(ctlId).on('hide.bs.modal', function (event) {
        var modal = $(this);
        modal.find('.content').html("");
    });
}
function OpenModalManual(title, src, mw) {
    var modal = $('#globalModalIframe');
    ShowViewLoading("#globalModalIframeContent");
    modal.find('.modal-title').text(title);
    modal.find('.modal-dialog').css({
        //width: w + '%', //probably not needed
        //height: h + '%', //probably not needed                             
        maxWidth: mw + '%', //probably not needed
    });
    $.ajax({
        type: "GET",
        url: src,
        success: function (data) {          
            $("#globalModalIframeContent").html(data);
        },
    });
    modal.modal('show');
}
function HideModalManual() {
    $('#globalModalIframe').modal("hide");
}
/**
 * Description: Hàm call ajax tới controller tính toán giá trị ts
 * @param {any} uri 
 * @param {any} param 
 * @param {any} async 
 * @param {any} callback 
 */
function ServicePost(uri, param, async, callback) {
    $.ajax({
        url: uri,
        type: 'POST',
        dataType: 'json',
        async: async === undefined ? false : async,
        data: param,
        success: function (res) {
            callback({ Data: res, Success: true });
        },
        error: function (result) {
            alert("Lỗi hệ thống: " + result);
        },
    });
}

/**
* Description: bind thong tin khau hao hao mon loai tai san
**/
//Create by BinhDC - 19/02/2020
function getThongTinKHHM() {
    if ($("#LOAI_TAI_SAN_ID").val() > 0) {
        var getLoaiTaiSanByIdURL = "/TaiSan/GetThongTinLoaiTaiSan?loaiTaiSanId=" + $("#LOAI_TAI_SAN_ID").val() + "&loaiHinhTaiSanId=" + $("#LOAI_HINH_TAI_SAN_ID").val();
        var loaiTaiSan = ajaxGet(getLoaiTaiSanByIdURL, true);
        loaiTaiSan.done(function (result) {
            if (result.Code == "00" && result.ObjectInfo != null) {
                var loaiTS = result.ObjectInfo;
                $("#HM_TiLe").val(loaiTS.HM_TyLe);
                if (loaiTS.KH_TY_LE == null) {
                    loaiTS.KH_TY_LE = 0;
                }
                $("#KH_TiLe").val(loaiTS.KH_TyLe);
            }
        }).fail(function (jqXHR, textStatus, errorThrown) {
            // If fail
            console.log(textStatus + ': ' + errorThrown);
        });
    } else
        return null;
}

//-------------------------
/**
 * Description: Ajax Get
 * @param {any} dataURL
 * @param {any} async true/false
 */
function ajaxGet(dataURL, async) {
    return $.ajax({
        url: dataURL,
        type: "GET",
        async: async === undefined ? false : async,
    });
}

/**
 * Description: Ajax Post
 * @param {any} _data
 * @param {any} dataURL
 */
function ajaxPost(_data, dataURL, async) {
    return $.ajax({
        url: dataURL,
        type: "POST",
        async: async === undefined ? false : async,
        data: _data
    });
}

/**
 * *Description: Ajax Delete
 * @param {any} dataURL
 * @param {any} async True/False
 */
function ajaxDelete(dataURL, async) {
    return $.ajax({
        url: dataURL,
        type: "DELETE",
        async: async === undefined ? false : async,
    });
}

/**
 * Kiem tra 2 input dong thoi co gia tri
 * @param {any} inputValue1 gia tri input
 * @param {any} inputValue2 gia tri input
 */
function doubleInputHasValue(inputValue1, inputValue2) {
    if (inputValue1 != null && inputValue1.Trim().length > 0 && inputValue2 != null || inputValue2 != null && inputValue2.Trim().length > 0 && inputValue1 != null) {
        return true;
    } else {
        return false;
    }
}

/**
 * Description: Hàm check chỉ được nhập 1 input trong 2 input, disable input còn lại
 *
 */
function enableInput(e) {
    var numberOfSeats = $(e.data.input).val();
    var numerictextbox = $(e.data.disableInput).data("kendoNumericTextBox");
    if (numberOfSeats > 0 && numberOfSeats != "") {
        numerictextbox.enable(false);
        $(e.data.disableInput).data("kendoNumericTextBox").value(0);
    }
    else {
        numerictextbox.enable(true);
    }
}

/**
 * ham phuc vu tinh toan gia tri tai san
 * */
//Create - 19/02/2020
function tinhGiaTriTaiSan() {
    var url = "/TaiSan/TinhToanGiaTriTS";
    var data = addDataTinhToan();
    var tinhToanGiaTriTS = ajaxPost(data, url, true);
    tinhToanGiaTriTS.done(function (result) {
        if (result.Code == "00") {
            var data = result.ObjectInfo;
            //bind du lieu hao mon
            $("#NVYeuCauChiTietModel_HM_GIA_TRI_CON_LAI").data("kendoNumericTextBox").value(data.HM_GiaTriConLai);
            $("#NVYeuCauChiTietModel_HM_LUY_KE").data("kendoNumericTextBox").value(data.HM_LuyKe);
            //$("#NVYeuCauChiTietModel_HM_SO_NAM_CON_LAI").data("kendoNumericTextBox").value(data.HM_NamConLai);
            //bind du lieu khau hao
            $("#NVYeuCauChiTietModel_KH_LUY_KE").data("kendoNumericTextBox").value(data.KH_LuyKe);
            $("#NVYeuCauChiTietModel_KH_THANG_CON_LAI").data("kendoNumericTextBox").value(data.KH_ThangConLai);
            $("#NVYeuCauChiTietModel_KH_CON_LAI").data("kendoNumericTextBox").value(data.KH_GiaTriConLai);
            $("#NVYeuCauChiTietModel_KH_GIA_TRI_TRICH_THANG").data("kendoNumericTextBox").value(data.KH_GiaTriTrichMotThang);
        } else if (result.Code == "01") {
            console.log("Error " + result.Code + ": " + result.ObjectInfo);
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        // If fail
        console.log(textStatus + ': ' + errorThrown);
    });
}
/**
 * ham tinh toan gia tri nếu thay đổi HM luỹ kế hoặc HM giá trị còn lại 
 * 12/03/2020
 * */

/**
 * 
 * @param {any} TongNguyenGiaSelector tổng nguyên giá selector  
 * @param {any} giatriThayDoiSelector giá trị thay đổi slector(vd: #HM_luy_ke,#HM_GIA_TRI_CON_LAI)
 */
function changegiatrisautinhtoan(TongNguyenGiaSelector, giatriThayDoiSelector) {
    var TS_NguyenGia = $(TongNguyenGiaSelector).val();
    var TS_GiaTriHMThayDoi = $(giatriThayDoiSelector).val();
    var TS_GiatriHMTraVe = parseInt(TS_NguyenGia - TS_GiaTriHMThayDoi);
    return TS_GiatriHMTraVe;

}


function addDataBDTangGiamNG() {
    var _data = {};
    return _data;
}

/**
 * ham lay du lieu de tinh toan gia tri ts
 * */
//Create by BinhDC - 19/02/2020
function addDataTinhToan() {
    var data = {
        LoaiHinhTaiSanId: $("#LOAI_HINH_TAI_SAN_ID").val(),
        TS_NgayBatDauTinh: $("#NGAY_SU_DUNG").val(),
        TS_NgayKetThucTinh: $("#NGAY_NHAP").val(),
        LoaiTaiSanId: $("#LOAI_TAI_SAN_ID").val(),
        LoaiTaiSanVoHinhId: $("#LOAI_TAI_SAN_DON_VI_ID").val(),
        KH_NgayBatDau: $("#NGAY_SU_DUNG").val(),
        TS_NguyenGia: $("#TongNguyenGia").val(),
        TS_NguyenGiaTinhKH: $("#NVYeuCauChiTietModel_KH_GIA_TINH_KHAU_HAO").val(),
        KH_ThangConLai: $("#NVYeuCauChiTietModel_KH_THANG_CON_LAI").val()
    }

    return data;
}

/**
 * Description: So sanh 2 gia tri dang so
 *  number1 < number2: return false
 *  number1 >= number2: return true
 * @param {any} number1
 * @param {any} number2
 */
function compare2Number(number1, number2) {
    if (number1 < number2) {
        return false;
    }
    else if (number1 >= number2) {
        return true;
    }
}

//------ BEGIN: các hàm thực hiện gọi tới các action thực hiện biến động tài sản-------------

/**
 * Description: hàm gọi tới controller thực hiện biến động tăng nguyên giá
 * @param {any} ts_guid guid của tài sản
 * @param {any} loaiLyDo loại lý do biến động tài sản
 */
function bienDongTaiSan(ts_guid, loaiLyDo) {
    HideModalManual();
    var url = "TaiSan/BienDongTaiSan?guid=" + ts_guid + "&loaiLyDoBienDong=" + loaiLyDo;
}


/**
 * Description: Function thực hiện gọi tới controller tạo một yêu cầu biến động
 */
function createYeuCauBDNguyenGia(_data) {
    var urlCreateYeuCauBD = "/YeuCau/InsertOrUpdateBienDongTS";
    //Lấy data từ sự kiện thực hiện sự kiện có tên _data
    var createYeuCauBD = ajaxPost(_data, urlCreateYeuCauBD, false);
    createYeuCauBD.done(function (result) {
        if (CheckValidMessageReturn(result)) {
            if (result.Code == '00') {
                ShowSuccessMessage(result.Message);
                location.href = '/YeuCau/ListBienDongTaiSan?loaiLyDoId=' + _data.LOAI_BIEN_DONG_ID + '&pageIndex=' + _data.pageIndex;
            }
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        // If fail
        console.log(textStatus + ': ' + errorThrown);
    });
}
// END: các hàm thực hiện gọi tới các action thực hiện biến động tài sản----------------------------
//function Duyet tai san
function DuyetTaiSan(strTaiSanIds) {
    bootbox.confirm({
        message: "Bạn có chắc chắn duyệt không?",
        className: 'bootbox-sm',
        buttons: {
            confirm: {
                label: 'Đồng ý',
            },
            cancel: {
                label: 'Hủy',
            },
        },
        callback: function (result) {
            if (result) {
                var _data = {
                    strTaiSanIds: strTaiSanIds,
                }
                $.ajax({
                    cache: false,
                    type: "POST",
                    data: _data,
                    url: "/BienDong/DuyetTaiSan",
                    success: function (msg) {
                        if (msg.Code == "00") {
                            ShowSuccessMessage(msg.Message);
                            var grid = $('#items-grid').data('kendoGrid');
                            if(grid)
                                grid.dataSource.page(grid.dataSource.page());
                            //location.href = '/TaiSan/ListYeuCauDuyetTaiSan';
                        }
                        else {
                            ShowErrorMessage(msg.Message);
                        }
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert(thrownError);
                    },
                    traditional: true
                });
            }
        },
    });
}
/**
 * Xóa hết tất cả thông báo lỗi*/
function hideAllValmsg() {
    $("span").filter(function () {
        return $(this).attr('data-valmsg-for') != null;
    }).text('').removeClass("field-validation-error text-danger").addClass("field-validation-valid");
}
function hideValmsg(Model) {
    $("span").filter(function () {
        return $(this).attr('data-valmsg-for') != null && $(this).attr('data-valmsg-for') == Model;
    }).text('').removeClass("field-validation-error text-danger").addClass("field-validation-valid");
}
/**
 * Hiển thị 1 lỗi cho 1 compoment
 * @param {any} model tên Model của compoment
 * @param {any} errorMessage chuỗi lỗi muốn hiển thị
 */
function showInvalidMessage(model, errorMessage) {
    var previous = $('span').filter(function () {
		return $(this).data("valmsg-for") == model;
    });
    previous.text(errorMessage);
    previous.removeClass("field-validation-valid").addClass("field-validation-error text-danger");
}
/**
 * 
 * @param {any} id id của compoment
 * @param {any} Model tên Model của compoment
 * @param {any} errorMessage chuỗi lỗi muốn hiển thị
 */
function checkValidateDateFormat(id, Model, errorMessage) {
    var date = $(id).val();
    if (date) {
        var temp = date.split('/');
        var d = new Date(temp[1] + '/' + temp[0] + '/' + temp[2]);
        if (d && (d.getMonth() + 1) == temp[1] && d.getDate() == Number(temp[0]) && d.getFullYear() == Number(temp[2])) {
            hideValmsg(Model);
            return true;
        }
        else {
            showInvalidMessage(Model, errorMessage);
            return false;
        };
    }
    return true;
}
function checkValidateFromDateToDate(idFromDate, idToDate, Model, errorMessage) {
    var fromDate = $(idFromDate).val();
    var toDate = $(idToDate).val();
    if (fromDate && toDate) {
        var fTemp = fromDate.split('/');
        var fDate = new Date(fTemp[1] + '/' + fTemp[0] + '/' + fTemp[2]);
        var tTemp = toDate.split('/');
        var tDate = new Date(tTemp[1] + '/' + tTemp[0] + '/' + tTemp[2]);
        if (!checkValidateDateFormat(idFromDate,"","") || !checkValidateDateFormat(idToDate,"","")) {
            return false;
        }
        else if (fDate <= tDate) {
            hideValmsg(Model);
            return true;
        }
        else {
            showInvalidMessage(Model, errorMessage);
            return false;
        }
    }
    return true;
}


/**
 *
 * @param {any} id id của compoment
 * @param {any} Model tên Model của compoment
 * @param {number} NumberToCompare số cần so sánh
 * @param {boolean} isCompareEqual có so sánh bằng không
 * @param {any} errorMessage chuỗi lỗi muốn hiển thị
 */
function checkValidateIsNumberHigherThan(id, Model, NumberToCompare,isCompareEqual, errorMessage)
{
    var num = $(id).val();
    if (num) {
        // so sánh lớn hơn hoặc bằng
        if (isCompareEqual) {
            if (num >= NumberToCompare) {
                hideValmsg(Model);
                return true;
            }
            else {
                showInvalidMessage(Model, errorMessage);
                return false;
			}
        }
        // so sánh lớn hơn
		else {
            if (num > NumberToCompare) {
                hideValmsg(Model);
                return true;
            }
            else {
                showInvalidMessage(Model, errorMessage);
                return false;
			}
		}
    }
    return true;
}


function checkValidateDateWithDateNow(idFromDate, Model, errorMessage) {
    var fromDate = $(idFromDate).val();
    if (fromDate) {
        var fTemp = fromDate.split('/');
        var fDate = new Date(fTemp[1] + '/' + fTemp[0] + '/' + fTemp[2]);
        if (!checkValidateDateFormat(idFromDate, "", "")) {
            return false;
        }
        else if (fDate <= Date.now()) {
            hideValmsg(Model);
            return true;
        }
        else {
            showInvalidMessage(Model, errorMessage);
            return false;
        }
    }
    return true;
}