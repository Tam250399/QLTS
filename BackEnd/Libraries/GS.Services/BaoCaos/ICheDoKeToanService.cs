using GS.Core.Domain.BaoCaos.TS_CDKT;
using System;
using System.Collections.Generic;

namespace GS.Services.BaoCaos
{
	public partial interface ICheDoKeToanService
	{
		IList<BaoCaoKiemKeTaiSan> BaoCaoKiemKeTaiSan(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Decimal DonViId = 0, string MaNhomTaiSan = null, string BoPhanIds = null, decimal? DonViTien = 1000);

		IList<BaoCaoKiemKeTaiSan> BaoCaoKiemKeTaiSanPhongBan(DateTime? NgayBatDau = null, DateTime? NgayKetThuc = null, Decimal DonViId = 0, string MaNhomTaiSan = null, string ListDonViBoPhan = null, decimal? DonViTien = 1000);

		IList<B04HQD19> CDKT_BS03_MS_B04H_BC_THTANGGIAM_TSCD(int Year, Int32 donViId = 0, int loaiTaiSan = 0, int donViTien = 1000, int donViDienTich = 1, int bacTaiSan = 1, string stringLoaiTaiSan = null, int LyDo = 0, decimal idQueueBaoCao = 0, string ListDonViBoPhan = null);

		IList<C55AHDQD19> CDKT_BS04_MS_C55A_HD_BANG_TINH_HAO_MON(int Nam, Decimal DonViId, string MaNhomTaiSan, Decimal BoPhanId = 0, int DonViTien = 1000);

		IList<C55AHDQD19> CDKT_BS05_BANG_TINH_KHAU_HAO_TSCD(int Nam, Decimal DonViId, string MaNhomTaiSan, Decimal BoPhanId = 0, int DonViTien = 1000);

		IList<SoGhiTangTaiSan> CDKT_BS06_SOGHITANG_TSCD(int Nam, Decimal DonViId, string MaNhomTaiSan, Decimal BoPhanId = 0, int DonViTien = 1000, string stringLyDo = null);

		IList<SoGhiGiamTaiSan> CDKT_BS07_SOGHIGIAM_TSCD(int Nam, Decimal DonViId, string MaNhomTaiSan, Decimal BoPhanId = 0, int DonViTien = 1000, string stringLyDo = null);

		IList<SoTaiSanS32HQD19> CDKT_B09_S32H_SO_TS(int Nam, Decimal DonViId, string MaNhomTaiSan, Decimal BoPhanId = 0, int DonViTien = 1000, bool isInTrongKy = false);

		IList<SoTaiSanS32HQD19> CDKT_B10_S32H_CCDC(int Nam, Decimal DonViId, string MaNhomTaiSan, Decimal BoPhanId = 0, int DonViTien = 1000);

		IList<SoTaiSanS32HQD19> CDKT_B11_S32H_SO_TS_CDCC(int Nam, Decimal DonViId, string MaNhomTaiSan, Decimal BoPhanId = 0, int DonViTien = 1000);

		IList<SoTaiSanS31HQD19> CDKT_BTC_B08_S24H_SO_TS(int Nam, Decimal DonViId, string MaNhomTaiSan, string ListDonViBoPhan = null, int DonViTien = 1000, bool isInTrongKy = false);
		IList<SoTaiSanS31HQD19> CDKT_BTC_B08_S31H_SO_TS(int Nam, Decimal DonViId, string MaNhomTaiSan, string ListDonViBoPhan = null, int DonViTien = 1000, bool isInTrongKy = false);

		IList<SoTaiSanS32HQD19> CDKT_BTC_B11_S24H_SO_TS_CDCC(int Nam, Decimal DonViId, string MaNhomTaiSan, string BoPhanId = null, int DonViTien = 1000, bool isInTrongKy = false);

		IList<BienBanKiemKe> BienBanKiemke(int KiemKeId = 0, int DonViTien = 1);
	}
}