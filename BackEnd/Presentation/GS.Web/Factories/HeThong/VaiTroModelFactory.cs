//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using DevExpress.Office.Utils;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.HeThong;
using GS.Services.HeThong;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Models.HeThong;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Web.Factories.HeThong
{
    public class VaiTroModelFactory : IVaiTroModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IVaiTroService _itemService;
        private readonly IQuyenService _quyenService;
        private readonly IQuyenVaiTroService _quyenVaiTroService;
        private readonly IWidgetService _widgetService;
        private readonly IVaiTroWidgetService _vaiTroWidgetService;
        private readonly INguoiDungService _nguoiDungService;
        private readonly IVaiTroNguoiDungService _vaiTroNguoiDungService;
        private readonly IWidgetModelFactory _widgetModelFactory;
        #endregion

        #region Ctor

        public VaiTroModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IVaiTroService itemService,
            IQuyenService quyenService,
            IQuyenVaiTroService quyenVaiTroService,
            INguoiDungService nguoiDungService,
            IVaiTroNguoiDungService vaiTroNguoiDungService,
            IVaiTroWidgetService vaiTroWidgetService,
            IWidgetService widgetService,
            IWidgetModelFactory widgetModelFactory
            )
        {
            this._quyenService = quyenService;
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._quyenVaiTroService = quyenVaiTroService;
            this._nguoiDungService = nguoiDungService;
            this._vaiTroNguoiDungService = vaiTroNguoiDungService;
            this._vaiTroWidgetService = vaiTroWidgetService;
            this._widgetService = widgetService;
            this._widgetModelFactory = widgetModelFactory;
        }
        #endregion

        #region VaiTro
        public VaiTroSearchModel PrepareVaiTroSearchModel(VaiTroSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public VaiTroListModel PrepareVaiTroListModel(VaiTroSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            if (searchModel.PageSize == 0) searchModel.PageSize = 10;
            //get items
            var items = _itemService.SearchVaiTros(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);
            //prepare list model
            var model = new VaiTroListModel
            {
                //fill in model values from the entity
                //Data = items.Select(c => c.ToModel<VaiTroModel>()),
                Data = items.Select(c => c.ToModel<VaiTroModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public VaiTroModel PrepareVaiTroModel(VaiTroModel model, VaiTro item, bool excludeProperties = false)
        {

            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<VaiTroModel>();
                var allVTWG = _vaiTroWidgetService.GetAllVaiTroWidgets();
                model.SelectedWidgetIds = allVTWG.Where(c => c.VAI_TRO_ID == item.ID).Select(c => c.WIDGET_ID.ToNumberInt32()).ToList();
                if(model.SelectedWidgetIds == null)
                {
                    model.SelectedWidgetIds = new List<int>();
                    if (model.SelectedWidgetIds.Count == 0)
                    {
                        model.SelectedWidgetIds.Add(0);
                    }
                }

                
            }
			//more
			model.ListWidgetAvailable = _widgetModelFactory.PrepareMultiSelectListWidget(model.SelectedWidgetIds);
            var listQuyen = _quyenService.GetQuyenByVaiTroId(model.ID);
            if (model == null)
            {
                listQuyen = _quyenService.GetAllQuyens();
            }
            model.lstQuyen = _quyenVaiTroService.GetMapByVaiTroId(model.ID).Select(c => Convert.ToInt32(c.QUYEN_ID)).ToList();
            var ListQuyen = _quyenVaiTroService.GetMapByVaiTroId(model.ID).Where(p=>p.QUYEN_ID>0).Select(m => m.quyen).ToList();


            foreach (var quyen in ListQuyen)
            {
                QuyenModel quyenModel = new QuyenModel();
                quyenModel = quyen.ToModel<QuyenModel>();
                model.ListQuyenModel.Add(quyenModel);
            }

            var listNguoiDung = _nguoiDungService.GetNguoiDungByVaiTroId(model.ID);
            if (model == null)
            {
                listNguoiDung = _nguoiDungService.GetAllNguoiDungs();
            }
            model.lstNguoiDung = _vaiTroNguoiDungService.GetMapByVaiTroId(model.ID).Select(c => Convert.ToInt32(c.NGUOI_DUNG_ID)).ToList();
            var ListNguoiDung = _vaiTroNguoiDungService.GetMapByVaiTroId(model.ID).Where(p=>p.NGUOI_DUNG_ID>0).Select(m => m.nguoiDung).ToList();
            foreach (var nguoiDung in ListNguoiDung)
            {
                NguoiDungModel nguoiDungModel = new NguoiDungModel();
                nguoiDungModel = nguoiDung.ToModel<NguoiDungModel>();
                model.ListNguoiDungModel.Add(nguoiDungModel);
            }
            return model;
        }
        public void PrepareVaiTro(VaiTroModel model, VaiTro item)
        {
            item.MA = model.MA;
            item.TEN = model.TEN;
            item.ID = model.ID;
            item.NGAY_TAO = model.NGAY_TAO;

        }
        public bool CheckMaVaiTro(string MA, decimal id = 0)
        {
            return !_itemService.KiemTraTrungMa(MA, id);
        }

        #endregion
    }
}

