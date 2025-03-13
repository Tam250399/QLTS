//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using System;


namespace GS.Core.Domain.HeThong
{
    public partial class VaiTro : BaseEntity
    {
        public String MA { get; set; }
        public String TEN { get; set; }
        public DateTime? NGAY_TAO { get; set; }

    }
}



