using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.ConsumingApi.TaiSanToanDan
{
    public class Kho_XuLyTaiSanToanDan
    {

    }
    public class handlingDecision
    {
        public string syncId { get; set; }
        public string syncDate { get; set; }
        public string syncUserName { get; set; }
        public string decisionNumber { get; set; }
        public string decisionDate { get; set; }

        public string handlingDate { get; set; }
        public int? handlingUnitId { get; set; }
        public string handlingForm { get; set; }
        public long? handlingCost { get; set; }
        public string notes { get; set; }
        public string createdDate { get; set; }
        public bool status { get; set; }
        public string deletedDate { get; set; }
        public int deletedUserId { get; set; }
		public long? id { get; set; }
	}
    public class nationalPublicAssetHandlings
    {
        public string syncId { get; set; }
        public string syncDate { get; set; }
        public string syncUserName { get; set; }
        public long? assetId { get; set; }
        public long? decisionId { get; set; }
        public long? quantity { get; set; }
        public float? volume { get; set; }
        public float? area { get; set; }
        public long? originalValue { get; set; }
        public long? residualValue { get; set; }
        public long? increasementValue { get; set; }
        public long? valueObtained { get; set; }
        public long? valueToBudget { get; set; }
        public long? valueTktg { get; set; }
        public int? handlingFormId { get; set; }
        public int? handlingMethodId { get; set; }
        public long? handlingCost { get; set; }
        public string contractNumber { get; set; }
        public string contractDate { get; set; }
        public int? transferUnitId { get; set; }
        public string notes { get; set; }
        public string deletedDate { get; set; }

        public int? deletedUserId { get; set; }
		public long? id { get; set; }


	}
}
