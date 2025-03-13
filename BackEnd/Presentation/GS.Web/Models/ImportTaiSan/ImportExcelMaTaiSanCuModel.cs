using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.ImportTaiSan
{
    public class ImportExcelMaTaiSanCuModel : BaseGSEntityModel
    {
        public ImportExcelMaTaiSanCuModel()
        {

        }
        public String DON_VI_MA { get; set; }
        public String TAI_SAN_MA_TSC { get; set; }
        public String TAI_SAN_MA_CU { get; set; }
        public int? Row { get; set; }
    }
}
