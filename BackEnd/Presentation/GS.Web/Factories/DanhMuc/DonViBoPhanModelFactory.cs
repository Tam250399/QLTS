//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Framework.Extensions;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Web.Factories.DanhMuc
{
    public class DonViBoPhanModelFactory : IDonViBoPhanModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IDonViBoPhanService _itemService;
        private readonly IDonViService _itemDonViServie;
        private readonly IHoatDongService _hoatDongService;
        #endregion

        #region Ctor

        public DonViBoPhanModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IDonViBoPhanService itemService,
            IDonViService itemDonViService,
            IHoatDongService hoatDongService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._itemDonViServie = itemDonViService;
            this._hoatDongService = hoatDongService;
        }
        #endregion

        #region DonViBoPhan
        public DonViBoPhanSearchModel PrepareDonViBoPhanSearchModel(DonViBoPhanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public DonViBoPhanListModel PrepareDonViBoPhanListModel(DonViBoPhanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchDonViBoPhans(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, DonViID: searchModel.DonViId, ParentId: searchModel.ParentID);

            //prepare list model
            var model = new DonViBoPhanListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                {
                    var m = c.ToModel<DonViBoPhanModel>();
                    m.TEN_DON_VI_BO_PHAN_CHA = c.DonViBoPhanCha != null ? c.DonViBoPhanCha.TEN : String.Empty;
                    m.CountSub = _itemService.CountPhongBanSub(c.ID);
                    m.pageIndex = searchModel.Page;
                    return m;
                }),
                Total = items.TotalCount
            };
            return model;
        }
        public DonViBoPhanModel PrepareDonViBoPhanModel(DonViBoPhanModel model, DonViBoPhan item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<DonViBoPhanModel>();
                model.TEN_DON_VI = _itemDonViServie.GetDonViById(Id: model.DON_VI_ID.GetValueOrDefault()).TEN;
            }
            //more
            if (!model.DON_VI_ID.HasValue)
                model.DON_VI_ID = _workContext.CurrentDonVi.ID;
            model.dllDonViBoPhan = PrepareSelectListDonViBoPhan(valSelected: model.PARENT_ID, DonViId: model.DON_VI_ID, isAddFirst: true, valueFirstRow: "");

            return model;
        }
        public void PrepareDonViBoPhan(DonViBoPhanModel model, DonViBoPhan item)
        {
            item.MA = model.MA;
            item.TEN = model.TEN;
            item.DIA_CHI = model.DIA_CHI;
            item.DIEN_THOAI = model.DIEN_THOAI;
            item.FAX = model.FAX;
            item.DON_VI_ID = model.DON_VI_ID;
            item.PARENT_ID = model.PARENT_ID;
            item.TREE_LEVEL = model.TREE_LEVEL;
            item.TREE_NODE = model.TREE_NODE;
        }
        public IList<SelectListItem> PrepareSelectListDonViBoPhan(decimal? valSelected = 0, decimal? DonViId = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn đơn vị cấp trên của bộ phận --", string valueFirstRow = "")
        {
            string tree = "";
            var _lstDvBp = _itemService.GetListDonViBoPhanTreeNodeByRoot(donViId: DonViId);
            var selectList = _lstDvBp.Select(c => new SelectListItem
            {
                Text = tree.PadLeft((c.TREE_LEVEL.ToNumberInt32()) * 4, '-') + c.TEN,
                Value = c.ID.ToString(),
                Selected = valSelected == c.ID
            }).ToList();
            if (isAddFirst)
                selectList.AddFirstRow(strFirstRow, valueFirstRow);
            return selectList;
        }

        public bool CheckTenDonViBoPhan(decimal? id, string ten = null)
        {
            return !_itemService.CheckTenDonViBoPhan(id: id, ten: ten);
        }

        #endregion
        public MessageReturn ImportDonViBoPhan(IMP_DonViBoPhanModel model)
        {
            MessageReturn rs = new MessageReturn();
            #region check

            if (String.IsNullOrEmpty(model.MA))
            {
                rs.Code = MessageReturn.NOT_FOUND_CODE;
                rs.Message = "MA IS NULL";
                return rs;
            }
            DonVi donVi = _itemDonViServie.GetReadOnlyDonViByMa(model.MA_DON_VI);
            if (!String.IsNullOrEmpty(model.MA_DON_VI) && donVi == null)
            {
                rs.Code = MessageReturn.NOT_FOUND_CODE;
                rs.Message = $"DON_VI_MA {model.MA_DON_VI} NOT FOUND";
                return rs;
            }
            try
            {
                if (!_itemService.CheckMaDonViBoPhan(donVi != null ? donVi.ID : default(decimal?), 0, model.MA))
                {
                    DonViBoPhan dvbp_parent = new DonViBoPhan();
                    if (!String.IsNullOrEmpty(model.MA_CHA))
                    {
                        dvbp_parent = _itemService.GetReadOnlyDonViBoPhanByMaAndDonViId(MA: model.MA_CHA, donViId: donVi != null ? donVi.ID : default(decimal?));
                    }
                    DonViBoPhan DVBPentity = new DonViBoPhan()
                    {
                        MA = model.MA,
                        TEN = model.TEN,
                        DIA_CHI = model.DIA_CHI,
                        DIEN_THOAI = model.DIEN_THOAI,
                        FAX = model.FAX,
                        DON_VI_ID = donVi != null ? donVi.ID : default(decimal?),
                        PARENT_ID = (dvbp_parent != null && dvbp_parent.ID > 0) ? dvbp_parent.ID : default(decimal?),
                    };
                    _itemService.InsertDonViBoPhan(DVBPentity, false);
                    _hoatDongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin", DVBPentity.ToModel<DonViBoPhanModel>(), "DonViBoPhan");
                    rs.Code = MessageReturn.SUCCESS_CODE;
                    rs.Message = "Done";
                    return rs;
                }
                else
                {
                    rs.Code = MessageReturn.SUCCESS_CODE;
                    rs.Message = "Exists";
                    return rs;
                }

            }
            catch (Exception ex)
            {

                rs.Code = MessageReturn.ERROR_CODE;
                rs.Message = ex.Message;
                return rs;
            }
            #endregion
        }
    }
}

