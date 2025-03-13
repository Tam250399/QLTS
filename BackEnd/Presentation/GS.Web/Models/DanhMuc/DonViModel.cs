//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Core.Domain.DanhMuc;
using GS.Web.Framework.Models;
using GS.Web.Models.HeThong;
using GS.Web.Validators.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.DanhMuc
{
    [Validator(typeof(DonViValidator))]
    public class DonViModel : BaseGSEntityModel
    {
        public DonViModel()
        {
            dllLoaiDonVi = new List<SelectListItem>();
            LstNguoiDungModel = new List<NguoiDungModel>();
            TRANG_THAI_ID = true;
            dllDiaBanTinh = new List<SelectListItem>();
            dllDiaBanHuyen = new List<SelectListItem>();
            dllDiaBanXa = new List<SelectListItem>();
           
        }
        public String MA { get; set; }
        public String TEN { get; set; }
        public bool IsMaQHNS { get; set; } = true;
        public String MA_BO { get; set; }
        public String MA_DIA_BAN { get; set; }
        public String MA_CHUONG { get; set; }
        public String MA_DVQHNS { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public String DIA_CHI { get; set; }
        public String DIEN_THOAI { get; set; }
        public String FAX { get; set; }
        public String MA_TINH { get; set; }
        public Decimal? NHOM_DON_VI_ID { get; set; }
        public Decimal? CAP_DON_VI_ID { get; set; }
        public Decimal? OLD_CAP_DON_VI_ID { get; set; }
        public String MA_HUYEN { get; set; }
        public String CQTC_MA { get; set; }
        public Decimal? CHE_DO_HACH_TOAN_ID { get; set; }
        public String SO_QUYET_DINH { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_QUYET_DINH { get; set; }
        public String SO_QUYET_DINH_GIAO_VON { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_QUYET_DINH_GIAO_VON { get; set; }
        public Decimal? PARENT_ID { get; set; }
        public Decimal? LOAI_DON_VI_ID { get; set; }
        public Decimal? LOAI_CAP_DON_VI_ID { get; set; }
        public Decimal? TREE_LEVEL { get; set; }
        public String TREE_NODE { get; set; }
        public Decimal? DIA_BAN_ID { get; set; }
        public Boolean TRANG_THAI_ID { get; set; }
        public Boolean? DON_VI_HIEN_THI { get; set; }
        public Boolean? TRANG_THAI_DONG_BO_ID { get; set; }
        public Boolean LA_DON_VI_NHAP_LIEU { get; set; }
        public Boolean? TRANG_THAI_THAY_DOI_ID { get; set; }
        public Boolean? CO_TAI_SAN { get; set; }
        public Boolean? KHONG_CHUYEN_MA { get; set; }
        public Boolean? LA_BAN_QL_DU_AN { get; set; }
        public Boolean LA_DON_VI_TU_CHU_TAI_CHINH { get; set; }
        public Boolean DA_CO_QUYET_DINH_GIAO_VON { get; set; }
        //add more
        public bool isSelected { get; set; }
        public string MA_DON_VI_CHA { get; set; }
        public String TEN_DON_VI_CHA { get; set; }
        public int SO_DON_VI_CON { get; set; }
        public SelectList dllCapDonVi { get; set; }
        public CapEnum CapEnum { get; set; }
        public string PARENT_NAME { get; set; }
        public string TEN_DIA_BAN { get; set; }
        public IList<SelectListItem> dllLoaiDonVi { get; set; }
        public enumCHE_DO_HACH_TOAN CHE_DO_HACH_TOANEnum { get; set; }
        public List<SelectListItem> dllQLTSCD { get; set; }
        public IList<NguoiDungModel> LstNguoiDungModel { get; set; }
        public int pageIndex { get; set; }
        public SelectList ddlLoaiCapDonVi { get; set; }
        public int? DB_ID { get; set; }
        public string TenLoaiDonVi { get; set; }
        public string TenCapDonVi { get; set; }
        public Decimal? TINH_ID { get; set; }
        public Decimal? HUYEN_ID { get; set; }
        public Decimal? XA_ID { get; set; }
        public Decimal CurrentDonViId { get; set; }
        public string CurrentDonViTen { get; set; }
        public IList<SelectListItem> dllDiaBanTinh { get; set; }
        public IList<SelectListItem> dllDiaBanHuyen { get; set; }
        public IList<SelectListItem> dllDiaBanXa { get; set; }
        public bool IS_XAC_NHAN_DU_LIEU { get; set; }
        public DateTime? NGAY_XAC_NHAN { get; set; }
        public bool IsCreateFromDMDC { get; set; }
        public bool IsThongBaoChuyenMa { get; set; }
        public bool check { get; set; }
        public bool IsQuyenSuaMaCha { get; set; }
    }
    public partial class DonViSearchModel : BaseSearchModel
    {
        public DonViSearchModel()
        {
            dllLoaiDonVi = new List<SelectListItem>();
            dllTinh = new List<SelectListItem>();
            dllboNganh = new List<SelectListItem>();
            DLLCapDiaPhuong = new List<SelectListItem>();
            SelectedCapDonVis = new List<int>();
            dllCapDonVi = new List<SelectListItem>();
            TreeLevel = 1;
            this.ListLoaiTaiSanId = new List<int>();
            DDLLoaiTaiSan = new List<SelectListItem>();
            DDLMucDichSuDung = new List<SelectListItem>();
            this.DDLCompareSign = new List<SelectListItem>();
        }
        public decimal nguoiDungId { get; set; }

        public string KeySearch { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_XAC_NHAN { get; set; }
        //add more
        public IList<SelectListItem> dllLoaiDonVi { get; set; }
        public CapEnum CapDonViEnum { get; set; }
        public IList<SelectListItem> dllCapDonVi { get; set; }
        public IList<int> SelectedCapDonVis { get; set; }
        public decimal? CapDonViSearch { get; set; }
        public decimal? LoaiDonViSearch { get; set; }
        public decimal? MucDichSuDungSearch { get; set; }
        public decimal? ParentID { get; set; }
        public decimal? TreeLevel { get; set; }
        public bool? IsQuangTri { get; set; }
        public bool? ischondonvi { get; set; }
        public bool? isSelectList { get; set; }
        public bool isStayInPage { get; set; }
        public int pageIndex { get; set; }
        public bool isIncludeAll { get; set; }
        public bool isOnlyNhapLieu { get; set; }

        public bool isTinh { get; set; }
        public IList<SelectListItem> dllTinh { get; set; }
        public IList<int> ListLoaiTaiSanId { get; set; }
        public decimal? LOAI_TAI_SAN_ID { get; set; }
        public IList<SelectListItem> DDLLoaiTaiSan { get; set; }
        public IList<SelectListItem> DDLMucDichSuDung { get; set; }
        public decimal? tinhId { get; set; }
        public IList<SelectListItem> dllboNganh { get; set; }
        public IList<SelectListItem> DLLCapDiaPhuong { get; set; }
        public string TenBoNganh { get; set; }
        public string MaBo { get; set; }
        public decimal? boNganhId { get; set; }
        public decimal? donViId { get; set; }
        [UIHint("InputAddon")]
        public decimal? DienTich_Value1 { get; set; }
        [UIHint("InputAddon")]
        public decimal? DienTich_Value2 { get; set; }
        public decimal? DienTich_CompareSign { get; set; }
        public List<SelectListItem> DDLCompareSign { get; set; }
        public bool xuatExcel { get; set; }
    }
    public partial class DonViListModel : BasePagedListModel<DonViModel>
    {

    }

    public partial class DonViExport
    {
        [DisplayName("MA")]
        public string MA { get; set; }
        [DisplayName("TEN")]
        public string TEN { get; set; }
        [DisplayName("DIA_CHI")]
        public string DIA_CHI { get; set; }
        [DisplayName("MA_DON_VI_CHA")]
        public string MA_CHA { get; set; }
        [DisplayName("TEN_DON_VI_CHA")]
        public string TEN_CHA { get; set; }
        [DisplayName("LOAI_HINH_DON_VI")]
        public string LOAI_HINH_DON_VI { get; set; } 
    }
}

