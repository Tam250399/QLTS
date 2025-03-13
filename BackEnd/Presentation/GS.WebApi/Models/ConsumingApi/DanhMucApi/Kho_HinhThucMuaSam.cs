using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.ConsumingApi.DanhMucApi
{
    public class Kho_HinhThucMuaSam
    {
        public long? id { get; set; }
        public int  actionType { get; set; }
        public string syncId { get; set; }
        public string code { get; set; }
        public string  name { get; set; }

    }
}
