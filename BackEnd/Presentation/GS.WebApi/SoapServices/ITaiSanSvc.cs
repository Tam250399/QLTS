using GS.Core.Domain.Common;
using GS.WebApi.Models;
using GS.WebApi.Models.TaiSanXacLap;
using GS.WebApi.Models.TaiSan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace GS.WebApi.SoapServices
{
    [ServiceContract]
    public interface ITaiSanSvc
    {
        #region TaiSan
        [OperationContract]
        IList<DBTaiSanModel> GetAllTaiSans();
        [OperationContract]
        MessageReturn UpdateTaiSan(DBTaiSanModel model);
        [OperationContract]
        MessageReturn UpdateTaiSans(List<DBTaiSanModel> ListModel);
        #endregion
        #region TaiSanNhatKy
        [OperationContract]
        MessageReturn UpdateTaiSanDaDongBo(List<string> ListMaTaiSan, List<QuyetDinh> ListQuyetDinhTichThu, string MaDonVi);
        #endregion
        #region TaiSanToanDan
        //[OperationContract]
        //List<QuyetDinhTichThuModel> GetQuyetDinhTichThuModels();
        //[OperationContract]
        //MessageReturn UpdateTaiSanToanDan(QuyetDinhTichThuModel model);
        //[OperationContract]
        //MessageReturn UpdateListTaiSanToanDan(List<QuyetDinhTichThuModel> ListModel);
        #endregion
    }
}
