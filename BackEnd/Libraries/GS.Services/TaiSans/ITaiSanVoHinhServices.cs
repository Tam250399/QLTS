//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/3/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.TaiSans;
using System.Collections.Generic;

namespace GS.Services.TaiSans
{
    public partial interface ITaiSanVoHinhService
    {
        #region TaiSanVoHinh
        IList<TaiSanVoHinh> GetAllTaiSanVoHinhs();
        IPagedList<TaiSanVoHinh> SearchTaiSanVoHinhs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        TaiSanVoHinh GetTaiSanVoHinhById(decimal Id);
        IList<TaiSanVoHinh> GetTaiSanVoHinhByIds(decimal[] newsIds);
        void InsertTaiSanVoHinh(TaiSanVoHinh entity);
        void UpdateTaiSanVoHinh(TaiSanVoHinh entity);
        void DeleteTaiSanVoHinh(TaiSanVoHinh entity);
        TaiSanVoHinh GetTaiSanVoHinhByTaiSanId(decimal taiSanId);

        #endregion
    }
}
