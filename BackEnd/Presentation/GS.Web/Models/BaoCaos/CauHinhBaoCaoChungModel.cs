using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.BaoCaos
{
    public class CauHinhBaoCaoChungModel: BaseGSEntityModel
    {
        public int MarginLeft
        {
            get
            {
                if (this._MarginLeft == 0)
                {
                    return 1;
                }
                else return this.MarginBottom;
            }
            set
            {
                this._MarginLeft = value;
            }
        }
        private int _MarginLeft { get; set; }
        public int MarginRight { get; set; }
        public int MarginTop { get; set; }
        public int MarginBottom { get; set; }
        public string Font { get; set; }
        public string Color { get; set; }
        public int WidthBorder { get; set; }
        public int StyleBorder { get; set; }
        public string ColorBorder { get; set; }

    }
}
