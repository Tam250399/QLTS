using DevExpress.XtraPrinting;
using GS.Core;
using GS.Core.Domain.BaoCaos.TS_BCQH;
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
namespace GS.Web.Models.BaoCaos.TaiSanTongHop
{
    public class BaoCaoTaiSanTongHopSearchModel : BaseSearchModel
    {
        public BaoCaoTaiSanTongHopSearchModel()
        {
            DDLDonVi = new List<SelectListItem>();
            DDLLoaiTaiSan = new List<SelectListItem>();
            DDLDonViTien = new List<SelectListItem>();
            DDLDonViDienTich = new List<SelectListItem>();
            DDLBanTaiSan = new List<SelectListItem>();
            this.DDLSo_luong_Object = new List<SelectListItem>();
            this.DDLCapBaoCao = new List<SelectListItem>();
            this.DDLBanThanhLy = new List<SelectListItem>();
            ddlNguonTaiSan = new List<SelectListItem>();
            ListLoaiTaiSanId = new List<int>();
            DDLLyDoGiam = new List<SelectListItem>();
            DDLLyDoTang = new List<SelectListItem>();
            DDLBacDonVi = new List<SelectListItem>();
            DDLCapDonVi = new List<SelectListItem>();
            DDLLoaiDonVi = new List<SelectListItem>();
            DDLLyDoBienDong = new List<SelectListItem>();
            DDLDuAn = new List<SelectListItem>();
            lstNhanHienThi = new Dictionary<string, string>();
            enumDinhDanhXlsExcel = XlsExportMode.SingleFile;
            enumDinhDanhXlsxExcel = XlsxExportMode.SingleFile;
        }
        [UIHint("DateNullable")]
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayBaoCao { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NgayKetThuc { get; set; }
        public bool IsExportExcel { get; set; }
        public bool IsHideDetail { get; set; }
        public bool IsDPAC { get; set; }
        public Decimal DonVi { get; set; }
        public Decimal LoaiTaiSan { get; set; }
        public Decimal LoaiHinhTaiSan { get; set; }
        public int DonViTien { get; set; }
        public int DonViDienTich { get; set; }
        public int DonViDienTichDat { get; set; }
        public int DonViDienTichNha { get; set; }
        public int BacTaiSan { get; set; }
        public int So_luong_Object { get; set; }
        public int LoaiLyDoBienDong { get; set; }
        [UIHint("InputYear")]
        public decimal NamBaoCao { get; set; }
        public int MauSo { get; set; }
        public IList<int> CapDonVi { get; set; }
        public String TEN_DON_VI { get; set; }
        public String TEN_DON_VI_CHA { get; set; }
        public String TEN_BO_NGANH { get; set; }
        public String MA_DON_VI { get; set; }
        public String MA_QUAN_HE_NGAN_SACH { get; set; }
        public int FileType { get; set; }
        public string MaBaoCao { get; set; }
        public bool IsBaoCaoThanhLy { get; set; }
        public bool IsTinh { get; set; }
        public bool IsHuyen { get; set; }
        public bool IsXa { get; set; }
        public int LyDoBanThanhLyId { get; set; }
        public int BanOrThanhLy { get; set; }
        public Boolean IsChuaCapNhapTien { get; set; }
        public int BacDonVi { get; set; }
        public string TenLoaiHinhDonVi { get; set; }
        public List<SelectListItem> DDLBacDonVi { get; set; }
        public enumLOAI_HINH_TAI_SAN enumLoaiHinhTaiSan { get; set; }
        public SelectList LoaiHinhTaiSanAvaliable { get; set; }
        public List<SelectListItem> DDLDonVi { get; set; }
        public List<SelectListItem> DDLLoaiTaiSan { get; set; }
        public List<SelectListItem> DDLLyDoGiam { get; set; }
        public List<SelectListItem> DDLLyDoTang { get; set; }
        public List<SelectListItem> DDLDonViTien { get; set; }
        public List<SelectListItem> DDLBanTaiSan { get; set; }
        public List<SelectListItem> DDLDonViDienTich { get; set; }
        public List<SelectListItem> DDLCapBaoCao { get; set; }
        public List<SelectListItem> DDLNamBaoCao { get; set; }
        public List<SelectListItem> DDLBanThanhLy { get; set; }
        public List<SelectListItem> DDLSo_luong_Object { get; set; }
        public List<SelectListItem> DDLCapDonVi { get; set; }
        public List<SelectListItem> DDLLoaiDonVi { get; set; }
        public List<SelectListItem> DDLDuAn { get; set; }
        public Dictionary<string, string> lstNhanHienThi { get; set; }
        public Decimal DuAnId { get; set; }
        public IList<int> ListLoaiTaiSanId { get; set; }
        public IList<int> ListLoaiDonViId { get; set; }
        public IList<int> ListDonViId { get; set; }
        public String StringLoaiTaiSan { get; set; }
        public String StringLoaiDonVi { get; set; }
        public String StringDonVi { get; set; }
        public String StringCapHanhChinh { get; set; }
        public List<SelectListItem> DDLLyDoBienDong { get; set; }
        public List<SelectListItem> ddlNguonTaiSan { get; set; }
        public List<SelectListItem> ddlHeThong { get; set; }
        public int LyDoID { get; set; }
        public int NguonTaiSanId { get; set; }
        public string tenDuAn { get; set; }
        public String StringLyDoID { get; set; }
        public string TenCapHanhChinh { get; set; }
        public string TenLoaiDonVi { get; set; }
        public string TenLoaiHinhTaiSanPrint { get; set; }
        public IList<int> ListLyDoID { get; set; }
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
                    return new List<enumTinhHuyenXaTrungUong> { };
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
        public List<enumNhanKieuTaiSanBaoCao> enumListNhanKieuTaiSanBaoCao
        {
            get
            {

                return new List<enumNhanKieuTaiSanBaoCao> { enumNhanKieuTaiSanBaoCao.TAI_SAN_CONG, enumNhanKieuTaiSanBaoCao.TAI_SAN_KET_CAU_HA_TANG, enumNhanKieuTaiSanBaoCao.TAI_SAN_TOAN_DAN };

            }
            set
            {

            }
        }
        public List<enumNHAN_HIEN_THI_LOAI_HINH_TSTD> enumListLoaiTSTD
        {
            get
            {
                return new List<enumNHAN_HIEN_THI_LOAI_HINH_TSTD> { enumNHAN_HIEN_THI_LOAI_HINH_TSTD.DAT, enumNHAN_HIEN_THI_LOAI_HINH_TSTD.NHA, enumNHAN_HIEN_THI_LOAI_HINH_TSTD.OTO, enumNHAN_HIEN_THI_LOAI_HINH_TSTD.TAI_SAN_KHAC };
            }
            set
            {

            }
        }
        public XlsExportMode enumDinhDanhXlsExcel { get; set; }
        public XlsxExportMode enumDinhDanhXlsxExcel { get; set; }
        public List<enumNHAN_HIEN_THI_LOAI_HINH_TS> enumListLoaiHinhTS
        {
            get;
            set;
        }
        //public List<enumCapDonVi> enumListCapDonVi 
        //{
        //    get; set;

        //}
        public Decimal? HE_THONG_ID { get; set; } 
        public Decimal? BAO_CAO_ID { get; set; } 
        public Decimal? NAM_BAO_CAO_DC { get; set; }
       
    }
}
