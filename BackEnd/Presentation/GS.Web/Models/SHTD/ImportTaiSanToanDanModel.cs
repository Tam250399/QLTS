using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.SHTD
{
    public class ImportTaiSanToanDanModel:BaseGSModel
    {
        public string MA_DON_VI { get; set; }
        public string TEN_QUYET_DINH { get; set; }
        public string SO_QUYET_DINH { get; set; }
        public DateTime? NGAY_QUYET_DINH { get; set; }
        public string TEN_DON_VI { get; set; }
        public string TS_TEN_TAI_SAN { get; set; }
        public string TS_LOAI_TAI_SAN { get; set; }
        public string TS_NGUON_GOC_TAI_SAN { get; set; }
        public decimal? TS_NGUYEN_GIA { get; set; }
        public decimal? TS_SO_LUONG { get; set; }
        public decimal? TS_GIA_TRI { get; set; }
        public decimal? TS_DIEN_TICH { get; set; }
        public decimal? TS_KHOI_LUONG { get; set; }
        public string XL_SO_QUYET_DINH { get; set; }
        public DateTime? XL_NGAY_QUYET_DINH { get; set; }
        public string XL_TEN_DON_VI { get; set; }
        public string XL_HINH_THUC_XU_LY { get; set; }
        public DateTime? XL_NGAY_XU_LY { get; set; }
        public string XL_HO_SO_GIAY_TO { get; set; }
        public decimal? XL_CHI_PHI_XU_LY { get; set; }
        public decimal? XL_SO_LUONG { get; set; }
        public string XL_DON_VI_CHUYEN { get; set; }
        public DateTime? XL_NGAY_CHUYEN { get; set; }
        public decimal? XL_SO_TIEN_THU_DUOC { get; set; }
        public decimal? XL_SO_TIEN_NOP_TKTG { get; set; }
        public string XL_DON_VI_DUOC_GIAO { get; set; }
        public DateTime? XL_NGAY_DIEU_CHUYEN { get; set; }
        public string XL_DON_VI_BAN_GIAO { get; set; }
        public DateTime? XL_NGAY_BAN_GIAO { get; set; }
        public Decimal? XL_NOP_NGAN_SACH { get; set; }
        //public List<ChiTietTaiSanModel> ListTaiSan { get; set; }
        //public List<ChiTietHinhThucXuLyModel> ListHinhThucXuLy { get; set; }
    }
    public class TaiSanToanDanModel
    {
        public TaiSanToanDanModel ()
        {
          
            this.ListTaiSan = new List<ChiTietTaiSanModel>();
        }
        public string MA_DON_VI { get; set; }
        public string TEN_QUYET_DINH { get; set; }
        public string SO_QUYET_DINH { get; set; }
        public DateTime? NGAY_QUYET_DINH { get; set; }
        public string TEN_DON_VI { get; set; }
        public List<ChiTietTaiSanModel> ListTaiSan { get; set; }
       // public List<ChiTietHinhThucXuLyModel> ListHinhThucXuLy { get; set; }
    }
    public class ChiTietTaiSanModel
    {
        public string TS_TEN_TAI_SAN { get; set; }
        public string TS_LOAI_TAI_SAN { get; set; }
        public string TS_NGUON_GOC_TAI_SAN { get; set; }
        public decimal? TS_NGUYEN_GIA { get; set; }
        public decimal? TS_SO_LUONG { get; set; }
        public decimal? TS_GIA_TRI { get; set; }
        public decimal? TS_DIEN_TICH { get; set; }
        public decimal? TS_KHOI_LUONG { get; set; }
        public string XL_SO_QUYET_DINH { get; set; }
        public DateTime? XL_NGAY_QUYET_DINH { get; set; }
        public string XL_TEN_DON_VI { get; set; }
        public string XL_HINH_THUC_XU_LY { get; set; }
        public DateTime? XL_NGAY_XU_LY { get; set; }
        public string XL_HO_SO_GIAY_TO { get; set; }
        public decimal? XL_CHI_PHI_XU_LY { get; set; }
        public decimal? XL_SO_LUONG { get; set; }
        public string XL_DON_VI_CHUYEN { get; set; }
        public DateTime? XL_NGAY_CHUYEN { get; set; }
        public decimal? XL_SO_TIEN_THU_DUOC { get; set; }
        public decimal? XL_SO_TIEN_NOP_TKTG { get; set; }
        public string XL_DON_VI_DUOC_GIAO { get; set; }
        public DateTime? XL_NGAY_DIEU_CHUYEN { get; set; }
        public string XL_DON_VI_BAN_GIAO { get; set; }
        public DateTime? XL_NGAY_BAN_GIAO { get; set; }
        public Decimal? XL_NOP_NGAN_SACH { get; set; }
    }
}
