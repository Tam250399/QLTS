using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.WebApi.Models.DMDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GS.WebApi.Factories
{
    public interface IDMDC_DanhMucSvcModelFactory
    {
        #region DiaBan
        MessageReturn DMDC(HttpWebRequest webRequest);
        MessageReturn InsertOrUpdateDMDCDiaBan(DMDC_DiaBanModel model);
        List<MessageReturnDMDC> InsertOrUpdateListDMDC_DiaBan(List<DMDC_DiaBanModel> model);
        #endregion
        #region QuocGia
        MessageReturn InsertOrUpdateDMDCQuocGia(DMDC_QuocGiaModel model);
        List<MessageReturnDMDC> InsertOrUpdateListDMDCQuocGia(List<DMDC_QuocGiaModel> ListModel);
        #endregion
        #region DonViNganSach
        MessageReturn InsertOrUpdateDMDCDonViNganSach(DMDC_DonViNganSachModel model);
        List<MessageReturnDMDC> InsertOrUpdateListDMDC_DonViNganSach(List<DMDC_DonViNganSachModel> ListModel);
        #endregion

        MessageReturn TransferData(DMDC_TransferData model);
    }
}
