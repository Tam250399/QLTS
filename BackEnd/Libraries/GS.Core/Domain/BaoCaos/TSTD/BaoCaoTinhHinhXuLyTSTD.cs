
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TSTD
{
    public class BaoCaoTinhHinhXuLyTSTD : BaseViewEntity
    {
        public int QUYET_DINH_ID { get; set; }
        public string TEN_QUYET_DINH { get; set; }
        public int LOAI_HINH_TAI_SAN_ID { get; set; }
        public int TEN_LOAI_HINH_TAI_SAN_ID
        {
            get ;
            set ;
        }
        public int SO_TIEN_THU { get; set; }
        public decimal SO_TIEN_NOP_TKTG { get; set; }
        public decimal CHI_PHI_XU_LY { get; set; }
        public int SO_TIEN_NOP_NSNN { get; set; }
       
    }
}



