//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/1/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.CCDC
{
    public enum enumMucDichXuatNhap
    {
        //CHON = 0,
        MUA_MOI = 1,
        //CHUYEN_NHUONG = 2,
        TIEP_NHAN = 3,
        NHAP_SO_DU_BAN_DAU = 4
    }
    public enum enumLoaiXuatNhap
    {
        NHAP_KHO = 1,
        PHAN_BO = 2,
        LUAN_CHUYEN = 3,
        DIEU_CHUYEN = 4,
        DIEU_CHUYEN_NGOAI = 5,
        GIAM_HONG_MAT = 6,
        GIAM_BAN = 7,
        GIAM_TIEU_HUY = 8,
        GIAM_KHAC = 13,
        //them ly do
        BI_THU_HOI = 10,
        THANH_LY = 11,
        GIAM_MAT_HUY_HOAI = 12
    }
    public enum enumLyDoGiam
    {        
        
        DIEU_CHUYEN = 4,
        DIEU_CHUYEN_NGOAI = 5,
        GIAM_HONG_MAT = 6,
        GIAM_BAN = 7,
        GIAM_TIEU_HUY = 8,
        GIAM_KHAC = 13,
        //Them ly do giam
        BI_THU_HOI = 10,
        THANH_LY = 11,
        GIAM_MAT_HUY_HOAI = 12
    }
    public partial class XuatNhap : BaseEntity
	{
        public XuatNhap()
        {
            this.GUID = Guid.NewGuid();
        }
		public Decimal? TRANG_THAI_ID {get;set;}
		public Decimal? FROM_XUAT_NHAP_ID {get;set;}
		public DateTime? NGAY_XUAT_NHAP {get;set;}
		public Decimal? DON_VI_ID {get;set;}
		public Decimal? NGUOI_DUNG_ID {get;set;}
		public Decimal? DON_VI_BO_PHAN_ID {get;set;}
		public String MA {get;set;}
		public String MA_LIEN_QUAN { get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public DateTime? NGAY_CAP_NHAP {get;set;}
		public String GHI_CHU {get;set;}
		public Boolean? IS_XUAT {get;set;}
		public Guid GUID {get;set;}
		public String TEN { get; set; }
        public Decimal? MUC_DICH_XUAT_NHAP_ID { get; set; }
        public String QUYET_DINH_SO { get; set; }
        public DateTime? QUYET_DINH_NGAY { get; set; }
        public Decimal? LOAI_XUAT_NHAP_ID { get; set; }
        public Boolean? IS_PHAN_BO_FIRST { get; set; }
    }
}



