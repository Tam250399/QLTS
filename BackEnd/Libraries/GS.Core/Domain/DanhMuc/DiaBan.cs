//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using System;


namespace GS.Core.Domain.DanhMuc
{
    public enum enumLOAI_DIABAN
    {
        ALL,
        TINH,
        HUYEN,
        XA,
    }
    public enum enumTRANG_THAI_DIABAN
    {
        KHONGKHADUNG,
        KHADUNG,
    }
    public partial class DiaBan : BaseEntity
    {
        public String MA { get; set; }
        public String TEN { get; set; }
        public String MO_TA { get; set; }
        public String MA_CHA { get; set; }
        public decimal? VUNG_KINH_TE_ID { get; set; }
        public String MA_PHAN_CAP { get; set; }
        public Decimal? PARENT_ID { get; set; }
        public String TREE_NODE { get; set; }
        public Decimal? TREE_LEVEL { get; set; }
        public decimal? TRANG_THAI_ID { get; set; }
        public decimal? LOAI_DIA_BAN_ID { get; set; }
        public Decimal? QUOC_GIA_ID { get; set; }
        public enumLOAI_DIABAN LoaiDiaBan
        {
            get => (enumLOAI_DIABAN)LOAI_DIA_BAN_ID;
            set => LOAI_DIA_BAN_ID = (int)value;
        }
        public enumTRANG_THAI_DIABAN TrangThai
        {
            get => (enumTRANG_THAI_DIABAN)TRANG_THAI_ID;
            set => TRANG_THAI_ID = (int)value;
        }
        public virtual QuocGia Quocgia { get; set; }
        public virtual DiaBan DiaBanCha { get; set; }
        public Decimal? DB_ID { get; set; }
    }
}



