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
    public partial interface IQuocGiaService
    {
        #region QuocGia
        IList<QuocGia> GetAllQuocGias();
        IList<QuocGia> GetAllQuocGiaChuaDB();
        IPagedList<QuocGia> SearchQuocGias(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        QuocGia GetQuocGiaById(decimal Id);
        IList<QuocGia> GetQuocGiaByIds(decimal[] newsIds);
        void InsertQuocGia(QuocGia entity);
        void UpdateQuocGia(QuocGia entity);
        void DeleteQuocGia(QuocGia entity);
        bool CheckMaQuocGia(string ma = null, decimal id = 0);
        QuocGia GetQuocGiaDB(string Ma = null, int ID_DB = 0, decimal ID = 0);
        void InsertListQuocGia(List<QuocGia> entities);
        void UpdateListQuocGia(List<QuocGia> entities);
        QuocGia GetQuocGia(string Ma = null, string Ten = null);
        QuocGia GetQuocGiaByMa(string ma);
        #endregion
    }
}
