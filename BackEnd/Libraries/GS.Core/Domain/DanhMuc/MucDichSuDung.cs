//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using System;


namespace GS.Core.Domain.DanhMuc
{
    public partial class MucDichSuDung : BaseEntity
    {
        public String MA { get; set; }
        public String TEN { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public enumLOAI_HINH_TAI_SAN LoaiHinhTaiSan
        {
            get => (enumLOAI_HINH_TAI_SAN)LOAI_HINH_TAI_SAN_ID;
            set => LOAI_HINH_TAI_SAN_ID = (int)value;
        }
        public int? DB_ID { get; set; }
        public string DB_ID_JSON { get; set; }
    }
}



