//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DanhMuc;
using System;

namespace GS.Core.Domain.TaiSans
{
    public partial class TaiSanDat : BaseEntity
    {
        public TaiSanDat()
        {

        }
        public TaiSanDat(TaiSanDat obj)
        {
            this.TAI_SAN_ID = obj.TAI_SAN_ID;
            this.DIA_CHI = obj.DIA_CHI;
            this.DIA_BAN_ID = obj.DIA_BAN_ID;
            this.DIEN_TICH = obj.DIEN_TICH;
            this.DIEN_TICH_XAY_NHA = obj.DIEN_TICH_XAY_NHA;
            this.TINH_ID = obj.TINH_ID;
            this.HUYEN_ID = obj.HUYEN_ID;
            this.XA_ID = obj.XA_ID;
            //this.HS_CNQSD_SO = obj.HS_CNQSD_SO;
            //this.HS_CNQSD_NGAY = obj.HS_CNQSD_NGAY;
            //this.HS_QUYET_DINH_GIAO_SO = obj.HS_QUYET_DINH_GIAO_SO;
            //this.HS_QUYET_DINH_GIAO_NGAY = obj.HS_QUYET_DINH_GIAO_NGAY;
            //this.HS_CHUYEN_NHUONG_SD_SO = obj.HS_CHUYEN_NHUONG_SD_SO;
            //this.HS_CHUYEN_NHUONG_SD_NGAY = obj.HS_CHUYEN_NHUONG_SD_NGAY;
            //this.HS_QUYET_DINH_CHO_THUE_SO = obj.HS_QUYET_DINH_CHO_THUE_SO;
            //this.HS_QUYET_DINH_CHO_THUE_NGAY = obj.HS_QUYET_DINH_CHO_THUE_NGAY;
            //this.HS_KHAC = obj.HS_KHAC;
        }

        public Decimal TAI_SAN_ID { get; set; }
        public String DIA_CHI { get; set; }
        public Decimal? DIA_BAN_ID { get; set; }
        public Decimal DIEN_TICH { get; set; }
        public Decimal? DIEN_TICH_XAY_NHA { get; set; }
        public decimal? TINH_ID { get; set; }
        public decimal? HUYEN_ID { get; set; }
        public decimal? XA_ID { get; set; }
        //public String HS_CNQSD_SO { get; set; }
        //public DateTime? HS_CNQSD_NGAY { get; set; }
        //public String HS_QUYET_DINH_GIAO_SO { get; set; }
        //public DateTime? HS_QUYET_DINH_GIAO_NGAY { get; set; }
        //public String HS_CHUYEN_NHUONG_SD_SO { get; set; }
        //public DateTime? HS_CHUYEN_NHUONG_SD_NGAY { get; set; }
        //public String HS_QUYET_DINH_CHO_THUE_SO { get; set; }
        //public DateTime? HS_QUYET_DINH_CHO_THUE_NGAY { get; set; }
        //public String HS_KHAC { get; set; }
        public virtual TaiSan taisan { get; set; }
        public virtual DiaBan diaban { get; set; }
    }
}



