//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 7/12/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.SHTD;

namespace GS.Services.SHTD
{
    public partial interface IKetQuaService 
    {    
    #region KetQua
        IList<KetQua> GetAllKetQuas();
        IList<KetQua> GetAllKetQuaChuaDongBo();
        IPagedList <KetQua> SearchKetQuas(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        KetQua GetKetQuaById(decimal Id);
        IList<KetQua> GetKetQuaBys(decimal TaiSanTDXuLy = 0, List<decimal> ListTaiSanTDXuLy = null);
        IList<KetQua> GetKetQuaByIds(decimal[] newsIds);
        void InsertKetQua(KetQua entity);
        void UpdateKetQua(KetQua entity);
        void DeleteKetQua(KetQua entity);
        KetQua GetKetQuaByDB_ID(string DB_Id);
        KetQua GetKetQuaByTSPAXLID(decimal tsTDXuLyId);
     #endregion
    }
}
