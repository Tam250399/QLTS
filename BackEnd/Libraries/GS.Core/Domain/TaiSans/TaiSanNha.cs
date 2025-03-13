//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using System;


namespace GS.Core.Domain.TaiSans
{
    public partial class TaiSanNha : BaseEntity
    {
        public TaiSanNha()
        {

        }
        public TaiSanNha(TaiSanNha obj)
        {
            this.TAI_SAN_ID = obj.TAI_SAN_ID;
            this.TAI_SAN_DAT_ID = obj.TAI_SAN_DAT_ID;
            this.DIEN_TICH_XAY_DUNG = obj.DIEN_TICH_XAY_DUNG;
            this.DIEN_TICH_SAN_XAY_DUNG = obj.DIEN_TICH_SAN_XAY_DUNG;
            this.NAM_XAY_DUNG = obj.NAM_XAY_DUNG;
            this.NGAY_SU_DUNG = obj.NGAY_SU_DUNG;
            this.DIA_CHI = obj.DIA_CHI;
            //this.HS_QUYET_DINH_BAN_GIAO = obj.HS_QUYET_DINH_BAN_GIAO;
            //this.HS_QUYET_DINH_BAN_GIAO_NGAY = obj.HS_QUYET_DINH_BAN_GIAO_NGAY;
            //this.HS_BIEN_BAN_NGHIEM_THU = obj.HS_BIEN_BAN_NGHIEM_THU;
            //this.HS_BIEN_BAN_NGHIEM_THU_NGAY = obj.HS_BIEN_BAN_NGHIEM_THU_NGAY;
            //this.HS_PHAP_LY_KHAC = obj.HS_PHAP_LY_KHAC;
            //this.HS_PHAP_LY_KHAC_NGAY = obj.HS_PHAP_LY_KHAC_NGAY;
        }
        public Decimal TAI_SAN_ID { get; set; }
        public Decimal? TAI_SAN_DAT_ID { get; set; }
        public Decimal? DIEN_TICH_XAY_DUNG { get; set; }
        public Decimal? DIEN_TICH_SAN_XAY_DUNG { get; set; }
        public Decimal? NAM_XAY_DUNG { get; set; }
        public DateTime? NGAY_SU_DUNG { get; set; }
        public String DIA_CHI { get; set; }
        //Ho so, giay to
        //public String HS_QUYET_DINH_BAN_GIAO { get; set; }
        //public DateTime? HS_QUYET_DINH_BAN_GIAO_NGAY { get; set; }
        //public String HS_BIEN_BAN_NGHIEM_THU { get; set; }
        //public DateTime? HS_BIEN_BAN_NGHIEM_THU_NGAY { get; set; }
        //public String HS_PHAP_LY_KHAC { get; set; }
        //public DateTime? HS_PHAP_LY_KHAC_NGAY { get; set; }
        public virtual TaiSan taisan { get; set; }
        public decimal? TINH_ID { get; set; }
        public decimal? HUYEN_ID { get; set; }
        public decimal? XA_ID { get; set; }
        public decimal? DIA_BAN_ID { get; set; }
    }
}



