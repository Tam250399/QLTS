using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_CDKT
{
    public class BaoCaoKiemKeTaiSan : BaseViewEntity
    {
        public String MATS { get; set; }
        public String TENTS { get; set; }
        public String NUOCSANXUAT { get; set; }
        public Decimal? NAMSANXUAT { get; set; }
        public String SOHIEU { get; set; }
        public Decimal? SS_SOLUONG { get; set; }
        public Decimal? SS_NGUYENGIA { get; set; }
        public Decimal? SS_GIATRICONLAI { get; set; }
        public Decimal? KK_SOLUONG { get; set; }
		public Decimal? KK_NGUYENGIA { get; set; }
		public Decimal? KK_GIATRICONLAI { get; set; }
        public Decimal? CL_THIEU { get; set; }
        public Decimal? CL_THUA { get; set; }
        public String DENGHITHANHLY { get; set; }
        public String DONVIBOPHAN { get; set; }
        public String NHOMTAISAN { get; set; }
        public decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public string TEN_HIEN_TRANG { get; set; }

        public string KIEN_NGHI_GIAI_QUYET_CHENH_LECH { get; set; }
    }
}
