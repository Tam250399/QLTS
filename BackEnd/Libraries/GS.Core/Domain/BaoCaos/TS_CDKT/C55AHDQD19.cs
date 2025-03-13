using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.TS_CDKT
{
    public class C55AHDQD19 : BaseViewEntity
    {
		public decimal? BD_ID { get; set; }
		public String MA_NHOM_TAI_SAN { get; set; }
        public String TEN_NHOM_TAI_SAN { get; set; }
		public string TEN_TAI_SAN { get; set; }
		public decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? NGUYEN_GIA { get; set; }
        public Decimal? TY_LE_HAO_MON { get; set; }
        public Decimal? HAO_MON_NAM { get; set; }
        public Decimal? TY_LE_KHAO_HAO { get; set; }
        public Decimal? KHAU_HAO_NAM { get; set; }
		public string CAP_2 { get; set; }
		public string CAP_3 { get; set; }
		public string CAP_4 { get; set; }
		public string CAP_5 { get; set; }
		public string TEN_2 { get; set; }
		public string TEN_3 { get; set; }
		public string TEN_4 { get; set; }
		public string TEN_5 { get; set; }
		public string MA_1 { get; set; }
		public string MA_2 { get; set; }
		public string MA_3 { get; set; }
		public string MA_4 { get; set; }
		public string MA_5 { get; set; }
		public int? TREE_LEVEL { get; set; }
	}
}
