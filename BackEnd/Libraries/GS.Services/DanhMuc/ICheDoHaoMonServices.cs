//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.DanhMuc;
using System;
using System.Collections.Generic;

namespace GS.Services.DanhMuc
{
    public partial interface ICheDoHaoMonService
    {
        #region CheDoHaoMon
        IList<CheDoHaoMon> GetAllCheDoHaoMons();
        IList<CheDoHaoMon> GetAllCheDoHaoMonChuaDb();
        IPagedList<CheDoHaoMon> SearchCheDoHaoMons(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        CheDoHaoMon GetCheDoHaoMonById(decimal Id);
        CheDoHaoMon GetCheDoHaoMonByMa(string Ma);
        IList<CheDoHaoMon> GetCheDoHaoMonByIds(decimal[] newsIds);
        void InsertCheDoHaoMon(CheDoHaoMon entity);
        void UpdateCheDoHaoMon(CheDoHaoMon entity);
        void DeleteCheDoHaoMon(CheDoHaoMon entity);
        CheDoHaoMon GetCheDoHaoMonByNgayNhap(DateTime NgayNhap);
		CheDoHaoMon GetNewestCheDoHaoMon();
        bool CheckMaCheDoHaoMon(decimal id, string ma);
        CheDoHaoMon GetCheDoHaoMonByIdDb(int ID_DB = 0);
        void InsertListCheDoHaoMon(List<CheDoHaoMon> entity);
        void UpdateListCheDoHaoMon(List<CheDoHaoMon> entity);
        #endregion
    }
}
