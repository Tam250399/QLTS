using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.ConsumingApi.KhauHao_HaoMon
{
    public class Kho_HaoMon
    {
        public long? id { get; set; }
        public string assetCode { get; set; }
        public int? year { get; set; }
        public float? amortizationRate { get; set; }
        public long? amortizationValue { get; set; }
        public long? accumulatedAmortizationValue { get; set; }
        public long? originalValue { get; set; }
        public long? residualValue { get; set; }
    }
    public class Kho_KhauHao
    {
        public long? id { get; set; }
        public string assetCode { get; set; }
        public int? year { get; set; }
        public int? month { get; set; }
        public float? depreciationRate { get; set; }
        public long? depreciationValue { get; set; }
        public long? accumulatedDepreciationValue { get; set; }
        public long? originalValue { get; set; }
        public long? residualValue { get; set; }
    }
}
