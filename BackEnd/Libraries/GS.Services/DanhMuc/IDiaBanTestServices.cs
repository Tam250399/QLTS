//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.DanhMuc;

namespace GS.Services.DanhMuc
{
    public partial interface IDiaBanTestService 
    {    
    #region DiaBanTest
        IList<DiaBanTest> GetAllDiaBanTests();
        IPagedList <DiaBanTest> SearchDiaBanTests(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null, decimal? PARENTID = 0);
        DiaBanTest GetDiaBanTestByMa(string Ma);
        DiaBanTest GetDiaBanTestById(decimal Id);
        IList<DiaBanTest> GetDiaBanTestByIds(decimal[] newsIds);
        IList<DiaBanTest> GetDiaBanForInputSearch(string name);
        void InsertDiaBanTest(DiaBanTest entity);
        void UpdateDiaBanTest(DiaBanTest entity);
        void DeleteDiaBanTest(DiaBanTest entity);    
     #endregion
	}
}
