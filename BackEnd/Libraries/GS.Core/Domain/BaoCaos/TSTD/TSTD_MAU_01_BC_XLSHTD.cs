using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TSTD
{
    public class TSTD_MAU_01_BC_XLSHTD : BaseViewEntity
    {
        public string TAI_SAN_TEN { get; set; }
        public string LOAI_TAI_SAN_TEN { get; set; }
        public string THONG_TIN_TAI_SAN { get; set; }
        public Decimal? GIA_TRI_TAI_SAN { get; set; }      
        public string GHI_CHU { get; set; }
        public string NGUON_GOC_TAI_SAN { get; set; }        
        public Decimal? CAP_1 { get; set; }
        public string TEN_1 { get; set; }
        public Decimal? CAP_2 { get; set; }
        public string TEN_2 { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? GIA_TRI_TINH { get; set; }
        public Decimal? NAM_SU_DUNG { get; set; }
        public string BIEN_KIEM_SOAT { get; set; }
        public Decimal? SO_CHO_NGOI { get; set; }
        public string DIA_CHI { get; set; }
        public string NHAN_XE_TEN { get; set; }
        public Decimal? TAI_SAN_DAT_ID { get; set; }
        

    }
}
