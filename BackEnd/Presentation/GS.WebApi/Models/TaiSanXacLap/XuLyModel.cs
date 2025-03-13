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
    [Validator(typeof(XuLyValidator))]
    public class XuLyModel : BaseGSApiModel
    {
        public XuLyModel()
        {

        }
        //[Required(ErrorMessage = "QUYET_DINH_SO null")]
        public String QUYET_DINH_SO { get; set; }
        //[Required(ErrorMessage = "QUYET_DINH_NGAY null")]
        public DateTime QUYET_DINH_NGAY { get; set; }
        public Decimal? CO_QUAN_BAN_HANH_ID { get; set; }
        public Decimal? DB_DON_VI_ID { get; set; }
        public String GHI_CHU { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public String NGUOI_QUYET_DINH { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        public Guid? GUID { get; set; }
        public Decimal? TYPE_ID { get; set; }
        public List<TSToanDanXuLyModel> LST_TS_XU_LY { get; set; }
        public List<ThuChiModel> LST_THU_CHI { get; set; }
        public string DB_ID { get; set; }
        //public String MA_LOAI_XU_LY { get; set; }
        #region Chi tiết
        public Decimal? SO_LUONG { get; set; }
        public Decimal? DIEN_TICH { get; set; }
        public Decimal? NGUYEN_GIA { get; set; }
        public Decimal? GIA_TRI { get; set; }
        public Decimal? GIA_TRI_GHI_TANG { get; set; }
        public Decimal? GIA_TRI_NSNN { get; set; }
        public Decimal? GIA_TRI_TKTG { get; set; }
        public Decimal? PHUONG_AN_XU_LY_ID { get; set; }
        public Decimal? DB_PHUONG_AN_XU_LY_ID { get; set; }
        public Decimal? HINH_THUC_XU_LY_ID { get; set; }
        public Decimal? DB_HINH_THUC_XU_LY_ID { get; set; }
        public Decimal? CHI_PHI_XU_LY { get; set; }
        public String HOP_DONG_SO { get; set; }
        public String HOP_DONG_NGAY { get; set; }
        public String GHI_CHU_CHI_TIET { get; set; }
        public Decimal DON_VI_CHUYEN_ID { get; set; }
        #endregion
    }
}
