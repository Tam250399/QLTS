using GS.Web.Reports.TheTaiSan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.BaoCaos.TS_CDKT
{
    public class TS_LIST_THE_TAI_SAN_CO_DINH
    {
        public Decimal TAI_SAN_ID { get; set; }
        public rptTHE_TAI_SAN_CO_DINH_TT107 THE_TAI_SAN_CO_DINH_TT107 { get; set; }
    }
    public class TS_LIST_THE_KIEM_KE_TSCD
    {
        public Decimal TAI_SAN_ID { get; set; }
        public rptTHE_KIEM_KE_TSCD THE_KIEM_KE_TSCD { get; set; }
    }
}
