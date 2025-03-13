//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.HeThong;
using GS.Web.Models.HeThong;
using System.Collections.Generic;

namespace GS.Web.Factories.HeThong
{
    public partial interface IQuyenModelFactory
    {
        #region Quyen

        QuyenSearchModel PrepareQuyenSearchModel(QuyenSearchModel searchModel);

        QuyenListModel PrepareQuyenListModel(QuyenSearchModel searchModel);

        QuyenModel PrepareQuyenModel(QuyenModel model, Quyen item, bool excludeProperties = false, List<decimal> lstquyenchon = null);

        void PrepareQuyen(QuyenModel model, Quyen item);
        bool CheckMaQuyen(string MA, decimal id = 0);
        #endregion
    }
}
