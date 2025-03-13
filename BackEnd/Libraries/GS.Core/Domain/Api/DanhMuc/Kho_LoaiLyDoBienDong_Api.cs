using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.Api.DanhMuc
{
    public class Kho_LoaiLyDoBienDong_Api
    {
        public long? id { get; set; }
        public string syncId { get; set; }
        public int actionType { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public long displayOrder { get; set; }
    }
}
