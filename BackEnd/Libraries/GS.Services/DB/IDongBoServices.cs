//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 15/5/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.DB;
using GS.Core.Domain.Api.TaiSanDBApi;
using GS.Core.Domain.Common;
using GS.Core.Domain.Api;
using System.IO;
using GS.Core.Domain.TaiSans;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.Api.DanhMuc;

namespace GS.Services.DB
{
    public partial interface IDongBoServices
    {
        #region DongBo
        Kho_TaiSan_BienDong PrepareBienDongDongBo(DongBoApi_BienDongTaiSan item, decimal nguonId);
        Kho_DongBoTaiSan ConfigDataByLoaiHinhTaiSan(Kho_DongBoTaiSan kho_DongBoTaiSan, decimal LoaiBienDongId);
        Kho_DonVi_Api PrepareKhoDonVi(decimal id);
        #endregion
    }
}
