//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 28/6/2021
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace GS.Core.Domain.BaoCaos
{
    public partial class LogQueueProcess : BaseEntity
    {
        public Decimal? STATUS { get; set; }
        public String ACTION { get; set; }
        public String OUTPUT { get; set; }
        public DateTime? TIME_START_SCAN { get; set; }
        public DateTime? TIME_STOP_SCAN { get; set; }
        public Decimal? QUEUE_ID { get; set; }

    }
}



