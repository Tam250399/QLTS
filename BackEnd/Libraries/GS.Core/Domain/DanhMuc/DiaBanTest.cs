//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.DanhMuc
{
	public partial class DiaBanTest : BaseEntity
	{
		public String MA {get;set;}
		public String TEN {get;set;}
		public String MO_TA {get;set;}
		public String TREE_NODE {get;set;}
		public String TREE_LEVEL {get;set;}
		public Decimal QUOC_GIA_ID {get;set;}
        public Decimal? PARENT_ID { get; set; }
        public virtual QuocGia QuocGia { get; set; }
        public virtual DiaBanTest DiaBanTestCha { get; set; }
    }
}



