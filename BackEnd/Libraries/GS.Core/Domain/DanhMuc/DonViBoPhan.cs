//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using System;


namespace GS.Core.Domain.DanhMuc
{
    public partial class DonViBoPhan : BaseEntity
    {
        public String MA { get; set; }
        public String TEN { get; set; }
        public String DIA_CHI { get; set; }
        public String DIEN_THOAI { get; set; }
        public String FAX { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        public Decimal? PARENT_ID { get; set; }
        public String TREE_NODE { get; set; }
        public Decimal? TREE_LEVEL { get; set; }
        public virtual DonViBoPhan DonViBoPhanCha { get; set; }
        public virtual DonVi DON_VI { get; set; }
        public Decimal? DB_ID { get; set; }
        //thêm tạm để đồng bộ từ tsnn
        public string DB_GUID { get; set; }

    }
}



