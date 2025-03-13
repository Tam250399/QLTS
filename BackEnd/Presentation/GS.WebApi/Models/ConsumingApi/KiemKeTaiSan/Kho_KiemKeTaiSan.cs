using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.ConsumingApi.KiemKeTaiSan
{
    public class Kho_KiemKeTaiSan
    {
        public assetInventoryReport assetInventoryReport { get; set; }
        public List<assetInventoryDetails> assetInventoryDetails { get; set; }
    }
    public class assetInventoryReport
    {
        public long? unitId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        /// <summary>
        /// hình thức kiểm kê
        /// </summary>
        public int? inventoryForm { get; set; }
        public string inventoryFormOther { get; set; }
        public string createdDate { get; set; }
        public string inventoryDate { get; set; }
        public string approvedDate { get; set; }

        public string approver { get; set; }

        public string inventoryCommittee { get; set; }
        public int? status { get; set; }
        public string notes { get; set; }
    }
    public class assetInventoryDetails
    {
        public List<assetInventoryMutations> assetInventoryMutations { get; set; }
        public string assetCode { get; set; }
        public string assetName { get; set; }
        public long? originalValue { get; set; }
        public long? residualValue { get; set; }

        public int? quantity { get; set; }
        public int? quantityInventory { get; set; }
        public int? quantityDifference { get; set; }
        public string usageState { get; set; }
        public string handlingProposed { get; set; }
        public string handlingResult { get; set; }
        public string notes { get; set; }
    }
    public class assetInventoryMutations
    {
        
    }
}
