using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.WebApi.Validator.TaiSanXacLap;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.TaiSanXacLap
{
	[Validator(typeof(ThuChiValidator))]
	public class ThuChiModel : BaseGSApiModel
	{
		public ThuChiModel()
		{

		}
		public Decimal? KET_QUA_ID { get; set; }
		//[Required(ErrorMessage = "SO_TIEN_PHAI_THU null")]
		public Decimal? SO_TIEN_PHAI_THU { get; set; }
		//[Required(ErrorMessage = "SO_TIEN_CON_PHAI_THU null")]
		public Decimal? SO_TIEN_CON_PHAI_THU { get; set; }
		//[Required(ErrorMessage = "SO_TIEN_THU null")]
		public Decimal? SO_TIEN_THU { get; set; }
		//[Required(ErrorMessage = "NGAY_THU null")]
		public DateTime? NGAY_THU { get; set; }
		public Decimal? TG_SO_TIEN { get; set; }
		public DateTime? TG_NGAY_NOP { get; set; }
		//[Required(ErrorMessage = "CHI_PHI null")]
		public Decimal? CHI_PHI { get; set; }
		//[Required(ErrorMessage = "XU_LY_ID null")]
		public Decimal? XU_LY_ID { get; set; }
		public string DB_ID { get; set; }
		public string DB_XU_LY_ID { get; set; }
		public Decimal? TYPE_ID { get; set; }
	}
}
