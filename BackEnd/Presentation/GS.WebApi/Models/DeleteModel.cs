using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models
{
    public class DeleteModel
    {
        public decimal ID { get; set; }
        public string Message { get; set; }
        public decimal? DB_ID { get; set; }
        public string DB_ID_JSON { get; set; }
        public decimal? LOAI_LY_DO_ID { get; set; }
        public string ID_DB { get; set; }
    }
}
