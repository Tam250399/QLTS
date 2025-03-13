//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 15/5/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.DB
{
    public partial class DBTaiSan : BaseEntity
    {
        public DBTaiSan()
        {
            this.GUID = Guid.NewGuid();
        }
        public Guid GUID { get; set; }
        public String QLDKTS_MA { get; set; }
        /// <summary>
        /// trường hợp dữ liệu là biến động sẽ lưu ID biến động của kho
        /// </summary>
		public String DB_MA { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_TAI_SAN_DON_VI_ID { get; set; }
        public Decimal PHAN_MEM_DONG_BO_ID { get; set; }
        public String DATA_JSON { get; set; }
        public DateTime NGAY_DONG_BO { get; set; }
        public Boolean? IS_TAI_SAN_TOAN_DAN { get; set; }
        public String QUYET_DINH_TICH_THU_SO { get; set; }
        public DateTime? QUYET_DINH_TICH_THU_NGAY { get; set; }
        public Decimal? TAI_KHOAN_DONG_BO_ID { get; set; }
        public bool? IS_BIEN_DONG { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public String RESPONSE { get; set; }

    }
    public enum enumTrangThaiDbTaiSan
    {
        DaInsert = 1,
        ChuaInsert = 2,
        DangChayJob = 3,
        LoiHT = -1,
        LoiDL = 0
    }
    public partial class DBTaiSanView : BaseViewEntity
    {
        public DBTaiSanView()
        {
            this.GUID = Guid.NewGuid();
        }
        public decimal ID { get; set; }
        public Guid GUID { get; set; }
        public String QLDKTS_MA { get; set; }
        /// <summary>
        /// trường hợp dữ liệu là biến động sẽ lưu ID biến động của kho
        /// </summary>
		public String DB_MA { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_TAI_SAN_DON_VI_ID { get; set; }
        public Decimal PHAN_MEM_DONG_BO_ID { get; set; }
        public String DATA_JSON { get; set; }
        public DateTime NGAY_DONG_BO { get; set; }
        public Decimal? IS_TAI_SAN_TOAN_DAN { get; set; }
        public String QUYET_DINH_TICH_THU_SO { get; set; }
        public DateTime? QUYET_DINH_TICH_THU_NGAY { get; set; }
        public Decimal? TAI_KHOAN_DONG_BO_ID { get; set; }
        public Decimal? IS_BIEN_DONG { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public String RESPONSE { get; set; }

    }
}



