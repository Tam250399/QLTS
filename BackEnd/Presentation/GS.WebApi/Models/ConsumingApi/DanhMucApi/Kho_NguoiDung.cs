using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.ConsumingApi.DanhMucApi
{
    public class Kho_NguoiDung
    {
        public int? actionType { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string passwordHash { get; set; }
        public string password { get; set; }
        public string phoneNumber { get; set; }
        public bool lockoutEnabled { get; set; }
        public string fullName { get; set; }
        public bool isAdministrator { get; set; }      
        public int? syncSource { get; set; }
        public int? isExploitingUser { get; set; }
        public long? id { get; set; }
        public int? unitId { get; set; }
    }
}
