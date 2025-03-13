//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 11/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.CCDC
{
    public enum enumTinhTrang
    {
        CHON = 0,
        DANG_SU_DUNG = 1,
        CHUA_SU_DUNG = 2,
        HONG_CHO_XU_LY = 3
    }
    public enum enumDeNghiXuLy
    {
        CHON = 0,
        GIAM_CONG_CU = 1,
        SUA_CHUA = 2,
        DIEU_CHUYEN = 3
    }
    public partial class KiemKeCongCu : BaseEntity
	{
		public Decimal KIEM_KE_ID {get;set;}
		public Decimal? CONG_CU_ID {get;set;}
		public bool IS_PHAT_HIEN_THUA {get;set;}
		public Decimal? SO_LUONG_THUA {get;set;}
        public Decimal? XUAT_NHAP_ID { get; set; }
        public Decimal? SO_LUONG_KIEM_KE { get; set; }
        public Decimal? SO_LUONG_SO_SACH { get; set; }
        public Decimal? TINH_TRANG_ID { get; set; }
        public Decimal? DE_NGHI_XU_LY { get; set; }
        public String GHI_CHU { get; set; }
        public String TEN_CONG_CU_THUA { get; set; }
        public Decimal? NHOM_CONG_CU_ID { get; set; }
        public Decimal? DON_VI_BO_PHAN_ID { get; set; }
        public Decimal? DON_GIA_THUA { get; set; }
        public Decimal? DON_GIA { get; set; }
        public virtual CongCu CongCu { get; set; }
        public virtual XuatNhap XuatNhap { get; set; }
        public virtual KiemKe KiemKe { get; set; }
    }
}



