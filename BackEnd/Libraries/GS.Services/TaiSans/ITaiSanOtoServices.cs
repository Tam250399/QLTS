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
    public partial interface ITaiSanOtoService
    {
        #region TaiSanOto
        IList<TaiSanOto> GetAllTaiSanOtos();
        IPagedList<TaiSanOto> SearchTaiSanOtos(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        /// <summary>
        /// Description: Search tài sản ô tô theo biển kiểm soát
        /// </summary>
        /// <param name="bienKiemSoat"></param>
        /// <returns></returns>
        TaiSanOto GetTaiSanOtoByBKS(string bienKiemSoat);
        TaiSanOto GetTaiSanOtoById(decimal Id);
        TaiSanOto GetTaiSanOtoByTaiSanId(decimal taiSanId);
        IList<TaiSanOto> GetTaiSanOtoByIds(decimal[] newsIds);
        void InsertTaiSanOto(TaiSanOto entity);
        void UpdateTaiSanOto(TaiSanOto entity);
        void DeleteTaiSanOto(TaiSanOto entity);
        decimal CountOtoByChucDanh(decimal chucDanhId, decimal donViId);
        #endregion
    }
}
