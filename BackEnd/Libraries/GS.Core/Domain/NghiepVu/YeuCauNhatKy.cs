//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.HeThong;
using System;


namespace GS.Core.Domain.NghiepVu
{
    public enum enumLOAI_NHATKY_YEUCAU
    {
        THEM_MOI = 1,
        SUA = 2,
        XOA = 3,
        DUYET = 4,
        TRA_LAI = 5,
        TRINH_DUYET = 6,
    }
    public partial class YeuCauNhatKy : BaseEntity
    {
        public Decimal? YEU_CAU_ID { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_NHAT_KY_ID { get; set; }
        public String DATA_JSON { get; set; }
        public Decimal? NGUOI_TAO_ID { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        public Decimal? DON_VI_BO_PHAN_ID { get; set; }
        public DateTime? NGAY_TAO { get; set; }
        public decimal? BIEN_DONG_ID { get; set; }
        public decimal? TAI_SAN_ID { get; set; }
        //public virtual YeuCau yeucau { get; set; }
        public virtual NguoiDung nguoidung { get; set; }
        public virtual DonVi donvi { get; set; }
        public virtual DonViBoPhan donvibophan { get; set; }
        public enumLOAI_NHATKY_YEUCAU LoaiNhatKy
        {
            get => (enumLOAI_NHATKY_YEUCAU)LOAI_NHAT_KY_ID;
            set => LOAI_NHAT_KY_ID = (int)value;
        }
    }
}



