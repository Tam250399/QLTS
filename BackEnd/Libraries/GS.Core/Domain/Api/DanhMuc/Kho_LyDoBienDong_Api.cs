using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.Api.DanhMuc
{
    public class Kho_LyDoBienDong_Api
    {
        public Kho_LyDoBienDong_Api()
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
