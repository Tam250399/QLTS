using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos.CCDC
{
    public class LietKeCCDCPhongBan : BaseEntity
    {
        public LietKeCCDCPhongBan()
        {
            ListLietKeCCDC = new List<LietKeCCDC>();
        }
        public String DonViBoPhan { get; set; }
        public Decimal? DonViBoPhanId { get; set; }
        public List<LietKeCCDC> ListLietKeCCDC { get; set; }
    }
}
