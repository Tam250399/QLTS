//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.TaiSans;
using GS.Web.Models.TaiSans;
namespace GS.Web.Factories.TaiSans
{
    public partial interface ITaiSanOtoModelFactory
    {
        #region TaiSanOto
        string genTenTaiSanOto(decimal? NhanXeId, decimal? dongXeId, string BKS);
        TaiSanOtoSearchModel PrepareTaiSanOtoSearchModel(TaiSanOtoSearchModel searchModel);

        TaiSanOtoListModel PrepareTaiSanOtoListModel(TaiSanOtoSearchModel searchModel);

        TaiSanOtoModel PrepareTaiSanOtoModel(TaiSanOtoModel model, TaiSanOto item, bool excludeProperties = false, bool isTangMoi = true);

        void PrepareTaiSanOto(TaiSanOtoModel model, TaiSanOto item);

        /// <summary>
        /// Desription: Check biển kiểm soát. <br></br>
        /// Không hợp lệ: return false <br></br>
        /// Hợp lệ: return true<br></br>
        /// </summary>
        /// <param name="bienKiemSoat">BKS cần check</param>
        /// <param name="isPhuongTienKhac"></param>
        /// <returns></returns>
        bool checkBienKiemSoat(string bienKiemSoat, decimal? id = 0, bool isPhuongTienKhac = false);

        bool checkSoChoNgoi(ref string messageReturn , decimal loaiTaiSan = 0, decimal soChoNgoi = 0,decimal taiTrong = 0);
        #endregion
    }
}
