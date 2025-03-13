//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using System;

namespace GS.Core.Domain.HeThong
{
    public partial class FileCongViec : BaseEntity
    {
        public FileCongViec()
        {
            this.GUID = Guid.NewGuid();
        }
        public Guid GUID { get; set; }
        public String TEN_FILE { get; set; }
        public String LOAI_FILE { get; set; }
        public DateTime NGAY_TAO { get; set; }
        public Decimal NGUOI_TAO { get; set; }
        public String GHI_CHU { get; set; }
        public Byte DA_XOA { get; set; }
        public Decimal LOAI_FILE_ID { get; set; }
        public byte[] NOI_DUNG_FILE { get; set; }
        public String DUOI_FILE { get; set; }
        public Decimal? KICH_THUOC { get; set; }
    }
}



