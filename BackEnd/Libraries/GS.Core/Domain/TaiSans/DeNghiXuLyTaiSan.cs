//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/11/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.TaiSans
{
    public partial class DeNghiXuLyTaiSan : BaseEntity
    {
        public Decimal? DE_NGHI_XU_LY_ID { get; set; }
        public Decimal TAI_SAN_ID { get; set; }
        public String PHUONG_THUC_XU_LY { get; set; }
        public Decimal? LY_DO_BIEN_DONG_ID { get; set; }

    }
}



