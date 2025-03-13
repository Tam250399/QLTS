using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.Api.DanhMuc
{
    public class Kho_DiaBan_Api
    {
        public int? actionType { get; set; }
        public string districtId { get; set; }
        public string name { get; set; }
        public bool isActive { get; set; }
        public long? id { get; set; }
        public string syncParentId { get; set; }
        public string syncId { get; set; }
        public string provinceId { get; set; }
        public string code { get; set; }
    }
}
