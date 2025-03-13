//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using GS.Core;
using GS.Core.Domain.BaoCaos;
using GS.Core.Domain.DanhMuc;

namespace GS.Services.DanhMuc
{
    public partial interface IBaoCaoService 
    {    
    #region BaoCao
        IList<BaoCao> GetAllBaoCaos();
        IPagedList <BaoCao> SearchBaoCaos(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        BaoCao GetBaoCaoById(decimal Id);
        IList<BaoCao> GetBaoCaoByIds(decimal[] newsIds);
        bool CheckMaBaoCao(decimal id = 0, string ma = null);
        BaoCao GetBaoCaoByMa(String ma);
        void InsertBaoCao(BaoCao entity);
        void UpdateBaoCao(BaoCao entity);
        void DeleteBaoCao(BaoCao entity);
        CauHinhBaoCao GetCauHinhBaoCaoByMa(string MaBC = null, decimal idCurrentDonVi = 0);
     #endregion
    }
}
