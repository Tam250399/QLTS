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
    public partial interface ITaiSanNhaModelFactory
    {
        #region TaiSanNha

        TaiSanNhaSearchModel PrepareTaiSanNhaSearchModel(TaiSanNhaSearchModel searchModel);

        TaiSanNhaListModel PrepareTaiSanNhaListModel(TaiSanNhaSearchModel searchModel);

        TaiSanNhaModel PrepareTaiSanNhaModel(TaiSanNhaModel model, TaiSanNha item, bool excludeProperties = false, bool checkCoDat = false, bool isTangMoi = true, bool isCreateTSDA = false, bool isPrepareForDDLDiaBan = true);

        void PrepareTaiSanNha(TaiSanNhaModel model, TaiSanNha item);
		/// <summary>
		/// với tài sản nhà không có đất, truyền mã địa bàn, đia chỉ nguyên bản vào để ra địa chỉ nhà
		/// </summary>
		/// <param name="DiaChi"></param>
		/// <param name="DiaBanId"></param>
		string PrepareDiaChiNhaByDiaBan(string DiaChi, decimal? DiaBanId);

        void PrepareForDDLDiaBan(TaiSanNhaModel model, decimal? DiaBanId);

        /// <summary>
        /// Description: Prepare thông tin hồ sơ, giấy tờ đất từ YeuCauChiTiet
        /// </summary>
        /// <param name="model"></param>
        /// <param name="item"></param>
        void PrepareHoSoNha(TaiSanNhaModel model, YeuCauChiTiet item);
		TaiSanListModel PrepareTaiSanListModelByDatId(decimal? DatId = 0);

		#endregion
	}
}
