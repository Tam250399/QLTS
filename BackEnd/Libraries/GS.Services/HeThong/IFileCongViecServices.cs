//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.HeThong;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace GS.Services.HeThong
{
    public partial interface IFileCongViecService
    {
        #region FileCongViec
        IList<FileCongViec> GetFileCongViecByIds(string ids);
        IPagedList<FileCongViec> SearchFileCongViecs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        FileCongViec GetFileCongViecById(decimal Id);
        byte[] GetWorkFileBits(IFormFile file);
        FileCongViec GetFileByGuid(Guid guid);
        void InsertFileCongViec(FileCongViec entity);
        void UpdateFileCongViec(FileCongViec entity);
        void DeleteFileCongViec(FileCongViec entity);
        FileCongViec GetFileByName(string name);
        bool CheckFileImpTaiSanDone(string name);
        #endregion
    }
}
