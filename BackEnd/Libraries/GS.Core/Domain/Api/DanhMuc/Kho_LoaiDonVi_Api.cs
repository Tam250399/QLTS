using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.Api.DanhMuc
{
    public class Kho_LoaiDonVi_Api
    {
        public string syncId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public long? parentId { get; set; }
        // public int? accountingStandard { get; set; }
        public int actionType { get; set; }
        public string syncParentId { get; set; }
        public long? accountingStandard { get; set; }
        public long? displayOrder { get; set; }
        public long? id { get; set; }

    }
}
