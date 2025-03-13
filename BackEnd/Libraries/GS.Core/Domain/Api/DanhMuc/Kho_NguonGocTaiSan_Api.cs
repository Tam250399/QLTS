using System;
using DevExpress.Xpo;

namespace GS.Core.Domain.Api.DanhMuc
{

    public class Kho_NguonGocTaiSan_Api
    {
        public string code { get; set; }
        public string name { get; set; }
        public int actionType { get; set; }
        public long? id { get; set; }
        public string syncId { get; set; }
        public string syncParentId { get; set; }
        public long? parentId { get; set; }

    }

}