//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Framework.Mvc.ModelBinding;
using GS.Web.Validators.HeThong;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.HeThong
{
    [Validator(typeof(VaiTroValidator))]
    public class VaiTroModel : BaseGSEntityModel
    {
        public VaiTroModel()
        {
            SelectedQuyen = new List<int>();
            ListQuyenIds = new List<SelectListItem>();
            ListQuyenModel = new List<QuyenModel>();
            ListNguoiDungModel = new List<NguoiDungModel>();
            ListWidgetAvailable = new List<SelectListItem>();
        }
        [GSResourceDisplayName("Mã")]
        public String MA { get; set; }
        [GSResourceDisplayName("Tên")]
        public String TEN { get; set; }
        [UIHint("DateNullable")]
        [GSResourceDisplayName("Ngày tạo")]
        public DateTime? NGAY_TAO { get; set; }
        public IList<int> SelectedQuyen { get; set; }
        [GSResourceDisplayName("Danh sách quyền")]
        public List<SelectListItem> ListQuyenIds { get; set; }
        public IList<int> lstQuyen { get; set; }
        public QuyenSearchModel QuyenSearchModel { get; set; }
        public IList<string> lstIdquyen { get; set; }
        public IList<QuyenModel> ListQuyenModel { get; set; }
        public IList<NguoiDungModel> ListNguoiDungModel { get; set; }
        public decimal QUYEN_ID { get; set; }
        public IList<int> lstNguoiDung { get; set; }
        public IList<SelectListItem> ListWidgetAvailable { get; set; }
        public IList<int> SelectedWidgetIds { get; set; }
		public bool IsPhanQuyen { get; set; }
	}
    public partial class VaiTroSearchModel : BaseSearchModel
    {
        public VaiTroSearchModel()
        {
        }
        [GSResourceDisplayName("Từ khóa")]
        public string KeySearch { get; set; }
        public string SearchQuyen { get; set; }
    }
    public partial class VaiTroListModel : BasePagedListModel<VaiTroModel>
    {

    }
}

