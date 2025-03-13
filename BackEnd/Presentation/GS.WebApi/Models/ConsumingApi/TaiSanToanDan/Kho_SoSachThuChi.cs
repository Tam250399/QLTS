using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.ConsumingApi.TaiSanToanDan
{
	public class DuLieuDongBoSoSachThuChi
	{
		public DuLieuDongBoSoSachThuChi()
		{
			this.data = new List<Kho_SoSachThuChi>();
		}

		public List<Kho_SoSachThuChi> data { get; set; }
	}
	public class Kho_SoSachThuChi
	{
		public Kho_SoSachThuChi()
		{
			handlingAccounting = new handlingDecisionAccounting();
			assets = new List<handlingAccountingNationalPublicAsset>();
		}
		public handlingDecisionAccounting handlingAccounting { get; set; }
		public IList<handlingAccountingNationalPublicAsset> assets { get; set; }
	}
	public class handlingDecisionAccounting
	{
        public handlingDecisionAccounting()
        {
			planId = new List<int>();
        }
		public string syncId { get; set; }
		public IList<int> planId { get; set; }
		public long? valueRemaining { get; set; }
		public long valueEstimated { get; set; }
		public long valueObtained { get; set; }
		public string obtainedDate { get; set; }
		public long? valueInCustody { get; set; }
		public string custodyDate { get; set; }
		public long handlingCost { get; set; }
		public string createdDate { get; set; }
		public int unitId { get; set; }
		public string notes { get; set; }
		//public long? id { get; set; }
	}
	public class handlingAccountingNationalPublicAsset
	{
		public string syncId { get; set; }
		public long resultAssetId { get; set; }
		public long? valueRemaining { get; set; }
		public long valueEstimated { get; set; }
		public long valueObtained { get; set; }
		public string obtainedDate { get; set; }
		public long? valueInCustody { get; set; }
		public string custodyDate { get; set; }
		public long handlingCost { get; set; }
		public long? id { get; set; }
	}
}
