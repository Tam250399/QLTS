using GS.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.CauHinh
{
    public class DonViCauHinh : ICauHinh
    {
        /// <summary>
        /// cấu hình dành cho báo cáo trên từng đơn vị
        /// </summary>
        public string BaoCao { get; set; }
    }
    
    public class CauHinhBaoCaoChung : ICauHinh
    {
        public string BaoCao { get; set; }
    }
    public class CauHinhMapBC : ICauHinh
    {
        public string BaoCao { get; set; }
    }
}
