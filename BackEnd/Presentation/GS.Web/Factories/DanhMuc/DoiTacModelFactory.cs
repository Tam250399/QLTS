//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Services;
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
    public class DoiTacModelFactory : IDoiTacModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IDoiTacService _itemService;
        private readonly IDonViBoPhanModelFactory _donViBoPhanModelFactory;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IDonViBoPhanService _donViBoPhanService;
        private readonly IDonViService _donViService;
        private readonly IHoatDongService _hoatDongService;

        #endregion

        #region Ctor

        public DoiTacModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IDoiTacService itemService,
            IDonViBoPhanModelFactory donViBoPhanModelFactory,
            INhanHienThiService nhanHienThiService,
            IDonViBoPhanService donViBoPhanService,
            IDonViService donViService,
            IHoatDongService hoatDongService

            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._donViBoPhanModelFactory = donViBoPhanModelFactory;
            this._nhanHienThiService = nhanHienThiService;
            this._donViBoPhanService = donViBoPhanService;
            this._donViService = donViService;
            this._hoatDongService = hoatDongService;
        }
        #endregion

        #region DoiTac
        public DoiTacSearchModel PrepareDoiTacSearchModel(DoiTacSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            searchModel.donviId = _workContext.CurrentDonVi.ID;
            searchModel.TenDonVi = _workContext.CurrentDonVi.TEN_DON_VI;
            return searchModel;
        }

        public DoiTacListModel PrepareDoiTacListModel(DoiTacSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchDoiTacs(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, donViId: searchModel.donviId);

            //prepare list model
            var model = new DoiTacListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                {
                    var m = c.ToModel<DoiTacModel>();
                    //m.TEN_LOAI_DOI_TAC = _nhanHienThiService.GetGiaTriEnum(c.LoaiDoiTac_enum);
                    //var _phongban = _donViBoPhanService.GetDonViBoPhanById(c.DON_VI_BO_PHAN_ID.Value);
                    //m.TenDonViBoPhan = _phongban.TEN;
                    return m;
                }),
                Total = items.TotalCount
            };
            return model;
        }
        public DoiTacModel PrepareDoiTacModel(DoiTacModel model, DoiTac item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<DoiTacModel>();
            }
            //more
            model.DON_VI_ID = _workContext.CurrentDonVi.ID;

            model.AvailableDonViBoPhan = _donViBoPhanModelFactory.PrepareSelectListDonViBoPhan(DonViId: model.DON_VI_ID, isAddFirst: true);
            var dllLoaiDoiTac = model.enumLOAI_DOI_TAC.ToSelectList().OrderByDescending(c => c.Value);
            model.dllLoaiDoiTac = new SelectList(dllLoaiDoiTac, "Value", "Text");
            if (model.ID > 0)
            {
                model.dllLoaiDoiTac = ((enumLOAI_DOI_TAC)(model.LOAI_DOI_TAC_ID ?? 0)).ToSelectList();
            }
            return model;
        }
        public void PrepareDoiTac(DoiTacModel model, DoiTac item)
        {
            item.MA = model.MA;
            item.TEN = model.TEN;
            item.DAI_DIEN = model.DAI_DIEN;
            item.DIA_CHI = model.DIA_CHI;
            item.DIEN_THOAI = model.DIEN_THOAI;
            item.MO_TA = model.MO_TA;
            item.LOAI_DOI_TAC_ID = model.LOAI_DOI_TAC_ID;
            item.DON_VI_BO_PHAN_ID = model.DON_VI_BO_PHAN_ID;

        }

        public List<SelectListItem> PrepareSelectListDoiTac(Decimal LoaiDoiTacId = 0, decimal? valSelected = 0, bool isAddFirst = false, string strFirstRow = "-- Chọn đơn vị sửa chữa --", string valueFirstRow = "")
        {
            var item = _itemService.GetDoiTacs(DonViId: _workContext.CurrentDonVi.ID, LoaiDoiTac: LoaiDoiTacId);
            var selectList = item.Select(c => new SelectListItem
            {
                Text = c.TEN,
                Value = c.ID.ToString(),
                Selected = valSelected == c.ID
            }).ToList();
            if (isAddFirst)
                selectList.AddFirstRow(strFirstRow, valueFirstRow);
            return selectList;
        }
        public bool CheckMaTrung(string Ma, decimal ID, decimal DON_VI_ID)
        {
            var obj = _itemService.GetDoiTacByMa(Ma);
            if (obj != null && obj.ID != ID && obj.DON_VI_ID == DON_VI_ID)
                return false;
            else return true;
        }


        #endregion
        #region Import DoiTac
        public MessageReturn ImportDoiTac(IMP_DoiTacModel model)
        {
            MessageReturn rs = new MessageReturn();
            #region check

            if (String.IsNullOrEmpty(model.MA))
            {
                rs.Code = MessageReturn.NOT_FOUND_CODE;
                rs.Message = "MA IS NULL";
                return rs;
            }
            else
            {
                if (_itemService.GetDoiTacByMa(model.MA) != null)
                {
                    rs.Code = MessageReturn.SUCCESS_CODE;
                    rs.Message = "EXISTS";
                    return rs;
                }
            }
            DonVi donVi = _donViService.GetReadOnlyDonViByMa(model.MA_DON_VI);
            if (!String.IsNullOrEmpty(model.MA_DON_VI) && donVi == null)
            {
                rs.Code = MessageReturn.NOT_FOUND_CODE;
                rs.Message = $"DON_VI_MA: {model.MA_DON_VI} NOT EXISTS";
                return rs;
            }
            #endregion
            #region Prepare
            try
            {
                DonViBoPhan phongBan = new DonViBoPhan();
                if (!string.IsNullOrEmpty(model.MA_DON_VI_BO_PHAN))
                {
                    phongBan = _donViBoPhanService.GetReadOnlyDonViBoPhanByMaAndDonViId(donViId: donVi.ID, MA: model.MA_DON_VI_BO_PHAN);
                }
                DoiTac doiTac = new DoiTac()
                {
                    MA = model.MA,
                    TEN = model.TEN,
                    DAI_DIEN = model.DAI_DIEN,
                    DIA_CHI = model.DIA_CHI,
                    DIEN_THOAI = model.DIEN_THOAI,
                    MO_TA = model.MO_TA,
                    LOAI_DOI_TAC_ID = model.LOAI_DOI_TAC_ID,
                    DON_VI_ID = donVi.ID,
                    DON_VI_BO_PHAN_ID = (phongBan != null && phongBan.ID > 0) ? phongBan.ID : default(decimal?),
                };
                _itemService.InsertDoiTac(doiTac);
                _hoatDongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", doiTac.ToModel<DoiTacModel>(), "DoiTac");
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
        #endregion Import DoiTac
    }
}

