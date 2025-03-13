//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.ThuocTinhs;
using GS.Web.Models.ThuocTinh;
using System;
using System.Collections.Generic;

namespace GS.Web.Factories.ThuocTinhs
{
    public partial interface IThuocTinhDataModelFactory 
    {    
        #region ThuocTinhData
    
        ThuocTinhDataSearchModel PrepareThuocTinhDataSearchModel(ThuocTinhDataSearchModel searchModel);
        
        ThuocTinhDataListModel PrepareThuocTinhDataListModel(ThuocTinhDataSearchModel searchModel);
        
        ThuocTinhDataModel PrepareThuocTinhDataModel(ThuocTinhDataModel model, ThuocTinhData item, bool excludeProperties = false);
        
        void PrepareThuocTinhData(ThuocTinhDataModel model, ThuocTinhData item);
        List<modelThuocTinh> PrepareListmodelThuocTinh(int LoaiHinhTaiSan = 0, int LoaiTaiSan = 0, int TaiSanId = 0);
        modelThuocTinh PreparemodelThuocTinhByThuocTinhData(ThuocTinhData ttData, int LoaiHinhTaiSan = 0, int LoaiTaiSan = 0, Guid GUID = new Guid());
        List<modelThuocTinh> PrepareListmodelThuocTinhForTaiSanTdXuLy(int PhuongAnXuLyId = 0, int TaiSanTdId = 0, Guid TaiSanXuLyGuid = new Guid());
        #endregion
    }
}
