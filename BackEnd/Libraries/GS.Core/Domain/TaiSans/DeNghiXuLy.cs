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
    public partial class DeNghiXuLy : BaseEntity
    {
        public Decimal DON_VI_ID { get; set; }
        public DateTime NGAY_DE_NGHI { get; set; }
        public DateTime NGAY_XU_LY { get; set; }
        public String NOI_DUNG { get; set; }
        public String GHI_CHU { get; set; }
        public String SO_PHIEU { get; set; }

    }
}



