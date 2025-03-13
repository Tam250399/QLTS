//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using System;
using System.Collections.Generic;

namespace GS.Services.DanhMuc
{
    public partial interface IDoiTacService
    {
        #region DoiTac
        IList<DoiTac> GetAllDoiTacs();
        IList<DoiTac> GetDoiTacs(Decimal? DonViId = 0, Decimal LoaiDoiTac = 0, string keySearch = null, bool isComboBox = false);
        IPagedList<DoiTac> SearchDoiTacs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, Decimal? donViId = 0);
        DoiTac GetDoiTacById(decimal Id);
        IList<DoiTac> GetDoiTacByIds(decimal[] newsIds);
        void InsertDoiTac(DoiTac entity);
        void UpdateDoiTac(DoiTac entity);
        void DeleteDoiTac(DoiTac entity);
        DoiTac GetDoiTacByMa(string Ma);
        #endregion
        DoiTac GetDoiTac(string Ma = null, string Ten = null, decimal? donViBoPhanId = null, decimal? loaiDoiTac = null);
        #region Read only
        DoiTac GetReadOnlyDoiTac(string Ma = null, string Ten = null, decimal? donViBoPhanId = null, decimal? loaiDoiTac = null);
        #endregion
    }
}
