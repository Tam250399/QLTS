using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.Api.TaiSanDBApi
{
    public class TaiSanDongBoApi:BaseEntity
    {
        public TaiSanDongBoApi()
        {
            this.LST_BIEN_DONG = new List<BienDongApi>();
            this.LST_HIEN_TRANG = new List<TaiSanHienTrangSuDungApi>();
            this.LST_NGUON_VON = new List<TaiSanNguonVonApi>();
            // this.YEU_CAU = new YeuCauApi();
        }
        public decimal? NGUON_TAI_SAN_ID { get; set; }
        public String TEN_TAI_SAN { get; set; }
        public String MA_TAI_SAN { get; set; }
        public String LOAI_TAI_SAN_TEN { get; set; }
        public decimal? LOAI_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_TAI_SAN_DON_VI_ID { get; set; }
        public DateTime? NGAY_NHAP { get; set; }
        public DateTime? NGAY_DUYET { get; set; }
        public DateTime? NGAY_SU_DUNG { get; set; }
        public String LY_DO_BIEN_DONG_MA { get; set; }   
        public String LY_DO_BIEN_DONG_TEN { get; set; }
        public String MA_LOAI_TAI_SAN { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? NGUYEN_GIA_BAN_DAU { get; set; }
        public String MA_DON_VI { get; set; }
        public Decimal? TRANG_THAI { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public String GHI_CHU { get; set; }
        public String DON_VI_BO_PHAN_MA { get; set; }
        public decimal? DON_VI_BO_PHAN_ID { get; set; }
        public decimal? NUOC_SAN_XUAT_ID { get; set; }
        public String NUOC_SAN_XUAT_MA { get; set; }
        public Decimal? NAM_SAN_XUAT { get; set; }
        public Decimal? GIA_MUA_TIEP_NHAN { get; set; }
        public Decimal? GIA_HOA_DON { get; set; }
        public String QUYET_DINH_SO { get; set; }
        public DateTime? QUYET_DINH_NGAY { get; set; }
        public String CHUNG_TU_SO { get; set; }
        public DateTime? CHUNG_TU_NGAY { get; set; }
        public String MA_DU_AN { get; set; }
        public String LST_BIEN_DONG_JSON { get; set; }
        public String LST_HIEN_TRANG_JSON { get; set; }
        public String LST_NGUON_VON_JSON { get; set; }
        public String TAI_SAN_JSON { get; set; }
        public Decimal CHE_DO_HAO_MON_ID { get; set; }
        public String MA_NHOM_DON_VI { get; set; }
        public Decimal? NAM { get; set; }
        public Decimal? MIEN_THUE_SO_TIEN { get; set; }
        public string DB_MA { get; set; }
        public Decimal CHE_DO_HAO_MON_ID_OLD { get; set; }
        public Decimal? HM_TY_LE { get; set; }
        public TaiSanClnApi TS_CLN { get; set; }
        public TaiSanDatApi TS_DAT { get; set; }
        public TaiSanNhaApi TS_NHA { get; set; }
        public TaiSanVktApi TS_VKT { get; set; }
        public TaiSanOtoApi TS_OTO { get; set; }
        public TaiSanOtoApi TS_PTK { get; set; }
        public TaiSanMayMocApi TS_MAY_MOC { get; set; }
        public TaiSanMayMocApi TS_DAC_THU { get; set; }
        public TaiSanMayMocApi TS_HUU_HINH_KHAC { get; set; }
        public TaiSanVoHinhApi TS_VO_HINH { get; set; }
        public List<BienDongApi> LST_BIEN_DONG { get; set; }
        public List<TaiSanNguonVonApi> LST_NGUON_VON { get; set; }
        public List<TaiSanHienTrangSuDungApi> LST_HIEN_TRANG { get; set; }
        public List<DB_KT_HaoMonModel> LST_HAO_MON { get; set; }
        public List<DB_KT_KhauHaoModel> LST_KHAU_HAO { get; set; }
        //public List<DB_KT_HaoMonModel> LST_HAO_MON { get; set; }
        // public YeuCauApi YEU_CAU { get; set; }
    }
    public class DB_KT_KhauHaoModel 
    {
        public decimal? ID { get; set; }
        public Decimal? GIA_TRI_KHAU_HAO { get; set; }
        public string MA_TAI_SAN { get; set; }
        public string MA_TAI_SAN_DB { get; set; }
        public decimal? NAM_KHAU_HAO { get; set; }
        public decimal? THANG_KHAU_HAO { get; set; }
        public decimal? TONG_GIA_TRI_CON_LAI { get; set; }
        public decimal? TONG_KHAU_HAO_LUY_KE { get; set; }
        public decimal? TONG_NGUYEN_GIA { get; set; }
        public decimal? TY_LE_KHAU_HAO { get; set; }
        public decimal? DB_ID { get; set; }

    }
    public class DB_KT_HaoMonModel
    {
        public decimal? ID { get; set; }
        public decimal? TAI_SAN_ID { get; set; }
        public string MA_TAI_SAN { get; set; }
        public string MA_TAI_SAN_DB { get; set; }
        public decimal? NAM_HAO_MON { get; set; }
        public decimal? GIA_TRI_HAO_MON { get; set; }
        public decimal? TONG_HAO_MON_LUY_KE { get; set; }
        public decimal? TONG_GIA_TRI_CON_LAI { get; set; }
        public decimal? TY_LE_HAO_MON { get; set; }
        public decimal? TONG_NGUYEN_GIA { get; set; }
        public decimal? DB_ID { get; set; }
    }
    //public class DB_KT_HaoMonModel
    //{
    //    public String MA_TAI_SAN { get; set; }
    //    public decimal? NAM_HAO_MON { get; set; }
    //    public decimal? GIA_TRI_HAO_MON { get; set; }
    //    public decimal? TONG_HAO_MON_LUY_KE { get; set; }
    //    public decimal? TONG_GIA_TRI_CON_LAI { get; set; }
    //    public decimal? TY_LE_HAO_MON { get; set; }
    //    public decimal? TONG_NGUYEN_GIA { get; set; }
    //}
    public class MssReturn : BaseViewEntity
    {
        public string STRJSON { get; set; }
    }
}
