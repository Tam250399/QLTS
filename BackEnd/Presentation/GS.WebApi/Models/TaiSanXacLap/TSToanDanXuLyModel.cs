using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.WebApi.Validator.TaiSanXacLap;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.TaiSanXacLap
{
    [Validator(typeof(TSToanDanXuLyValidator))]
    public class TSToanDanXuLyModel : BaseGSApiModel
    {
        public TSToanDanXuLyModel()
        {
          
        }
        public Guid GUID { get; set; }
        //[Required(ErrorMessage = "TAI_SAN_ID null")]
        public Decimal? TAI_SAN_ID { get; set; }
        //[Required(ErrorMessage = "XU_LY_ID null")]
        public Decimal? XU_LY_ID { get; set; }
        //[Required(ErrorMessage = "SO_LUONG null")]
        public Decimal? SO_LUONG { get; set; }
        //[Required(ErrorMessage = "PHUONG_AN_XU_LY_ID null")]
        public Decimal? PHUONG_AN_XU_LY_ID { get; set; }
        //[Required(ErrorMessage = "HINH_THUC_XU_LY_ID null")]
        //public Decimal? HINH_THUC_XU_LY_ID { get; set; }
        public String GHI_CHU { get; set; }
        public String MA_PHUONG_AN_XU_LY { get; set; }
        public String MA_HINH_THUC_XU_LY { get; set; }
        public Decimal? TYPE_ID { get; set; }
        public List<KetQuaXuLyTaiSanModel> LTS_KET_QUA_TSXL { get; set; }
        public string DB_ID { get; set; }
        public string DB_XU_LY_ID { get; set; }
        public string DB_TAI_SAN_ID { get; set; }
    }
}
