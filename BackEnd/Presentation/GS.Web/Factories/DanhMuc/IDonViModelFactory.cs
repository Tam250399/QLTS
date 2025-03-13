//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DMDC;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GS.Web.Factories.DanhMuc
{
	public partial interface IDonViModelFactory
	{
		#region DonVi

		DonViSearchModel PrepareDonViSearchModel(DonViSearchModel searchModel);

		DonViListModel PrepareDonViListModel(DonViSearchModel searchModel);

		DonViListModel PrepareDonViListChonDonViModel(DonViSearchModel searchModel);

		DonViListModel PrepareDonViDieuChuyenListModel(DonViSearchModel searchModel);
		DonViSearchModel PrepareDonViChuaNhapTaiSanSearchModel(DonViSearchModel searchModel);
		DonViListModel PrepareDonViChuaNhapTaiSanListModel(DonViSearchModel searchModel);
		DonViSearchModel PrepareKiemTraTaiSanSearchModel(DonViSearchModel searchModel);
		DonViListModel PrepareKiemTraTaiSanListModel(DonViSearchModel searchModel);
		DonViModel PrepareDonViModel(DonViModel model, DonVi item, bool excludeProperties = false);
		IList<DonViModel> PrepareListDonViMuaSamForInputSearch(string tenDonVi = null);

		void PrepareDonVi(DonViModel model, DonVi item);

		DonViLichSu GetDonViLichSu(DonVi before, DonVi after);

		bool CheckMaDonVi(decimal id = 0, string ma = null);

		IList<SelectListItem> PrepareSelectListDonVi(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn đơn vị --", string valueFirstRow = "");

		IList<SelectListItem> PrepareSelectListDonViUsingProc(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn đơn vị --", string valueFirstRow = "", decimal IdDonVi = 0);
		Task<IList<SelectListItem>> PrepareSelectListDonViForMultilSelectInput(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn đơn vị --", string valueFirstRow = "", decimal IdDonVi = 0, int take = 15, string keySearch = null, string ListSelectedId = null);

		IList<SelectListItem> PrepareSelectListBoNganhTinh(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn đơn vị --", string valueFirstRow = "");
		IList<SelectListItem> PrepareSelectListBoNganhTinhTrucThuoc(decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn đơn vị --", string valueFirstRow = "", decimal IdDonVi = 0);
		/// <summary>
		/// Description: Lấy đơn vị văn phòng quản lý tsc, đơn vị dự án quản lý tsda, Đơn vị khác<br></br>
		/// - tsda: tài sản dự án<br></br>
		/// - tsc: tài sản công
		/// </summary>
		/// <param name="donViId"></param>
		/// <param name="isBanQuanLyDuAn">Loại hình đơn vị có phải ban quản lý dự án, hoặc có đơn vị con quản lý tài sản dự án<br></br>
		/// </param>
		/// <param name="isCreateTSDA">check nhập tsc or tsda đối với đơn vị ban quản lý dự án</param>
		/// <returns></returns>
		DonVi GetDonViWithConditions(decimal? donViId = 0, bool? isBanQuanLyDuAn = false, bool? isCreateTSDA = false);

		/// <summary>
		///
		/// </summary>
		/// <param name="pLA_BAN_QL_DU_AN">là đơn vị dự án của đơn vị</param>
		/// <returns></returns>
		DonVi PrepareDonViConChoBQLDA(decimal parentId, bool? pLA_BAN_QL_DU_AN = false);

		IList<SelectListItem> PrepareDonViAvailabele(string tenDonVi = null);

		List<SelectListItem> PrepareDdlDonVi(int DonViCha = 0);
		DonViSearchModel PrepareDonViXacNhanSearchModel(DonViSearchModel searchModel);
		DonViListModel PrepareDonViXacNhanListModel(DonViSearchModel searchModel);

		#endregion DonVi

		#region Kiểm tra loại hình đơn vị

		DonViListModel PrepareListKiemTraLoaiHinhDonVi(DonViSearchModel searchModel);

		KiemTraLoaiHinhDonViModel PrepareKiemTraLoaiHinhDonViModel(KiemTraLoaiHinhDonViModel model);
        DonViSearchModel PrepareDonViSearchModel2(DonViSearchModel searchModel);
        List<DonViExport> PrepareDonViExport(DonViSearchModel searchModel);
		List<DonViExport> PrepareDonViChuaNhapTaiSanExport(DonViSearchModel searchModel);
		List<DonViExport> PrepareDonViKiemTraTaiSanExport(DonViSearchModel searchModel);
		DonViModel PrepareDonViModelFromDMDC(DMDC_DonViNganSach dMDC_DonViNganSach);

		DonViListModel PrepareDonViChuaCoMaTListModel(DonViSearchModel searchModel);

		#endregion Kiểm tra loại hình đơn vị
	}
}