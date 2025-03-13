//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using GS.Core.Domain.TaiSans;
using GS.Web.Framework.Models;
using GS.Web.Validators.TaiSans;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.TaiSans
{
    [Validator(typeof(TaiSanKiemKeHoiDongValidator))]
	public class TaiSanKiemKeHoiDongModel :BaseGSEntityModel
	{
        public TaiSanKiemKeHoiDongModel(){
        
        }
		public Decimal KIEM_KE_ID {get;set;}
		public String HO_TEN {get;set;}
		public String CHUC_VU {get;set;}
		public String DAI_DIEN {get;set;}
		public Decimal? VI_TRI_ID {get;set;}
        // more
        public bool isFirst { get; set; }
        public SelectList DDLViTriHoiDong { get; set; }
        public enumViTriKiemKeHoiDong viTriKiemKeHoiDong { get; set; }
        public string TenViTri { get; set; }

    }
    public partial class TaiSanKiemKeHoiDongSearchModel :BaseSearchModel {
        public TaiSanKiemKeHoiDongSearchModel()
        {
        }
        public string KeySearch {get;set;}
        public int KiemKeId { get; set; }
    }
   public partial class TaiSanKiemKeHoiDongListModel : BasePagedListModel<TaiSanKiemKeHoiDongModel>
    {
       
    }
}

