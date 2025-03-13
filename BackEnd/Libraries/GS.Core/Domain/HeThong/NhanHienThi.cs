//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using System;


namespace GS.Core.Domain.HeThong
{
    public partial class NhanHienThi : BaseEntity
    {
        public NhanHienThi()
        {

        }
        public NhanHienThi(string _ma, string _giatri)
        {
            MA = _ma;
            GIA_TRI = _giatri;
        }
        public String MA { get; set; }
        public String GIA_TRI { get; set; }

    }
}



