//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DanhMuc;
using System;


namespace GS.Core.Domain.TaiSans
{
    public partial class TaiSanOto : BaseEntity
    {
        public TaiSanOto()
        {

        }
        public TaiSanOto(TaiSanOto obj)
        {
            this.TAI_SAN_ID = obj.TAI_SAN_ID;
            this.BIEN_KIEM_SOAT = obj.BIEN_KIEM_SOAT;
            this.SO_CHO_NGOI = obj.SO_CHO_NGOI;
            this.DUNG_TICH = obj.DUNG_TICH;
            this.CHUC_DANH_ID = obj.CHUC_DANH_ID;
            this.TAI_TRONG = obj.TAI_TRONG;
            this.SO_KHUNG = obj.SO_KHUNG;
            this.NHAN_XE_ID = obj.NHAN_XE_ID;
            this.CONG_XUAT = obj.CONG_XUAT;
            this.SO_MAY = obj.SO_MAY;
            this.SO_CAU_XE = obj.SO_CAU_XE;
            this.GCN_DANG_KY = obj.GCN_DANG_KY;
            this.CO_QUAN_CAP_DANG_KY = obj.CO_QUAN_CAP_DANG_KY;
            this.NGAY_DANG_KY = obj.NGAY_DANG_KY;
        }
        public Decimal TAI_SAN_ID { get; set; }       
        public String BIEN_KIEM_SOAT { get; set; }
        public Decimal? SO_CHO_NGOI { get; set; }
        public Decimal? DUNG_TICH { get; set; }
        public Decimal? CHUC_DANH_ID { get; set; }
        //Thêm vào để xử lý yêu cầu chọn nhiều chức danh 1 lúc -- hungnt
        public string LIST_CHUC_DANH_ID { get; set; }
        public Decimal? TAI_TRONG { get; set; }
        public String SO_KHUNG { get; set; }
        public Decimal? NHAN_XE_ID { get; set; }
        public Decimal? CONG_XUAT { get; set; }
        public Decimal? DONG_XE_ID { get; set; }
        public String SO_MAY { get; set; }
        public decimal? SO_CAU_XE { get; set; }
        public string GCN_DANG_KY { get; set; }
        public string CO_QUAN_CAP_DANG_KY { get; set; }
        public DateTime? NGAY_DANG_KY { get; set; }
        public virtual ChucDanh chucDanh { get; set; }
        public virtual TaiSan taisan { get; set; }
        public virtual NhanXe nhanxe { get; set; }
    }
}



