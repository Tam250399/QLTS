using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.Api.DanhMuc
{
    public class Kho_CheDoHaoMon_Api
    {
        public int? id { get; set; }
        public int actionType { get; set; }
        public string syncId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public bool isActive { get; set; }
    }
}
