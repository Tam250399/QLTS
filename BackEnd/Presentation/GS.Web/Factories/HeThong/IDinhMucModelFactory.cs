//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 18/6/2021
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.HeThong;
using GS.Web.Models.HeThong;
using System;

namespace GS.Web.Factories.HeThong
{
    public partial interface IDinhMucModelFactory 
    {
        #region DinhMuc

        KetQuaDinhMuc CheckDinhMucOto(decimal? ChucDanhId = null, decimal? LoaiTaiSanId = 0, decimal? DonViId = 0, DateTime? NgayGhiTang = null, decimal? NguyenGia = null);
        DinhMucSearchModel PrepareDinhMucSearchModel(DinhMucSearchModel searchModel);
        
        DinhMucListModel PrepareDinhMucListModel(DinhMucSearchModel searchModel);
        
        DinhMucModel PrepareDinhMucModel(DinhMucModel model, DinhMuc item, bool excludeProperties = false);
        
        void PrepareDinhMuc(DinhMucModel model, DinhMuc item);
        bool CheckMaDinhMuc(decimal id, string Ma);
        bool CheckSoQuyetDinhDinhMuc(decimal id, string Ma);
        #endregion
    }
}
