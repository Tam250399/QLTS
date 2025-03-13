//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.SHTD;
using GS.Web.Models.SHTD;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace GS.Web.Factories.SHTD
{
    public partial interface IQuyetDinhTichThuModelFactory 
    {
        #region QuyetDinhTichThu
        bool CheckTongGiaTriTichThu(Guid Guid, decimal TongGiaTriTichThu);
        QuyetDinhTichThuSearchModel PrepareQuyetDinhTichThuSearchModel(QuyetDinhTichThuSearchModel searchModel);
        
        QuyetDinhTichThuListModel PrepareQuyetDinhTichThuListModel(QuyetDinhTichThuSearchModel searchModel);
        
        QuyetDinhTichThuModel PrepareQuyetDinhTichThuModel(QuyetDinhTichThuModel model, QuyetDinhTichThu item, bool excludeProperties = false, bool prepareDDL = false);
        
        void PrepareQuyetDinhTichThu(QuyetDinhTichThuModel model, QuyetDinhTichThu item);
        List<SelectListItem> PrepareListModelForBaoCao();
        bool CheckDaTonTaiKetQuaTheoTaiSan(decimal QuyetDinhID);
        List<TaiSanToanDanModel> ImportExcel(List<ImportTaiSanToanDanModel> ListImport);
        List<SelectListItem> PrepareDDLQuyetDinhForPhuongAn(bool isAddFirst = true, List<int> ListQuyetDinhId = null);
        int DuyetListQuyetDinhTichThu(string strListQuyetDinhID);
        #endregion
    }
}
