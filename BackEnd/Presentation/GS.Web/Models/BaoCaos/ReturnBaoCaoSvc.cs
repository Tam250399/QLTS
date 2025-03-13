using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.BaoCaos
{
	public class ReturnBaoCaoSvc : BaseGSModel
	{
		public string MA_BAO_CAO { get; set; }
		public string JsonValue { get; set; }
		public Byte[] BinaryValue { get; set; }
		public int LOAI_DATA_TRA_VE_ID { get; set; }
		public string DUOI_FILE { get; set; }
		public string LOAI_FILE { get; set; }
	}
}
