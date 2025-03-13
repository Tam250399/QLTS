//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/1/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Data.Extensions;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.DanhMuc;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.DanhMuc;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Factories.DanhMuc
{
    public class NhomCongCuModelFactory : INhomCongCuModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly INhomCongCuService _itemService;
        private readonly IDonViModelFactory _donViModelFactory;
        private readonly IDonViService _donViService;
        private readonly IHoatDongService _hoatDongService;
        #endregion

        #region Ctor

        public NhomCongCuModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            INhomCongCuService itemService,
            IDonViModelFactory donViModelFactory,
            IDonViService donViService,
            IHoatDongService hoatDongService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._donViModelFactory = donViModelFactory;
            this._donViService = donViService;
            this._hoatDongService = hoatDongService;
        }
        #endregion

        #region NhomCongCu
        public NhomCongCuSearchModel PrepareNhomCongCuSearchModel(NhomCongCuSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public NhomCongCuListModel PrepareNhomCongCuListModel(NhomCongCuSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchNhomCongCus(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, ParentId: searchModel.ParentId, donViId: _workContext.CurrentDonVi.ID);

            //prepare list model
            var model = new NhomCongCuListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                {
                    var m = c.ToModel<NhomCongCuModel>();
                    m.TEN_CHA = c.PARENT_ID != null ? _itemService.GetNhomCongCuById((decimal)c.PARENT_ID).TEN : "";
                    m.CountSub = _itemService.CountNhomCongCuSub(c.ID);
                    return m;
                }),
                Total = items.TotalCount
            };
            return model;
        }
        public NhomCongCuModel PrepareNhomCongCuModel(NhomCongCuModel model, NhomCongCu item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<NhomCongCuModel>();
            }
            //more
            model.selectListsParent = PrepareDDLNhomCongCu(_workContext.CurrentDonVi.ID, new List<decimal> { model.ID }, isAddFirst: false);
            model.NGUOI_DUNG_ID = _workContext.CurrentCustomer.ID;
            return model;
        }
        public void PrepareNhomCongCu(NhomCongCuModel model, NhomCongCu item)
        {
            item.TEN = model.TEN;
            item.THOI_HAN_PHAN_BO = model.THOI_HAN_PHAN_BO;
            item.DON_VI_TINH_ID = model.DON_VI_TINH_ID;
            item.PARENT_ID = model.PARENT_ID;
        }
        public virtual List<SelectListItem> PrepareDDLNhomCongCu(decimal donViId = 0, List<decimal> list_exceptID = null, bool isAddFirst = false, string strFirstRow = "-- Chọn nhóm công cụ, dụng cụ --", string valueFirstRow = "")
        {
            string tree = "";
            var item = _itemService.GetNhomCongCus(donViId, list_exceptID).OrderBy(c => c.TREE_NODE).ThenBy(m => m.TEN);
            var ddl = item.Select(c => new SelectListItem
            {
                Text = tree.PadLeft((c.TREE_LEVEL.ToNumberInt32()) * 4, '-') + c.TEN,
                Value = c.ID.ToString()
            }).ToList();
            if (isAddFirst)
                ddl.AddFirstRow(strFirstRow, valueFirstRow);
            return ddl;
        }

        public MessageReturn ImportNhomCongCu(IMP_NhomCongCuModel model)
        {
            MessageReturn rs = new MessageReturn();
            #region check
            DonVi donVi = _donViService.GetReadOnlyDonViByMa(model.MA_DON_VI);
            if (!String.IsNullOrEmpty(model.MA_DON_VI) && donVi == null)
            {
                rs.Code = MessageReturn.ERROR;
                rs.Message = $"MA_DON_VI: {model.MA_DON_VI} NOT EXISTS";
                return rs;
            }
            if (String.IsNullOrEmpty(model.MA))
            {
                rs.Code = MessageReturn.NOT_FOUND_CODE;
                rs.Message = "MA IS NULL";
                return rs;
            }
            else
            {
                if (_itemService.GetReadOnlyNhomCongCu(donViId: donVi?.ID, ma: model.MA) != null)
                {
                    rs.Code = MessageReturn.SUCCESS_CODE;
                    rs.Message = "EXISTS";
                    return rs;
                }
            }


            #endregion
            #region Prepare
            try
            {
                NhomCongCu nhomCongCuCha = _itemService.GetNhomCongCuByMa(model.MA_CHA);
                NhomCongCu nhomCCDCentity = new NhomCongCu()
                {
                    MA = model.MA,
                    TEN = model.TEN,
                    DON_VI_ID = donVi?.ID,
                    THOI_HAN_PHAN_BO = model.THOI_HAN_PHAN_BO,
                    DON_VI_TINH_ID = model.DON_VI_TINH,
                    PARENT_ID = nhomCongCuCha?.ID
                };
                _itemService.InsertNhomCongCu(entity: nhomCCDCentity);
                _hoatDongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", nhomCCDCentity.ToModel<NhomCongCuModel>(), "NhomCongCu");
            }
            catch (Exception ex)
            {

                rs.Code = MessageReturn.ERROR_CODE;
                rs.Message = ex.Message;
                return rs;
            }
            #endregion  Prepare
            rs.Code = MessageReturn.SUCCESS_CODE;
            rs.Message = "Done";
            return rs;
        }
        #endregion
    }
}

