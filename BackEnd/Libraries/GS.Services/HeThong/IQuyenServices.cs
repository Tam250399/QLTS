//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.HeThong;
using System.Collections.Generic;

namespace GS.Services.HeThong
{
    public partial interface IQuyenService
    {
        #region Quyen
        IList<Quyen> GetAllQuyens();
        IPagedList<Quyen> SearchQuyens(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, IList<int> ListQuyenDaChon = null, decimal idVaiTro = 0);
        Quyen GetQuyenById(decimal Id);
        void InsertQuyen(Quyen entity);
        void UpdateQuyen(Quyen entity);
        void DeleteQuyen(Quyen entity);
        bool Authorize(Quyen quyen);
        bool Authorize(string permissionRecordSystemName);

        bool KiemTraTrungMa(string Ma, decimal Id);
        IList<Quyen> GetQuyenByVaiTroId(decimal vaiTro);
        #endregion
    }
}
