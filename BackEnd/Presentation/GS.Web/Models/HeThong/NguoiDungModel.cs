//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using DevExpress.Xpo;
using FluentValidation.Attributes;
using GS.Core.Domain.DB;
using GS.Core.Domain.HeThong;
using GS.Web.Framework.Models;
using GS.Web.Framework.Mvc.ModelBinding;
using GS.Web.Models.DanhMuc;
using GS.Web.Validators.HeThong;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.HeThong
{
    [Validator(typeof(NguoiDungValidator))]
    public class NguoiDungModel : BaseGSEntityModel
    {
        public NguoiDungModel()
        {
            this.GUID = Guid.NewGuid();
            SelectedVaiTro = new List<int>();
            ListVaiTroIds = new List<SelectListItem>();
            ListDonViModel = new List<DonViModel>();
            ListHoatDongModel = new List<HoatDongModel>();
            ListAllDonViModels = new List<DonViSearchModel>();
            ListNguoiDungModel = new List<NguoiDungModel>();
        }
        public Guid GUID { get; set; }
        [GSResourceDisplayName("Tên đăng nhập")]
        public String TEN_DANG_NHAP { get; set; }
        [GSResourceDisplayName("Mật khẩu")]
        public String MAT_KHAU { get; set; }
        public String MAT_KHAU_KEY { get; set; }
        [GSResourceDisplayName("Tên đầy đủ")]
        public String TEN_DAY_DU { get; set; }
        [GSResourceDisplayName("Email")]
        public String EMAIL { get; set; }
        [GSResourceDisplayName("Ngày tạo")]
        public DateTime NGAY_TAO { get; set; }
        [GSResourceDisplayName("Trạng thái")]
        public Int32 TRANG_THAI_ID { get; set; }
        [GSResourceDisplayName("Ngày truy cập")]
        public DateTime? NGAY_TRUY_CAP { get; set; }
        public String IP_TRUY_CAP { get; set; }
        [GSResourceDisplayName("Ghi chú")]
        public String GHI_CHU { get; set; }
        [GSResourceDisplayName("Quản trị")]
        public bool IS_QUAN_TRI { get; set; }
        [GSResourceDisplayName("Ứng dụng")]
        public String UNG_DUNG { get; set; }
        [GSResourceDisplayName("Số điện thoại")]
        public String MOBILE { get; set; }
        public String TMP_MAT_KHAU { get; set; }
        [GSResourceDisplayName("Trạng thái")]
        public SelectList LIST_TRANG_THAI { get; set; }
        public decimal? CURRENT_DON_VI_ID { get; set; }
        public decimal? NGUOI_TAO { get; set; }
        public IList<int> SelectedVaiTro { get; set; }
        public List<SelectListItem> ListVaiTroIds { get; set; }
        public string MA_DON_VI { get; set; }
        public string MA_VAI_TRO { get; set; }
        public NguoiDungStatusID nguoiDungStatusID
        {
            get => (NguoiDungStatusID)TRANG_THAI_ID;
            set => TRANG_THAI_ID = (int)value;
        }
        [GSResourceDisplayName("Mã cán bộ")]
        public String MA_CAN_BO { get; set; }
        public String PASSWORD_HASH { get; set; }
        /// <summary>
        /// Thiet dat nguoi dung da chon, trong viec gan vai tro
        /// </summary>
        public bool IsDaChon { get; set; }
        public bool IsEdit { get; set; }
        public bool IsCapDonVi { get; set; }
        public IList<int> lstDonVi { get; set; }
        public IList<DonViModel> ListDonViModel { get; set; }
        public IList<DonViSearchModel> ListAllDonViModels { get; set; }
        public string Ten_TRANG_THAI { get; set; }
        public IList<HoatDongModel> ListHoatDongModel { get; set; }

        public IList<int> lstNguoiDung { get; set; }
        public IList<NguoiDungModel> ListNguoiDungModel { get; set; }
    }
    public partial class NguoiDungSearchModel : BaseSearchModel
    {
        public NguoiDungSearchModel()
        {
            SelectedToaAn = new List<int>();
            ListToaAnIds = new List<SelectListItem>();
            ddlDonViBoTinh = new List<SelectListItem>();
            ddlDonViQuanHuyen = new List<SelectListItem>();
            ListNguoiDungDaChon = new List<int>();
            ListNguoiDungModel = new List<NguoiDungModel>();
            ddlTrangThai = new List<SelectListItem>();
        }
        [GSResourceDisplayName("Từ khóa")]
        public string KeySearch { get; set; }
        public decimal TrangThaiId { get; set; }
        public List<SelectListItem> ddlTrangThai { get; set; }
        public string Tendaydu { get; set; }
        public string Macanbo { get; set; }
        public decimal DonViBoTinhId { get; set; }
        public decimal DonViQuanHuyenId { get; set; }
        public List<SelectListItem> ddlDonViBoTinh { get; set; }
        public List<SelectListItem> ddlDonViQuanHuyen { get; set; }

        public IList<int> SelectedToaAn { get; set; }
        [GSResourceDisplayName("Danh sách tòa án")]
        public List<SelectListItem> ListToaAnIds { get; set; }
        public decimal idVaiTro { get; set; }
        public IList<int> ListNguoiDungDaChon { get; set; }
        public LogLevel nguoiDungStatus { get; set; }

        public decimal DonViId { get; set; }
        public IList<NguoiDungModel> ListNguoiDungModel { get; set; }
    }
    public partial class NguoiDungListModel : BasePagedListModel<NguoiDungModel>
    {

    }
    [Validator(typeof(DoiMatKhauValidator))]
    public partial class DoiMatKhauModel : BaseGSModel
    {
        public decimal IDNguoiDung { get; set; }
        public bool isResetmk { get; set; }
        [NoTrim]
        [DataType(DataType.Password)]
        [GSResourceDisplayName("Mật khẩu cũ")]
        public string MATKHAUCU { get; set; }

        [NoTrim]
        [DataType(DataType.Password)]
        [GSResourceDisplayName("Mật khẩu mới")]
        public string MATKHAUMOI { get; set; }

        [NoTrim]
        [DataType(DataType.Password)]
        [GSResourceDisplayName("Xác nhận mật khẩu")]
        public string XACNHANMK { get; set; }

        public string KETQUA { get; set; }
        public string KhoCSDLValidation { get; set; }
        public string TokenSSO { get; set; }
        public bool isQuanTri { get; set; }
        public bool isNotCurrent { get; set; }
    }
    public class NguoiDungExcelModel : BaseGSEntityModel
    {
        public NguoiDungExcelModel()
        {
        }
        public String TEN_DANG_NHAP { get; set; }
        public String MAT_KHAU { get; set; }
        public String TEN_DAY_DU { get; set; }
        public string MA_DON_VI { get; set; }
        public decimal? CURRENT_DON_VI_ID { get; set; }
        public string MA_VAI_TRO { get; set; }
        public decimal VAI_TRO_ID { get; set; }
        public string ERROR_MESSGAE { get; set; }
    }
    public partial class ResultAction
    {
        public ResultAction(bool Result, string Message)
        {
            this.Result = Result;
            this.Message = Message;
        }
        public bool Result { get; set; }
        public string Message { get; set; }
    }
    public class Kho_NguoiDung
    {
        public int? actionType { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string passwordHash { get; set; }
        public string password { get; set; }
        public string phoneNumber { get; set; }
        public bool lockoutEnabled { get; set; }
        public string fullName { get; set; }
        public bool isAdministrator { get; set; }
        public int? syncSource { get; set; }
        public int? isExploitingUser { get; set; }
        public long? id { get; set; }
        public int? unitId { get; set; }
    }
    public class InsertLogModel
    {
        public ResponseApi ResponseApi { get; set; }
        public object Data { get; set; }
        public string LoaiDuLieu { get; set; }
    }
}

