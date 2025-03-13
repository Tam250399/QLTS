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
    public partial interface ITaiSanNhaService
    {
        #region TaiSanNha
        IList<TaiSanNha> GetTaiSanNhas(decimal? taisanDatId = 0);
        IPagedList<TaiSanNha> SearchTaiSanNhas(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null,decimal? taiSanDatId=0);
        TaiSanNha GetTaiSanNhaById(decimal Id);
        void GetDiaBanInfoByMaDiaBan(decimal? DiaBanId = 0, TaiSanNha item = null);
        IList<TaiSanNha> GetTaiSanNhaByIds(decimal[] newsIds);
        IList<TaiSanNha> GetTaiSanNhaByDatId(decimal taiSanDatId);
        TaiSanNha GetTaiSanNhaByTaiSanId(decimal TaiSanId);
        void DeleteTaiSanNhaByTaiSanDatId(decimal taiSanDatId);
        void InsertTaiSanNha(TaiSanNha entity);
        void UpdateTaiSanNha(TaiSanNha entity);
        void DeleteTaiSanNha(TaiSanNha entity);
        #endregion
    }
}
