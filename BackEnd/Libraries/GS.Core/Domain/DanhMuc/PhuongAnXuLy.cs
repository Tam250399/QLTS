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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Core.Domain.DanhMuc
{
	public partial class PhuongAnXuLy : BaseEntity
	{
		public String MA {get;set;}
		public String TEN {get;set;}
		public Decimal? SAP_XEP {get;set;}
		public String CONFIG_CAU_HINH { get; set; }
        public Decimal? DB_ID { get; set; }

    }

    public partial class ThuocTinhPhuongAnXuLy : BaseEntity
    {
        public Guid GUID { get; set; }
        public int TYPE { get; set; }
        public int LOAI_BIEN { get; set; } //đã tồn tại hay chưa tồn tại trong database
        public string VALUE { get; set; }
        public string NAME { get; set; }
        public string VALUE_DEFAULT { get; set; }
        public List<ThuocTinhPhuongAnXuLy> THUOC_TINH { get; set; }
        public List<SelectListItem> OPTION { get; set; }
        public bool IS_VALIDATE { get; set; }
    }
    public static class enumMaPhuongAnXuLy 
    {
        public static string DIEU_CHUYEN = "DC";
        public static string BAN = "BAN";
        public static string CN = "CN";
        public static string DN = "DN";
        public static string TH = "TH";
        public static string NSNN = "NSNN";
        public static string KHAC = "KHAC";
        
    
    }
}



