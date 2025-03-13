using GS.Core.Domain.SHTD;
using GS.Web.Models.SHTD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Factories.SHTD
{
    public partial interface IKetQuaTaiSanModelFactory
    {
        #region KetQuaTaiSan

        KetQuaTaiSanSearchModel PrepareKetQuaTaiSanSearchModel(KetQuaTaiSanSearchModel searchModel);

        KetQuaTaiSanListModel PrepareKetQuaTaiSanListModel(KetQuaTaiSanSearchModel searchModel);

        KetQuaTaiSanModel PrepareKetQuaTaiSanModel(KetQuaTaiSanModel model, KetQuaTaiSan item, bool excludeProperties = false);

        void PrepareKetQuaTaiSan(KetQuaTaiSanModel model, KetQuaTaiSan item);
        decimal PrePareSoLuongConLai(decimal TaiSanTDXuLyId, decimal XuLyKetQuaId, decimal KetQuaTaiSanId);

        #endregion
    }
}
