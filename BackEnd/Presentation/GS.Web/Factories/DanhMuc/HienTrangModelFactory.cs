//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.DanhMuc;
using GS.Services;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.NghiepVu;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Web.Factories.DanhMuc
{
    public class HienTrangModelFactory : IHienTrangModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IHienTrangService _itemService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly ILoaiDonViModelFactory _loaiDonViModelFactory;
        #endregion

        #region Ctor

        public HienTrangModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IHienTrangService itemService,
            INhanHienThiService nhanHienThiService,
            ILoaiDonViModelFactory loaiDonViModelFactory
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService; ;
            this._nhanHienThiService = nhanHienThiService;
            this._loaiDonViModelFactory = loaiDonViModelFactory;
        }
        #endregion

        #region HienTrang
        public HienTrangSearchModel PrepareHienTrangSearchModel(HienTrangSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.LoaiHinhTaiSanAvailable = ((enumLOAI_HINH_TAI_SAN)searchModel.LoaiHinhTSId).ToSelectList();
            searchModel.KieuDuLieuAvailable = ((enumKIEU_DU_LIEU)searchModel.KieuDuLieuId).ToSelectList();
            searchModel.SetGridPageSize();
            return searchModel;
        }

        public HienTrangListModel PrepareHienTrangListModel(HienTrangSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchHienTrangs(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, LoaiHinhTS: searchModel.LoaiHinhTSId);

            //prepare list model
            var model = new HienTrangListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                {
                    var m = c.ToModel<HienTrangModel>();
                    m.TEN_KIEU_DU_LIEU = _nhanHienThiService.GetGiaTriEnum(c.KieuDuLieu);
                    m.TEN_LOAI_TAI_SAN = _nhanHienThiService.GetGiaTriEnum(c.LoaiHinhTS);
                    if (m.NHOM_HIEN_TRANG_ID == null || m.NHOM_HIEN_TRANG_ID == 1) // Do không muốn xóa loại hiên trạng kinh doanh -không kd nên xử lý cho = Tài sản công
                    {
                        m.NHOM_HIEN_TRANG_ID = 0;
                    }
                    m.TEN_NHOM_HIEN_TRANG = _nhanHienThiService.GetGiaTriEnum((enumNHOM_HIEN_TRANG)m.NHOM_HIEN_TRANG_ID);
                    return m;
                }),

                Total = items.TotalCount
            };
            return model;
        }
        public HienTrangModel PrepareHienTrangModel(HienTrangModel model, HienTrang item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<HienTrangModel>();
            }
            if (model.ID > 0)
            {
                model.LoaiHinhTaiSanAvaliable = ((enumLOAI_HINH_TAI_SAN)model.LOAI_HINH_TAI_SAN_ID).ToSelectList();
                model.KieuDuLieuAvaliable = ((enumKIEU_DU_LIEU)model.KIEU_DU_LIEU_ID).ToSelectList();
                if (model.NHOM_HIEN_TRANG_ID > 0)
                    model.NhomHienTrangAvaliable = ((enumNHOM_HIEN_TRANG)(model.NHOM_HIEN_TRANG_ID ?? 0)).ToSelectList(valuesToExclude: new int[] { 1 });
                else
                    model.NhomHienTrangAvaliable = model.enumNhomHienTrang.ToSelectList(valuesToExclude: new int[] { 1 });
                if (model.HIEN_THI_ID > 0)
                    model.HienTrangHienThiAvaliable = ((enumHienTrang_HIEN_THI_ID)(model.HIEN_THI_ID ?? 0)).ToSelectList();
                else
                    model.HienTrangHienThiAvaliable = model.enumHienThiId.ToSelectList();
                model.SelectedLoaiDonViIds = new List<int>();
                //model.SelectedLoaiDonViIds.Add(0);
                if (item.LOAI_DON_VI_AP_DUNG != null)
                {
                    if (item.LOAI_DON_VI_AP_DUNG.Length > 0)
                    {
                        var listDonVi = item.LOAI_DON_VI_AP_DUNG.Split(',').ToList();
                        foreach (var i in listDonVi)
                        {
                            if (!String.IsNullOrEmpty(i))
                            {
                                model.SelectedLoaiDonViIds.Add(Convert.ToInt32(i));
                            }
                        }
                    }
                }
                else
                {
                    model.SelectedLoaiDonViIds.Add(0);
                }
            }
            else
            {
                model.LoaiHinhTaiSanAvaliable = model.enumLoaiHinhTaiSan.ToSelectList();
                model.KieuDuLieuAvaliable = model.enumKieuDuLieu.ToSelectList();
                model.NhomHienTrangAvaliable = model.enumNhomHienTrang.ToSelectList(valuesToExclude: new int[] { 1 });
                model.HienTrangHienThiAvaliable = model.enumHienThiId.ToSelectList();
            }
            //more
            model.ddlLoaiDonVi = _loaiDonViModelFactory.PrepareMultiSelectLoaiDonVi(model.SelectedLoaiDonViIds, false);

            return model;
        }
        public void PrepareHienTrang(HienTrangModel model, HienTrang item)
        {
            item.TEN_HIEN_TRANG = model.TEN_HIEN_TRANG;
            item.LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID;
            item.KIEU_DU_LIEU_ID = model.KIEU_DU_LIEU_ID;
            item.NHOM_HIEN_TRANG_ID = model.NHOM_HIEN_TRANG_ID;
            item.HIEN_THI_ID = model.HIEN_THI_ID;
            item.LOAI_DON_VI_AP_DUNG = model.LOAI_DON_VI_AP_DUNG;
            item.SAP_XEP = model.SAP_XEP;
        }

        public IList<HienTrangModel> PrepareListHienTrangModel(HienTrangSearchModel searchModel)
        {
            var items = _itemService.ListHienTrangsByFields(loaiHinhTSId: searchModel.LoaiHinhTSId, kieuDuLieuID: searchModel.KieuDuLieuId, nhomHienTrangId: searchModel.NhomHienTrangId);
            var model = items.Select(p => p.ToModel<HienTrangModel>()).ToList();
            return model;
        }
        /// <summary>
        /// Truyền vào list hiện trạng hiện trạng và donViID , check xem trong list hiện trạng có hiện trạng nào bị nhập sai so với loại hình đơn vi không
        /// true là có nhập sai, false là không sai
        /// </summary>
        /// <param name="donViID"></param>
        /// <param name="listHienTrangId"></param>
        /// <returns></returns>
        public bool CheckIsListHienTrangNhapSai(decimal donViID, IList<ObjHienTrang> listHienTrangId = null)
        {
            if (donViID <= 0 || listHienTrangId == null)
            {
                return true; // Coi như nhập sai
            }

            return listHienTrangId.Any(c => (!_itemService.CheckHienTrangTheoLoaiDonVi(donViID, c.HienTrangId ?? 0) && (c.GiaTriCheckbox == true || c.GiaTriNumber >0|| c.GiaTriNumber < 0))) ;
        }
        /// <summary>
        /// Truyền vào hiện trạng hiện trạng và donViID, check xem hiện trạng đó có bị nhập sai so với loại hình đơn vi không
        /// true là có nhập sai, false là không sai
        /// </summary>
        /// <param name="donViID"></param>
        /// <param name="ObjHienTrang"></param>
        /// <returns></returns>
        public bool CheckIsHienTrangNhapSai(decimal donViID, ObjHienTrang ObjHienTrang = null)
        {
            if (donViID <= 0 || ObjHienTrang == null)
            {
                return false; // Coi như nhập đúng
            }

            return (!_itemService.CheckHienTrangTheoLoaiDonVi(donViID, ObjHienTrang.HienTrangId ?? 0) && (ObjHienTrang.GiaTriCheckbox == true || ObjHienTrang.GiaTriNumber > 0 || ObjHienTrang.GiaTriNumber < 0));
        }

        public bool IsAnyHienTrangSai(IList<ObjHienTrang> lstHienTrang, decimal donViId)
        {
            var countError = 0;
            if (lstHienTrang != null && lstHienTrang.Count() > 0)
            {

                foreach (var hienTrang in lstHienTrang)
                {
                    if (hienTrang != null)
                    {
                        // nếu mà hiện trạng không đúng với loại hình đơn vị nhưng có giá trị thì báo validate
                        if (CheckIsHienTrangNhapSai(donViId, hienTrang))
                        {
                            countError += 1;                       
                        }
                    }
                }

            }
            return countError > 0;

        }
        
        #endregion
    }
}

