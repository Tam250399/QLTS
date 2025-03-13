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
    public partial interface IVaiTroService
    {
        #region VaiTro
        IList<VaiTro> GetAllVaiTros();
        IPagedList<VaiTro> SearchVaiTros(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null);
        VaiTro GetVaiTroById(decimal Id);
        VaiTro GetVaiTroByMa(string ma);
        VaiTro GetVaiTro(string Ma);
        void InsertVaiTro(VaiTro entity);
        void UpdateVaiTro(VaiTro entity);
        void DeleteVaiTro(VaiTro entity);
        bool KiemTraTrungMa(string Ma, decimal Id);
        IList<VaiTro> GetVaiTros(IList<int> VaiTroIds = null);
        #endregion
    }
}
