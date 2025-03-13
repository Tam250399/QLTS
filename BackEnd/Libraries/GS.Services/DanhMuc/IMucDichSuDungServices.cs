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
    public partial interface IMucDichSuDungService
    {
        #region MucDichSuDung
        IList<MucDichSuDung> GetMucDichSuDungs();
        IList<MucDichSuDung> GetMucDichSuDungChuaDb();
        IPagedList<MucDichSuDung> SearchMucDichSuDungs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        MucDichSuDung GetMucDichSuDungById(decimal Id);
        IList<MucDichSuDung> GetMucDichSuDungByIds(decimal[] newsIds);
        void InsertMucDichSuDung(MucDichSuDung entity);
        void UpdateMucDichSuDung(MucDichSuDung entity);
        void DeleteMucDichSuDung(MucDichSuDung entity);
        MucDichSuDung GetMucDichSuDungByMa(string MA);
        MucDichSuDung GetMucDichSuDungByTen(string Ten);
        void InsertListMucDichSuDung(List<MucDichSuDung> entity);
        void UpdateListMucDichSuDung(List<MucDichSuDung> entity);
        MucDichSuDung GetMucDichSuDungByID_DB(int ID_DB=0);
        #endregion
    }
}
