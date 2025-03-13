//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using System;


namespace GS.Core.Domain.DanhMuc
{
    public static class enumCheDoHaoMon_ThongTu
    {
        public const string CDHM_TT45 = "TT45";
    }
    public partial class CheDoHaoMon : BaseEntity
    {
        public String MA_CHE_DO { get; set; }
        public String TEN_CHE_DO { get; set; }
        public DateTime? TU_NGAY { get; set; }
        public DateTime? DEN_NGAY { get; set; }
        public int? DB_ID { get; set; }
    }
}