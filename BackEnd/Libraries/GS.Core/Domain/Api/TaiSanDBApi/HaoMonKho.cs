using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.Api.TaiSanDBApi
{
    public class DuLieuHaoMonKho
    {
        public DuLieuHaoMonKho()
        {
            this.data = new List<HaoMonKhoModel>();
        }
        public List<HaoMonKhoModel> data { get; set; }
    }
    public class HaoMonKho : BaseViewEntity
    {
        [JsonIgnore]
        public decimal? TAI_SAN_ID { get; set; }
        public long? id { get; set; }
        public string assetCode { get; set; }
        public int year { get; set; }
        public float amortizationRate { get; set; }
        public Double amortizationValue { get; set; }
        public Double accumulatedAmortizationValue { get; set; }
        public long originalValue { get; set; }
        public Double residualValue { get; set; }
        public string syncSourceId { get; set; }

    }


    public class AssetCodeHaoMon : BaseViewEntity
    {
        public string assetCode { get; set; }
    }

    public class HaoMonKhoModel
    {
        public HaoMonKhoModel()
        {

        }
        public HaoMonKhoModel(HaoMonKho haoMonKho)
        {
            this.id = haoMonKho.id;
            this.assetCode = haoMonKho.assetCode;
            this.year = haoMonKho.year;
            this.amortizationRate = haoMonKho.amortizationRate;
            this.amortizationValue = haoMonKho.amortizationValue;
            this.accumulatedAmortizationValue = haoMonKho.accumulatedAmortizationValue;
            this.originalValue = haoMonKho.originalValue;
            this.residualValue = haoMonKho.residualValue;
            this.syncSourceId = haoMonKho.syncSourceId;
        }
        public string syncSourceId { get; set; }
        public long? id { get; set; }
        public string assetCode { get; set; }
        public int year { get; set; }
        public float amortizationRate { get; set; }
        public Double amortizationValue { get; set; }
        public Double accumulatedAmortizationValue { get; set; }
        public long originalValue { get; set; }
        public Double residualValue { get; set; }

    }
}
