//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.TaiSans
{
    public enum enumTinhTrang
    {
        DANG_SU_DUNG = 1,
        CHUA_SU_DUNG = 2,
        HONG_CHO_XU_LY = 3,
        THIEU = 4,
        THUA =5
         
    }
    public enum enumDeNghiXuLy
    {
        //SUA_CHUA_LON = 1,
        //NANG_CAP = 2,
        //DANH_GIA_LAI = 3,
        //DIEU_CHUYEN = 4,
        //GIAM_KHAC = 5
        BAN = 1,
        CHUYEN_NHUONG = 2,
        TIEU_HUY = 3,
        DIEU_CHUYEN_NGOAI_HE_THONG = 4,
        THANH_LY = 5,
        DIEU_CHUYEN = 6,
        BI_THU_HOI = 7,
        KHAC = 8
    }
	public partial class TaiSanKiemKeTaiSan : BaseEntity
	{
		public Decimal KIEM_KE_ID {get;set;}
		public Decimal? TAI_SAN_ID {get;set;}
		public Decimal? SO_LUONG_KIEM_KE {get;set;}
		public Decimal? SO_LUONG_SO_SACH {get;set;}
		public bool IS_PHAT_HIEN_THUA {get;set;}
		public Decimal? NGUYEN_GIA_SO_SACH { get;set; }
        public Decimal? NGUYEN_GIA_KIEM_KE { get; set; }
        public Decimal? GIA_TRI_CON_LAI_SO_SACH { get; set; }
        public Decimal? GIA_TRI_CON_LAI_KIEM_KE { get; set; }
        public Decimal? TINH_TRANG_ID { get; set; }
        public Decimal? DE_NGHI_XU_LY_ID { get; set; }

        public String KIEN_NGHI_GQTST { get; set; }
        public String GHI_CHU { get; set; }
        public String TEN_TAI_SAN_THUA { get; set; }
        public Decimal NHOM_TAI_SAN_ID { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        public Decimal? DON_VI_BO_PHAN_ID { get; set; }
        public virtual TaiSan taiSan { get; set; }

        
    }
}



