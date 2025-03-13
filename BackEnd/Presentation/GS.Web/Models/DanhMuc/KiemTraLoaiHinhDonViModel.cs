using FluentValidation.Attributes;
using GS.Web.Validators.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.DanhMuc
{
	[Validator(typeof(KiemTraLoaiHinhDonViValidator))]
	public class KiemTraLoaiHinhDonViModel
	{
		public string DonViIds { get; set; }
		public Decimal? LOAI_CAP_DON_VI_ID { get; set; }
		public SelectList ddlLoaiCapDonVi { get; set; }
		public Decimal? CAP_DON_VI_ID { get; set; }
		public SelectList dllCapDonVi { get; set; }
		public string TEN_DIA_BAN { get; set; }
		public Decimal? DIA_BAN_ID { get; set; }
		public Decimal? LOAI_DON_VI_ID { get; set; }
		public IList<SelectListItem> dllLoaiDonVi { get; set; }
	}
}
