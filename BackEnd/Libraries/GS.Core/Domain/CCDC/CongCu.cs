//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/1/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.CCDC
{
    public enum enumTrangThaiCongCu
    {
        CHON = 0,
        DANG_SU_DUNG = 1,
        CHUA_SU_DUNG = 2,
        HONG_CHO_XU_LY = 3
    }
    public enum enumTrangThaiPhanBo
    {
        ChuaPhanBo = 0,
        DaPhanBo =1
    }
    public partial class CongCu : BaseEntity
    {
        public CongCu()
        {
            this.GUID = Guid.NewGuid();
        }
        public String MA { get; set; }
        public String MA_DB { get; set; }
        public Decimal? NHOM_CONG_CU_ID { get; set; }
        public String GHI_CHU { get; set; }
        public Decimal? NGUOI_TAO_ID { get; set; }
        public DateTime NGAY_TAO { get; set; }
        public Decimal DON_VI_ID { get; set; }
        public String TEN { get; set; }
        public Guid GUID { get; set; }
    }
}



