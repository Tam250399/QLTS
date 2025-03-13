using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.ThuocTinhs
{
    public partial class ThuocTinhEntity:BaseEntity
    {
        public Guid GUID { get; set; }
        public string NAME { get; set; }
        public string TYPE { get; set; }
        public string VALUE { get; set; }
        public string FIELD { get; set; }
        public List<ThuocTinhEntity> THUOC_TINH { get; set; }
        public List<SelectListItem> OPTION { get; set; }
        public bool IS_VALIDATE { get; set; }
    }
}
