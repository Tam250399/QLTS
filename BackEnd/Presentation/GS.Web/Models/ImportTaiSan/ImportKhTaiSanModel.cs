using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.ImportTaiSan
{
    public class ImportKhTaiSanModel : BaseGSEntityModel
    {
        public ImportKhTaiSanModel()
        {
        }
        public String DON_VI_MA { get; set; }
        public String TAI_SAN_MA_DB { get; set; }
        public Decimal NAM_KHAU_HAO { get; set; }
        public Decimal? GIA_TRI_KHAU_HAO { get; set; }
        public Decimal? TONG_KHAU_HAO_LUY_KE { get; set; }
        public Decimal? TONG_GIA_TRI_CON_LAI { get; set; }
        public Decimal? TY_LE_KHAU_HAO { get; set; }
        public Decimal? TONG_NGUYEN_GIA { get; set; }
        public Int32? THANG_KHAU_HAO { get; set; }
        public int? Row { get; set; }
    }
}
