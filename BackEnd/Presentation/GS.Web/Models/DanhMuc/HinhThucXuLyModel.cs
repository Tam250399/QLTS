//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.DanhMuc
{
    [Validator(typeof(PhuongAnXuLyValidator))]
	public class PhuongAnXuLyModel :BaseGSEntityModel
	{
        public PhuongAnXuLyModel(){
        
        }
		public String MA {get;set;}
		public String TEN {get;set;}
		public Decimal? SAP_XEP {get;set;}
        public String CONFIG_CAU_HINH { get; set; }
    }
    public partial class PhuongAnXuLySearchModel :BaseSearchModel {
        public PhuongAnXuLySearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class PhuongAnXuLyListModel : BasePagedListModel<PhuongAnXuLyModel>
    {
       
    }
    public partial class ThuocTinhPhuongAnXuLyModel: BaseGSEntityModel
    {
        public ThuocTinhPhuongAnXuLyModel()
        {
            this.GUID = Guid.NewGuid();
            this.THUOC_TINH = new List<ThuocTinhPhuongAnXuLyModel>();
            this.OPTION = new List<SelectListItem>();
            this.ddlLoaiDuLieu = new List<SelectListItem>();
            this.ValueListInt = new List<int>();
            this.IS_VALIDATE = false;
            this.Is_Default = false;
        }
        public Guid GUID { get; set; }
        public int TYPE { get; set; }
        public int LOAI_BIEN { get; set; } //đã tồn tại hay chưa tồn tại trong database
        public string VALUE { get; set; }
        public string NAME { get; set; }
        public string VALUE_DEFAULT { get; set; }
        public List<ThuocTinhPhuongAnXuLyModel> THUOC_TINH { get; set; }
        public List<SelectListItem> OPTION { get; set; }      
        public bool IS_VALIDATE { get; set; }
        public Guid GuidView { get; set; }
        public Guid GuidParrent { get; set; }
        public int LoaiDuLieuId { get; set; }
        public int LoaiBienId { get; set; }
        public int LoaiDuLieuIdParrent { get; set; }
        public int stt { get; set; }
        public bool is_viewtong { get; set; }
        public List<SelectListItem> ddlLoaiDuLieu { get; set; }
        public int ValueSo { get; set; }
        public string ValueChuoi { get; set; }
        [UIHint("DateNullable")]
        public DateTime? ValueNgayThang { get; set; }
        public List<int> ValueListInt { get; set; }
        public string ValueTT { get; set; }
        public string NameTT { get; set; }
        public int SapXep { get; set; }
        public int ThuocTinhId { get; set; }
        public List<int> Value_Default { get; set; }
        public bool Is_Default { get; set; }

    }
}

