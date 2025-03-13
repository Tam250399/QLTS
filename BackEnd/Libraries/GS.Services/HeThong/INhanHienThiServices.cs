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
    public partial interface INhanHienThiService
    {
        #region NhanHienThi
        IList<NhanHienThi> GetAllNhanHienThis();
        IPagedList<NhanHienThi> SearchNhanHienThis(int pageIndex = 0, int pageSize = int.MaxValue, string Ma = null, string giaTri = null);
        NhanHienThi GetNhanHienThiById(decimal Id);
        NhanHienThi GetNhanHienThiByMa(string Ma);
        string GetGiaTri(string Ma);
        string GetGiaTriEnum<TEnum>(TEnum enumValue) where TEnum : struct;
        string GetListGiaTriEnum1<TEnum>(List<TEnum> enumValue) where TEnum : struct;
        void InsertNhanHienThi(NhanHienThi entity);
        void UpdateNhanHienThi(NhanHienThi entity);
        void DeleteNhanHienThi(NhanHienThi entity);
        #endregion
    }
}
