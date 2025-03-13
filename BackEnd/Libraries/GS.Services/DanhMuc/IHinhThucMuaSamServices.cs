//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.DanhMuc;
using System.Collections.Generic;

namespace GS.Services.DanhMuc
{
    public partial interface IHinhThucMuaSamService
    {
        #region HinhThucMuaSam
        IList<HinhThucMuaSam> GetHinhThucMuaSams();
        IPagedList<HinhThucMuaSam> SearchHinhThucMuaSams(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        HinhThucMuaSam GetHinhThucMuaSamById(decimal Id);
        IList<HinhThucMuaSam> GetHinhThucMuaSamByIds(decimal[] newsIds);
        void InsertHinhThucMuaSam(HinhThucMuaSam entity);
        void UpdateHinhThucMuaSam(HinhThucMuaSam entity);
        void DeleteHinhThucMuaSam(HinhThucMuaSam entity);
        HinhThucMuaSam GetHinhThucMuaSamByMa(string MA);
        bool CheckMaHinhThucMuaSam(decimal id, string ma);
        #endregion
    }
}
