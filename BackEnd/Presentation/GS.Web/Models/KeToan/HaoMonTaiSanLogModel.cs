using FluentValidation.Attributes;
using GS.Core.Domain.BienDongs;
using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.KeToan
{
	public class HaoMonTaiSanLogModel : BaseGSEntityModel
	{
		public HaoMonTaiSanLogModel(BienDong _bd)
		{
			TAI_SAN_ID = _bd.TAI_SAN_ID;
			NGAY_TINH = _bd.NGAY_BIEN_DONG;
			NAM_TINH = _bd.NGAY_BIEN_DONG.Value.Year;
		}
		public HaoMonTaiSanLogModel()
		{
		}
		public Decimal? TAI_SAN_ID { get; set; }
		public DateTime? NGAY_TINH { get; set; }
		public Decimal? NAM_TINH { get; set; }
	}
}
