//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.TaiSans;
using System.Collections.Generic;

namespace GS.Services.TaiSans
{
    public partial interface ITaiSanDatService
    {
        #region TaiSanDat
        void GetDiaBanInfoByMaDiaBan(decimal? DiaBanId = 0, TaiSanDat item = null);
		IList<TaiSanDat> GetAllTaiSanDats();
        IPagedList<TaiSanDat> SearchTaiSanDats(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        TaiSanDat GetTaiSanDatByTaiSanId(decimal TaiSanId);
        TaiSanDat GetTaiSanDatById(decimal Id);
        TaiSanDat GetTaiSanDatByMaTSAndDonVi(string maTS = null, decimal? donViId = null);
        IList<TaiSanDat> GetTaiSanDatByIds(decimal[] newsIds);
        void InsertTaiSanDat(TaiSanDat entity);
        void UpdateTaiSanDat(TaiSanDat entity);
        void DeleteTaiSanDat(TaiSanDat entity);
        TaiSanDat CheckDiaChiTaiSanDat(string diaChi, decimal? diaBanId = 0, decimal? donViId = 0);
        #endregion
    }
}
