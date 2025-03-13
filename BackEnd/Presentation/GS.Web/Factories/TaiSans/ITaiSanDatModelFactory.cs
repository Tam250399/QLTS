//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Web.Models.TaiSans;
namespace GS.Web.Factories.TaiSans
{
    public partial interface ITaiSanDatModelFactory
    {
        #region TaiSanDat

        TaiSanDatSearchModel PrepareTaiSanDatSearchModel(TaiSanDatSearchModel searchModel);

        TaiSanDatListModel PrepareTaiSanDatListModel(TaiSanDatSearchModel searchModel);

        TaiSanDatModel PrepareTaiSanDatModel(TaiSanDatModel model, TaiSanDat item, bool excludeProperties = false, bool isTangMoi = true);

        void PrepareTaiSanDat(TaiSanDatModel model, TaiSanDat item);

		/// <summary>
		/// Description: Prepare thông tin hồ sơ, giấy tờ đất từ YeuCauChiTiet
		/// </summary>
		/// <param name="model"></param>
		/// <param name="item"></param>
		void PrepareHoSoDat(TaiSanDatModel model, YeuCauChiTiet item);
		bool CheckDiaChiTaiSanDat(string diaChi, decimal? diaBanId = 0, decimal? donViId = 0, decimal? id = 0);
		#endregion
	}
}
