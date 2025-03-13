using GS.Web.Framework.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.CauHinh
{
	public class DieuKienLocTaiSanModel : BaseGSEntityModel
	{
		public DieuKienLocTaiSanModel()
		{
			ddlLoaiDuLieuId = new List<SelectListItem>();
		}
		public string MA_LOC_TAI_SAN { get; set; }
		public int? LOAI_DU_LIEU_ID { get; set; }
		public string TEN_LOAI_DU_LIEU { get; set; }
		public string TEN_HIEN_THI { get; set; }
		public string VALUE { get; set; }
		public List<SelectListItem> ddlLoaiDuLieuId { get; set; }
	}
	public class  DieuKienLocTaiSanSearchModel : BaseSearchModel
	{
		public string KeySearch { get; set; }
		public int? LOAI_DU_LIEU_ID { get; set; }
		public List<SelectListItem> DDL_LOAI_DU_LIEU { get; set; }
	}

	public class  DieuKienLocTaiSanListModel : BasePagedListModel<DieuKienLocTaiSanModel>
	{

	}
}
