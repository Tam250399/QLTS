//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using System;


namespace GS.Core.Domain.DanhMuc
{
    public partial class DoiTac : BaseEntity
    {
		public DoiTac()
		{

		}
        public String MA { get; set; }
        public String TEN { get; set; }
        public String DAI_DIEN { get; set; }
        public String DIA_CHI { get; set; }
        public String DIEN_THOAI { get; set; }
        public String MO_TA { get; set; }
        public Decimal? LOAI_DOI_TAC_ID { get; set; }
        public Decimal? DON_VI_BO_PHAN_ID { get; set; }
        public Decimal DON_VI_ID { get; set; }
        public virtual DonVi donvi { get; set; }
        public enumLOAI_DOI_TAC LoaiDoiTac_enum
        {
            get => (enumLOAI_DOI_TAC)LOAI_DOI_TAC_ID;
            set => LOAI_DOI_TAC_ID = (int)value;
        }

    }

    public enum enumLOAI_DOI_TAC
    {
       NhaCungCap = 1,
       KhachHang = 0,
    }
}



