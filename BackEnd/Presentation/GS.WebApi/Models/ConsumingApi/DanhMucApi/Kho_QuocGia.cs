using GS.Web.Framework.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.ConsumingApi.DanhMucApi
{
    public class Kho_QuocGia
    {
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int? actionType { get; set; }
        public string syncId { get; set; }
        public Int64? id { get; set; }
    }
}
