using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GS.WebApi.Models.DMDC
{
    public class DMDC_TransferData
    {
        public string xmlMsg { get; set; }
    }

    [XmlRoot("Data")]
    public class DMDC_data
    {
        public GsHeader Header { get; set; }
        public String Body { get; set; }
    }

    [XmlRoot("Header")]
    public class GsHeader
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
    }

    //public class GsHeader: SoapHeader
    //{
    //    [StringLength(250, ErrorMessage = "MA tối đa 250 ký tự")]
    //    [Required(ErrorMessage = "Version null")]
    //    public string Version { get; set; }
    //    [StringLength(50, ErrorMessage = "MA tối đa 50 ký tự")]
    //    [Required(ErrorMessage = "Sender_Code null")]
    //    public string Sender_Code { get; set; }
    //    [StringLength(250, ErrorMessage = "MA tối đa 250 ký tự")]
    //    [Required(ErrorMessage = "Sender_Name null")]
    //    public string Sender_Name { get; set; }
    //    [StringLength(50, ErrorMessage = "MA tối đa 50 ký tự")]
    //    [Required(ErrorMessage = "Receiver_Code null")]
    //    public string Receiver_Code { get; set; }
    //    [StringLength(250, ErrorMessage = "MA tối đa 250 ký tự")]
    //    [Required(ErrorMessage = "Receiver_Name null")]
    //    public string Receiver_Name { get; set; }
    //    [StringLength(10, ErrorMessage = "MA tối đa 10 ký tự")]
    //    [Required(ErrorMessage = "Tran_Code null")]
    //    public string Tran_Code { get; set; }
    //    [StringLength(150, ErrorMessage = "MA tối đa 150 ký tự")]
    //    [Required(ErrorMessage = "Tran_Name null")]
    //    public string Tran_Name { get; set; }
    //    [StringLength(50, ErrorMessage = "MA tối đa 50 ký tự")]
    //    [Required(ErrorMessage = "Msg_ID null")]
    //    public string Msg_ID { get; set; }
    //    [StringLength(50, ErrorMessage = "MA tối đa 50 ký tự")]
    //    [Required(ErrorMessage = "Msg_RefID null")]
    //    public string Msg_RefID { get; set; }
    //    [StringLength(19, ErrorMessage = "MA tối đa 19 ký tự")]
    //    [Required(ErrorMessage = "Send_Date null")]
    //    public string Send_Date { get; set; }
    //    [StringLength(50, ErrorMessage = "MA tối đa 50 ký tự")]
    //    [Required(ErrorMessage = "Original_Code null")]
    //    public string Original_Code { get; set; }
    //    [StringLength(250, ErrorMessage = "MA tối đa 250 ký tự")]
    //    [Required(ErrorMessage = "Original_Name null")]
    //    public string Original_Name { get; set; }
    //    [StringLength(19, ErrorMessage = "MA tối đa 19 ký tự")]
    //    [Required(ErrorMessage = "Export_Date null")]
    //    public string Export_Date { get; set; }
    //    [StringLength(5, ErrorMessage = "MA tối đa 5 ký tự")]
    //    public string Notes { get; set; }
    //    [StringLength(5, ErrorMessage = "MA tối đa 5 ký tự")]
    //    [Required(ErrorMessage = "Tran_Num null")]
    //    public string Tran_Num { get; set; }
    //    [StringLength(50, ErrorMessage = "MA tối đa 50 ký tự")]
    //    [Required(ErrorMessage = "Path null")]
    //    public string Path { get; set; }
    //    [StringLength(3, ErrorMessage = "MA tối đa 3 ký tự")]
    //    [Required(ErrorMessage = "NumMsg_InGroup null")]
    //    public string NumMsg_InGroup { get; set; }
    //    [StringLength(10, ErrorMessage = "MA tối đa 10 ký tự")]
    //    [Required(ErrorMessage = "SPARE1 null")]
    //    public string SPARE1 { get; set; }
    //    [StringLength(10, ErrorMessage = "MA tối đa 10 ký tự")]
    //    [Required(ErrorMessage = "SPARE2 null")]
    //    public string SPARE2 { get; set; }
    //    [StringLength(10, ErrorMessage = "MA tối đa 10 ký tự")]
    //    [Required(ErrorMessage = "SPARE3 null")]
    //    public string SPARE3 { get; set; }
    //    [StringLength(10, ErrorMessage = "MA tối đa 10 ký tự")]
    //    [Required(ErrorMessage = "Finish_Code null")]
    //    public string Finish_Code { get; set; }
    //}
    [XmlRoot(ElementName = "DATA")]
    public class GsBodyDiaBan
    {
        [XmlElement("ROW", typeof(DMDC_DiaBanModel))]
        public List<DMDC_DiaBanModel> data { get; set; }
    }

    [XmlRoot(ElementName = "DATA")]
    public class GsBodyDonVi
    {
        [XmlElement("ROW", typeof(DMDC_DonViNganSachModel))]
        public List<DMDC_DonViNganSachModel> data { get; set; }
    }

    [XmlRoot(ElementName = "DATA")]
    public class GsBodyQuocGia
    {
        [XmlElement("ROW", typeof(DMDC_QuocGiaModel))]
        public List<DMDC_QuocGiaModel> data { get; set; }
    }
}