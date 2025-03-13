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

namespace GS.Services.DB
{
    public partial interface IDBTaiSanService
    {
        #region TaiSan
        IList<DBTaiSan> GetAllTaiSans(bool IsTaiSanToanDan = false, decimal TrangThaiId = 0, List<decimal> ListTrangThai = null, int? TakeNumber = null, String maDonVi = null);
        IPagedList<DBTaiSan> SearchTaiSans(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? donViId = null, string MaTaiSan = null, string MaTaiSanDB = null, decimal? LoaiHinhTaiSan = null, decimal? LoaiTaiSan = null, decimal? TrangThai = null, DateTime? NgayDB = null);
        DBTaiSan GetTaiSanById(decimal Id);
        DBTaiSan GetTaiSanByMa(string QLDKTS_MA = null, string DB_MA = null, string DON_VI_DONG_BO_ID = null, decimal? nguonTaiSanId = null);
        IList<DBTaiSan> GetTaiSanByIds(decimal[] newsIds);
        void InsertTaiSan(DBTaiSan entity);
        void UpdateTaiSan(DBTaiSan entity);
        void InsertTaiSan(List<DBTaiSan> entities);
        void UpdateTaiSan(List<DBTaiSan> entities);
        void DeleteTaiSan(DBTaiSan entity);
        //MessageReturn InsertOrUpdateTaiSanNhaNuoc(TaiSanDongBoApi taiSanApi);
        MessageReturn PrepareTaiSanDongBo(TaiSan taiSan = null, TaiSanDongBoApi taiSanApi = null);
        MessageReturn InsertOrUpdateTaiSanToanDan(QuyetDinhTichThuApi quyetDinhTichThuApi);
        List<MessageReturn> ImportExcel(Stream stream);
        MessageReturn DelelteTaiSanDB(string MaTaiSan);
        //void DongBoTaiSan()
        MessageReturn insertOrupdateTaiSanByJson(string json, string ma_db, bool IsTaiSanKho = false, decimal cheDoHaoMon = 23);
        DBTaiSan GetDuLieuDongBoByGuid(string GUID);
        List<DBTaiSanView> GET_TAI_SAN_CAN_DONG_BO(string maDonVi = null, int TakeNumber = 500, int isBienDong = 0);
        void UPDATE_TRANG_THAI_DB_TAI_SAN(List<DBTaiSanView> dBTaiSans, decimal trangThai);
        #endregion
    }
}
