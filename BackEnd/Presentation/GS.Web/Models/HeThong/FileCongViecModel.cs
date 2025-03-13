//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Web.Framework.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.HeThong
{
    public class FileCongViecModel : BaseGSEntityModel
    {
        public FileCongViecModel()
        {
            this.GUID = Guid.NewGuid();
        }
        public Guid GUID { get; set; }
        public String TEN_FILE { get; set; }
        public String LOAI_FILE { get; set; }
        public DateTime NGAY_TAO { get; set; }
        public Decimal NGUOI_TAO { get; set; }
        public String TenNguoiTao { get; set; }
        public String GHI_CHU { get; set; }
        public Byte DA_XOA { get; set; }
        public Decimal LOAI_FILE_ID { get; set; }
        public byte[] NOI_DUNG_FILE { get; set; }
        public String DUOI_FILE { get; set; }
        public Decimal? KICH_THUOC { get; set; }
        public String TAI_XUONG_URL
        {
            get
            {
                return string.Format("/FileCongViec/DownloadFile?downloadGuid={0}", this.GUID);
            }
        }
        public String MimeType { get; set; }
        public MimeTypeGroup mimeTypeGroup
        {
            get { return MimeType.getMimeTypeGroup(); }
        }
        public string ContentLengthText
        {
            get
            {
                if (KICH_THUOC > 1024)
                    return Convert.ToDecimal((decimal)KICH_THUOC / 1024m).ToString("###.#0") + "M";
                return KICH_THUOC.ToString() + "KB";
            }
        }
    }
    public partial class FileCongViecSearchModel : BaseSearchModel
    {
        public FileCongViecSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class FileCongViecListModel : BasePagedListModel<FileCongViecModel>
    {

    }
    public class testModel : BaseGSEntityModel
    {
        [UIHint("WorkFile")]
        public string WorkFileIds { get; set; }
        [UIHint("Date")]
        public DateTime WorkDate { get; set; }
        [UIHint("DateNullable")]
        public DateTime? WorkDateNull { get; set; }
        public Decimal WorkNumber { get; set; }
        public Decimal? WorkNumberNull { get; set; }
        [UIHint("WorkCurrency")]
        public String WorkCurrency { get; set; }
    }
}

