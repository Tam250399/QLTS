//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 18/5/2020
//----------------------------------------------------------------------------------------------------------------------
using System;

namespace GS.Core.Domain.Api
{
    public class XuLyApi : BaseEntity
    {
        public XuLyApi()
        {

        }
        public String QUYET_DINH_SO { get; set; }
        public DateTime? QUYET_DINH_NGAY { get; set; }
        public DateTime? NGAY_XU_LY { get; set; }
        public String MA_DON_VI { get; set; }
        public String HINH_THUC { get; set; }
        public Decimal? CHI_PHI { get; set; }
        public String GHI_CHU { get; set; }
        public Decimal? LOAI_XU_LY_ID { get; set; }
        #region Chi tiết
        public Decimal? SO_LUONG { get; set; }
        public Decimal? DIEN_TICH { get; set; }
        public Decimal? NGUYEN_GIA { get; set; }
        public Decimal? GIA_TRI { get; set; }
        public Decimal? GIA_TRI_GHI_TANG { get; set; }
        public Decimal? GIA_TRI_NSNN { get; set; }
        public Decimal? GIA_TRI_TKTG { get; set; }
        public String MA_PHUONG_AN_XU_LY { get; set; }
        public String MA_HINH_THUC_XU_LY { get; set; }
        public Decimal? CHI_PHI_XU_LY { get; set; }
        public String HOP_DONG_SO { get; set; }
        public DateTime? HOP_DONG_NGAY { get; set; }
        public String GHI_CHU_CHI_TIET { get; set; }
        public String MA_DON_VI_CHUYEN { get; set; }
        #endregion
    }
}
