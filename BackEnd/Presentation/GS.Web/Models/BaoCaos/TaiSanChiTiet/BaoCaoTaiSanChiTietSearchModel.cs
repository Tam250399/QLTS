using DevExpress.XtraPrinting;
using GS.Core;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.SHTD;
using GS.Core.Domain.TaiSans;
using GS.Web.Framework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.BaoCaos.TaiSanChiTiet
{
    public class BaoCaoTaiSanChiTietSearchModel : BaseSearchModel
    {
        public BaoCaoTaiSanChiTietSearchModel()
        {
            DDLDonVi = new List<SelectListItem>();
            DDLLoaiTaiSan = new List<SelectListItem>();
            DDLDonViTien = new List<SelectListItem>();
            DDLDonViDienTich = new List<SelectListItem>();
            DDLBanTaiSan = new List<SelectListItem>();
            this.DDLSo_luong_Object = new List<SelectListItem>();
            this.DDLCapBaoCao = new List<SelectListItem>();
            this.DDLBanThanhLy = new List<SelectListItem>();
            this.ListLoaiTaiSanId = new List<int>();
            DDLLyDoGiam = new List<SelectListItem>();
            DDLLyDoTang = new List<SelectListItem>();
            DDLDonViKhoiLuong = new List<SelectListItem>();
            DDLLyDoBienDong = new List<SelectListItem>();
            DDLQuyetDinhTichThuTSTD = new List<SelectListItem>();
            this.DDLNhomCongCu = new List<SelectListItem>();
            this.DDLDonViBoPhan = new List<SelectListItem>();
            this.DDLTangOrGiam = new List<SelectListItem>();
            this.ListNguonGocId = new List<int>();
            this.DDLNguonGocTaiSan = new List<SelectListItem>();
            this.DDLCompareSign = new List<SelectListItem>();
            this.DDLBacDonVi = new List<SelectListItem>();
            this.DDLLoaiDonVi = new List<SelectListItem>();
            this.ddlNguonTaiSan = new List<SelectListItem>();
            this.DDLHinhThucXuLy = new List<SelectListItem>();
            this.DDLCapDonVi = new List<SelectListItem>();
            lstNhanHienThi = new Dictionary<string, string>();
            enumDinhDanhXlsExcel = XlsExportMode.SingleFile;
            enumDinhDanhXlsxExcel = XlsxExportMode.SingleFile;
            ListLyDoID = new List<int>() { };
            //enumListLoaiHinhTaiSan = new List<enumLOAI_HINH_TAI_SAN>();
        }
        [UIHint("DateNullable")]
        public DateTime? NgayBaoCao { get; set; }
        [UIHint("DateTimeMMYYYY")]
        public DateTime? ThangBaoCao { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NgayBatDau { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NgayKetThuc { get; set; }
        public Decimal DonVi { get; set; }
        public Decimal LoaiTaiSan { get; set; }
        public Decimal LoaiHinhTaiSan { get; set; }
        public int TaiSanId { get; set; }
        public string ListTaiSanId { get; set; }
        public int NguonTaiSanId { get; set; }
        public int LoaiTaiSanId { get; set; }
        public int DonViTien { get; set; }
        public int DonViDienTich { get; set; }
        public int DonViDienTichDat { get; set; }
        public int DonViDienTichNha { get; set; }
        public int DonViKhoiLuong { get; set; }
        public int BacTaiSan { get; set; }
        public int So_luong_Object { get; set; }
        public int LoaiLyDoBienDong { get; set; }
        [UIHint("InputYear")]
        public decimal NamBaoCao { get; set; }
        public  decimal NAM_BAO_CAO_DC { get; set; }
        public  decimal HE_THONG_ID { get; set; }
        public List<SelectListItem> DDLNamBaoCao { get; set; }
        public List<SelectListItem> DDLHeThong { get; set; }
        public int MauSo { get; set; }
        public String TEN_DON_VI { get; set; }
        public String DIA_CHI_DON_VI { get; set; }
        // lấy cấp đơn vị
        public decimal? CURRENT_CAP_DON_VI { get; set; }
        public String TEN_BO_NGANH { get; set; }
        public String MA_DON_VI { get; set; }
        public String MA_QUAN_HE_NGAN_SACH { get; set; }
        public bool IsBaoCaoThanhLy { get; set; }
        public int LyDoBanThanhLyId { get; set; }
        public int BanOrThanhLy { get; set; }
        public int FileType { get; set; }
        public string MaBaoCao { get; set; }
        public enumLOAI_HINH_TAI_SAN enumLoaiHinhTaiSan { get; set; }
        public enumTinhHuyenXaTrungUong enumCapHanhChinh { get; set; }
        public enumTinhHuyenXa enumCapDonVi { get; set; }
        public Boolean IsChuaCapNhapTien { get; set; }
        public int BacDonVi { get; set; }
        public IList<int> CapDonVi { get; set; }
        public bool isInTrongKy { get; set; }
        public bool IsShowTaiFileDoiChieu { get; set; }
        public string TenLoaiDonVi { get; set; }
        public string TenCapDonViTinhHuyenXa { get; set; }
        public List<SelectListItem> DDLBacDonVi { get; set; }
        public SelectList LoaiHinhTaiSanAvaliable { get; set; }
        public List<SelectListItem> DDLDonVi { get; set; }
        public IList<SelectListItem> DDLLoaiTaiSan { get; set; }
        public List<SelectListItem> DDLLyDoGiam { get; set; }
        public List<SelectListItem> DDLLyDoTang { get; set; }
        public List<SelectListItem> DDLDonViTien { get; set; }
        public List<SelectListItem> DDLBanTaiSan { get; set; }
        public List<SelectListItem> DDLDonViDienTich { get; set; }
        public List<SelectListItem> DDLCapBaoCao { get; set; }
        public List<SelectListItem> DDLBanThanhLy { get; set; }
        public List<SelectListItem> DDLDonViKhoiLuong { get; set; }
        public List<SelectListItem> DDLSo_luong_Object { get; set; }
        public List<SelectListItem> DDLLyDoBienDong { get; set; }
        public List<SelectListItem> DDLQuyetDinhTichThuTSTD { get; set; }
        public List<SelectListItem> DDLNhomCongCu { get; set; }
        public List<SelectListItem> DDLDonViBoPhan { get; set; }
        public List<SelectListItem> DDLTangOrGiam { get; set; }
        public List<SelectListItem> DDLNguonGocTaiSan { get; set; }
        public List<SelectListItem> DDLCapDonVi { get; set; }
        public List<SelectListItem> DDLLoaiDonVi { get; set; }
        public List<SelectListItem> DDLNhanXe { get; set; }
        public List<SelectListItem> ddlNguonTaiSan { get; set; }
        public List<SelectListItem> DDLHinhThucXuLy { get; set; }
        public IList<int> ListLoaiTaiSanId { get; set; }
        public IList<int> ListLoaiDonViId { get; set; }
        public IList<int> ListDonViId { get; set; }
        public string ListLyDoIdString { get; set; }
        public String StringLoaiTaiSan { get; set; }


        public List<SelectListItem> DDLChucDanh{ get; set; }
        public string TenChucDanhPrint { get; set; }
        public string TenHinhThucXuLy { get; set; }
        public string TenCapDonViPrint { get; set; }

        public String StringListLyDoTang{ get; set; }
        public String StringListLyDoGiam { get; set; }
        public String StringLyDoTangGiam { get; set; }
        
        public List<enumLOAI_HINH_TAI_SAN> enumListLoaiHinhTaiSan
        {
            get
            {
                if (StringLoaiTaiSan != null)
                {
                    return StringLoaiTaiSan.Split(',').Select(c => (enumLOAI_HINH_TAI_SAN)(Convert.ToInt32(c))).ToList();

                }
                else
                {
                    return new List<enumLOAI_HINH_TAI_SAN> { enumLOAI_HINH_TAI_SAN.ALL };
                }
            }
            set
            {
                StringLoaiTaiSan = string.Join(',', value.Select(c => (int)c).ToList());
            }
        }
        public List<enumLyDoBienDongBC> enumListLyDoBienDongBC
        {
            get
            {
                if (StringLyDoID != null)
                {
                    return StringLyDoID.Split(',').Select(c => (enumLyDoBienDongBC)(Convert.ToInt32(c))).ToList();

                }
                else
                {
                    return new List<enumLyDoBienDongBC> { enumLyDoBienDongBC.TatCa };
                }
            }
            set
            {
                StringLyDoID = string.Join(',', value.Select(c => (int)c).ToList());
            }
        }
        public List<enumNHAN_HIEN_THI_LOAI_HINH_TSTD> enumListLoaiHinhTSTD
        {
            get;
            set;
        }
        public List<enumNHAN_HIEN_THI_LOAI_HINH_TS> enumListLoaiHinhTS
        {
            get;
            set;
        }
        public List<enumTinhHuyenXaTrungUong> enumListCapHanhChinh
        {
            get
            {
                if (StringCapHanhChinh != null)
                {
                    return StringCapHanhChinh.Split(',').Select(c => (enumTinhHuyenXaTrungUong)(Convert.ToInt32(c))).ToList();

                }
                else
                {
                    return new List<enumTinhHuyenXaTrungUong> {};
                }
            }
            set
            {
                StringCapHanhChinh = string.Join(',', value.Select(c => (int)c).ToList());
            }
        }
        public List<enumTinhHuyenXa> enumListCapDonVi
        {
            get
            {
                if (StringCapHanhChinh != null)
                {
                    return StringCapHanhChinh.Split(',').Select(c => (enumTinhHuyenXa)(Convert.ToInt32(c))).ToList();

                }
                else
                {
                    return new List<enumTinhHuyenXa> { };
                }
            }
            set
            {
                StringCapHanhChinh = string.Join(',', value.Select(c => (int)c).ToList());
            }
        }
        public String StringDonVi { get; set; }
        public String StringLoaiDonVi { get; set; }
        public String StringNguonGocTaiSan { get; set; }
        public String StringCapHanhChinh { get; set; }
        public String StringLyDoID { get; set; }
        public String StringDisplayListLoaiTaiSan { get; set; }
        public IList<int> ListLyDoID { get; set; }
        public int BacNguonGocTSTD { get; set; }
        public int MauGiamTSTD { get; set; }
        public int LyDoGiamTSTD { get; set; }
        public string SoQuyetDinh { get; set; }
        #region Thông tin quyết định tịch thu
        public int QuyetDinhTichThuTSTD { get; set; }
        
        public string NgayQuyetDinhTichThuString { get; set; }
        public string NguoiQuyetDinh { get; set; }
        public string SoQuyetDinhTichThu { get; set; }
        #endregion

        public int LyDoID { get; set; }
        public int DonViBoPhan { get; set; }
        public string ListStringDonVi { get; set; }
        public string ListDonViBoPhan { get; set; }
        public string TenListDonViBoPhan { get; set; }
        public string TenListNhomCongCu { get; set; }
        public string ListStringCongCu { get; set; }
        public IList<int> ListCongCu { get; set; }
        public int TangOrGiam { get; set; }
        public string TenLoaiHinhDonVi { get; set; }
        public string TenLoaiHinhTaiSan { get; set; }
        public string TEN_DON_VI_CAP_TREN { get; set; }
        public String MA_DON_VI_CAP_TREN { get; set; }
        public string TenBoPhan { get; set; }
        public string TenCoQuanBanHanh { get; set; }
        public string TenTaiSan { get; set; }
        public List<int> ListNguonGocId { get; set; }
        public Decimal? HinhThucXuLyId { get; set; }
        public bool IsExportExcel { get; set; }
        //báo cáo tài chính
        public List<SelectListItem> DDLCompareSign { get; set; }
        //số tầng
        [UIHint("InputAddon")]
        public decimal? SoTang_Value1 { get; set; }
        [UIHint("InputAddon")]
        public decimal? SoTang_Value2 { get; set; }
        public decimal? SoTang_CompareSign { get; set; }
        //năm sử dụng
        [UIHint("InputYear")]
        public decimal? NamSD_Value1 { get; set; }
        [UIHint("InputYear")]
        public decimal? NamSD_Value2 { get; set; }
        public decimal? NamSD_CompareSign { get; set; }
        //năm sản xuất
        [UIHint("InputYear")]
        public decimal? NamSx_Value1 { get; set; }
        [UIHint("InputYear")]
        public decimal? NamSx_Value2 { get; set; }
        public decimal? NamSx_CompareSign { get; set; }
        //diện tích
        [UIHint("InputAddon")]
        public decimal? DienTich_Value1 { get; set; }
        [UIHint("InputAddon")]
        public decimal? DienTich_Value2 { get; set; }
        public decimal? DienTich_CompareSign { get; set; }
        //Số chỗ ngồi
        [UIHint("InputAddon")]
        public decimal? SoChoNgoi_Value1 { get; set; }
        [UIHint("InputAddon")]
        public decimal? SoChoNgoi_Value2 { get; set; }
        public decimal? SoChoNgoi_CompareSign { get; set; }
        //Tải trọng
        [UIHint("InputAddon")]
        public decimal? TaiTrong_Value1 { get; set; }
        [UIHint("InputAddon")]
        public decimal? TaiTrong_Value2 { get; set; }
        public decimal? TaiTrong_CompareSign { get; set; }


        

        //Nguyên giá NS
        [UIHint("InputAddon")]
        public decimal? NguyenGiaNS_Value1 { get; set; }
        [UIHint("InputAddon")]
        public decimal? NguyenGiaNS_Value2 { get; set; }
        public decimal? NguyenGiaNS_CompareSign { get; set; }
        //Nguyên giá Khác
        [UIHint("InputAddon")]
        public decimal? NguyenGiaKhac_Value1 { get; set; }
        [UIHint("InputAddon")]
        public decimal? NguyenGiaKhac_Value2 { get; set; }
        public decimal? NguyenGiaKhac_CompareSign { get; set; }
        //Nguyên giá ODA
        [UIHint("InputAddon")]
        public decimal? NguyenGiaODA_Value1 { get; set; }
        [UIHint("InputAddon")]
        public decimal? NguyenGiaODA_Value2 { get; set; }
        public decimal? NguyenGiaODA_CompareSign { get; set; }
        //Nguyên giá Viện trợ
        [UIHint("InputAddon")]
        public decimal? NguyenGiaVienTro_Value1 { get; set; }
        [UIHint("InputAddon")]
        public decimal? NguyenGiaVienTro_Value2 { get; set; }
        public decimal? NguyenGiaVienTro_CompareSign { get; set; }
        //Tổng nguyên giá
        [UIHint("InputAddon")]
        public decimal? TongNguyenGia_Value1 { get; set; }
        [UIHint("InputAddon")]
        public decimal? TongNguyenGia_Value2 { get; set; }
        public decimal? TongNguyenGia_CompareSign { get; set; }
        //Đơn giá
        [UIHint("InputAddon")]
        public decimal? DonGia_Value1 { get; set; }
        [UIHint("InputAddon")]
        public decimal? DonGia_Value2 { get; set; }
        public decimal? DonGia_CompareSign { get; set; }
        //Giá trị còn lại
        [UIHint("InputAddon")]
        public decimal? GTCL_Value1 { get; set; }
        [UIHint("InputAddon")]
        public decimal? GTCL_Value2 { get; set; }
        public decimal? GTCL_CompareSign { get; set; }
        //tỷ lệ chất lượng
        [UIHint("InputAddon")]
        public decimal? TyLeChatLuong_Value1 { get; set; }
        [UIHint("InputAddon")]
        public decimal? TyLeChatLuong_Value2 { get; set; }
        public decimal? TyLeChatLuong_CompareSign { get; set; }


        //số cầu
        [UIHint("InputAddon")]
        public decimal? SoCau_Value1 { get; set; }
        [UIHint("InputAddon")]
        public decimal? SoCau_Value2 { get; set; }
        public decimal? SoCau_CompareSign { get; set; }
        public string TenCauXePrint { get; set; }
        public Dictionary<string, string> lstNhanHienThi { get; set; }
        public Decimal? DU_AN_ID { get; set; }
        public Decimal? NHAN_XE_ID { get; set; }

        public Decimal? CHUC_DANH_ID { get; set; }

        public IList<SelectListItem> DDLDuAn { get; set; }
        public int KiemKeId { get; set; }
        public string tenDuAn { get; set; }
        public string HoiDongKiemKe { get; set; }
        public XlsExportMode enumDinhDanhXlsExcel { get; set; }
        public XlsxExportMode enumDinhDanhXlsxExcel { get; set; }
    }
}

