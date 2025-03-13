using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.ConsumingApi.DanhMucApi
{
    public class Kho_MucDichSuDung
    {
        public long? id { get; set; }
        public string syncId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int? actionType { get; set; }
        public int? assetTypeId { get; set; }
        public List<long?> assetTypeIds { get; set; }
    }
}
