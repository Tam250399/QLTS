//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2021
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.DB;

namespace GS.Services.DB
{
    public partial interface IDBTempTaiSanCuService 
    {    
    #region DBTempTaiSanCu
        IList<DBTempTaiSanCu> GetAllDBTempTaiSanCus();
        IPagedList <DBTempTaiSanCu> SearchDBTempTaiSanCus(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        DBTempTaiSanCu GetDBTempTaiSanCuById(decimal Id);
        IList<DBTempTaiSanCu> GetDBTempTaiSanCuByIds(decimal[] newsIds);
        void InsertDBTempTaiSanCu(DBTempTaiSanCu entity);
        void UpdateDBTempTaiSanCu(DBTempTaiSanCu entity);
        void DeleteDBTempTaiSanCu(DBTempTaiSanCu entity);    
     #endregion
	}
}
