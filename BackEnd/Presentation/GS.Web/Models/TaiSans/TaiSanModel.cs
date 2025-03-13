//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Core;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Web.Framework.Models;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.NghiepVu;
using GS.Web.Validators.TaiSans;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.TaiSans
{
    [Validator(typeof(TaiSanValidator))]
    public class TaiSanModel : BaseGSEntityModel
    {
        public TaiSanModel()
        {
            LoaiTaiSanAvailable = new List<SelectListItem>();
            LyDoTangAvailable = new List<SelectListItem>();
            HinhThucMuaSamAvailable = new List<SelectListItem>();
            MucDichDuocGiaoAvailable = new List<SelectListItem>();
            BoPhanSuDungAvailable = new List<SelectListItem>();

            QuocGiaAvailable = new List<SelectListItem>();
            SelectedNguonVonIds = new List<int>();
            NguonVonAvailable = new List<SelectListItem>();
            lstNguonVonModel = new List<NguonVonModel>();
            nvYeuCauChiTietModel = new YeuCauChiTietModel();
        }


        public String MA { get; set; }
        public String TEN { get; set; }
        public Decimal? LOAI_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_TAI_SAN_DON_VI_ID { get; set; }
        public Decimal? DU_AN_ID { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public String QUYET_DINH_SO { get; set; }
        [UIHint("DateNullable")]
        public DateTime? QUYET_DINH_NGAY { get; set; }
        public Decimal? QUYET_DINH_NGUOI_ID { get; set; }
        public Decimal? NUOC_SAN_XUAT_ID { get; set; }
        public Decimal? LY_DO_BIEN_DONG_ID { get; set; }
        public Decimal? DOI_TAC_ID { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_DUYET { get; set; }
        [UIHint("InputYear")]
        public Decimal? NAM_SAN_XUAT { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_NHAP { get; set; }
        public DateTime? NGAY_CAP_NHAT { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_SU_DUNG { get; set; }
        public String GHI_CHU { get; set; }
        public Decimal? DON_VI_BO_PHAN_ID { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public Decimal? NGUOI_TAO_ID { get; set; }
        public Guid GUID { get; set; }
        public String CHUNG_TU_SO { get; set; }
        [UIHint("DateNullable")]
        public DateTime? CHUNG_TU_NGAY { get; set; }
        public decimal? NGUYEN_GIA_BAN_DAU { get; set; }
        [UIHint("InputAddon")]
        public Decimal? GIA_MUA_TIEP_NHAN { get; set; }
        public bool? IS_XAC_NHAN { get; set; }
        public DateTime? NGAY_XAC_NHAN { get; set; }
        public bool? IS_MIEN_THUE { get; set; }
        [UIHint("InputAddon")]
        public decimal? GIA_HOA_DON { get; set; }
        [UIHint("InputAddon")]
        public decimal? MIEN_THUE_SO_TIEN { get; set; }
        public String MA_QLDKTS40 { get; set; }
        public bool? IS_DUYET { get; set; }
        public String MA_DB { get; set; }//mã đồng bộ
        public decimal? PHAN_LOAI_TAI_SAN { get; set; }
        //add more 
        public bool? IsBanQuanLyDuAn { get; set; }
        public IList<SelectListItem> LoaiTaiSanAvailable { get; set; }
        public IList<SelectListItem> LyDoTangAvailable { get; set; }
        public IList<SelectListItem> DDLIsMienThue { get; set; }
        public IList<SelectListItem> HinhThucMuaSamAvailable { get; set; }
        public IList<SelectListItem> MucDichDuocGiaoAvailable { get; set; }
        public IList<SelectListItem> BoPhanSuDungAvailable { get; set; }
        public IList<SelectListItem> QuocGiaAvailable { get; set; }
        public IList<SelectListItem> NguonVonAvailable { get; set; }
        public IList<SelectListItem> DuAnAvailable { get; set; }
        public decimal? HinhThucMuaSamId { get; set; }
        public decimal? MucDichDuocGiaoId { get; set; }
        public IList<int> SelectedNguonVonIds { get; set; }
        public YeuCauChiTietModel nvYeuCauChiTietModel { get; set; }
        public TaiSanDatModel taisandatModel { get; set; }
        public TaiSanOtoModel taisanOtoModel { get; set; }
        public TaiSanNhaModel taisannhaModel { get; set; }
        public TaiSanMayMocModel taisanmaymocModel { get; set; }
        public TaiSanClnModel taisanClnModel { get; set; }
        public TaiSanVktModel taisanVktModel { get; set; }
        public TaiSanVoHinhModel taisanvohinhModel { get; set; }
        public IList<NguonVonModel> lstNguonVonModel { get; set; }

        public string strTaiSanNguonVonIds { get; set; }
        public string DonViMa { get; set; }
        public string DonViTen { get; set; }
        public string NguoiTaoTen { get; set; }
        public Decimal? LoaiLyDoBienDongId { get; set; }
        // trạng thái 
        public bool ContinueAndAddNha { get; set; }
        public SelectList Trangthailist { get; set; }
        public string tentrangthai { get; set; }
        public string TenLoaiHinhTaiSan { get; set; }
        public string TenLoaiTaiSan { get; set; }
        public string TenDonVi { get; set; }
        public string TenLoaiTaiSanCha { get; set; }
        public string TenBoPhanSuDung { get; set; }
        public SelectList LoaiHinhTaiSanAvailable { get; set; }
        public enumLOAI_HINH_TAI_SAN enumLoaiHinhTaiSan { get; set; }
        public bool cohoso { get; set; }
        public String TS_NGUYEN_GIA_MOI_NHAT { get; set; }
        public String TS_GTCL_MOI_NHAT { get; set; }
        public String DAT_DIA_CHI { get; set; }
        public String DiaChi { get; set; }
        public decimal? TongDienTich { get; set; }
        public decimal? NguyenGiaTaiSan { get; set; }
        public String strHM_GIA_TRI_CON_LAI { get; set; }
        public String strNguyenGiaVN { get; set; }
        public String strDienTichSanVN { get; set; }
        public decimal? YeuCauStatusId { get; set; }
        public decimal? YeuCauId { get; set; }
        public int SoLuongNhanBan { get; set; }
        public decimal? CountYeuCauTs { get; set; }
        public bool IsHaveChuaDuyet { get; set; }
        public bool? isTraCuu { get; set; }
        [UIHint("InputAddon")]
        public decimal? HM_SO_NAM_CON_LAI { get; set; }
        [UIHint("InputAddon")]
        public decimal? HM_TY_LE { get; set; }
        //add more
        public int pageIndex { get; set; }

        public bool IsHaveDaDuyet { get; set; }
        public string strLyDoTuChoi { get; set; }
        public Decimal DON_VI_CHE_DO_HACH_TOAN_ID { get; set; }
        public bool IdChon { get; set; }
        //khai thac
        public decimal? KHAI_THAC_ID { get; set; }
        public Decimal? DIEN_TICH_KT { get; set; }
        public Decimal? DIEN_TICH_KHAI_THAC { get; set; }
        public decimal? idKhaiThac { get; set; }
        [UIHint("InputAddon")]
        public DateTime? ngaykhaithac_tu { get; set; }
        [UIHint("InputAddon")]
        public DateTime? ngaykhaithac_den { get; set; }
        public string hopDongSo { get; set; }
        [UIHint("InputAddon")]
        public DateTime? hopDongNgay { get; set; }
        public decimal? doitacid { get; set; }
        public string doitacten { get; set; }
        public decimal? phuongthucchothueid { get; set; }
        public string phuongthucchothueten { get; set; }
        public decimal? dongiachothue { get; set; }
        //Thêm Phương Thức mua sắm
        public decimal? PHUONG_THUC_MUA_SAM_ID { get; set; }
        public decimal? DON_VI_MUA_SAM_TAP_TRUNG_ID { get; set; }
        public string TenDonViMuaSamTapTrung { get; set; }
        public string TenPhuongThucMuaSam { get; set; }
        public IList<SelectListItem> DDLPhuongThucMuaSam { get; set; }
        // end khai thac
        [UIHint("InputAddon")]
        public Decimal? TONG_HOA_HONG_CHIET_KHAU { get; set; }
        [UIHint("InputAddon")]
        public Decimal? HOA_HONG_NOP_NSNN { get; set; }
        [UIHint("InputAddon")]
        public Decimal? HOA_HONG_DE_LAI_DON_VI { get; set; }
        public bool? isTangMoi { get; set; }
        public bool? isCreateTSDA { get; set; }
        public LoaiTaiSanModel LoaiTaiSanModel { get; set; }
        public LoaiTaiSanKhauHao LoaiTaiSanKhauHao { get; set; }
        public decimal CHE_DO_HACH_TOAN_ID { get; set; }
        public bool TRANG_THAI_KH { get; set; }
        public decimal? BoPhanChaId { get; set; }
        public TaiSanKhauHaoListModel TaiSanKhauHaoListModel { get; set; }
        // check là tsql như tscđ có nguyên giá >= 5tr <= 10tr
        public bool isTSQL { get; set; }
        public string TenLoaiDonVi { get; set; }
        // validate Thêm
        public string HIEN_TRANG_SU_DUNG_ALL { get; set; }
        //Cho phép duyệt thay đổi thông tin tài sản dkts
        public bool IsDisableTSDKTS { get; set; }
        public string TenDonViDieuChuyen { get; set; }
        public string TenNguonTaiSan { get; set; }
        public bool? Checktinhkhauhao { get; set; }
        public bool? IsTinhHaoMon { get; set; }
        public int? SO_LUONG_HAO_MON_TAI_SAN { get; set; }
    }

    public partial class TaiSanSearchModel : BaseSearchModel
    {
        public TaiSanSearchModel()
        {
            BoPhanSuDungAvailable = new List<SelectListItem>();
            ListTaiSanDaChon = new List<int>();
            LoaiTaiSanAvailable = new List<SelectListItem>();
            NguoiDungAvailable = new List<SelectListItem>();
            NguoiDungNhanAvailable = new List<SelectListItem>();
            ddlNguonTaiSan = new List<SelectListItem>();
            ddlDonViAvaiale = new List<SelectListItem>();
            SelectedDonViIds = new List<int>();
            ddlLoaiHinhTaiSanDatNha = new List<SelectListItem>();
            ddlLoaiDonViAvaiale = new List<SelectListItem>();
            ddlLoaiDonVi = new List<SelectListItem>();
            ddlCapDonVi = new List<SelectListItem>();
            this.DDLCompareSign = new List<SelectListItem>();
        }
        public string KeySearch { get; set; }
        public decimal TRANG_THAI_ID { get; set; }
        public bool? isHaoMon { get; set; }
        public enumHanhDongListDuyetTaiSan enumHanhDongListDuyetTaiSan { get; set; }
        public SelectList Trangthailist { get; set; }
        public enumTRANG_THAI_TAI_SAN enumtrangthaitaisan { get; set; }
        public decimal LOAI_HINH_TAI_SAN_ID { get; set; }
        public decimal LOAI_TAI_SAN_ID { get; set; }
        public SelectList LoaiHinhTaiSanAvailable { get; set; }
        public IList<int> LoaiHinhTaiSanSelected { get; set; }
        public IList<SelectListItem> BoPhanSuDungAvailable { get; set; }
        public IList<SelectListItem> LoaiTaiSanAvailable { get; set; }
        public IList<SelectListItem> NguoiDungAvailable { get; set; }
        public IList<SelectListItem> NguoiDungNhanAvailable { get; set; }
        public List<SelectListItem> ddlNguonTaiSan { get; set; }
        public List<SelectListItem> ddlDonViAvaiale { get; set; }
        public List<SelectListItem> ddlLoaiHinhTaiSanDatNha{ get; set; }
        public List<SelectListItem> ddlLoaiHinhTaiSanOto{ get; set; }
        public IList<SelectListItem> ddlLoaiDonVi { get; set; }
        public IList<SelectListItem> ddlCapDonVi { get; set; }
        [UIHint("InputAddon")]
        public decimal? DienTich_Value1 { get; set; }
        [UIHint("InputAddon")]
        public decimal? DienTich_Value2 { get; set; }
        public decimal? DienTich_CompareSign { get; set; }
        public List<SelectListItem> DDLCompareSign { get; set; }
        public IList<int> SelectedCapDonVis { get; set; }
        public decimal? CapDonViSearch { get; set; }
        public decimal? LoaiDonViSearch { get; set; }
        public decimal? MucDichSuDungSearch { get; set; }
        public IList<SelectListItem> DDLLoaiTaiSan { get; set; }
        public IList<SelectListItem> DDLMucDichSuDung { get; set; }
        public string StrDonViId { get; set; }
        public IList<int> SelectedDonViIds { get; set; }
        public List<SelectListItem> ddlLoaiDonViAvaiale { get; set; }
        public int? LoaiDonViId { get; set; }
        public int? NguonTaiSanId { get; set; }
        public int? NguonTaiSanIdTo { get; set; }
        public bool isCheckDonVi { get; set; }
        public Decimal? DON_VI_BO_PHAN_ID { get; set; }
        [UIHint("DateNullable")] 
        public DateTime? Fromdate { get; set; }
        [UIHint("DateNullable")]
        public DateTime? Todate { get; set; }
        public decimal Loai_Ly_Do_ID { get; set; }
        public bool loaihinhtaisancheck { get; set; }
        public decimal? donviId { get; set; }
        public bool isToanQuoc { get; set; }
        public bool istren500 { get; set; }
        public bool isduoi500 { get; set; }
        public string strLoaiHinhTSIds { get; set; }
        public string TenDonVi { get; set; }
        public string TenNguoiTao { get; set; }
        public bool isDuyet { get; set; }
        public int? pageIndex { get; set; }
        public decimal NguoiTaoId { get; set; }
        public decimal NguoiNhanId { get; set; }
        public bool IsAutoDuyetTaiSanDuoi500 { get; set; }
        public bool IsDeleteDVCon { get; set; }

        //search khai thac tai san
        public decimal? idKhaiThac { get; set; }
        public bool? isNhapLieu { get; set; }
        public IList<int> ListTaiSanDaChon { get; set; }
        public decimal? donvikhaithacid { get; set; }
        public string maTaiSanchon { get; set; }
        public decimal? DIEN_TICH_KHAI_THAC { get; set; }
        public string tenTaiSan { get; set; }
        public string maTaiSan { get; set; }
        public string MA { get; set; }
        public decimal? KHAI_THAC_ID { get; set; }
        public decimal? DU_AN_ID { get; set; }
        public string strTaiSanIds { get; set; }
        public bool? IsDanhSachTaiSanDuAn { get; set; }

        public DateTime? ngayTu { get; set; }
        public DateTime? ngayDen { get; set; }
        public string MA_CAU_HINH_CANH_BAO_TS { get; set; }
        public decimal DE_NGHI_XU_LY_ID { get; set; }
        public bool IsChonTaiSan { get; set; }
    }
    public partial class TaiSanListModel : BasePagedListModel<TaiSanModel>
    {

    }

    public partial class TinhGiaTriTSModel
    {
        //
        public Decimal? DON_VI_CHE_DO_HACH_TOAN_ID { get; set; }

        public Decimal? LoaiTaiSanId { get; set; }
        public Decimal? LoaiTaiSanDonViId { get; set; }
        public Decimal? LoaiHinhTaiSanId { get; set; }
        public DateTime? TS_NgayKetThucTinh { get; set; }   //ngày nhập
        public DateTime? TS_NgayBatDauTinh { get; set; }    //ngày đưa vào sử dụng
        public DateTime? KH_NgayBatDau { get; set; } //ngày bắt đầu tính kh 
        public Decimal? TS_NguyenGia { get; set; }
        public Decimal? TS_GiaTriHienTai { get; set; }
        public Decimal? TS_TyLeNguyenGiaTrichKH { get; set; }
        public Decimal? TS_TyLeNguyenGiaTinhHM { get; set; }
        public Decimal? TS_NguyenGiaTinhKH { get; set; }
        public Decimal? TS_NguyenGiaTinhHM { get; set; } //nguyên giá trừ nguyên giá tình hao mòn
                                                         // bien dong
        public Decimal? TaiSanId { get; set; }
        public DateTime? TS_NgayBienDong { get; set; }
        public Decimal? TS_NguyenGiaTang { get; set; }
        public Decimal? TS_GTCLTruocBD { get; set; }//dùng các biến động tăng, giảm, điều chuyển
        public Decimal? TS_GTCLSauBD { get; set; } //dùng các biến động tăng, giảm, điều chuyển
        public Decimal? LoaiLyDoBienDong { get; set; }
        //Hao mon
        public Decimal? HM_GiaTriTinh { get; set; }
        public Decimal? HM_GiaTriConLai { get; set; }
        public Decimal? HM_NamConLai { get; set; }
        public Decimal? HM_NamSuDung { get; set; }
        public Decimal? HM_NamTheoQD { get; set; }
        public Decimal? HM_TyLe { get; set; }
        public Decimal? HM_LuyKe { get; set; }
        public Decimal? HM_GiaTriHaoMonMotNam { get; set; }

        //Khau Hao
        public Decimal? KH_GiaTriTinh { get; set; }
        public Decimal? KH_GiaTriConLai { get; set; }
        public Decimal? KH_ThangConLai { get; set; }
        public Decimal? KH_ThangSuDung { get; set; }
        public Decimal? KH_ThangTheoQD { get; set; }
        public Decimal? KH_TyLe { get; set; }
        public Decimal? KH_LuyKe { get; set; }
        public Decimal? KH_GiaTriTrichMotThang { get; set; }

        //Tổng giá trị còn lại
        public Decimal? HMKM_GiaTriConLai { get; set; }
    }
    public class BD_GTCL
    {
        public Decimal? TS_GTCLTruocBD { get; set; }//dùng các biến động tăng, giảm, điều chuyển
        public Decimal? TS_GTCLSauBD { get; set; } //dùng các biến động tăng, giảm, điều chuyển

    }
    public partial class DuyetDangKyTaiSanSearchModel : BaseSearchModel
    {
        public DuyetDangKyTaiSanSearchModel()
        {
        }
        [UIHint("DateNullable")]
        public DateTime? DateFrom { get; set; }
        [UIHint("DateNullable")]
        public DateTime? DateTo { get; set; }
        public SelectList DDLLoaiHinhTaiSan { get; set; }
        public enumLOAI_HINH_TAI_SAN enumLoaiHinhTaiSan { get; set; }
        public SelectList DDLTrangThaiYeuCau { get; set; }
        public enumTRANG_THAI_YEU_CAU enumTrangThaiYeuCau { get; set; }
        public string KeySearch { get; set; }
        public decimal? LoaiHinhTaiSanId { get; set; }
        public decimal? TrangThaiId { get; set; }
    }
    public class ToolXoaTaiSanModel : BaseGSEntityModel
    {
        public ToolXoaTaiSanModel()
        {
            IS_XOA_CON = false;
        }
        public decimal? DON_VI_ID { get; set; }
        public string DON_VI_MA { get; set; }
        public decimal? NGUOI_DUNG_ID { get; set; }
        public DateTime NGAY_XOA { get; set; }
        public DateTime? NGAY_XOA_XONG { get; set; }
        public Boolean IS_XOA_CON { get; set; }
    }

    public class TaiSanExport
    {
        [DisplayName("Mã tài sản")]
        public string MA { get; set; }
        [DisplayName("Tên tài sản")]
        public string TEN { get; set; }
        [DisplayName("Loại tài sản")]
        public string LOAI_TAI_SAN { get; set; }
        [DisplayName("Nguyên giá")]
        public decimal? NGUYEN_GIA { get; set; }
        [DisplayName("Đơn vị được giao SD")]
        public string DON_VI_SU_DUNG { get; set; }
        [DisplayName("Ngày sử dụng")]
        public DateTime? NGAY_SU_DUNG { get; set; }
    }
    public class TaiSanExportOto
    {
        [DisplayName("Mã tài sản")]
        public string MA { get; set; }
        [DisplayName("Tên tài sản")]
        public string TEN { get; set; }
        [DisplayName("Loại tài sản")]
        public string LOAI_TAI_SAN { get; set; }
        [DisplayName("Đơn vị sử dụng")]
        public string DON_VI_SU_DUNG { get; set; }
    }
}

