//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using System;


namespace GS.Core.Domain.DanhMuc
{
    public enum ENUMTRANG_THAI_NGUON_VON
	{
        InActive=0,
        Active=1
	}
    public partial class NguonVon : BaseEntity
    {
        public String TEN { get; set; }
        public int? AP_DUNG_ID { get; set; }
        public String GHI_CHU { get; set; }
        public decimal? TRANG_THAI_ID { get; set; }
        public decimal? THU_TU { get; set; }
    }
    public partial class NguonVonModelCore : BaseEntity
    {
        public String TEN { get; set; }
        public int? AP_DUNG_ID { get; set; }
        public String GHI_CHU { get; set; }
        public decimal? GiaTri { get; set; }
        public decimal? LoaiHinhTaiSanId { get; set; }
    }
	public enum enumNGUON_VON_DEFAULT
	{
		NGAN_SACH = 1, // id nguon von ngan sach from dm_nguon_von
		KHAC = 3,// id nguon von khac from dm_nguon_von
	}
    //nguon von import
    public enum enumNGUON_VON_IMPORT
    {
        NGAN_SACH = 1, // id nguon von ngan sach from dm_nguon_von
        KHAC = 3,// id nguon von khac from dm_nguon_von
        VIEN_TRO = 4,
        QUY_HĐSN = 17
    }
    public enum enumNGUON_VON_DU_AN
    {
        NGAN_SACH = 1, // id nguon von ngan sach from dm_nguon_von
        KHAC = 3,// id nguon von khac from dm_nguon_von
        ODA = 16,
        VTPCP = 4
    }
}



