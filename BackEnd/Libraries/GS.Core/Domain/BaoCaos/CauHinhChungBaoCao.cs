using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos
{
    public class CauHinhChungBaoCao :BaseEntity
    {
        public int MarginLeft { get; set; }
        public int MarginRight { get; set; }
        public int MarginTop { get; set; }
        public int MarginBottom { get; set; }
        public string Font { get; set; }
        public string Color { get; set; }
        
    }
   
}
