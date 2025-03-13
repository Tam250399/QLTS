using GS.Core.Domain.BaoCaos.CCDC;
using GS.Core.Domain.BaoCaos.TS_CDKT;
using GS.Core.Domain.CCDC;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Services.BaoCaos
{
    public partial interface ICongCuDungCuService
    {
        IList<BaoCaoKiemKe> BaoCaoKiemKeCCDC(DateTime NgayBaoCao, Decimal DonViId, string BoPhanId = null, string NhomCongCuId = null, Decimal DonViBoPhan = 0, int DonViTien = 1000);
        IList<BaoCaoKiemKe> BaoCaoKiemKePhongBanCCDC(DateTime NgayBaoCao, Decimal DonViId, string BoPhanId = null, string NhomCongCuId = null, Decimal DonViBoPhan = 0, int DonViTien = 1000);
        IList<BaoCaoKiemKe> BaoCaoTongHopKiemKe(DateTime NgayBaoCao, Decimal DonViId, string BoPhanId = null, string NhomCongCuId = null, Decimal DonViBoPhan = 0, int DonViTien = 1000);
        IList<LietKeCCDC> BaoCaoLietKeCCDC(DateTime NgayBaoCao, Decimal DonViId, string BoPhanId = null, string NhomCongCuId = null, int DonViTien = 1000);
        IList<TangGiamCongCu> BaoCaoTangGiamCongCu(DateTime NgayFrom, DateTime NgayTo, Decimal DonViId, string BoPhanId = null, string NhomCongCuId = null, Decimal TangOrGiam = 0, int DonViTien = 1000,string ListLyDoTang = null, string ListLyDoGiam = null);
        IList<BaoCaoSuaChua> BaoCaoSuaChuaCongCu(DateTime NgayFrom, DateTime NgayTo, Decimal DonViId, string BoPhanId = null, string NhomCongCuId = null, int DonViTien = 1000);
        IList<BaoCaoHongMat> BaoCaoHongMatCongCu(DateTime NgayFrom, DateTime NgayTo, Decimal DonViId, string BoPhanId = null, string NhomCongCuId = null, int DonViTien = 1000);
        IList<BaoCaoPhanBo> BaoCaoPhanBoCongCu(DateTime NgayBaoCao, Decimal DonViId, string BoPhanId = null, string NhomCongCuId = null, int DonViTien = 1000);
        IList<BienBanKiemKeCCDC> BienBanKiemke(int KiemKeId = 0, int DonViTien = 1);
        IList<TongHopTon> BaoCaoTongHopTon(DateTime NgayFrom, DateTime NgayTo, Decimal DonViId, int DonViTien = 1000);
        IList<TongHopCCDC> BaoCaoTongHopCCDC(DateTime NgayBaoCao, String StringDonVi = null, string BoPhanId = null, string NhomCongCuId = null, int DonViTien = 1000, int MauSo = 1, int BacDonVi = 1);
    }
}
