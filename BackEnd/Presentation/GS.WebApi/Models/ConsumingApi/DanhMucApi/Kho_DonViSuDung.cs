using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.ConsumingApi.DanhMucApi
{
    public class Kho_DonViSuDung
    {
        public long? id { get; set; }
        public long? parentId { get; set; }
        public int actionType { get; set; }
        public string syncId { get; set; }
        public string syncParentId { get; set; }
        public long? unitId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public string fax { get; set; }
        //thêm vào để đồng bộ guid bên tsnn
        public string guid { get; set; }
        public bool isActive { get; set; }
    }
}
