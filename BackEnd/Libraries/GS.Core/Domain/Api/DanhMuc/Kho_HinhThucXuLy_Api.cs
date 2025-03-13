using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.Api.DanhMuc
{
    public class Kho_HinhThucXuLy_Api
    {
        public long? id { get; set; }
        public long actionType { get; set; }
        public long syncId { get; set; }
        public string code { get; set; }
        public long? displayOrder { get; set; }
        public string name { get; set; }
    }
}
