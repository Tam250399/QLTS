using GS.Web.Framework.Models;
using GS.Web.Models.NghiepVu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.ImportTaiSan
{
    public class ImportBdGiamTaiSanModel : BaseGSEntityModel
    {
        public ImportBdGiamTaiSanModel()
        {
        }
        public String DON_VI_MA { get; set; }
        public String TAI_SAN_MA_DB { get; set; }
        public String LOAI_TAI_SAN_MA { get; set; }
        public String TEN { get; set; }
        public DateTime? NGAY_SU_DUNG { get; set; } //Ngày sử dụng  
        public String QUYET_DINH_SO { get; set; }
        public DateTime? QUYET_DINH_NGAY { get; set; }
        public String LOAI_BIEN_DONG_ID { get; set; } //Loại giảm
        public String LY_DO_GIAM_ID { get; set; } //Lý do giam
        public DateTime NGAY_GIAM { get; set; } //Ngày kê khai biến động
        public String HINH_THUC_XU_LY_ID { get; set; }
        public int? IS_BAN_THANH_LY { get; set; }
        public String CHUNG_TU_SO { get; set; }
        public DateTime? CHUNG_TU_NGAY { get; set; }
        public Decimal? PHI_THU { get; set; } //Số tiền thu được
        public Decimal? PHI_BU_DAP { get; set; } //Chi phí xử lý tài sản
        public Decimal? PHI_NOP_NGAN_SACH { get; set; } //Nộp tài khoản tạm giữ
        public String DON_VI_NHAN_DIEU_CHUYEN_MA { get; set; }
        public String GHI_CHU { get; set; }
        /// <summary>
        /// dữ liệu chuyển đổi bổ sung
        /// </summary>
        public Decimal? LOAI_TAI_SAN_ID { get; set; }
        public Decimal TAI_SAN_ID { get; set; }
        public String TAI_SAN_MA { get; set; }
        public Decimal? DON_VI_NHAN_DIEU_CHUYEN_ID { get; set; }
        public Decimal? NGUYEN_GIA { get; set; }
        public Decimal? DAT_TONG_DIEN_TICH { get; set; }
        public Decimal? NHA_DIEN_TICH_XD { get; set; }
        public Decimal? NHA_TONG_DIEN_TICH_XD { get; set; }
        public Decimal? VKT_DIEN_TICH { get; set; }
        public decimal? HTSD_TRU_SO_LAM_VIEC { get; set; }
        public decimal? HTSD_HDSN_KINH_DOANH { get; set; }
        public decimal? HTSD_HDSN_KHONG_KINH_DOANH { get; set; }
        public decimal? HTSD_HDSN_LDLK { get; set; }
        public decimal? HTSD_HDSN_CHO_THUE { get; set; }
        public decimal? HTSD_DE_O { get; set; }
        public decimal? HTSD_BI_LAN_CHIEM { get; set; }
        public decimal? HTSD_BO_TRONG { get; set; }
        public decimal? HTSD_SDHH { get; set; }
        public decimal? HTSD_KHAC { get; set; }
        public int? Row { get; set; }
        public partial class HienTrangList
        {
            public IList<ObjHienTrang> lstObjHienTrang { get; set; }
        }
    }
}
