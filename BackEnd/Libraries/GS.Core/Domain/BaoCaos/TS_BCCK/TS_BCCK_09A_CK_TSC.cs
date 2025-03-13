using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCCK
{
    public class TS_BCCK_09A_CK_TSC : BaseViewEntity
    {
        public Decimal TAI_SAN_ID { get; set; }
        public String TAI_SAN_TEN { get; set; }
        public String TAI_SAN_MA_DB { get; set; }
        public String LY_DO_TANG_TEN { get; set; }
        public Decimal? LY_DO_TANG_ID { get; set; }
        public Decimal? LY_DO_TANG_DB_ID { get; set; }
        public string LOAI_TAI_SAN_TEN { get; set; }
		public int? TREE_LEVEL { get; set; }
		public int? CAP_1 { get; set; }
		public String CAP_2 { get; set; }
		public String CAP_3 { get; set; }
		public String CAP_4 { get; set; }
		public String CAP_5 { get; set; }
		public string TEN_1 { get; set; }
		public string TEN_2 { get; set; }
		public string TEN_3 { get; set; }
		public string TEN_4 { get; set; }
		public string TEN_5 { get; set; }
		//More
        public String DON_VI_TINH { get; set; }
        public int? SO_LUONG { get; set; }
        public String NHAN_HIEU { get; set; }
        public String NUOC_SAN_XUAT { get; set; }
        public Decimal? NAM_SAN_XUAT { get; set; }
        public Decimal? GIA_MUA_THUE { get; set; }
        public String HINH_THUC_MUA_SAM_TEN { get; set; }
        //public String NHA_CUNG_CAP { get; set; } //Chưa có nhà cung cấp, bổ sung phía sau
        //public Decimal? GIA_TRI_HOA_HONG { get; set; } //Giá trị các khoản hoa hồng, chiết khấu, khuyến mãi thu được khi thực hiện mua sắm 
       // public Decimal? GIA_TRI_NOP_NSNN { get; set; }
        //public Decimal? GIA_TRI_DE_LAI_DON_VI { get; set; }
        public String GHI_CHU { get; set; }
        public string TAI_SAN_MA { get; set; }
        public decimal? LOAI_TAI_SAN_DB_ID { get; set; }
    }
}
