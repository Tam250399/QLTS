using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.WebApi.Validator.BaoCaoSvc;
using System;
using System.Collections.Generic;

namespace GS.WebApi.Models.BaoCaoSvc
{
	[Validator(typeof(ThamSoNhanBaoCaoSvcValidator))]
	public class ThamSoNhanBaoCaoSvcSearchModel : BaseGSApiModel
	{
		//Thông tin yêu cầu
		public string MA_BAO_CAO { get; set; }

		public int LOAI_DATA_TRA_VE { get; set; }

		//Thông tin lọc báo cáo
		public int? NHOM_TAI_SAN_LON { get; set; }
		public IList<decimal> LIST_DON_VI_ID { get; set; }

		public decimal? DON_VI_TINH_GIA_TRI { get; set; }
		public decimal? DON_VI_TINH_DIEN_TICH { get; set; }
		public decimal? DON_VI_TINH_SO_LUONG { get; set; }
		public decimal? DON_VI_KHOI_LUONG { get; set; }
		public DateTime? NGAY_BAO_CAO { get; set; }
		public DateTime? TU_NGAY { get; set; }
		public DateTime? DEN_NGAY { get; set; }
		public decimal? NAM { get; set; }
		public int? BAC_DON_VI { get; set; }
		public int? CAP_DON_VI { get; set; }
		public int? BAC_TAI_SAN { get; set; }
		public IList<decimal> LIST_LOAI_HINH_TAI_SAN_ID { get; set; }
		public IList<decimal> LIST_LOAI_DON_VI_ID { get; set; }
		public IList<decimal> LIST_LY_DO_ID { get; set; }
		public Guid fileGuid { get; set; }
		public int? MAU_SO_ID { get; set; }
		public int? IS_TINH { get; set; }
		public int? IS_HUYEN { get; set; }
		public int? IS_XA { get; set; }
	}
}