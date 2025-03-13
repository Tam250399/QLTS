using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.HeThong
{
    public class CauHinhDongBoModel
    {
        public string UrlKhoCSDLQG { get; set; }
        public string UrlSearchLog { get; set; }
    }
    public class CauHinhDongBoDanhMucModel
    {
        public string UrlDanhMuc { get; set; }
    }
    public class CauHinhDongBoTaiSanModel
    {
        public bool? DONG_BO_REAL_TIME { get; set; }
        public string UrlTaiSan { get; set; }     
    }
}
