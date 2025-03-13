//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Web.Models.NghiepVu;
using System;

namespace GS.Web.Factories.NghiepVu
{
	public partial interface IYeuCauModelFactory
	{
		#region YeuCau

		/// <summary>
		/// Prepare Thông tin từ entity Biến động sang entity yêu cầu
		/// </summary>
		/// <param name="_bd">BienDong entity</param>
		/// <param name="_yc">YeuCau entity</param>
		/// <param name="isCopy">check sao chép thông tin</param>
		/// <returns></returns>
		YeuCau PrepareYeuCauFromBienDong(BienDong _bd, YeuCau _yc, bool isCopy = false);

		YeuCauSearchModel PrepareYeuCauSearchModel(YeuCauSearchModel searchModel);

		YeuCauListModel PrepareYeuCauListModel(YeuCauSearchModel searchModel);

		YeuCauModel PrepareYeuCauModel(YeuCauModel model, YeuCau item, bool isInfoBienDong = false);

		YeuCau PrepareYeuCau(YeuCau item, TaiSan taisan);

		/// <summary>
		/// Description: Prepare yeu cau phục vụ biến động tài sản
		/// </summary>
		/// <param name="model">YeuCauModel</param>
		/// <param name="taiSan">Thông tin tài sản tạo biến động</param>
		/// <returns></returns>
		//YeuCauModel PrepareYeuCauModelFromTS(YeuCauModel model, TaiSan taiSan, YeuCau item);

		/// <summary>
		/// Description: Prepare các thông tin chung của tài sản từ entity TaiSan vào yeuCauModel
		/// Biến động thay đổi nguyên giá
		/// </summary>
		/// <param name="yeuCauModel"></param>
		/// <param name="taiSan"></param>
		/// <returns></returns>
		YeuCauModel PrepareThongTinChungTS(YeuCauModel yeuCauModel, TaiSan taiSan);

		/// <summary>
		/// Description: Prepare entity yêu cầu biến động
		/// </summary>
		/// <param name="model"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		YeuCau PrepareYeuCauForBDEdit(YeuCauModel model, YeuCau item);

		YeuCau PrepareYeuCauEntity(YeuCauModel model, YeuCau item);

		#endregion YeuCau

		#region Prepare YeuCauModel from BienDong

		/// <summary>
		/// Description: Function prepare YeuCauModel from an BienDong's entity
		/// </summary>
		/// <param name="model">YeuCauModel</param>
		/// <param name="entityBD">an BienDong's entity </param>
		/// <returns></returns>
		YeuCauModel PrepareYeuCauModelFromBienDong(YeuCauModel model, TaiSan taiSan, YeuCau item);

		YeuCau PrepareYeuCauFromYeuCauCu(YeuCau _new, YeuCau _old = null);
		void UpdateYeuCauTuChoi(YeuCau yeuCau, string Note);
		#endregion Prepare YeuCauModel from BienDong

		#region GiamTaiSan

		YeuCauModel PrepareYeuCauModelGiamTaiSan(YeuCauModel model, YeuCau item);
		YeuCauModel PrepareYeuCauModelGiamNhieuTaiSan(YeuCauModel model, YeuCau item);
		YeuCau PrepareYeuCauGiamTaiSan(YeuCauModel model, YeuCau item);
		YeuCau PrepareYeuCauForDieuChuyenKemTheo(YeuCau ycDieuChuyenKem, TaiSan taisan);

		#endregion GiamTaiSan

		#region Validator

		bool IsTrungNgayBD(DateTime? ngay_BD, decimal TaiSanId, decimal YCID);
		/// <summary>
		/// Check ngày biến động có phải lớn nhất không. Sử dụng cho thêm mới biến động cho nhiều tài sản
		/// </summary>
		/// <param name="ngay_BD"></param>
		/// <param name="strTaiSanIds"></param>
		/// <returns></returns>
		bool IsNgayBienDongMoiNhat(DateTime? ngay_BD, string strTaiSanIds);
		/// <summary>
		/// Check ngày biến động có phải lớn nhất không.
		/// </summary>
		/// <param name="ngay_BD"></param>
		/// <param name="TaiSanId"></param>
		/// <param name="YCID"></param>
		/// <returns></returns>
		bool IsNgayBienDongMoiNhat(DateTime? ngay_BD, decimal TaiSanId, decimal YCID);

		/// <summary>
		/// true: hop le
		/// false: khong hop le
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		bool CheckDonViDieuChuyen(YeuCauModel model);
		bool CheckTrungDonViDieuChuyen(YeuCauModel model);
		bool IsLonHonNgayBD(DateTime? ngay_BD, decimal TaiSanId, decimal YCID);
		bool CheckEachNguonVonMinusIsValid(decimal TaiSanId, string NguonVonJson);
		bool CheckDienTichBienDong(YeuCauModel model);
		bool CapNhatThongTinDiaBanAndHienTrang(YeuCauModel model);
		#endregion Validator
	}
}