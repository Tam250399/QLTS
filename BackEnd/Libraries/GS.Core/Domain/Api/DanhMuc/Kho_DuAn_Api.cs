using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.Api.DanhMuc
{
    public class Kho_DuAn_Api
    {
        public int? id { get; set; }
        public int? actionType { get; set; }
        public string syncId { get; set; }
        public string syncDate { get; set; }
        public string syncUserName { get; set; }
        public int? unitId { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string decisionNumber { get; set; }
        public long? expense { get; set; }
        public long? expenseStateBudget { get; set; }
        public long? expenseOda { get; set; }
        public long? expenseAid { get; set; }
        public long? expenseOther { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string investor { get; set; }
        public string location { get; set; }
        public string notes { get; set; }
    }
}
