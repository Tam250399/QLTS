using GS.Web.Framework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.KeToan
{
	public class HaoMonTaiSanModel : BaseGSEntityModel
	{
		public HaoMonTaiSanModel()
		{
		}

		public Decimal TAI_SAN_ID { get; set; }
		public String MA_TAI_SAN { get; set; }
		public String TAI_SAN_TEN { get; set; }
		[UIHint("InputYear")]
		public Decimal NAM_HAO_MON { get; set; }
		[UIHint("InputAddon")]
		public Decimal? GIA_TRI_HAO_MON { get; set; }
		[UIHint("InputAddon")]
		public Decimal? TONG_HAO_MON_LUY_KE { get; set; }
		[UIHint("InputAddon")]
		public Decimal TONG_GIA_TRI_CON_LAI { get; set; }
		public Decimal? TY_LE_HAO_MON { get; set; }
		[UIHint("InputAddon")]
		public Decimal? TONG_NGUYEN_GIA { get; set; }
		[UIHint("InputAddon")]
		public Decimal? TONG_HAO_MON_LUY_KE_NAM_TRUOC { get; set; }
		public Decimal TRANG_THAI_DONG_BO { get; set; }
		public bool IsLock { get; set; }

	}
	public partial class HaoMonTaiSanSearchModel : BaseSearchModel
	{
		public HaoMonTaiSanSearchModel()
		{
			ddlLoaiHinhTaiSan = new List<SelectListItem>();
		}

		public string KeySearch { get; set; }
		[UIHint("InputYear")]
		public decimal NamHaoMon { get; set; }
		public decimal LoaiHinhTaiSan { get; set; }
		public List<SelectListItem> ddlLoaiHinhTaiSan { get; set; }
	}

	public partial class HaoMonTaiSanListModel : BasePagedListModel<HaoMonTaiSanModel>
	{
	}
	public class PairGTCLAndHaoMon
	{
		public decimal GIA_TRI_HAO_MON { get; set; }
		public decimal TONG_GIA_TRI_CON_LAI { get; set; }
		public decimal TONG_HAO_MON_LUY_KE { get; set; }
	}
}