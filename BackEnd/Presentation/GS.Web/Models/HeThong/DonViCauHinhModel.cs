using GS.Core.Domain.CauHinh;
using GS.Web.Framework.Models;
using System.Collections.Generic;

namespace GS.Web.Models.HeThong
{
	public class DonViCauHinhModel : BaseGSModel, ICauHinhModel
	{
		public DonViCauHinhModel()
		{
			cauHinhDuyetTaiSans = new List<CauHinhDuyetTaiSan>();
		}
		public string IsAutoDuyetTaiSanDuoi500 { get; set; }

		public string BaoCao { get; set; }
		public List<CauHinhDuyetTaiSan> cauHinhDuyetTaiSans { get; set; }
	}
	
}