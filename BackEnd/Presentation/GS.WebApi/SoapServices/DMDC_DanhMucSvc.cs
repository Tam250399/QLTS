using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Services.Logging;
using GS.WebApi.Factories;
using GS.WebApi.Models.DMDC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace GS.WebApi.SoapServices
{
    public class DMDC_DanhMucSvc : IDMDC_DanhMucSvc
    {
        #region Ctor

        private readonly IDMDC_DanhMucSvcModelFactory _dMDC_DanhMucSvcModelFactory;
        private readonly ILogger _logger;

        public DMDC_DanhMucSvc(
            IDMDC_DanhMucSvcModelFactory dMDC_DanhMucSvcModelFactory,
            ILogger logger
        )
        {
            this._logger = logger;
            this._dMDC_DanhMucSvcModelFactory = dMDC_DanhMucSvcModelFactory;
        }

        #endregion Ctor

        public List<MessageReturnDMDC> transferData(String xmlMsg)
        {
            if (string.IsNullOrEmpty(xmlMsg))
                return new List<MessageReturnDMDC>() { MessageReturnDMDC.CreateErrorMessage("Không có dữ liệu") };
            NhatKy nhatKy = _logger.InsertLog(logLevel: LogLevel.Information, shortMessage: "DMDC_DanhMucSvc/transferData", dataJson: xmlMsg);
            try
            {
                //var xmlMsg = model.xmlMsg;
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlMsg);
                XmlNode xmlNode = xmlDocument.SelectSingleNode("Data");
                string xmlHeader = xmlNode["Header"].OuterXml;
                var nodeBody = xmlNode.SelectSingleNode("BODY");
                string xmlBody = nodeBody["DATA"].OuterXml;

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(GsHeader));
                XmlTextReader textReaderHeader = new XmlTextReader(new StringReader(xmlHeader));
                var header = (GsHeader)xmlSerializer.Deserialize(textReaderHeader);

                XmlTextReader textReaderBody = new XmlTextReader(new StringReader(xmlBody));

                if (header.Tran_Code == enumDMDC_TranCode.DiaBan)
                {
                    xmlSerializer = new XmlSerializer(typeof(GsBodyDiaBan));
                    var body = (GsBodyDiaBan)xmlSerializer.Deserialize(textReaderBody);
                    var rs = _dMDC_DanhMucSvcModelFactory.InsertOrUpdateListDMDC_DiaBan(body.data);
                    _logger.InsertLog(logLevel: LogLevel.Information, shortMessage: nhatKy.GUID.ToString(), dataJson: rs.toStringJson());
                    return rs;
                }
                else if (header.Tran_Code == enumDMDC_TranCode.QuocGia)
                {
                    xmlSerializer = new XmlSerializer(typeof(GsBodyQuocGia));
                    var body = (GsBodyQuocGia)xmlSerializer.Deserialize(textReaderBody);
                    var rs = _dMDC_DanhMucSvcModelFactory.InsertOrUpdateListDMDCQuocGia(body.data);
                    _logger.InsertLog(logLevel: LogLevel.Information, shortMessage: nhatKy.GUID.ToString(), dataJson: rs.toStringJson());
                    return rs;
                }
                else if (header.Tran_Code == enumDMDC_TranCode.DonViNganSach)
                {
                    xmlSerializer = new XmlSerializer(typeof(GsBodyDonVi));
                    var body = (GsBodyDonVi)xmlSerializer.Deserialize(textReaderBody);
                    var rs = _dMDC_DanhMucSvcModelFactory.InsertOrUpdateListDMDC_DonViNganSach(body.data);
                    _logger.InsertLog(logLevel: LogLevel.Information, shortMessage: nhatKy.GUID.ToString(), dataJson: rs.toStringJson());
                    return rs;
                }
                return new List<MessageReturnDMDC>() { MessageReturnDMDC.CreateErrorMessage("Error: dữ liệu không đúng") };
            }
            catch (Exception ex)
            {
                return new List<MessageReturnDMDC>() { MessageReturnDMDC.CreateErrorMessage("Error: " + ex.Message) };
            }
        }
    }
}