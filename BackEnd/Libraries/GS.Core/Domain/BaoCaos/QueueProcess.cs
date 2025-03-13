using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos
{
	public enum enumTRANG_THAI_QUEUE_BAO_CAO
	{
		TAT_CA = -1,
		TRANG_THAI_CHO = 0,
		DANG_LAY_DU_LIEU = 1,
		DANG_CHO_XUAT_FILE =2,
		DANG_XUAT_FILE = 3,
		DA_EXPORT_FILE = 4,
		DA_LUU_FILE = 5,
		LOI = 6,
		//đồng bộ báo cáo
		DB_TRANG_THAI_CHO = 10,
		DB_DANG_LAY_DU_LIEU = 11,
		DB_DANG_CHO_XUAT_FILE = 12,
		DB_DANG_XUAT_FILE = 13,
		DB_DA_EXPORT_FILE = 14,
		DB_DA_LUU_FILE = 15,
		DB_LOI = 16,
	}

    public enum enumTypeQueue_Process
    {
        QLTSC = 0,
        CSDLQGTSC = 1
    }
	public enum enumEXPORT_FILE_TYPE
	{
		WORD_DOCX=0,
		EXCEL_XLSX =1,
		PDF = 2,
		JSON =3,
		WORD_DOC =4,
		EXCEL_XLS =5
	}
	public class QueueProcess : BaseEntity
	{
		public string MA_BAO_CAO { get; set; }
		public int DON_VI_ID { get; set; }
		public int TYPE_QUEUE_PROCESS_ID { get; set; }
		public string DATA_JSON { get; set; }
		public int? TRANG_THAI_ID { get; set; }
		public DateTime NGAY_TAO { get; set; }
		public int? NGUOI_TAO_ID { get; set; }
		public Guid GUID { get; set; }
		public int? FILE_TYPE { get; set; }
		public Guid GUID_FILE { get; set; }
		public DateTime? TIME_START_GET_DATA { get; set; }
		public DateTime? TIME_END_GET_DATA { get; set; }
		public string STATEMENT { get; set; }
		public string SEARCH_MODEL_JSON { get; set; }
		public string SEARCH_MODEL_CLASS { get; set; }
		public string REPORT_CLASS { get; set; }
		public string MODEL_DATA_TYPE { get; set; }
		public string RESPONSE { get; set; }
        public decimal? DB_QUEUE_PROCESS_ID { get; set; }
    }

    public class QueueProcessSearch : BaseEntity
    {
        public string MA_BAO_CAO { get; set; }
        public int DON_VI_ID { get; set; }
        public int TYPE_QUEUE_PROCESS_ID { get; set; }
        public int? TRANG_THAI_ID { get; set; }
        public DateTime NGAY_TAO { get; set; }
        public int? NGUOI_TAO_ID { get; set; }
        public Guid GUID { get; set; }
        public int? FILE_TYPE { get; set; }
        public Guid GUID_FILE { get; set; }
        public DateTime? TIME_START_GET_DATA { get; set; }
        public DateTime? TIME_END_GET_DATA { get; set; }
        public string STATEMENT { get; set; }
        public string SEARCH_MODEL_JSON { get; set; }
        public string SEARCH_MODEL_CLASS { get; set; }
        public string REPORT_CLASS { get; set; }
        public string MODEL_DATA_TYPE { get; set; }
        public string RESPONSE { get; set; }
        public decimal? DB_QUEUE_PROCESS_ID { get; set; }
    }
}
