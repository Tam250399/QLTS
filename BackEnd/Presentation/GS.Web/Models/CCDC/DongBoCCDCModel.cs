using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.CCDC
{
    public class DongBoCCDCModel
    {
        public String LOAI { get; set; }
        public String MA { get; set; }
        public String TEN { get; set; }
        public String BO_PHAN_SD { get; set; }
        public DateTime NGAY_GHI_TANG { get; set; }
        public Decimal SO_LUONG { get; set; }
        public String DON_VI_TINH { get; set; }
        public Decimal DON_GIA_MUA { get; set; }
        public Decimal THANH_TIEN { get; set; }
        public String MA_NGUON_GOC { get; set; }
        public String MA_TINH_TRANG { get; set; }
        public String SO_CHUNG_TU { get; set; }
        public DateTime? NGAY_CHUNG_TU { get; set; }
        public String NHA_CC { get; set; }
        public DateTime NAM_THEO_DOI { get; set; }
        public Decimal PB_KY { get; set; }
        public Decimal PB_THOI_GIAN { get; set; }
        public Decimal PB_TY_LE { get; set; }
        public Decimal PB_THOI_GIAN_CON_LAI { get; set; }
        public Decimal PB_GIA_TRI_MOT_KY { get; set; }
        public Decimal PB_GIA_TRI_DA_PB { get; set; }
        public Decimal PB_GIA_TRI_PB_CON_LAI { get; set; }
    }
}
