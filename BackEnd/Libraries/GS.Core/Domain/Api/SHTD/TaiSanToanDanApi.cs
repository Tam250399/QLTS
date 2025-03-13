//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 18/5/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace GS.Core.Domain.Api
{
    public class TaiSanToanDanApi : BaseEntity
    {
        public TaiSanToanDanApi()
        {
            this.ListXuLy = new List<XuLyApi>();
        }      
        public String TEN { get; set; }
        public String MA_NGUON_GOC_TAI_SAN { get; set; }
        public String MA_LOAI_TAI_SAN { get; set; }
        public Decimal? NGUYEN_GIA { get; set; }
        public Decimal? SO_LUONG { get; set; }
        public Decimal? KHOI_LUONG { get; set; }
        public Decimal? GIA_TRI { get; set; }
        public String GHI_CHU { get; set; }
        public Decimal? DIEN_TICH { get; set; }
        public List<XuLyApi> ListXuLy { get; set; }
    }
}
