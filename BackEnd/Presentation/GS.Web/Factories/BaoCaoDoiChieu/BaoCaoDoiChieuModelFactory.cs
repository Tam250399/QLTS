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
using GS.Core.Domain.BaoCaoDoiChieus;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.BaoCaoDoiChieus;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.BaoCao;
using Microsoft.AspNetCore.Mvc.Rendering;
using GS.Web.Factories.DanhMuc;
using GS.Services;
using GS.Services.DanhMuc;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace GS.Web.Factories.BaoCao
{
    public class BaoCaoDoiChieuModelFactory : IBaoCaoDoiChieuModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IBaoCaoDoiChieuService _itemService;
        private readonly IDonViModelFactory _donViModelFactory;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IDonViService _donViService;
        private readonly IFileCongViecService _fileCongViecService;
        private readonly IGSFileProvider _fileProvider;
        #endregion

        #region Ctor

        public BaoCaoDoiChieuModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IBaoCaoDoiChieuService itemService,
            IDonViModelFactory donViModelFactory,
            INhanHienThiService nhanHienThiService,
            IDonViService donViService,
            IFileCongViecService fileCongViecService,
            IGSFileProvider fileProvider
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
            _fileProvider = fileProvider;
        }
        #endregion

        #region BaoCaoDoiChieu
        public BaoCaoDoiChieuSearchModel PrepareBaoCaoDoiChieuSearchModel(BaoCaoDoiChieuSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.ddlDonVi = _donViModelFactory.PrepareSelectListDonViUsingProc().ToList();
            searchModel.NamBaoCao = 2020;
            searchModel.SetGridPageSize();
            return searchModel;
        }

        public BaoCaoDoiChieuListModel PrepareBaoCaoDoiChieuListModel(BaoCaoDoiChieuSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            var donvi = _workContext.CurrentDonVi.ID;
            //get items
            var items = _itemService.SearchBaoCaoDoiChieus(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, donviId: donvi, namBaoCao: searchModel.NamBaoCao, donVi: searchModel.DonVi); ;

            //prepare list model
            var model = new BaoCaoDoiChieuListModel
            {
                //fill in model values from the entity
                //Data = items.Select(c => c.ToModel<BaoCaoDoiChieuModel>()),
                Data = items.Select(c =>
                {
                    var m = c.ToModel<BaoCaoDoiChieuModel>();
                    m.PhanBaoCao = _nhanHienThiService.GetGiaTriEnum(c.PhanBaoCao);
                    m.PhanMem = _nhanHienThiService.GetGiaTriEnum(c.PhanMem);
                    var donVi = _donViService.GetDonViById(c.DON_VI_ID ?? 0);
                    m.TenDonVi = donVi != null ? donVi.TEN : "";
                    var file = _fileCongViecService.GetFileCongViecById(m.FILE_ID);
                    if (file != null)
                    {
                        m.TenFile = file.TEN_FILE;
                    }
                    //m.pageIndex = searchModel.Page;
                    return m;
                }).ToList(),
                Total = items.TotalCount
            };
            return model;
        }
        public BaoCaoDoiChieuModel PrepareBaoCaoDoiChieuModel(BaoCaoDoiChieuModel model, BaoCaoDoiChieu item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<BaoCaoDoiChieuModel>();
            }
            //more
            //model.DON_VI_ID = (int)_workContext.CurrentDonVi.ID;
            model.DDLDonVi = _donViModelFactory.PrepareSelectListDonViUsingProc().ToList();
            model.DDLHeThong = (new enumHeThong()).ToSelectList().ToList();
            //model.DDLHeThong.AddFirstRow("---Chọn phần mềm---");
            model.DDLPhanBaoCao = (new enumPhanBaoCao()).ToSelectList().ToList();
            var file = _fileCongViecService.GetFileCongViecById(model.FILE_ID);
            if (file != null)
            {
                model.TenFile = file.TEN_FILE;
            }
            //model.DDLPhanBaoCao.AddFirstRow("---Chọn phần báo cáo---","");
            return model;
        }
        public void PrepareBaoCaoDoiChieu(BaoCaoDoiChieuModel model, BaoCaoDoiChieu item)
        {
            item.DON_VI_ID = model.DON_VI_ID;
            item.BAO_CAO_ID = model.BAO_CAO_ID;
            item.NAM_BAO_CAO = model.NAM_BAO_CAO;
            item.HE_THONG_ID = model.HE_THONG_ID;
            item.NGAY_TAO = model.NGAY_TAO;
            item.NGAY_CAP_NHAT = model.NGAY_CAP_NHAT;
            item.FILE_ID = model.FILE_ID;
            //item.TenFile = model.TenFile;
        }
        public IList<string> InsertBaoCaoDoiChieuFromFolder(string folderName = null, int loai = 0)
        {
            var directoryPath = $@"{folderName}";
            var filePathList = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);
            var listError = new List<string>();
            foreach (var filePath in filePathList)
            {
                try
                {
                    var fileName = _fileProvider.GetFileName(filePath);

                    var listInfo = fileName.Split('_');
                    var maDonVi = listInfo[0];
                    var maBaoCao = listInfo[1];
                    var nam = listInfo[2];
                    var phan = "";
                    var nguon = "";

                    if (maBaoCao == "02A")
                    {
                        phan = listInfo[3];
                        var lastString = listInfo[4].Split(".");
                        nguon = lastString[0];
                    }
                    else
                    {
                        var lastString = listInfo[3].Split(".");
                        nguon = lastString[0];

                    }
                    BaoCaoDoiChieu item = new BaoCaoDoiChieu();
                    item.DON_VI_ID = _donViService.GetDonViByMa(maDonVi).ID;
                    item.BAO_CAO_ID = GetBaoCaoId(phan);
                    item.NAM_BAO_CAO = int.Parse(nam);
                    item.HE_THONG_ID = GetHeThongId(nguon, loai);

                    item.NGAY_CAP_NHAT = DateTime.Today;
                    item.NGAY_TAO = DateTime.Today;

                    var fileCongViec = InsertFileCongViecFromFolder(filePath);
                    if (fileCongViec != null)
                    {
                        var baoCaos = _itemService.GetBaoCaoDoiChieu(donViId: item.DON_VI_ID, namBaoCao: item.NAM_BAO_CAO, baoCaoId: item.BAO_CAO_ID, heThongId: item.HE_THONG_ID);
                        if (baoCaos != null)
                            _itemService.DeleteBaoCaoDoiChieu(baoCaos);
                        item.FILE_ID = fileCongViec.ID;
                        _itemService.InsertBaoCaoDoiChieu(item);
                    }
                }
                catch (Exception e)
                {
                    listError.Add(filePath + " Error :" + e.Message);
                    continue;
                }



            }
            return listError;

        }
        private int GetBaoCaoId(string phan)
        {
            if (string.IsNullOrEmpty(phan))
            {
                return (int)enumPhanBaoCao.BAO_CAO_1A;
            }
            switch (phan)
            {
                case "P1":
                    return (int)enumPhanBaoCao.TONG_HOP_CHUNG;
                case "P2":
                    return (int)enumPhanBaoCao.LOAI_HINH_DON_VI;
                case "P3":
                    return (int)enumPhanBaoCao.DON_VI_TRUC_THUOC;
                default:
                    return 0;
            }
        }
        private int GetHeThongId(string nguon, int loai)
        {

            switch (nguon)
            {
                case "DKTS":
                case "ĐKTS":
                    return (int)enumHeThong.DKTS;
                case "TSC":
                    if (loai == 0)
                    {
                        return (int)enumHeThong.QLTSC_DKTS;

                    }
                    else
                    {
                        return (int)enumHeThong.QLTSC_QLTSNN;
                    }
                case "KHO":
                    return (int)enumHeThong.KHO;
                case "CTNS":
                case "CNTS":
                    return (int)enumHeThong.CTNS;
                case "HTDB":
                case "HTĐB":
                    return (int)enumHeThong.HTDB;
                case "QLTSNN":
                case "TSNN":
                    return (int)enumHeThong.QLTSNN;
                default:
                    return 0;
            }
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