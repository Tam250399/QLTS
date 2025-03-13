using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.ConsumingApi.TaiSanToanDan
{
	public class DuLieuDongBoPhuongAnXuLy
	{
		public DuLieuDongBoPhuongAnXuLy()
		{
			this.data = new List<Kho_PhuongAnXuLy>();
		}

		public List<Kho_PhuongAnXuLy> data { get; set; }
	}
	public class Kho_PhuongAnXuLy
	{
		public Kho_PhuongAnXuLy()
		{
			handlingPlan = new handlingDecisionPlan();
			assets = new List<handlingPlanNationalPublicAsset>();
		}
		public handlingDecisionPlan handlingPlan { get; set; }
		public IList<handlingPlanNationalPublicAsset> assets { get; set; }
	}
	public class handlingDecisionPlan
	{
		public string syncId { get; set; }
		public string decisionNumber { get; set; }
		public string decisionDate { get; set; }
		public string decider { get; set; }
		public string createdDate { get; set; }
		public int unitId { get; set; }
		public int? issuedUnitId { get; set; }
		public string issuedUnitName { get; set; }
		public string notes { get; set; }
		//public long? id { get; set; }
	}
	public class handlingPlanNationalPublicAsset {
		//đồng bộ phương án xử lý
		public string syncId { get; set; }
		public long assetId { get; set; }
		public int? handlingFormId { get; set; }
		public int? handlingMethodId { get; set; }
		public int? planId { get; set; }
		public double? handlingCalculationUnitValue { get; set; }
		public string handlingCalculationUnitType { get; set; }
		//public long? id { get; set; }
	}
}
