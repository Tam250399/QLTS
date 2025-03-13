//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using System;


namespace GS.Core.Domain.DanhMuc
{
    public partial class QuocGia : BaseEntity
    {
        public String MA { get; set; }
        public String TEN { get; set; }
        public String MO_TA { get; set; }
        public int? DB_ID  { get; set; }

    }
}



