using GS.Core.Domain.BaoCaos.TS_BCTH;
using GS.Web.Framework.Models;
using GS.Web.Reports.TS_BCTH;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.BaoCaos
{
    public class QueueProcessModel : BaseGSEntityModel
    {
        public string MA_BAO_CAO { get; set; }
        public int DON_VI_ID { get; set; }
        public int TYPE_QUEUE_PROCESS_ID { get; set; }
        public decimal? DB_QUEUE_PROCESS_ID { get; set; }
        public string DATA_JSON { get; set; }
        public int? TRANG_THAI_ID { get; set; }
        public DateTime NGAY_TAO { get; set; }
        public int? NGUOI_TAO_ID { get; set; }
        public Guid GUID { get; set; }
        public int? FILE_TYPE { get; set; }
        public Guid GUID_FILE { get; set; }
        public DateTime? TIME_START_GET_DATA { get; set; }
        public DateTime? TIME_END_GET_DATA { get; set; }
        public string STATEMENT { get; set; }
        //
        public string strTrangThai { get; set; }
        public string TenBaoCao { get; set; }
        public string TenNguoiTao { get; set; }
        public string guidResponse { get; set; }
        public string RESPONSE { get; set; }
        public string strThoiGianLayDuLieu { get; set; }

        public String TenAndMaDonVi { get; set; }
        public bool IsShowSendRequest { get; set; }

    }
    public partial class QueueProcessSearchModel : BaseSearchModel
    {
        public QueueProcessSearchModel()
        {
            this.ddlTrangThai = new List<SelectListItem>();
            this.isDongBo = false;
        }

        public string KeySearch { get; set; }
        public String guid { get; set; }
        public String guidResponse { get; set; }
        [UIHint("DateNullable")]
        public DateTime? FromDate { get; set; }
        [UIHint("DateNullable")]
        public DateTime? ToDate { get; set; }
        public List<SelectListItem> ddlTrangThai { get; set; }
        public decimal TrangThaiId { get; set; } = -1;
        public bool? isDongBo { get; set; }
    }
    public partial class QueueProcessListModel : BasePagedListModel<QueueProcessModel>
    {

    }
}
