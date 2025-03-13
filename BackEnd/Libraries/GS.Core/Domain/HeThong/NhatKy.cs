//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using System;

namespace GS.Core.Domain.HeThong
{
    public partial class NhatKy : BaseEntity
    {
        public NhatKy()
        {
            this.GUID = Guid.NewGuid();
        }
        public String TEN_DANG_NHAP { get; set; }
        public String MO_TA { get; set; }
        public DateTime NGAY_TAO { get; set; }
        public String TEN_DAY_DU { get; set; }
        public String DU_LIEU { get; set; }
        public String PHAN_LOAI { get; set; }
        public String IP_ADDRESS { get; set; }
        public Guid GUID { get; set; }
        public String UNG_DUNG { get; set; }
        public String PAGE_URL { get; set; }
        public int CAP_DO_ID { get; set; }
        public String NOI_DUNG { get; set; }

    }
    public enum LogLevel
    {
        /// <summary>
        /// Debug
        /// </summary>
        All = 0,
        /// <summary>
        /// Debug
        /// </summary>
        Debug = 10,

        /// <summary>
        /// Information
        /// </summary>
        Information = 20,

        /// <summary>
        /// Warning
        /// </summary>
        Warning = 30,

        /// <summary>
        /// Error
        /// </summary>
        Error = 40,

        /// <summary>
        /// Fatal
        /// </summary>
        Fatal = 50
    }
}



