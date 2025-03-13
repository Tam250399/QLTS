using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.Api.TaiSanDBApi
{
    public class TaiSanHienTrangSuDungApi
    {
        public decimal? HIEN_TRANG_ID { get; set; }
        public Guid BIEN_DONG_GUID { get; set; }
        public decimal? NHOM_HIEN_TRANG_ID { get; set; }
        public decimal? KIEU_DU_LIEU_ID { get; set; }
        public string TEN_HIEN_TRANG { get; set; }
        public string GIA_TRI_TEXT { get; set; }
        public decimal? GIA_TRI_NUMBER { get; set; }
        public bool? GIA_TRI_CHECKBOX { get; set; }
    }
}
