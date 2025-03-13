//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 20/5/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.HeThong
{
	public partial class VaiTroWidget : BaseEntity
	{
		public Decimal VAI_TRO_ID { get; set; }
		public Decimal WIDGET_ID { get; set; }
		public virtual VaiTro vaiTro { get; set;}
		public virtual Widget widget { get; set; }
		
	}
}



