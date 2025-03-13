//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 22/3/2021
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.DB
{
    public partial class LogsDongBoCsdlqg : BaseEntity
    {
        public String UUID { get; set; }
        public String MA_QLTSC { get; set; }
        public String MA_CSDLQG { get; set; }
        public String MO_TA { get; set; }
        public Decimal? TRANG_THAI { get; set; }

    }
    public enum enumLOGS_DONG_BO_CSDLQG_TRANG_THAI
    {
        THANH_CONG = 1,
        LOI = 0,
        LOI_CAU_TRUC = -1
    }
}



