using GS.Web.Framework.Models;
using GS.Web.Models.NghiepVu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.ImportTaiSan
{
    public class ImportBdTangGiamNguyenGiaModel : BaseGSEntityModel
    {
        public ImportBdTangGiamNguyenGiaModel()
        {

        }
        public String DON_VI_MA { get; set; }
        public String TAI_SAN_MA_DB { get; set; }
        public String LOAI_TAI_SAN_MA { get; set; }
        public String TEN { get; set; }
        public String LOAI_BIEN_DONG_ID { get; set; } 
        public String QUYET_DINH_SO { get; set; }
        public DateTime? QUYET_DINH_NGAY { get; set; }
        public String LY_DO_BIEN_DONG_MA { get; set; }
        public DateTime NGAY_BIEN_DONG { get; set; }
        public decimal NV_NGAN_SACH { get; set; }
        public decimal NV_KHAC { get; set; }
        public Decimal? DIEN_TICH_DATNHA_TANG { get; set; }
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
        public String GHI_CHU { get; set; }
        //
        public decimal TAI_SAN_ID { get; set; }
        public String TAI_SAN_MA { get; set; }
        public DateTime? NGAY_SU_DUNG { get; set; }
        public decimal LY_DO_BIEN_DONG_ID { get; set; } 
        public int? Row { get; set; }
        public partial class HienTrangList
        {
            public IList<ObjHienTrang> lstObjHienTrang { get; set; }
        }
    }
}
