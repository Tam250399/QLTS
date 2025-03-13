using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.Api.DanhMuc
{
    public class Kho_HienTrang_Api
    {
        public string code { get; set; }
        public string name { get; set; }
        public int actionType { get; set; }
        public string syncId { get; set; }
        public long? id { get; set; }
        public bool isAreaUsage { get; set; }
        public List<long> assetTypeIds { get; set; }
    }
}
