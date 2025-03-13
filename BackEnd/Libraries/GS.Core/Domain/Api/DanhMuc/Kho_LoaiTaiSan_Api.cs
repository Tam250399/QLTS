using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.Api.DanhMuc
{
    public class Kho_LoaiTaiSan_Api
    {
        public int? actionType { get; set; }
        public long? id { get; set; }
        public string syncId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int? parentId { get; set; }
        public string syncParentId { get; set; }
        public long? treeId { get; set; }
        public string syncTreeId { get; set; }
        public bool isActive { get; set; }
        public long? depreciationPeriod { get; set; }
        public long? depreciationRate { get; set; }
        public long? amortizationPeriod { get; set; }
        public float? amortizationRate { get; set; }
        public string calculationUnit { get; set; }
        public int assetTypeGroupId { get; set; }
    }
}
