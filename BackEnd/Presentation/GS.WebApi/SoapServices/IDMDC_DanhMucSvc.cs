using GS.Core.Domain.Common;
using System.Collections.Generic;
using System.ServiceModel;

namespace GS.WebApi.SoapServices
{
    [ServiceContract(Namespace = "http://service_dmdc.mof.gov.vn/")]
    public interface IDMDC_DanhMucSvc
    {
        //#endregion
        //[OperationContract]
        //List<MessageReturnDMDC> TransferData(DMDC_TransferData model);
        //[OperationContract]
        //List<MessageReturnDMDC> TransferData(string xmlMsg);
        [OperationContract]
        List<MessageReturnDMDC> transferData(string xmlMsg);
        //[OperationContract]
        //string transferData(string xmlMsg);

        //[OperationContract]
        //string transferData([FromBody] DMDC_TransferData model);
    }
}