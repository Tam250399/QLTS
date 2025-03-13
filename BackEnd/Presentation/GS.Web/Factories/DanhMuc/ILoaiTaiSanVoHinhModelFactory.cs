//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/3/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DanhMuc;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GS.Web.Factories.DanhMuc
{
    public partial interface ILoaiTaiSanVoHinhModelFactory 
    {    
        #region LoaiTaiSanVoHinh
    
        LoaiTaiSanVoHinhSearchModel PrepareLoaiTaiSanVoHinhSearchModel(LoaiTaiSanVoHinhSearchModel searchModel);
        
        LoaiTaiSanVoHinhListModel PrepareLoaiTaiSanVoHinhListModel(LoaiTaiSanVoHinhSearchModel searchModel);
        
        LoaiTaiSanVoHinhModel PrepareLoaiTaiSanVoHinhModel(LoaiTaiSanVoHinhModel model, LoaiTaiSanDonVi item, bool excludeProperties = false);

        bool CheckMaLoaiTaiSanDonVi(decimal id, string ma);


        IList<SelectListItem> PrepareSelectListLoaiTaiSanDonVi(decimal? valSelected = 0, decimal? cheDoId = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn loại Tài sản --", decimal? loaiHinhTaiSanId = 0, string valueFirstRow = "", decimal? donViId = 0);

        void PrepareLoaiTaiSanDonVi(LoaiTaiSanVoHinhModel model, LoaiTaiSanDonVi item);
        #endregion
    }
}
