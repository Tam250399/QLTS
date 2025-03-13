using GS.Core.Domain.SHTD;
using GS.Web.Models.SHTD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Factories.SHTD
{
    public partial interface IXuLyKetQuaModelFactory
    {
        #region XuLyKetQua

        XuLyKetQuaSearchModel PrepareXuLyKetQuaSearchModel(XuLyKetQuaSearchModel searchModel);

        XuLyKetQuaListModel PrepareXuLyKetQuaListModel(XuLyKetQuaSearchModel searchModel);

        XuLyKetQuaModel PrepareXuLyKetQuaModel(XuLyKetQuaModel model, XuLyKetQua item, bool excludeProperties = false);

        void PrepareXuLyKetQua(XuLyKetQuaModel model, XuLyKetQua item);

        #endregion
    }
}
