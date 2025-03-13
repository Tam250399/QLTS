using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_BCDA
{
    public class TS_BCDA_02G_KT_TSDA:BaseViewEntity
    {
       public string ten_du_an { get; set; }
        public decimal? don_vi_id { get; set; }
        public string ten_don_vi{ get; set; }
        public decimal? so_luong_ket_thuc { get; set; }
        public decimal? so_luong_ket_thuc_co_thu_tuc { get; set; }
        public decimal? so_luong_ket_thuc_khong_thu_tuc { get; set; }
    }
}
