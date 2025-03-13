using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.Api.DanhMuc
{
    public class Kho_NguoiDung_Api
    {
        public int? actionType { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string passwordHash { get; set; }
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
