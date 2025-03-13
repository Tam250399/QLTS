using GS.Core.Domain.CauHinh;
using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.HeThong
{
	public class CauHinhTuDongDuyetModel : BaseGSModel, ICauHinhModel
	{
		public CauHinhTuDongDuyetModel()
		{
			cauHinhDuyetTaiSans = new List<CauHinhDuyetTaiSan>();
		}
		public string IsAutoDuyetTaiSanDuoi500 { get; set; }

		public List<CauHinhDuyetTaiSan> cauHinhDuyetTaiSans { get; set; }
        public bool tsQuanLyNhuTSCD { get; set; }
        public bool IsShowCheckTSQLNhuTSCD { get; set; }
		public bool IsShowCheckTSQLChuKySo { get; set; }
		public bool tsQuanLyChuKySo { get; set; }

	}
}
