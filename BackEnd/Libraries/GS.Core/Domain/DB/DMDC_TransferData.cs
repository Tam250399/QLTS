using System;
using System.Collections;
using System.Xml.Serialization;


namespace GS.Core.Domain.DB
{
    [XmlRoot("Header")]
    public class GsHeaderXml
    {
        public string Version { get; set; }
        public string Sender_Code { get; set; }
        public string Sender_Name { get; set; }
        public string Receiver_Code { get; set; }
        public string Receiver_Name { get; set; }
        public string Tran_Code { get; set; }
        public string Tran_Name { get; set; }
        public string Msg_ID { get; set; }
        public string Msg_RefID { get; set; }
        public string Send_Date { get; set; }
        public string Original_Code { get; set; }
        public string Original_Name { get; set; }
        public string Export_Date { get; set; }
        public string Notes { get; set; }
        public string Tran_Num { get; set; }
        public string Path { get; set; }
        public string NumMsg_InGroup { get; set; }
        public string SPARE1 { get; set; }
        public string SPARE2 { get; set; }
        public string SPARE3 { get; set; }
        public string Finish_Code { get; set; }
        public string Finish_Name { get; set; }
    }
}