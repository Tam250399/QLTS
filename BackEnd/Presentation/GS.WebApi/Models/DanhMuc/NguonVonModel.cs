using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.DanhMuc
{
    public class NguonVonModel: BaseGSApiModel
    {
        public String TEN { get; set; }
        public int? AP_DUNG_ID { get; set; }    
        public String GHI_CHU { get; set; }
    }
}
