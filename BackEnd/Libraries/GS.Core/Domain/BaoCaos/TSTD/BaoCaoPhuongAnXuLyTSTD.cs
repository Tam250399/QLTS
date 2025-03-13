using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TSTD
{
     public class BaoCaoPhuongAnXuLyTSTD: BaseViewEntity
    {
        public string TAI_SAN_TEN { get; set; }
        public string LOAI_TAI_SAN_TEN { get; set; }
        public string NGUON_GOC_TAI_SAN { get; set; }
        public Decimal? CAP_1 { get; set; }
        public string TEN_1 { get; set; }
        public Decimal? CAP_2 { get; set; }
        public string TEN_2 { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public string XL_QUYET_DINH_SO { get; set; }
        public string XL_QUYET_DINH_NGAY { get; set; }
        public string XL_NGUOI_QUYET_DINH { get; set; }
        public string QD_QUYET_DINH_SO { get; set; }
        public string QD_QUYET_DINH_NGAY { get; set; }
        public string QD_NGUOI_QUYET_DINH { get; set; }
        public string HINH_THUC_XU_LY_TEN { get; set; }
        public decimal? HINH_THUC_XU_LY_DB_ID { get; set; }
    }
}
               
              
            
