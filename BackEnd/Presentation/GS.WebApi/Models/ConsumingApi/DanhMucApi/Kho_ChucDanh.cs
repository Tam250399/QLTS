using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.ConsumingApi.DanhMucApi
{
    public class Kho_ChucDanh
    {
        public long? id { get; set; }
        public int? actionType { get; set; }
        public string syncId { get; set; }
        public string syncDate { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        /// <summary>
        /// 1: bộ(trung ương)
        /// 2: Tỉnh (địa phương)
        /// </summary>
        public int? administrativeLevel { get; set; }
        public long? personalCarPriceLimit { get; set; }
    }
}
