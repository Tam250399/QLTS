//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using System;


namespace GS.Core.Domain.DanhMuc
{
    public partial class DuAn : BaseEntity
    {
        public String MA { get; set; }
        public String TEN { get; set; }
        public Decimal? LOAI_DU_AN_ID { get; set; }
        public DateTime? NGAY_BAT_DAU { get; set; }
        public DateTime? NGAY_KET_THUC { get; set; }
        public Decimal? TONG_KINH_PHI { get; set; }
        public Decimal? HINH_THUC_ID { get; set; }
        public String SO_QUYET_DINH_PHE_DUYET { get; set; }
        public String CHU_DAU_TU { get; set; }
        public String DIA_DIEM { get; set; }
        public String NGUON_VON { get; set; }
        public String GHI_CHU { get; set; }
        public Decimal? NGUON_NS { get; set; }
        public Decimal? NGUON_ODA { get; set; }
        public Decimal? NGUON_VIEN_TRO { get; set; }
        public Decimal? NGUON_KHAC { get; set; }
        public String KIEU { get; set; }
        public String CO_QUAN_TAI_CHINH { get; set; }
        public String MA_LOAI_DU_AN { get; set; }
        public String MA_NHOM_DU_AN { get; set; }
        public String TEN_NHOM_DU_AN { get; set; }
        public String MA_HT { get; set; }
        public String TEN_HT { get; set; }
        public String HT_QUANLY { get; set; }
        public Boolean? HIEU_LUC { get; set; }
        //public String MA_DVQHNS { get; set; }
        public String MA_DU_AN_CU { get; set; }
        public Boolean? TRANG_THAI_ID { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        public Decimal? DON_VI_CU_ID { get; set; }
        public Decimal? DB_ID { get; set; }
        public virtual DonVi donVi { get; set; }

    }

	public enum enumTRANG_THAI_DU_AN
	{
		CHON_TRANG_THAI = 0,
		CO_HIEU_LUC = 1,
	}
}



