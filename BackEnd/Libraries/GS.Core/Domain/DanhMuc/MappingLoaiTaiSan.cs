using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.DanhMuc
{
   public partial class MappingLoaiTaiSan:BaseEntity
    {
        public String NEW_MA_LOAI_TAI_SAN { get; set; }
        public String OLD_MA_LOAI_TAI_SAN { get; set; }
        public Decimal? NEW_LOAI_TAI_SAN_ID { get; set; }
        public Decimal? OLD_LOAI_TAI_SAN_ID { get; set; }
        public Decimal? NEW_CHE_DO_HAO_MON_ID { get; set; }
        public Decimal? OLD_CHE_DO_HAO_MON_ID { get; set; }
        public virtual LoaiTaiSan OldLoaiTaiSan { get; set; }
        public virtual LoaiTaiSan NewLoaiTaiSan { get; set; }
    }
}





