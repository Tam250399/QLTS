//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.SHTD;
using static GS.Core.Domain.SHTD.QuyetDinhTichThu;

namespace GS.Services.SHTD
{
    public partial interface IQuyetDinhTichThuService 
    {    
    #region QuyetDinhTichThu
        IList<QuyetDinhTichThu> GetAllQuyetDinhTichThus();
        IList<QuyetDinhTichThu> GetQuyetDinhTichThus();
        IPagedList <QuyetDinhTichThu> SearchQuyetDinhTichThus(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, int LoaiXuLy = 0, string SoQuyetDinh = "", DateTime? NgayQuyetDinhTu = null, DateTime? NgayQuyetDinhDen = null, int LoaiTaiSan = 0, int NguonGocTaiSan = 0, int TrangThaiId = (int)enumTRANGTHAI_QUYETDINH_TSTD.DUYET, Decimal LoaiHinhTaiSanId = 0, bool? isTichThu = null);
        QuyetDinhTichThu GetQuyetDinhTichThuById(decimal Id);
        IList<QuyetDinhTichThu> GetQuyetDinhTichThuByIds(decimal[] newsIds);
        void InsertQuyetDinhTichThu(QuyetDinhTichThu entity);
        void UpdateQuyetDinhTichThu(QuyetDinhTichThu entity);
        void DeleteQuyetDinhTichThu(QuyetDinhTichThu entity);
        QuyetDinhTichThu GetQuyetDinhTichThuByGuid(Guid guid);
        QuyetDinhTichThu GetQuyetDinhTichThu(string SoQuyetDinh = null, DateTime? NgayQuyetDinh=null, string MaDonVi = null);
        QuyetDinhTichThu GetQuyetDinhTichThuByDB_ID(string DB_Id);
        IList<QuyetDinhTichThu> GetAllQuyetDinhTichThusChuaDongBo();
     #endregion
    }
}
