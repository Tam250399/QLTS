//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using System;


namespace GS.Core.Domain.DanhMuc
{
    public partial class LoaiDonVi : BaseEntity
    {
        public String MA { get; set; }
        public String TEN { get; set; }
        public Decimal? PARENT_ID { get; set; }
        public Decimal? SAP_XEP { get; set; }
        public String MA_PHAN_CAP { get; set; }
        public Decimal? TREE_LEVEL { get; set; }
        public String TREE_NODE { get; set; }
        public Decimal? CHE_DO_HOACH_TOAN_ID { get; set; }
        public Decimal? DB_ID { get; set; }
    }  
}



