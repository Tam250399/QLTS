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
using GS.Core.Domain.DanhMuc;

namespace GS.Core.Domain.SHTD
{
	public partial class TaiSanTdXuLy : BaseEntity
	{
        public TaiSanTdXuLy()
        {
            GUID = Guid.NewGuid();
        }
		public Guid GUID { get; set; }
		public Decimal TAI_SAN_ID {get;set;}
		public Decimal XU_LY_ID {get;set;}
		public Decimal? SO_LUONG {get;set;}
		public Decimal? PHUONG_AN_XU_LY_ID { get; set; }
		public Decimal? HINH_THUC_XU_LY_ID { get; set; }
		public Decimal? DON_VI_DIEU_CHUYEN_ID { get; set; }
		public String GHI_CHU { get; set; }
		public string DB_ID { get; set; }
		public string DB_XU_LY_ID { get; set; }
		public string DB_TAI_SAN_ID { get; set; }
		public virtual XuLy xuly { get; set; }
		public virtual TaiSanTd taisantd { get; set; }
		public virtual HinhThucXuLy hinhthucxuly { get; set; }

	}
	public partial class ListSoLuong
	{
		public Decimal TAI_SAN_ID { get; set; }
		public Decimal SO_LUONG { get; set; }
	}
}



