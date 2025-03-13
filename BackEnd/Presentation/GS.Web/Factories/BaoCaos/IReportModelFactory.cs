using GS.Core.Domain.BaoCaos.TS_BCCK;
using GS.Core.Domain.BaoCaos.TS_BCTC;
using GS.Core.Domain.BaoCaos.TS_BCTH;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.TaiSanChiTiet;
using GS.Web.Models.BaoCaos.TaiSanTongHop;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;

namespace GS.Web.Factories.BaoCaos
{
	public partial interface IReportModelFactory
	{
		void PrePareLyDoTangVaLyDoGiamSearchModel(BaoCaoTaiSanChiTietSearchModel searchModel);
		string PrePareListLyDoTangGiamStringModel(string ListLyDoIdString);
		string PrePareListLyDoGiamStringModel(IList<int> model) ;
		string PrePareListLyDoTangStringModel(IList<int> model) ;
		ThongTinBaoCaoModel ReportStatistical(List<object> listObject);
		ThongTinBaoCaoModel PrepareViewreport(string maBaoCaoEnum, ThongTinBaoCaoHeaderModel thongTinBaoCaoHeaderModel);
		bool CheckMaCauHinhBaoCao(string maBaoCao);
		BaoCaoTaiSanChiTietSearchModel PrepareBaoCaoTaiSanChiTietSearchModel(BaoCaoTaiSanChiTietSearchModel searchModel, Boolean IsMulti = false);
		BaoCaoTaiSanChiTietSearchModel PrepareBaoCaoTaiSanCongKhaiSearchModel(BaoCaoTaiSanChiTietSearchModel searchModel, Boolean IsMulti = false);
		BaoCaoTaiSanChiTietSearchModel PrepareBaoCaoCheDoKeToanSearchModel(BaoCaoTaiSanChiTietSearchModel searchModel, Boolean IsMulti = false);
		BaoCaoTaiSanChiTietSearchModel PrepareBaoCaoTaiSanTraCuuSearchModel(BaoCaoTaiSanChiTietSearchModel searchModel, Boolean IsMulti = false);

		BaoCaoTaiSanChiTietSearchModel PrepareBaoCaoTaiSanTDSearchModel(BaoCaoTaiSanChiTietSearchModel searchModel, Boolean IsMulti = false);

		BaoCaoTaiSanTongHopSearchModel PrepareBaoCaoTaiSanTongHopSearchModel(BaoCaoTaiSanTongHopSearchModel searchModel, Boolean IsMulti = false);
		BaoCaoTaiSanTongHopSearchModel PrepareBaoCaoTaiSanBanQLDuAnSearchModel(BaoCaoTaiSanTongHopSearchModel searchModel);
		List<SelectListItem> GetSelectListsCapBaoCao(DateTime NgayBaoCao);

		void PrePareDonViBaoCao(BaoCaoTaiSanTongHopSearchModel searchModel);
		void PrePareDonViBaoCaoCT(BaoCaoTaiSanChiTietSearchModel searchModel);
		void PrePareDonViBaoCaoTraCuu(BaoCaoTaiSanChiTietSearchModel searchModel);

		CauHinhBaoCaoModel PrePareCauHinhBaoCaoModel(CauHinhBaoCaoModel model, string MaBaoCao);

		CauHinhBaoCaoChungModel PrePareCauHinhBaoCaoChungModel(CauHinhBaoCaoChungModel model);

		string PrePareStringDonViBoPhanModel(string model);
		BaoCaoTaiSanChiTietSearchModel PrepareBaoCaoKeKhaiSearchModel(BaoCaoTaiSanChiTietSearchModel searchModel, Boolean IsMulti = false, Boolean isTaiSanDuAn = false);
		void PrepareEnumeBaoCaoChiTiet(BaoCaoTaiSanChiTietSearchModel searchModel);
		BaoCaoTaiSanChiTietSearchModel PrepareBaoCaoCCDCSearchModel(BaoCaoTaiSanChiTietSearchModel searchModel, bool IsMulti = false);
		ThongTinBaoCaoValueModel GetPropertiesToObject<T>(T obj);
		List<LayoutReportModel> PrepareLayoutExport(List<InputHeaderReportModel> ListModel);
		MemoryStream prepareExcelEntity<T>(MemoryStream stream, List<T> listData, string workSheetName, bool addSTT);
        #region check mã cấu hình báo cáo đồng bộ
        bool CheckMaCauHinhBaoCaoDB(string maBaoCao);
        //List<TS_BCTH_02A_DK_TSNN> Group_TS_BCTH_02A_DK_TSNN(List<TS_BCTH_02A_DK_TSNN> models);
        List<TS_BCTH_02B_DK_TSNN> Group_TS_BCTH_02B_DK_TSNN(List<TS_BCTH_02B_DK_TSNN> models);
        List<TS_BCTH_02C_DK_TSNN> Group_TS_BCTH_02C_DK_TSNN(List<TS_BCTH_02C_DK_TSNN> models);
        List<TS_BCTH_02D_DK_TSNN> Group_TS_BCTH_02D_DK_TSNN(List<TS_BCTH_02D_DK_TSNN> models);
        List<TS_BCTH_02E_DK_TSNN> Group_TS_BCTH_02E_DK_TSNN(List<TS_BCTH_02E_DK_TSNN> models);
        List<TS_BCTH_02F_DK_TSNN> Group_TS_BCTH_02F_DK_TSNN(List<TS_BCTH_02F_DK_TSNN> models);
        //List<TS_BCTH_02G_DK_TNSS> Group_TS_BCTH_02G_DK_TNSS(List<TS_BCTH_02G_DK_TNSS> models);
        //List<TS_BCTH_02H_DK_TSNN> Group_TS_BCTH_02H_DK_TSNN(List<TS_BCTH_02H_DK_TSNN> models);
        List<TS_BCTH_08A_DK_TSC> Group_TS_BCTH_08A_DK_TSC(List<TS_BCTH_08A_DK_TSC> models);
        List<TS_BCTH_08B_DK_TSC> Group_TS_BCTH_08B_DK_TSC(List<TS_BCTH_08B_DK_TSC> models);
        List<TS_BCTC_03_DK_TSNN> Group_TS_BCTC_03_DK_TSNN(List<TS_BCTC_03_DK_TSNN> models);
        List<TS_BCCK_09A_CK_TSC> Group_TS_BCCK_09A_CK_TSC(List<TS_BCCK_09A_CK_TSC> models);
        List<TS_BCCK_09C_CK_TSC> Group_TS_BCCK_09C_CK_TSC(List<TS_BCCK_09C_CK_TSC> models);
        List<TS_BCCK_09D_CK_TSC> Group_TS_BCCK_09D_CK_TSC(List<TS_BCCK_09D_CK_TSC> models);
        List<TS_BCCK_09DD_CK_TSC> Group_TS_BCCK_09DD_CK_TSC(List<TS_BCCK_09DD_CK_TSC> models);
		List<TS_BCCK_10A_CK_TSC> Group_TS_BCCK_10A_CK_TSC(List<TS_BCCK_10A_CK_TSC> models);
		List<TS_BCCK_10B_CK_TSC> Group_TS_BCCK_10B_CK_TSC(List<TS_BCCK_10B_CK_TSC> models);
		List<TS_BCCK_10C_CK_TSC> Group_TS_BCCK_10C_CK_TSC(List<TS_BCCK_10C_CK_TSC> models);
		List<TS_BCCK_10D_CK_TSC> Group_TS_BCCK_10D_CK_TSC(List<TS_BCCK_10D_CK_TSC> models);
		List<TS_BCCK_11A_CK_TSC> Group_TS_BCCK_11A_CK_TSC(List<TS_BCCK_11A_CK_TSC> models);
		List<TS_BCCK_11B_CK_TSC> Group_TS_BCCK_11B_CK_TSC(List<TS_BCCK_11B_CK_TSC> models);
		List<TS_BCCK_11C_CK_TSC> Group_TS_BCCK_11C_CK_TSC(List<TS_BCCK_11C_CK_TSC> models);
		List<TS_BCCK_11D_CK_TSC> Group_TS_BCCK_11D_CK_TSC(List<TS_BCCK_11D_CK_TSC> models);
		#endregion
	}
}