using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.Api.TaiSanDBApi
{
   public class TaiSanNguonVonApi
    {
        public String TEN_NGUON_VON { get; set; }
        public Decimal? NGUON_VON_ID { get; set; }
        public Decimal? GIA_TRI { get; set; }
        public Guid BIEN_DONG_GUID { get; set; }
    }
}
