using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.Api
{
    public class TSToanDanXuLyApi : BaseEntity
    {
        public Decimal? SO_LUONG { get; set; }
        public Decimal? DIEN_TICH { get; set; }
        public Decimal? NGUYEN_GIA { get; set; }
        public Decimal? GIA_TRI { get; set; }
        public Decimal? GIA_TRI_GHI_TANG { get; set; }
        public Decimal? GIA_TRI_NSNN { get; set; }
        public String GIA_TRI_TKTG { get; set; }
        public String MA_PHUONG_AN_XU_LY { get; set; }
        public String MA_HINH_THUC_XU_LY { get; set; }
        public Decimal? CHI_PHI_XU_LY { get; set; }
        public String HOP_DONG_SO { get; set; }
        public DateTime? HOP_DONG_NGAY { get; set; }
        public String GHI_CHU { get; set; }
        public String MA_DON_VI_CHUYEN { get; set; }
    }
}
