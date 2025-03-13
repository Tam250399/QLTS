using GS.Core.Domain.KT;
using GS.Web.Models.KeToan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Factories.KeToan
{
	public partial interface IHaoMonTaiSanLogModelFactory
	{
		HaoMonTaiSanModel ChotHaoMon(decimal taiSanId, decimal namHaoMon);
		KhauHaoTaiSanModel ChotKhauHao(decimal taiSanId, decimal namKhauHao, decimal thangKhauHao);
		bool ChotToanBoTaiSan(decimal? donViId = 0, decimal? namKeKhai = 0, decimal? taiSanId = 0, decimal? loaiHinhTaiSanId = 0, bool checkLogHaoMon = true);
		HaoMonTaiSan PrepareHMTSEntity(HaoMonTaiSan hmtsEntity, HaoMonTaiSanModel hmtsData);
		KhauHaoTaiSan PrepareKHTSEntity(KhauHaoTaiSan khtsEntity, KhauHaoTaiSanModel khtsData);
	}
}
