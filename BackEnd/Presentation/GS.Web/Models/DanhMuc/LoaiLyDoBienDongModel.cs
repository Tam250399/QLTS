//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 3/10/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.DM;

namespace GS.Web.Models.DM
{
    [Validator(typeof(LoaiLyDoBienDongValidator))]
	public class LoaiLyDoBienDongModel :BaseGSEntityModel
	{
        public LoaiLyDoBienDongModel(){
        
        }
		public String MA {get;set;}
		public String TEN {get;set;}
		public Decimal? PARENT_ID {get;set;}
		public String TREE_NODE {get;set;}
		public Decimal? TREE_LEVEL {get;set;}
	}
    public partial class LoaiLyDoBienDongSearchModel :BaseSearchModel {
        public LoaiLyDoBienDongSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class LoaiLyDoBienDongListModel : BasePagedListModel<LoaiLyDoBienDongModel>
    {
       
    }
}

