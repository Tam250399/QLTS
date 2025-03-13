using DevExpress.XtraPrinting;
using GS.Core.Domain.BaoCaos.TS_BCQH;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.SHTD;
using GS.Core.Domain.TaiSans;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GS.Core.Domain.Api.BaoCao
{
    public class BaoCaoTaiSanDongBoSearch
    {
        public BaoCaoTaiSanDongBoSearch()
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
        #region
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
        public List<SelectListItem> DDLLyDoBienDong { get; set; }
        public List<SelectListItem> ddlNguonTaiSan { get; set; }
        public int LyDoID { get; set; }
        public int NguonTaiSanId { get; set; }
        public string tenDuAn { get; set; }
        public String StringLyDoID { get; set; }
        public IList<int> ListLyDoID { get; set; }
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
        #endregion
        public DateTime? NgayBaoCao { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string MaBaoCao { get; set; }
        public decimal? NamBaoCao { get; set; }
        public decimal? LoaiHinhTaiSan { get; set; }
        //more
        public int? BacDonVi { get; set; }
        public decimal? BacNguonGocTSTD { get; set; }
        public decimal? CapDonVi { get; set; }
        public string MaDiaBan { get; set; }
        public int? QuyetDinhTichThuTSTD { get; set; }
        public int? NguonGoc { get; set; }
        public int BacTaiSan { get; set; }
        public int DonViTien { get; set; }
        public int DonViDienTich { get; set; }
        public int DonViDienTichDat { get; set; }
        public int DonViDienTichNha { get; set; }
        public int DonViKhoiLuong { get; set; }
        public String StringLoaiTaiSan { get; set; }
        public String StringDonVi { get; set; }
        public String StringLoaiDonVi { get; set; }
        public Decimal DonVi { get; set; }
        public int FileType { get; set; }
        public String TEN_DON_VI { get; set; }
        public String TEN_DON_VI_CHA { get; set; }
        public String TEN_BO_NGANH { get; set; }
        public String MA_DON_VI { get; set; }
        public String MA_QUAN_HE_NGAN_SACH { get; set; }
        public string TenLoaiHinhDonVi { get; set; }
        public Guid FileGuid { get; set; }
        public int? NHOM_TAI_SAN_LON { get; set; }
    }
    public class MapBaoCaoTaiSanDongBoSearch
    {
        public string NgayBaoCao { get; set; }
        public string NgayBatDau { get; set; }
        public string NgayKetThuc { get; set; }
        public string MaBaoCao { get; set; }
        public string NamBaoCao { get; set; }
        public string LoaiHinhTaiSan { get; set; }
        //more
        public string BacDonVi { get; set; }
        public string BacNguonGocTSTD { get; set; }
        public string CapDonVi { get; set; }
        public string MaDiaBan { get; set; }
        public string QuyetDinhTichThuTSTD { get; set; }
        public string NguonGoc { get; set; }
        public string BacTaiSan { get; set; }
        public string DonViTien { get; set; }
        public string DonViDienTich { get; set; }
        public string DonViDienTichDat { get; set; }
        public string DonViDienTichNha { get; set; }
        public string DonViKhoiLuong { get; set; }
        public string StringLoaiTaiSan { get; set; }
        public string StringDonVi { get; set; }
        public string StringLoaiDonVi { get; set; }
        public string DonVi { get; set; }
        public string FileType { get; set; }
        public string FileGuid { get; set; }
        public string NHOM_TAI_SAN_LON { get; set; }
    }
}
