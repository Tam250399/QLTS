using GS.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.CauHinh
{
	public 	class CauHinhTuDongDuyet:ICauHinh
	{
        public string IsAutoDuyetTaiSanDuoi500 { get; set; }
    }
    public class CauHinhDuyetTaiSan
    {
        public int LOAI_HINH_TAI_SAN { get; set; }
        public decimal NGUYEN_GIA { get; set; }
        public bool IS_AUTO_DUYET_DUOI_NG { get; set; }
        public bool IS_AUTO_DUYET_IMPORT { get; set; }
    }
}
