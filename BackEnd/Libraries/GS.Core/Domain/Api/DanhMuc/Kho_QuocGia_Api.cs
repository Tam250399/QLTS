using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.Api.DanhMuc
{
    public class Kho_QuocGia_Api
    {
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int? actionType { get; set; }
        public string syncId { get; set; }
        public Int64? id { get; set; }
    }
}
