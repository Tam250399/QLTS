//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
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
using GS.Core.Domain.SHTD;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.SHTD;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.SHTD;
using GS.Services.DanhMuc;
using GS.Web.Models.ThuocTinh;
using GS.Services.ThuocTinhs;
using GS.Web.Factories.ThuocTinhs;
using GS.Core.Domain.ThuocTinhs;
using GS.Web.Factories.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using GS.Core.Domain.DanhMuc;
using GS.Services;

namespace GS.Web.Factories.SHTD
{
    public class XuLyModelFactory : IXuLyModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IXuLyService _itemService;
        private readonly ITaiSanTdService _taiSanTdService;
        private readonly IHinhThucXuLyService hinhThucXuLyService;
        private readonly IPhuongAnXuLyService _phuongAnXuLyService;
        private readonly IThuocTinhDataService _thuocTinhDataService;
        private readonly IThuocTinhDataModelFactory _thuocTinhDataModelFactory;
        private readonly IThuocTinhModelFactory _thuocTinhModelFactory;
        private readonly ITaiSanTdXuLyService _taisanTdXuLyService;
        private readonly IHinhThucXuLyModelFactory _phuongAnModelFactory;
        private readonly ITaiSanTdXuLyModelFactory _taiSanTdXuLyModelFactory;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly ILoaiTaiSanModelFactory _loaiTaiSanModelFactory;
        private readonly IDonViModelFactory _donViModelFactory;
        private readonly IDonViService _donViService;
        private readonly IQuyetDinhTichThuService _quyetDinhTichThuService;
        private readonly IQuyetDinhTichThuModelFactory _quyetDinhTichThuModelFactory;
        private readonly IThuChiService _thuChiService;
        #endregion

        #region Ctor

        public XuLyModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IXuLyService itemService,
            ITaiSanTdService taiSanTdService,
            IHinhThucXuLyService hinhThucXuLyService,
            IPhuongAnXuLyService phuongAnXuLyService,
            IThuocTinhDataService thuocTinhDataService,
            IThuocTinhDataModelFactory thuocTinhDataModelFactory,
            IThuocTinhModelFactory thuocTinhModelFactory,
             ITaiSanTdXuLyService taisanTdXuLyService,
             IHinhThucXuLyModelFactory hinhThucXuLyModelFactory,
             ITaiSanTdXuLyModelFactory taiSanTdXuLyModelFactory,
             ILoaiTaiSanService loaiTaiSanService,
             ILoaiTaiSanModelFactory loaiTaiSanModelFactory,
             IDonViModelFactory donViModelFactory,
             IDonViService donViService,
             IQuyetDinhTichThuService quyetDinhTichThuService,
             IQuyetDinhTichThuModelFactory quyetDinhTichThuModelFactory,
             IThuChiService thuChiService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._taiSanTdService = taiSanTdService;
            this.hinhThucXuLyService = hinhThucXuLyService;
            this._phuongAnXuLyService = phuongAnXuLyService;
            this._thuocTinhDataService = thuocTinhDataService;
            this._thuocTinhDataModelFactory = thuocTinhDataModelFactory;
            this._thuocTinhModelFactory = thuocTinhModelFactory;
            this._taisanTdXuLyService = taisanTdXuLyService;
            this._phuongAnModelFactory = hinhThucXuLyModelFactory;
            this._taiSanTdXuLyModelFactory = taiSanTdXuLyModelFactory;
            this._loaiTaiSanService = loaiTaiSanService;
            this._loaiTaiSanModelFactory = loaiTaiSanModelFactory;
            this._donViModelFactory = donViModelFactory;
            this._donViService = donViService;
            this._quyetDinhTichThuService = quyetDinhTichThuService;
            this._quyetDinhTichThuModelFactory = quyetDinhTichThuModelFactory;
            this._thuChiService = thuChiService;
        }
        #endregion

        #region XuLy
        public XuLySearchModel PrepareXuLySearchModel(XuLySearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.DDLLoaiTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(cheDoId: 23, isAddFirst: true, isDisabled:false).ToList();
            searchModel.DDLPhuongAnXuLy = _phuongAnXuLyService.GetAllPhuongAnXuLys().Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.TEN
                }).ToList();
            searchModel.DDLPhuongAnXuLy.Insert(0, new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "0", Text = "Chọn phương án" });
            searchModel.DDLHinhThucXuLy.Insert(0, new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "0", Text = "Chọn hình thức" });
            searchModel.SetGridPageSize();
            return searchModel;
        }

        public XuLyListModel PrepareXuLyListModel(XuLySearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchXuLys(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, LoaiXuLy: searchModel.LOAI_XU_LY_ID,SoQuyetDinh:searchModel.SoQuyetDinh,NgayQuyetDinhTu:searchModel.NgayQuyetDinhTu,NgayQuyetDinhDen:searchModel.NgayQuyetDinhDen,LoaiTaiSan:searchModel.LoaiTaiSanId,NgayXuLyTu:searchModel.NgayXuLyTu,NgayXuLyDen:searchModel.NgayXuLyDen,PhuongAnXuLy:searchModel.PhuongAnXuLy,HinhThucXuLy:searchModel.PhuongThucXuLy,DonViId:_workContext.CurrentDonVi.ID);

            //prepare list model
            var model = new XuLyListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareXuLyModelForList(new XuLyModel(),c)),
                Total = items.TotalCount
            };
            return model;
        }
        public XuLyModel PrepareXuLyModelForList(XuLyModel model, XuLy item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<XuLyModel>();
                var coquanbanhanh = _donViService.GetDonViById(model.CO_QUAN_BAN_HANH_ID != null ? (decimal)model.CO_QUAN_BAN_HANH_ID : 0);
                if (coquanbanhanh != null)
                    model.CO_QUAN_BAN_HANH_TEN = coquanbanhanh.TEN;
                model.ChiPhi = model.CHI_PHI.ToVNStringNumber();
            }
            //more
            return model;
        }
        public List<XuLyModel> PrepareDDLForXuLyModel(List<XuLyModel> listmodel, XuLyModel model)
        {
            if (listmodel != null && listmodel.Count() > 0)
            {
                var ddlTaiSanTD = _taiSanTdService.GetAllTaiSanTds().Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.TEN + " - " + c.SO_LUONG
                }).ToList();
                ddlTaiSanTD.Insert(0, new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "0", Text = "Chọn tài sản toàn dân" });
                var ddlHinhThuc = _phuongAnXuLyService.GetAllPhuongAnXuLys().Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.TEN
                }).ToList();
                ddlHinhThuc.Insert(0, new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "0", Text = "Chọn hình thức" });
                foreach (var item in listmodel)
                {
                    if (item.taisanxuly.HINH_THUC_XU_LY_ID != null)
                    {
                        item.DDLPhuongAn = _phuongAnModelFactory.DDLPhuongAnByHinhThuc(HinhThucId: (int)item.taisanxuly.HINH_THUC_XU_LY_ID);
                    }
                    item.DDLHinhThuc = ddlHinhThuc;
                    item.DDLTaiSanTD = ddlTaiSanTD;

                    if (item.taisanxuly.HINH_THUC_XU_LY_ID == null)
                    {
                        item.taisanxuly.HINH_THUC_XU_LY_ID = 0;
                    }
                    if (item.taisanxuly.PHUONG_AN_XU_LY_ID == null)
                    {
                        item.taisanxuly.PHUONG_AN_XU_LY_ID = 0;
                    }
                }
            }

            return listmodel;
        }
        public XuLyModel PrepareXuLyModel(XuLyModel model, XuLy item, bool excludeProperties = false, bool is_DLL = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<XuLyModel>();
                var taisanxulys = _taisanTdXuLyService.GetTaiSanTdsXuLyByXuLyId((int)model.ID);
                if (taisanxulys != null && taisanxulys.Count() > 0)
                {
                    var tsxl = taisanxulys.FirstOrDefault();
                    model.taisanxuly = tsxl.ToModel<TaiSanTdXuLyModel>();
                    taisanxulys.Remove(tsxl);
                    if (taisanxulys.Count() > 0)
                    {
                        model.listmodel = taisanxulys.Select(c => new XuLyModel() { taisanxuly = c.ToModel<TaiSanTdXuLyModel>() }).ToList();
                        model.listmodel = PrepareDDLForXuLyModel(model.listmodel, null);
                    }
                }
                //model.listQuyetDinh = _taisanTdXuLyService.GetTaiSanTdsXuLyByXuLyId(XuLyId: item.ID, TrangThaiId: (decimal)item.TRANG_THAI_ID)
                //                      .Select(c => (int)c.taisantd.QUYET_DINH_TICH_THU_ID).Distinct().ToList();

                // Ban đầu cho list quyết định rỗng 
                model.listQuyetDinh = new List<int>();
            }
            //more
            if (is_DLL)
            {
                model.DDLBoNganh = _donViModelFactory.PrepareSelectListBoNganhTinh(valSelected: model.CO_QUAN_BAN_HANH_ID != null ? model.CO_QUAN_BAN_HANH_ID : 0, isAddFirst: true).ToList();
                model.DDLQuyetDinhTichThu = _quyetDinhTichThuModelFactory.PrepareDDLQuyetDinhForPhuongAn(false, model.listQuyetDinh);
            }
            return model;
        }
        public void PrepareXuLy(XuLyModel model, XuLy item)
        {
            item.GUID = model.GUID;
            item.QUYET_DINH_SO = model.QUYET_DINH_SO;
            item.QUYET_DINH_NGAY = model.QUYET_DINH_NGAY;
            //item.HINH_THUC = model.HINH_THUC;
            //item.CHI_PHI = model.CHI_PHI;
            item.GHI_CHU = model.GHI_CHU;
            //item.LOAI_XU_LY_ID = model.LOAI_XU_LY_ID;
            item.NGAY_TAO = model.NGAY_TAO;
            item.NGUOI_QUYET_DINH = model.NGUOI_QUYET_DINH;
            item.CO_QUAN_BAN_HANH_ID = model.CO_QUAN_BAN_HANH_ID;
            //item.CO_QUAN_BAN_HANH_TEN = model.CO_QUAN_BAN_HANH_TEN;
            item.TRANG_THAI_ID = model.TRANG_THAI_ID;
    }
        public List<modelThuocTinh> PrepareListmodelThuocTinh(Guid GUID = new Guid(), int PhuongAnXuLyId = 0, int TaiSanTdId = 0,Guid TaiSanXuLyGuid = new Guid())
        {
            var listmodel = new List<modelThuocTinh>();
            var ttHinhThuc = _phuongAnXuLyService.GetPhuongAnXuLyById(PhuongAnXuLyId);
            var tsxl = _taisanTdXuLyService.GetTaiSanTdXuLyByGuId(TaiSanXuLyGuid);
            if (tsxl != null)
            {               
                    // lấy data thuộc tính theo tài sản xử lý
                    var item = _thuocTinhDataService.GetThuocTinhDataByTaiSanId(TaiSanTdXuLyId: (int)tsxl.ID);
                if (item != null && item.Count() > 0)
                {
                    //chuyển đổi từ dạng json về model
                    listmodel = item.Select(c => _thuocTinhDataModelFactory.PreparemodelThuocTinhByThuocTinhData(c, GUID: GUID)).ToList();
                }
                else if (ttHinhThuc != null)
                {
                    if (ttHinhThuc.CONFIG_CAU_HINH != null)
                    {
                        var tt = ttHinhThuc.CONFIG_CAU_HINH.toEntity<ThuocTinhEntity>();
                        if (tt != null)
                        {
                            //gán guid từ view nhóm
                            var ttmodel = _thuocTinhModelFactory.PreparemodelThuocTinhByThuocTinhEntity(new modelThuocTinh(), tt);
                            ttmodel.GuidView = GUID;
                            foreach (var ttcon in ttmodel.THUOC_TINH)
                            {
                                ttcon.GuidView = GUID;
                            }
                            listmodel.Add(ttmodel);
                        }
                    }
                }

            }
            else if (ttHinhThuc != null)
            {
                if (ttHinhThuc.CONFIG_CAU_HINH != null)
                {
                    var tt = ttHinhThuc.CONFIG_CAU_HINH.toEntity<ThuocTinhEntity>();
                    if (tt != null)
                    {
                        //gán guid từ view nhóm
                        var ttmodel = _thuocTinhModelFactory.PreparemodelThuocTinhByThuocTinhEntity(new modelThuocTinh(), tt);
                        ttmodel.GuidView = GUID;
                        foreach (var ttcon in ttmodel.THUOC_TINH)
                        {
                            ttcon.GuidView = GUID;
                        }
                        listmodel.Add(ttmodel);
                    }
                }
            }
            return listmodel;
        }
        public List<SelectListItem> PrepareDDLPhuongAnXuLyTaiSan(bool isAddFirst = true,int valSelected = 0)
        {
            var listqd = _itemService.GetPhuongAnXuLyTaiSans(_workContext.CurrentDonVi.ID);
            var DDL = listqd.Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text =  c.QUYET_DINH_SO + '-' + c.QUYET_DINH_NGAY.toDateVNString(),
                Selected = (valSelected==c.ID)
            }).ToList();
            if(valSelected > 0 && listqd.Where(c => c.ID == valSelected).FirstOrDefault()== null)
            {
                var qd = _itemService.GetXuLyById(valSelected);
                DDL.Insert(0, new SelectListItem
                {
                    Value = qd.ID.ToString(),
                    Text = qd.QUYET_DINH_SO + '-' + qd.QUYET_DINH_NGAY.toDateVNString(),
                    Selected = (valSelected == qd.ID)
                });
            }
            if (isAddFirst)
                DDL.Insert(0, new SelectListItem { Value = "0", Text = "Chọn phương án xử lý" });
            return DDL;
        }
        public List<SelectListItem> PrepareDDLKetQuaXuLyTaiSan(bool isAddFirst = true, int valSelected = 0)
        {
            var listqd = _itemService.GetKetQuaXuLyTaiSans(_workContext.CurrentDonVi.ID).Select(c => new SelectListItem
            {
                Value = c.ID.ToString(),
                Text = c.QUYET_DINH_SO + '-' + c.QUYET_DINH_NGAY.toDateVNString(),
                Selected = (valSelected == c.ID)
            }).ToList();
            if (isAddFirst)
                listqd.Insert(0, new SelectListItem { Value = "0", Text = "Chọn kết quả xử lý" });
            return listqd;
        }

        /// <summary>
        /// Cho phép chọn nhiều phương án xử lý 1 lúc
        /// </summary>
        /// <param name="isAddFirst"></param>
        /// <param name="valSelected"></param>
        /// <returns></returns>
        public List<SelectListItem> PrepareMultiDDLKetQuaXuLyTaiSan(bool isAddFirst = true, IList<int> valSelected = null)
        {
            var result = new List<SelectListItem>();
            var listqd = _itemService.GetKetQuaXuLyTaiSans(_workContext.CurrentDonVi.ID);
            if (valSelected != null && valSelected.Count() > 0)
            {
                // Trường hợp sửa thì sẽ chỉ hiển thị value đã chọn và disable không cho người dùng sửa
                result = listqd.Where(c => valSelected.Contains((int)c.ID)).Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.QUYET_DINH_SO + '-' + c.QUYET_DINH_NGAY.toDateVNString(),
                    Selected = true
                }).ToList();
            }
            else
            {
                if (valSelected != null)
                {
                    // trường hợp thêm mới thì loại bỏ hết tất cả các xử lý đã được thêm vào thu chi

 
                    // tìm ra các xử lý đã có thu chi
                    var ListXuLyIdExist = _thuChiService.GetAllThuChis().SelectMany(c => c.LIST_XU_LY_ID.ToListInt()).Distinct().ToList();

                    // loại bỏ các xử lý đã có thu chi khỏi danh sách thu chi thỏa mãn điều kiện
                    result = listqd.Where(c => !ListXuLyIdExist.Contains((int)c.ID)).Select(c => new SelectListItem
                    {
                        Value = c.ID.ToString(),
                        Text = c.QUYET_DINH_SO + '-' + c.QUYET_DINH_NGAY.toDateVNString(),
                        Selected = false
                    }).ToList();
                }
               
            }
            if (isAddFirst)
                result.Insert(0, new SelectListItem { Value = "", Text = "Chọn kết quả xử lý" });
            return result;
        }
        public XuLyModel PrepareEditXuLyModel(XuLyModel model, XuLy item, bool excludeProperties = false)
        {
            var is_copy = model.Is_Copy;
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<XuLyModel>();
                var taisanxulys = _taisanTdXuLyService.GetTaiSanTdsXuLyByXuLyId((int)model.ID);
                if (taisanxulys != null && taisanxulys.Count() > 0)
                {
                    //lấy listvuviec
                    model.ListVuViec = string.Join(',',(_taiSanTdService.GetTaiSanTdByIds(taisanxulys.Select(c => c.TAI_SAN_ID).ToArray()).Where(c=>c.quyetdinh!=null).Select(c => (int)c.quyetdinh.ID)));
                    foreach(var tsxl in taisanxulys)
                    {
                        var modeltsxl = _taiSanTdXuLyModelFactory.PrepareTaiSanTdXuLyModel(new TaiSanTdXuLyModel(), tsxl);
                        model.taisanxuly.ListModel.Add(modeltsxl);
                    }
                }               
            }
            //more
            model.Is_Copy = is_copy;
            return model;
        }
        #endregion
    }
}

