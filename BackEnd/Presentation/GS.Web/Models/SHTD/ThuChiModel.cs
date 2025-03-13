//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 7/12/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.SHTD;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.SHTD
{
    [Validator(typeof(ThuChiValidator))]
	public class ThuChiModel :BaseGSEntityModel
	{
        public ThuChiModel(){
            Guid = Guid.NewGuid();
            listModel = new List<ThuChiModel>();
            this.DDLKetQuaXuLy = new List<SelectListItem>();
            Is_Disable = true;
        }
		public Decimal? KET_QUA_ID {get;set;}
        [UIHint("InputAddon")]
        public Decimal? SO_TIEN_PHAI_THU {get;set;}
        [UIHint("InputAddon")]
        public Decimal? SO_TIEN_CON_PHAI_THU { get; set; }
        [UIHint("InputAddon")]
        public Decimal? SO_TIEN_THU {get;set;}
        [UIHint("DateNullable")]
        public DateTime? NGAY_THU {get;set;}
        [UIHint("DateNullable")]
        public DateTime? NGAY_THU_FIRST { get; set; }
        [UIHint("InputAddon")]
        public Decimal? TG_SO_TIEN {get;set;}
        [UIHint("DateNullable")]
        public DateTime? TG_NGAY_NOP {get;set;}
        [UIHint("InputAddon")]
        public Decimal? CHI_PHI {get;set;}
        [UIHint("InputAddon")]
        public Decimal? SO_TIEN_NOP_NSNN { get; set; }
        public Decimal? XU_LY_ID { get; set; }
        
        public string DB_ID { get; set; }
        public string DB_XU_LY_ID { get; set; }
        public String TenKetQua { get; set; }
        public Boolean Is_First { get; set; }
        public Boolean Is_Disable { get; set; }
        public Guid Guid { get; set; }
        //Thêm vào xử lý chọn nhiều paxl
        public string LIST_XU_LY_ID { get; set; }
        public IList<int> SelectedXuLyIds { get; set; }
        public List<SelectListItem> DDLKetQuaXuLy { get; set; }
        public decimal? DON_VI_ID { get; set; }
        public List<ThuChiModel> listModel { get; set; }
    }
    public partial class ThuChiSearchModel :BaseSearchModel {
        public ThuChiSearchModel()
        {
        }
        public string KeySearch {get;set;}
        public decimal KetQuaId { get; set; }
        public string ListXuLyIdString { get; set; }
    }
   public partial class ThuChiListModel : BasePagedListModel<ThuChiModel>
    {
       
    }
    public partial class ThuChiDongBoModel : BaseGSEntityModel
    {
        public Decimal? KET_QUA_ID { get; set; }
        public Decimal? SO_TIEN_PHAI_THU { get; set; }
        public Decimal? SO_TIEN_CON_PHAI_THU { get; set; }
        public Decimal? SO_TIEN_THU { get; set; }
        public DateTime? NGAY_THU { get; set; }
        public Decimal? TG_SO_TIEN { get; set; }
        public DateTime? TG_NGAY_NOP { get; set; }
        public Decimal? CHI_PHI { get; set; }
        public Decimal? XU_LY_ID { get; set; }
        public string DB_ID { get; set; }
        public string DB_XU_LY_ID { get; set; }
    }
}

