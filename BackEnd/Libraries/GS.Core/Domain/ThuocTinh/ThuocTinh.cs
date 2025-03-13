//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.ThuocTinhs
{
	public enum enumLoaiDuLieuCauHinh
	{
		ALL = 0,//tất cả
		NB = 1,//number
		ST = 2,//string
		CB = 3,//checkbox
		RD = 4,//radio
		DDL = 5,//dropdownlist
		DT = 6,//datetime
		OBJ = 7,//object
		MS = 8//mutiselect
	}
	public enum enumFieldCauHinh
	{
		HOP_DONG_SO = 0,//hợp đồng số
		HOP_DONG_NGAY = 1,//hợp đồng ngày
		SO_TIEN_THU = 2,//số tiền thu
		DON_VI_CHUYEN_ID = 3//đơn vị chuyển 
	}
	public partial class ThuocTinh : BaseEntity
	{

		public String MA {get;set;}
		public String TEN {get;set;}
		public String CAU_HINH {get;set;}
		public DateTime? NGAY_TAO {get;set;}
		public Decimal? NGUOI_TAO_ID {get;set;}
		public Decimal? DON_VI_ID {get;set;}
		
	}
}



