//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 5/6/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using GS.Core;
using GS.Core.Domain.KT;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

namespace GS.Services.KT
{
    public partial interface IKhauHaoTaiSanService 
    {    
    #region KhauHaoTaiSan
        IList<KhauHaoTaiSan> GetAllKhauHaoTaiSans();
        IPagedList <KhauHaoTaiSan> SearchKhauHaoTaiSans(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null, decimal ThangKhauHao = 0, decimal NamKhauHao = 0, decimal LoaiHinhTaiSan = 0, decimal DonViId = 0);
        IList<KhauHaoTaiSan> GetListKhauHaoTaiSans(decimal? taiSanId = 0, decimal? namKhauHao = 0, decimal? thangKhauHao = 0, string MaTaiSan = null);
        KhauHaoTaiSan GetKhauHaoTaiSanById(decimal Id);
        IList<KhauHaoTaiSan> GetKhauHaoTaiSanByIds(decimal[] newsIds);
        void InsertKhauHaoTaiSan(KhauHaoTaiSan entity);
        void UpdateKhauHaoTaiSan(KhauHaoTaiSan entity);
        void DeleteKhauHaoTaiSan(KhauHaoTaiSan entity);
        void DeleteKhauHaoTaiSanByNgay(decimal? taiSanId, decimal namKH, decimal? thangKH);
        /// <summary>
        /// Description: Insert hoặc update bản ghi chốt giá trị tài sản (Khấu hao)
        /// </summary>
        /// <param name="p_TaiSanId"></param>
        /// <param name="p_NamKhauHao"></param>
        /// <param name="p_ThangKhauHao"></param>
        /// <returns></returns>
        bool InsertOrUpdateRecordTblKTKH(decimal p_TaiSanId, decimal p_NamKhauHao, decimal p_ThangKhauHao);

        void ChotGiaTriKhauHaoTaiSan(decimal p_TaiSanId, DateTime p_FromDate);
        void UpdateKhauHaoTaiSan(IEnumerable<KhauHaoTaiSan> entity);
        void InsertKhauHaoTaiSan(IEnumerable<KhauHaoTaiSan> entity);

        /*13/4/2022*/
        List<KhauHaoTaiSan> GetListKHTSSau(decimal TaiSanId, decimal? Thang, decimal Nam);
        decimal GetLKKHTruocDo(KhauHaoTaiSan haoMonTaiSan);
        KhauHaoTaiSan GetKhauHaoTaiSanByTSIdandThangNam(decimal? taiSanId = 0, decimal? namKhauHao = 0, decimal? thangKhauHao = 0, string MaTaiSan = null);
        #endregion

        #region chốt khấu hao dơn vi
        bool ChotToanBoKhauHaoDonVi(decimal donViId);
        bool ChotKhauHaoOneTs(decimal taiSanId, decimal namBatDau, decimal thangBatDau, decimal namKetThuc, decimal thangKetThuc);
        #endregion
    }
}
