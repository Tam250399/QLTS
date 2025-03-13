using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.BaoCaos
{
    public enum enumLoaiBaoCao
    {
        BaoCaoChiTietTaiSan = 1,
        BaoCaoTongHopTaiSan = 2,
		BaoCaoQuocHoiTaiSan = 3,
		BaoCaoCongKhaiTaiSan = 4,
		BaoCaoKeKhaiTaiSanCong =5,
		BaoCaoSoHuuToanDan =6,
        BaoCaoCongCuDungCu = 7,
        BaoCaoCheDoKeToan = 8,
        BaoCaoTraCuuSoLieu = 9,
        BaoCaoDuAn = 10,
        BaoCaoTaiChinhNhaNuoc = 11,
        PhieuXacNhanThongTin = 12,
        BaoCaoBanQuanLyDuAn = 13,
        BaoCaoDoiChieuDuLieuTongHop = 14,
        BaoCaoDoiChieuDuLieu = 15,
        BaoCaoDoiChieuDuLieuAdmin = 16,
    }
    public class BaoCaoTaiSanModel: BaseGSEntityModel
    {
        
        public int LoaiBaoCao { get; set; }
        public bool isDonViNhapLieu { get; set; }
        public bool IsShowBaoCaoDoiChieu { get; set; }
    }
}
