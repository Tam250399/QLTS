//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 22/5/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.KT;
using GS.Core.Domain.Api.TaiSanDBApi;

namespace GS.Services.KT
{
    public partial interface IHaoMonTaiSanLogService 
    {    
    #region HaoMonTaiSanLog
        IList<HaoMonTaiSanLog> GetAllHaoMonTaiSanLogs();
        IPagedList <HaoMonTaiSanLog> SearchHaoMonTaiSanLogs(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null);
        HaoMonTaiSanLog GetHaoMonTaiSanLogById(decimal Id);
        /// <summary>
        /// mỗi tài sản thì có nhiều nhất 1 bản ghi log
        /// </summary>
        /// <param name="TaiSanId"></param>
        /// <returns></returns>
        HaoMonTaiSanLog GetHaoMonTaiSanLogByTaiSanId(decimal TaiSanId);
        IList<HaoMonTaiSanLog> GetHaoMonTaiSanLogByIds(decimal[] newsIds);
        void InsertHaoMonTaiSanLog(HaoMonTaiSanLog entity);
        void UpdateHaoMonTaiSanLog(HaoMonTaiSanLog entity);
        void DeleteHaoMonTaiSanLog(HaoMonTaiSanLog entity);
        #region Funtion thực hiện chốt giá trị tài sản (table: KT_*)
        HaoMonTaiSan ChotHaoMon(decimal taiSanId, decimal namHaoMon);
        KhauHaoTaiSan ChotKhauHao(decimal taiSanId, decimal namKhauHao, decimal thangKhauHao);
        bool ChotToanBoTaiSan(decimal? donViId = 0, decimal? namKeKhai = 0, decimal? taiSanId = 0, decimal? loaiHinhTaiSanId = 0);
        #endregion Funtion thực hiện chốt giá trị tài sản (table: KT_*)
        #endregion
    }
}
