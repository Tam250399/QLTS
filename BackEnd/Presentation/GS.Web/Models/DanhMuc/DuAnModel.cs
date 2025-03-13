//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.DanhMuc
{
	[Validator(typeof(DuAnValidator))]
	public class DuAnModel : BaseGSEntityModel
	{
		public DuAnModel()
		{
			dllDuAn = new List<SelectListItem>();
		}
		public String MA { get; set; }
		public String TEN { get; set; }
		public Decimal? LOAI_DU_AN_ID { get; set; }
		[UIHint("DateNullable")]
		public DateTime? NGAY_BAT_DAU { get; set; }
		[UIHint("DateNullable")]
		public DateTime? NGAY_KET_THUC { get; set; }
		[UIHint("InputAddon")]
		public Decimal? TONG_KINH_PHI { get; set; }
		public Decimal? HINH_THUC_ID { get; set; }
		public String SO_QUYET_DINH_PHE_DUYET { get; set; }
		public String CHU_DAU_TU { get; set; }
		public String DIA_DIEM { get; set; }
		public String NGUON_VON { get; set; }
		public String GHI_CHU { get; set; }
		[UIHint("InputAddon")]
		public Decimal? NGUON_NS { get; set; }
		[UIHint("InputAddon")]
		public Decimal? NGUON_ODA { get; set; }
		[UIHint("InputAddon")]
		public Decimal? NGUON_VIEN_TRO { get; set; }
		[UIHint("InputAddon")]
		public Decimal? NGUON_KHAC { get; set; }
		public String KIEU { get; set; }
		public String CO_QUAN_TAI_CHINH { get; set; }
		public String MA_LOAI_DU_AN { get; set; }
		public String MA_NHOM_DU_AN { get; set; }
		public String TEN_NHOM_DU_AN { get; set; }
		public String MA_HT { get; set; }
		public String TEN_HT { get; set; }
		public String HT_QUANLY { get; set; }
		public Boolean? HIEU_LUC { get; set; }
		public String MA_DVQHNS { get; set; }
		public String MA_DU_AN_CU { get; set; }
		public Boolean? TRANG_THAI_ID { get; set; }
		public Decimal? DON_VI_ID { get; set; }
		public Decimal? DON_VI_CU_ID { get; set; }

		//add more
		public String strVNTongKinhPhi { get; set; }
		public IList<SelectListItem> dllDuAn { get; set; }
		public int pageIndex { get; set; }
	}
	public partial class DuAnSearchModel : BaseSearchModel
	{
		public DuAnSearchModel()
		{
		}
		public string KeySearch { get; set; }
		public Decimal? donviId { get; set; }
		public Boolean? TRANG_THAI_ID { get; set; }
		public Decimal? LOAI_DU_AN_ID { get; set; }
		public String TenDonVi { get; set; }
		public String TenDonViCha { get; set; }
		public int? pageIndex { get; set; }
	}
	public partial class DuAnListModel : BasePagedListModel<DuAnModel>
	{

	}
}

