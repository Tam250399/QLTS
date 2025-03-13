using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.ConsumingApi.TaiSanToanDan
{
	public class DuLieuDongBoKetQuaXuLy
	{
		public DuLieuDongBoKetQuaXuLy()
		{
			this.data = new List<Kho_KetQuaXuLy>();
		}

		public List<Kho_KetQuaXuLy> data { get; set; }
	}
	public class Kho_KetQuaXuLy
	{
		public Kho_KetQuaXuLy()
		{
			handlingResult = new handlingDecisionResult();
			assets = new List<handlingResultNationalPublicAsset>();
		}
		public handlingDecisionResult handlingResult { get; set; }
		public IList<handlingResultNationalPublicAsset> assets { get; set; }
	}
	public class handlingDecisionResult
	{
		public string syncId { get; set; }
		public long planId { get; set; }
		public string decisionNumber { get; set; }
		public string decisionDate { get; set; }
		public string decider { get; set; }
		public string createdDate { get; set; }
		public int unitId { get; set; }
		public string issuedUnitName { get; set; }
		public string notes { get; set; }
		//public long? id { get; set; }
	}
	public class handlingResultNationalPublicAsset
	{
		public string syncId { get; set; }
		public long planAssetId { get; set; }
		public long planId { get; set; }
		public int handlingFormId { get; set; }
		//public int handlingMethodId { get; set; }
		public double? handlingCalculationUnitValue { get; set; }
		public string handlingCalculationUnitType { get; set; }
		//public string handlingDate { get; set; }
		//public int? transferUnitId { get; set; }
		//public string transferDate { get; set; }
		//public string contractNumber { get; set; }
		//public string contractDate { get; set; }
		//public string documentNumber { get; set; }
		//public string documentDate { get; set; }
		//public string contractPartner { get; set; }
		//public long? assetValue { get; set; }
		//public long? valueObtained { get; set; }
		//public long? valueToBudget { get; set; }
		//public long? valueInCustody { get; set; }
		//public string otherDocuments { get; set; }
		//public string notes { get; set; }
		//public string transferUnitName { get; set; }
		//public long? id { get; set; }
	}
}
