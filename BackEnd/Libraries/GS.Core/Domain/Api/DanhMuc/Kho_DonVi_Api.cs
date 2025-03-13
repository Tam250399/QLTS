using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.Api.DanhMuc
{
    public class Kho_DonVi_Api
    {
        public string syncId { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string nationalBudgetCode { get; set; }
        public int? unitLevelId { get; set; }
        public int? unitTypeId { get; set; }
        public int? parentId { get; set; }
        public int? regionId { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public string fax { get; set; }
        public int accountingStandard { get; set; }
        public bool? isActive { get; set; }
        public int? actionType { get; set; }
        public long? id { get; set; }
        public string syncParentId { get; set; }
        public int? administrativeLevel { get; set; }
        public long? functionalUnitType { get; set; }
    }
}
