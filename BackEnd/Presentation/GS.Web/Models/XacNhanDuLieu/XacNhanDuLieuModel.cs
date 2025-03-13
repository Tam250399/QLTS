using GS.Core.Domain.DanhMuc;
using GS.Web.Framework.Models;
using GS.Web.Models.HeThong;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.XacNhanDuLieu
{
    public class XacNhanDuLieuModel: BaseGSEntityModel
    {
        public Decimal? DON_VI_ID { get; set; }
        public DonVi DonVi { get; set; }
        public Decimal FILE_DOI_CHIEU_1 { get; set; }
        public Decimal FILE_DOI_CHIEU_2 { get; set; }
        public Decimal FILE_DOI_CHIEU_3 { get; set; }
        public Decimal FILE_DOI_CHIEU_4 { get; set; }
        public Decimal FILE_DOI_CHIEU_5 { get; set; }
        public Decimal FILE_DOI_CHIEU_6 { get; set; }
        public Decimal FILE_DOI_CHIEU_7 { get; set; }
        public Decimal FILE_DOI_CHIEU_8 { get; set; }
        public Decimal FILE_DOI_CHIEU_9 { get; set; }
        public Decimal FILE_DOI_CHIEU_10 { get; set; }
        public Decimal FILE_DOI_CHIEU_11 { get; set; }
        public Decimal FILE_DOI_CHIEU_12 { get; set; }
        public bool IS_XAC_NHAN_DU_LIEU { get; set; }
        public Decimal? TREE_LEVEL { get; set; }
    }
}
