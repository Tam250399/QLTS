using GS.Core.Domain.DB;
using GS.WebApi.Models.ConsumingApi.TaiSanApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.ConsumingApi
{
    public class InsertLogModel
    {
        public ResponseApi ResponseApi { get; set; }
        public object Data { get; set; }
        public string LoaiDuLieu { get; set; }
    }
}
