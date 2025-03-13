//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/7/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.TaiSans
{
    public enum enumHinhThucKhaiThac
    {
        ALL= 0,
        SXKD = 1,
        CHO_THUE_TS = 2,
        LDLK = 3,
        KHAI_THAC_KHAC =4
    }
    public enum enumPhuongThucChoThue
    {
        all = 0,
        DAU_GIA = 1,
        TRUC_TIEP = 2,

    } 
    public enum enumHinhThucLDLK
    {
        //all = 0,
        TU_QUAN_LY = 1,
        GOP_VON = 2,
        THANH_LAP_PHAP_NHAN_MOI = 3,

    }
    public partial class KhaiThac : BaseEntity
    {
        public Decimal? DON_VI_ID { get; set; }
        public Decimal? LOAI_HINH_KHAI_THAC_ID { get; set; }
        public DateTime NGAY_KHAI_THAC { get; set; }
        public String QUYET_DINH_SO { get; set; }
        public DateTime QUYET_DINH_NGAY { get; set; }
        public String NGUOI_DUYET { get; set; }
        public String NOI_DUNG { get; set; }
        public String GHI_CHU { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public DateTime? KHAI_THAC_NGAY_TU { get; set; }
        public DateTime? KHAI_THAC_NGAY_DEN { get; set; }
        public String HOP_DONG_SO { get; set; }
        public DateTime? HOP_DONG_NGAY { get; set; }
        public Decimal? DOI_TAC_ID { get; set; }
        public Decimal? SO_TIEN_TAM_TINH { get; set; }
        public Decimal? CHO_THUE_GIA { get; set; }
        public Decimal? CHO_THUE_PHUONG_THUC_ID { get; set; }
        public Decimal? LDLK_HINH_THUC_ID { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public Decimal? NGUOI_TAO_ID { get; set; }
        public string DB_ID { get; set; }
        public String TEN_PHAP_NHAN_MOI { get; set; }
    }
}



