//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Core.Domain.DanhMuc;
using GS.Web.Framework.Models;
using GS.Web.Validators.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace GS.Web.Models.DanhMuc
{
	[Validator(typeof(DoiTacValidator))]
	public class DoiTacModel : BaseGSEntityModel
	{
		public DoiTacModel()
		{
		}
		public String MA { get; set; }
		public String TEN { get; set; }
		public String DAI_DIEN { get; set; }
		public String DIA_CHI { get; set; }
		public String DIEN_THOAI { get; set; }
		public String MO_TA { get; set; }
		public Decimal? LOAI_DOI_TAC_ID { get; set; }
		public Decimal? DON_VI_BO_PHAN_ID { get; set; }
		public Decimal DON_VI_ID { get; set; }

		//add more
		public String TEN_LOAI_DOI_TAC { get; set; }
		public String TenDonViBoPhan { get; set; }
		public IList<SelectListItem> AvailableDonViBoPhan { get; set; }
		public enumLOAI_DOI_TAC enumLOAI_DOI_TAC { get; set; }
		public SelectList dllLoaiDoiTac { get; set; }
	}
	public partial class DoiTacSearchModel : BaseSearchModel
	{
		public DoiTacSearchModel()
		{
		}
		public string KeySearch { get; set; }
		public Decimal? donviId { get; set; }
		public String TenDonVi { get; set; }
	}
	public partial class DoiTacListModel : BasePagedListModel<DoiTacModel>
	{

	}

	#region Import DoiTac (DonViChoThue - ThienHoang)
	public class IMP_DoiTacModel{
		public IMP_DoiTacModel()	
		{

		}
		public String MA { get; set; }
		public String TEN { get; set; }
		public String DAI_DIEN { get; set; }
		public String DIA_CHI { get; set; }
		public String DIEN_THOAI { get; set; }
		public String MO_TA { get; set; }
		public Decimal? LOAI_DOI_TAC_ID { get; set; }
		public Decimal? DON_VI_BO_PHAN_ID { get; set; }
		public Decimal DON_VI_ID { get; set; }
		public String MA_DON_VI { get; set; }
		public String MA_DON_VI_BO_PHAN { get; set; }
		//add more
		public String MESSAGE { get; set; }
	}
	#endregion Import DoiTac (DonViChoThue - ThienHoang)
}

