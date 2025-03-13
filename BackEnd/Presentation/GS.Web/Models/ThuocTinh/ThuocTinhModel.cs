//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.ThuocTinh;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.ThuocTinh
{
    [Validator(typeof(ThuocTinhValidator))]
	public class ThuocTinhModel :BaseGSEntityModel
	{
        public ThuocTinhModel(){
            this.ddlLoaiDuLieu = new List<SelectListItem>();
            this.ddlDonVi = new List<SelectListItem>();
            this.ddlLoaiTaiSanId = new List<SelectListItem>();
        }
		public String MA {get;set;}
		public String TEN {get;set;}
		public String CAU_HINH {get;set;}
        [UIHint("Date")]
		public DateTime? NGAY_TAO {get;set;}
		public Decimal? NGUOI_TAO_ID {get;set;}
		public Decimal? DON_VI_ID {get;set;}
        public int LoaiDuLieuId { get; set; }
        public List<SelectListItem> ddlLoaiDuLieu { get; set; }
        public List<SelectListItem> ddlDonVi { get; set; }
        public List<modelThuocTinh> ListThuocTinh { get; set; }
        public List<int> ListLoaiTaiSanId { get; set; }
        public List<SelectListItem> ddlLoaiTaiSanId { get; set; }
        public string LoaiDuLieu { get; set; }
        public string TenDonVi { get; set; }
        public string TenNguoiTao { get; set; }
        public int SapXep { get; set; }
    }
    public partial class ThuocTinhSearchModel :BaseSearchModel {
        public ThuocTinhSearchModel()
        {
        }
        public string KeySearch {get;set;}
    }
   public partial class ThuocTinhListModel : BasePagedListModel<ThuocTinhModel>
    {
       
    }
    public partial class modelThuocTinh : BaseGSEntityModel
    {
        public modelThuocTinh()
        {
            this.GUID = Guid.NewGuid();
            this.Ma = Guid.NewGuid();
            this.OPTION = new List<SelectListItem>();
            this.ddlLoaiDuLieu = new List<SelectListItem>();
            this.ValueListInt = new List<int>();
            this.IS_VALIDATE = false;
            this.Is_Default = false;
        }
        public Guid GUID { get; set; }
        public string NAME { get; set; }
        public string TYPE { get; set; }
        public string VALUE { get; set; }
        public string FIELD { get; set; }
        public List<modelThuocTinh> THUOC_TINH { get; set; }
        public List<SelectListItem> OPTION { get; set; }
        public bool IS_VALIDATE { get; set; }
        public Guid GuidParrent { get; set; }
        public Guid GuidView { get; set; }
        public Guid Ma { get; set; }
        public int LoaiDuLieuId { get; set; }
        public int LoaiDuLieuIdParrent { get; set; }
        public int stt { get; set; }
        public bool is_viewtong { get; set; }
        public List<SelectListItem> ddlLoaiDuLieu { get; set; }
        [UIHint("InputAddon")]
        public Decimal? ValueSo { get; set; }
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
        public String TenValueCombobox { get; set; }
    }
    public partial class modelValue
    {
        public Guid Guid { get; set; }
        public Guid GuidParrent { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}

