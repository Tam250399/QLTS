using GS.Core.Domain.BaoCaos.TheTaiSan;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Services.BaoCaos
{
	public partial interface IInTheTaiSanServices
	{
		IList<TS_THE_TAI_SAN_CO_DINH> TS_THE_TAI_SAN_CO_DINH(DateTime? ngayBaoCao = null, Decimal? taiSanId = 0);
		/// <summary>
		/// Thẻ tài sản cố đin S11-DNN TT133
		/// </summary>
		/// <param name="ngayBaoCao"></param>
		/// <param name="taiSanId"></param>
		/// <returns></returns>
		IList<THE_TSCD_TT133> TS_THE_TAI_SAN_CO_DINH_TT133(DateTime? ngayBaoCao = null, Decimal? taiSanId = 0);
		IList<TS_THE_KIEM_KE_TAI_SAN> TS_THE_KIEM_KE( Decimal? taiSanId = 0);
		IList<TS_THE_TAI_SAN_CO_DINH> TS_LIST_THE_TAI_SAN_CO_DINH(DateTime? ngayBaoCao = null, string listTaiSanId = null);
		IList<THE_TSCD_TT133> TS_LIST_THE_TAI_SAN_CO_DINH_TT133(DateTime? ngayBaoCao = null, string listTaiSanId = null);
		IList<TS_THE_KIEM_KE_TAI_SAN> TS_LIST_THE_KIEM_KE(string listTaiSanId = null);
	}
}
