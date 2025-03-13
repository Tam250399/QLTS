//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/10/2021
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
using GS.Core.Domain.BaoCaoDienTu;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.BaoCaoDienTus;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.BaoCao;
using Microsoft.AspNetCore.Mvc.Rendering;
using GS.Web.Factories.DanhMuc;
using GS.Services;
using GS.Services.DanhMuc;
using System.IO;
using Microsoft.AspNetCore.Http;
using GS.Web.Models.BaoCaoDienTu;

namespace GS.Web.Factories.BaoCaoDienTus
{
    public class BaoCaoDienTuModelFactory : IBaoCaoDienTuModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IBaoCaoDienTuService _itemService;
        private readonly IDonViModelFactory _donViModelFactory;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IDonViService _donViService;
        private readonly IFileCongViecService _fileCongViecService;
        private readonly IGSFileProvider _fileProvider;
        private readonly IDonViService _donviService;
        private readonly INguoiDungService _nguoiDungService;
        #endregion

        #region Ctor

        public BaoCaoDienTuModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IBaoCaoDienTuService itemService,
            IDonViModelFactory donViModelFactory,
            INhanHienThiService nhanHienThiService,
            IDonViService donViService,
            IFileCongViecService fileCongViecService,
            IGSFileProvider fileProvider,
            INguoiDungService nguoiDungService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._donViModelFactory = donViModelFactory;
            this._nhanHienThiService = nhanHienThiService;
            this._donViService = donViService;
            this._fileCongViecService = fileCongViecService;
            this._fileProvider = fileProvider;
            this._donViService = donViService;
            this._nguoiDungService = nguoiDungService;
        }
        #endregion

        #region BaoCaoDoiChieu
        public BaoCaoDienTuSearchModel PrepareBaoCaoDienTuSearchModel(BaoCaoDienTuSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.ddlTrangThai = (enumTrangThaiBaoCao.TAT_CA).ToSelectList().ToList();
            searchModel.SetGridPageSize();
            return searchModel;
        }

        public BaoCaoDienTuListModel PrepareBaoCaoDienTuListModel(BaoCaoDienTuSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            var donvi = _workContext.CurrentDonVi.ID;
            //get items
            var items = _itemService.SearchBaoCaoDienTu(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, donVi: donvi, NGAY_BAO_CAO: searchModel.NGAY_BAO_CAO, TRANG_THAI_ID: searchModel.TRANG_THAI_ID); 

            //prepare list model
            var model = new BaoCaoDienTuListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                {
                    var m = c.ToModel<BaoCaoDienTuModel>();
                    var donVi = _donViService.GetDonViById(c.DON_VI_ID ?? 0);
                    var nguoidung = _nguoiDungService.GetNguoiDungById(c.NGUOI_DUYET_ID ?? 0);
                    m.TenTrangThai = _nhanHienThiService.GetGiaTriEnum(c.TenTrangThai);
                    if(nguoidung != null)
                    {
                        m.TenNguoiDuyet = nguoidung.TEN_DAY_DU;
                    }
                    if (m.FILE_ID > 0)
                    {
                        var file = _fileCongViecService.GetFileCongViecById(m.FILE_ID);
                        if (file != null)
                        {
                            m.TenFile = file.TEN_FILE;
                        }
                    }

                    return m;
                }).ToList(),
                Total = items.TotalCount
            };
            return model;
        }
        public BaoCaoDienTuListModel PrepareBaoCaoChoDuyetListModel(BaoCaoDienTuSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            var donvi = _workContext.CurrentDonVi.ID;
            //get items
            var items = _itemService.SearchBaoCaoChoDuyet(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, donVi: donvi, NGAY_BAO_CAO: searchModel.NGAY_BAO_CAO, TRANG_THAI_ID: searchModel.TRANG_THAI_ID);

            //prepare list model
            var model = new BaoCaoDienTuListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                {
                    var m = c.ToModel<BaoCaoDienTuModel>();
                    var donVi = _donViService.GetDonViById(c.DON_VI_ID ?? 0);
                    var nguoidung = _nguoiDungService.GetNguoiDungById(c.NGUOI_DUYET_ID ?? 0);
                    m.TenTrangThai = _nhanHienThiService.GetGiaTriEnum(c.TenTrangThai);
                    if(nguoidung != null)
                    {
                        m.TenNguoiDuyet = nguoidung.TEN_DAY_DU;
                    }
                    //m.pageIndex = searchModel.Page;
                    return m;
                }).ToList(),
                Total = items.TotalCount
            };
            return model;
        }
        public BaoCaoDienTuModel PrepareBaoCaoDienTuModel(BaoCaoDienTuModel model, BaoCaoDienTu item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<BaoCaoDienTuModel>();
            }
            //more
            //model.DON_VI_ID = (int)_workContext.CurrentDonVi.ID;
            model.DDLTrangThai = (new enumTrangThaiBaoCao()).ToSelectList().ToList();
            model.DDLTrangThai.AddFirstRow("---Tất cả---","");
            var file = _fileCongViecService.GetFileCongViecById(model.FILE_ID);
            if (file != null)
            {
                model.TenFile = file.TEN_FILE;
            }
            return model;
        }
        public void PrepareBaoCaoDienTu(BaoCaoDienTuModel model, BaoCaoDienTu item)
        {
            item.DON_VI_ID = model.DON_VI_ID;
            item.TEN = model.TEN;
            item.NGAY_BAO_CAO = model.NGAY_BAO_CAO;
            item.NGUOI_DUYET_ID = model.NGUOI_DUYET_ID;
            item.NGUOI_TAO_ID = model.NGUOI_TAO_ID;
            item.SO_VAN_BAN = model.SO_VAN_BAN;
            item.TINH_HINH_BAN_HANH_VAN_BAN = model.TINH_HINH_BAN_HANH_VAN_BAN;
            item.TINH_HINH_TANG_GIAM = model.TINH_HINH_TANG_GIAM;
            item.THUC_TRANG = model.THUC_TRANG;
            item.TINH_HINH_THUC_HIEN = model.TINH_HINH_THUC_HIEN;
            item.DANH_GIA_TICH_CUC = model.DANH_GIA_TICH_CUC;
            item.DANH_GIA_TINH_HINH = model.DANH_GIA_TINH_HINH;
            item.CONG_TAC_CHI_DAO = model.CONG_TAC_CHI_DAO;
            item.KET_QUA_KHAC = model.KET_QUA_KHAC;
            item.KIEN_NGHI = model.KIEN_NGHI;
            item.NOI_NHAN = model.NOI_NHAN;
            item.LY_DO_TU_CHOI = model.LY_DO_TU_CHOI;
            item.FILE_ID = model.FILE_ID;
            //item.FILE_ID = model.FILE_ID;
            //item.TenFile = model.TenFile;
        }
        public BaoCaoDienTuModel PrepareBaoCaoDienTuModelView(BaoCaoDienTuModel model, BaoCaoDienTu item)
        {
            model = item.ToModel<BaoCaoDienTuModel>();
            //var dv = _donviService.GetDonViById(item.DON_VI_ID ?? 0);
            //model.DonViMa = dv.MA;
            //model.DonViTen = dv.TEN;
            var nguoiTao = _nguoiDungService.GetNguoiDungById(item.NGUOI_TAO_ID);
            if (nguoiTao != null)
            {
                model.NguoiTaoTen = nguoiTao.TEN_DAY_DU;
            }
            var file = _fileCongViecService.GetFileCongViecById(item.FILE_ID??0);
            if( file != null)
            {
                model.GUID = file.GUID;
            }
            return model;
        }
        public FileCongViec InsertFileCongViecFromFolder(string filePath)
        {
            var fileContent = _fileProvider.ReadAllBytes(filePath);
            var fwork = new FileCongViec
            {
                GUID = Guid.NewGuid(),
                NOI_DUNG_FILE = fileContent,
                LOAI_FILE = "application/pdf",
                //we store filename without extension for downloads
                TEN_FILE = _fileProvider.GetFileName(filePath),
                DUOI_FILE = ".pdf",
                NGAY_TAO = DateTime.Now,
                NGUOI_TAO = _workContext.CurrentCustomer.ID,
                KICH_THUOC = Convert.ToInt32(fileContent.LongLength / 1024) //luu thanh kb
            };

            _fileCongViecService.InsertFileCongViec(fwork);
            return fwork;
        }
        public void SaveWorkFileOnDisk(FileCongViec item, byte[] fileContent)
        {
            string _pathStore = item.NGAY_TAO.ToPathFolderStore();
            _pathStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), _pathStore);
            _fileProvider.CreateDirectory(_pathStore);
            var _fileStore = _fileProvider.Combine(_pathStore, item.GUID.ToString() + item.DUOI_FILE);
            _fileProvider.WriteAllBytes(_fileStore, fileContent);
        }
        #endregion
    }
}