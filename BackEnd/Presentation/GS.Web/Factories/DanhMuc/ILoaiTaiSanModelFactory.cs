//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DanhMuc;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace GS.Web.Factories.DanhMuc
{
    public partial interface ILoaiTaiSanModelFactory
    {
        #region LoaiTaiSan
        List<LoaiHinhTaiSanModel> PrepareListLoaiHinhTaiSanModelForSelect();
        IList<SelectListItem> PrepareSelectlistListLoaiHinhTaiSan(string FirstRow = "-- Chọn loại tài sản --", bool isExcutedTSDT = false);
        LoaiTaiSanSearchModel PrepareLoaiTaiSanSearchModel(LoaiTaiSanSearchModel searchModel);

        LoaiTaiSanListModel PrepareLoaiTaiSanListModel(LoaiTaiSanSearchModel searchModel);
        LoaiTaiSanListModel PrepareLoaiTaiSanToanDanListModel(LoaiTaiSanSearchModel searchModel);
        LoaiTaiSanListModel PrepareChonLoaiTaiSanListModel(LoaiTaiSanSearchModel searchModel);
        LoaiTaiSanModel PrepareLoaiTaiSanModel(LoaiTaiSanModel model, LoaiTaiSan item, bool excludeProperties = false);

        void PrepareLoaiTaiSan(LoaiTaiSanModel model, LoaiTaiSan item);
        bool CheckMaLoaiTaiSan(decimal Id, string Ma, decimal CheDoHaoMonId);
        /// <summary>
        /// Kiểm tra xem loai tai san co tai san con khong
        /// Neu co return true<br/>
        /// khong co return false<br/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CheckLoaiTaiSanCha(decimal id);
        IList<SelectListItem> PrepareSelectListLoaiTaiSan(decimal? valSelected = 0, 
            decimal? cheDoId = 0, 
            bool isAddFirst = false, 
            string strFirstRow = null, 
            decimal? loaiHinhTaiSanId = 0, 
            string valueFirstRow = "", 
            bool isDisabled = true, 
            List<decimal> listLoaiHinhTaiSanId = null, 
            decimal? ParentLoaiTaiSanId = null,
            bool? isCreateOrEditTaiSan = false, 
            decimal? donViId = 0,
            bool isAll = false,
            List<decimal> listLoaiHinhTaiSanIgnore = null,
            bool? tsQLNTSCD = false);
        IList<SelectListItem> PrepareSelectListTaiSanDatNha(decimal? valSelected = 0,
            decimal? cheDoId = 0, bool isAddFirst = false, string strFirstRow = null,
            decimal? loaiHinhTaiSanId = 0, string valueFirstRow = "", bool isDisabled = true,
            int[] listLoaiHinhTaiSanId = null, decimal? ParentLoaiTaiSanId = null,
            bool? isCreateOrEditTaiSan = false, decimal? donViId = 0, bool isAll = false, List<decimal> listLoaiHinhTaiSanIgnore = null, bool? tsQLNTSCD = false);
        List<LoaiHinhTaiSanModel> PrepareListLoaiHinhTaiSanModel();
        List<LoaiHinhTaiSanModel> PrepareListLoaiHinhTaiSanDinhMucModel();
        String PrepareTenTaiSanByListId(string stringID);
        #endregion

    }
}
