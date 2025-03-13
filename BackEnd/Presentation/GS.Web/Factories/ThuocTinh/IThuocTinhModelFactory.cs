//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.ThuocTinhs;
using GS.Web.Models.ThuocTinh;
using System.Collections.Generic;

namespace GS.Web.Factories.ThuocTinhs
{
    public partial interface IThuocTinhModelFactory 
    {    
        #region ThuocTinh
    
        ThuocTinhSearchModel PrepareThuocTinhSearchModel(ThuocTinhSearchModel searchModel);
        modelThuocTinh PreparemodelThuocTinh(modelThuocTinh model);


        ThuocTinhListModel PrepareThuocTinhListModel(ThuocTinhSearchModel searchModel);
        
        ThuocTinhModel PrepareThuocTinhModel(ThuocTinhModel model, ThuocTinh item, bool excludeProperties = false);
        
        void PrepareThuocTinh(ThuocTinhModel model, ThuocTinh item);
        modelThuocTinh PreparemodelThuocTinhByThuocTinh(ThuocTinh ttData, int LoaiHinhTaiSan = 0, int LoaiTaiSan = 0, int? Sapxep = null);
        modelThuocTinh PreparemodelThuocTinhByThuocTinhEntity(modelThuocTinh model, ThuocTinhEntity item);
        List<ThuocTinhEntity> ToThuocTinhEntites(List<modelThuocTinh> listmodel);
        List<ThuocTinhEntity> ChuyenDoi(List<modelThuocTinh> listmodel);
        List<modelThuocTinh> ToThuocTinhModel(List<modelThuocTinh> listmodel);

        #endregion
    }
}
