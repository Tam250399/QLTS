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
    public partial interface IHaoMonTaiSanService 
    {    
    #region HaoMonTaiSan
        IList<HaoMonTaiSan> GetAllHaoMonTaiSans(bool IsDongBo=false);
        IPagedList <HaoMonTaiSan> SearchHaoMonTaiSans(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null, decimal NamHaoMon = 0, decimal LoaiHinhTaiSan = 0, decimal DonViId = 0);
        IList<HaoMonTaiSan> GetListHaoMonTaiSans(decimal? taiSanId = 0, decimal? namHaoMon = 0);
        HaoMonTaiSan GetHaoMonTaiSanById(decimal Id);
        HaoMonTaiSan GetHaoMonTaiSanByTSIdandNamBaoCao(decimal? tsId = 0, decimal? namBaoCao = 0, string MaTaiSan=null);
        IList<HaoMonTaiSan> GetHaoMonTaiSanByIds(decimal[] newsIds);
        void InsertHaoMonTaiSan(HaoMonTaiSan entity);
        void InsertHaoMonTaiSan(IEnumerable<HaoMonTaiSan> entity);
        void UpdateHaoMonTaiSan(HaoMonTaiSan entity);
        void UpdateHaoMonTaiSan(IEnumerable<HaoMonTaiSan> entity);
        void DeleteHaoMonTaiSan(HaoMonTaiSan entity);
        void DeleteHmtsByIdTaiSan(decimal IdTaiSan);
        /// <summary>
        /// Description: Insert hoặc update bản ghi chốt giá trị tài sản
        /// </summary>
        /// <param name="p_TaiSanId"></param>
        /// <param name="p_NamHaoMon"></param>
        /// <returns></returns>
        bool InsertOrUpdateRecordTblKTHM(decimal p_TaiSanId, decimal p_NamHaoMon);

        /// <summary>
        /// Function thực hiện chức năng chốt giá trị tài sản từ năm đến năm hiện tại
        /// </summary>
        /// <param name="p_TaiSanId">id tài sản chốt</param>
        /// <param name="p_Year">năm bắt đầu chốt</param>
        void ChotGiaTriHaoMonTaiSan(decimal p_TaiSanId, DateTime p_Year);
        HaoMonTaiSan GetHaoMonTaiSanGanNhat(decimal? taiSanId = 0);
        /// <summary>
        /// Lấy ra hao mòn lũy kế của năm trước
        /// </summary>
        /// <param name="haoMonTaiSan"></param>
        /// <returns></returns>
        decimal GetLKHMTruocDo(HaoMonTaiSan haoMonTaiSan);
        /// <summary>
        /// lấy ra danh sách hao mòn tài sản sau năm truyền vào
        /// </summary>
        /// <param name="TaiSanId"></param>
        /// <param name="Nam"></param>
        /// <returns></returns>
        List<HaoMonTaiSan> GetListHMTSSauNam(decimal TaiSanId, decimal Nam);
        List<HaoMonKho> GetHaoMonDongBo(string maDonVi = null, decimal? taiSanId = null, decimal? nguonTaiSan = null);
        void UPDATE_TRANG_THAI_DONG_BO_HAO_MON(List<HaoMonKho> haoMonKhos, decimal trangThai = 0);
        List<AssetCodeHaoMon> GetHaoMonCanCheckLog(string maDonVi = null, decimal? nguonTaiSan = null, decimal? trangThaiDB = 1);
        bool ChotToanBoHaoMonDonVi(decimal donViId);
        

        #endregion
    }
}
