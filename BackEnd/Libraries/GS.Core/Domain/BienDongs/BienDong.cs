//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.TaiSans;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GS.Core.Domain.BienDongs
{
    public partial class BienDong : BaseEntity
    {
        public BienDong()
        {

        }
        public BienDong(BienDong obj)
        {
            this.TAI_SAN_ID = obj.TAI_SAN_ID;
            this.TAI_SAN_MA = obj.TAI_SAN_MA;
            this.TAI_SAN_TEN = obj.TAI_SAN_TEN;
            this.LOAI_TAI_SAN_ID = obj.LOAI_TAI_SAN_ID;
            this.LOAI_HINH_TAI_SAN_ID = obj.LOAI_HINH_TAI_SAN_ID;
            this.NGUYEN_GIA = obj.NGUYEN_GIA;
            this.DON_VI_BO_PHAN_ID = obj.DON_VI_BO_PHAN_ID;
            this.CHUNG_TU_SO = obj.CHUNG_TU_SO;
            this.CHUNG_TU_NGAY = obj.CHUNG_TU_NGAY;
            this.NGAY_BIEN_DONG = obj.NGAY_BIEN_DONG;
            this.NGAY_SU_DUNG = obj.NGAY_SU_DUNG;
            this.LOAI_BIEN_DONG_ID = obj.LOAI_BIEN_DONG_ID;
            this.LY_DO_BIEN_DONG_ID = obj.LY_DO_BIEN_DONG_ID;
            this.TRANG_THAI_ID = obj.TRANG_THAI_ID;
            this.DON_VI_ID = obj.DON_VI_ID;
            this.NGUOI_TAO_ID = obj.NGUOI_TAO_ID;
            this.NGAY_TAO = obj.NGAY_TAO;
            this.GUID = obj.GUID;
            this.GHI_CHU = obj.GHI_CHU;
            this.QUYET_DINH_NGAY = obj.QUYET_DINH_NGAY;
            this.QUYET_DINH_SO = obj.QUYET_DINH_SO;
            this.IS_BIENDONG_CUOI = obj.IS_BIENDONG_CUOI;
            this.LOAI_TAI_SAN_DON_VI_ID = obj.LOAI_TAI_SAN_DON_VI_ID;
            this.DAT_TONG_DIEN_TICH = obj.DAT_TONG_DIEN_TICH;
            this.NHA_TONG_DIEN_TICH_XD = obj.NHA_TONG_DIEN_TICH_XD;
            this.VKT_DIEN_TICH = obj.VKT_DIEN_TICH;
        }

        public Decimal TAI_SAN_ID { get; set; }
        public String TAI_SAN_MA { get; set; }
        public String TAI_SAN_TEN { get; set; }
        public Decimal? LOAI_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? NGUYEN_GIA { get; set; }
        public Decimal? DON_VI_BO_PHAN_ID { get; set; }
        public String CHUNG_TU_SO { get; set; }
        public DateTime? CHUNG_TU_NGAY { get; set; }
        public DateTime? NGAY_BIEN_DONG { get; set; }
        public DateTime? NGAY_DUYET { get; set; }
        public Decimal? NGUOI_DUYET_ID { get; set; }
        public DateTime? NGAY_SU_DUNG { get; set; }
        public Decimal? LOAI_BIEN_DONG_ID { get; set; }
        public Decimal? LY_DO_BIEN_DONG_ID { get; set; }
        public Decimal TRANG_THAI_ID { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        public Decimal? NGUOI_TAO_ID { get; set; }
        public DateTime NGAY_TAO { get; set; }
        public Guid GUID { get; set; }
        public String GHI_CHU { get; set; }
        public DateTime? QUYET_DINH_NGAY { get; set; }
        public String QUYET_DINH_SO { get; set; }
		public bool? IS_BIENDONG_CUOI { get; set; }
        public Decimal? LOAI_TAI_SAN_DON_VI_ID { get; set; }
        public virtual LoaiTaiSan loaitaisan { get; set; }
        public virtual TaiSan taisan { get; set; }
        public virtual NguoiDung nguoidung { get; set; }
        public virtual DonVi donvi { get; set; }
        public virtual LyDoBienDong lydobiendong { get; set; }
        public virtual DonViBoPhan donvibophan { get; set; }
        //public Guid? DB_GUID { get; set; }
        public string ID_DB { get; set; }
        public Decimal TRANG_THAI_DONG_BO { get; set; }
        public Decimal? HOA_HONG_NOP_NSNN { get; set; }
        public Decimal? HOA_HONG_DE_LAI_DON_VI { get; set; }
        public Decimal? DAT_TONG_DIEN_TICH { get; set; }
        public Decimal? NHA_TONG_DIEN_TICH_XD { get; set; }
        public Decimal? VKT_DIEN_TICH { get; set; }
        public String MA_BO { get; set; }
    }
    public class GiaTriTaiSan : BaseViewEntity {
        public decimal? TAI_SAN_ID { get; set; }
        public decimal? NHA_TONG_DIEN_TICH_XD_CU { get; set; }
        public decimal? NHA_DIEN_TICH_XD_CU { get; set; }
        public decimal? NGUYEN_GIA_CU { get; set; }
        public decimal? DAT_TONG_DIEN_TICH_CU { get; set; }
        public decimal? VKT_CHIEU_DAI_CU { get; set; }
        public decimal? VKT_DIEN_TICH_CU { get; set; }
        public decimal? VKT_THE_TICH_CU { get; set; }
        public decimal? HM_GIA_TRI_CON_LAI_CU { get; set; }
        [NotMapped]
        public decimal? DAT_GIA_TRI_QUYEN_SU_DUNG { get; set; }

    }
    public class GiaTriNguonVon: BaseViewEntity
	{
        public decimal? OLD_NGUON_VON_NS { get; set; }
        public decimal? OLD_NGUON_VON_VIEN_TRO { get; set; }
        public decimal? OLD_NGUON_VON_SU_NGHIEP { get; set; }
        public decimal? OLD_NGUON_VON_KHAC { get; set; }
        public decimal? NEW_NGUON_VON_NS { get; set; }
        public decimal? NEW_NGUON_VON_VIEN_TRO { get; set; }
        public decimal? NEW_NGUON_VON_SU_NGHIEP { get; set; }
        public decimal? NEW_NGUON_VON_KHAC { get; set; }
    }
    public class GiaTriNguyenGia : BaseViewEntity
    {
        public decimal? NGUYEN_GIA { get; set; }
    }
}



