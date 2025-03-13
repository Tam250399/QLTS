using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.CCDC
{
    public class ThongTinCongCuModel : BaseGSEntityModel
    {
        public string MaCongCu { get; set; }
        public string TenLoaiBienDong { get; set; }
        public string StrNgayBienDong { get; set; }

        public DateTime? NgayBienDong { get; set; }
        public string StrNgayTao { get; set; }
        public string  NguoiTao { get; set; }
        public string GhiChu { get; set; }
        public decimal? Soluong { get; set; }
    }
    public class ThongTinCongCuListModel : BasePagedListModel<ThongTinCongCuModel>
    {

    }
}
