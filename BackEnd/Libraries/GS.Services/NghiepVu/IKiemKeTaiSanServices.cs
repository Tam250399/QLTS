using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Services.NghiepVu
{
    public partial interface IKiemKeTaiSanServices
    {
        Boolean KIEM_KE_TAI_SAN(Decimal DonViId = 0, Decimal? DonViBoPhanId = null, DateTime? NgayKiemKe = null, Decimal KiemKeId = 0);
    }
}
