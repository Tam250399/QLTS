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


namespace GS.Core.Domain.DanhMuc
{
	public static class enumMaLoaiQuyetDinh
	{
		public static string TichThu = "001";
		public static string XacLap = "";
    }
	public partial class NguonGocTaiSan : BaseEntity
	{
		public String MA {get;set;}
		public String TEN {get;set;}
        public int? DB_ID { get; set; }
        public String TREE_NODE { get; set; }
        public Decimal? TREE_LEVEL { get; set; }
        public Decimal? PARENT_ID { get; set; }
	}
}



