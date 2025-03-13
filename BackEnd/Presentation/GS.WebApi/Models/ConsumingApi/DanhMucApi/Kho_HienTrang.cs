using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.ConsumingApi.DanhMucApi
{
    public class Kho_HienTrang
    {
        public Kho_HienTrang()
        {
            this.assetTypeIds = new List<long?>();
        }    
        public string code { get; set; }
        public string name { get; set; }
        public int actionType { get; set; }
        public string syncId { get; set; }
        public long? id { get; set; }
        public bool isAreaUsage { get; set; }
        public List<long?> assetTypeIds { get; set; }
    }
}
