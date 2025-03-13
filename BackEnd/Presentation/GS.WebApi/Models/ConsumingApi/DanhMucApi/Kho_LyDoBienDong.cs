using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.ConsumingApi.DanhMucApi
{
    public class Kho_LyDoBienDong
    {
        public Kho_LyDoBienDong()
        {
            this.assetTypeIds = new List<long>();
        }
        public int? actionType { get; set; }
        public string syncId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public List<long> assetTypeIds { get; set; }
        public long? causeTypeId { get; set; }
        public long? displayOrder { get; set; }
        public long? id { get; set; }
    }
}
