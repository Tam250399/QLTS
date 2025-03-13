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
using System.Data;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Data.Extensions;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.CauHinh;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Core.Domain.SHTD;
using GS.Core.Domain.DB;
using GS.Services.DB;
using GS.Services.DanhMuc;
using static GS.Core.Domain.SHTD.QuyetDinhTichThu;
using GS.Core.Domain.DanhMuc;

namespace GS.Services.SHTD
{
    public partial class QuyetDinhTichThuService : IQuyetDinhTichThuService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<QuyetDinhTichThu> _itemRepository;
        private readonly ITaiSanTdService _taiSanTdService;
        private readonly ITaiSanNhatKyService _taiSanNhatKyService;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly INguonGocTaiSanService _nguonGocTaiSanService;
        private readonly IRepository<DonVi> _donViRepository;
        private readonly IDonViService _donViService;
        #endregion

        #region Ctor

        public QuyetDinhTichThuService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IRepository<QuyetDinhTichThu> itemRepository,
            ITaiSanTdService taiSanTdService,
            ITaiSanNhatKyService taiSanNhatKyService,
            ILoaiTaiSanService loaiTaiSanService,
            INguonGocTaiSanService nguonGocTaiSanService,
            IRepository<DonVi> donViRepository, 
            IDonViService donViService
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._taiSanTdService = taiSanTdService;
            this._workContext = workContext;
            this._taiSanNhatKyService = taiSanNhatKyService;
            this._loaiTaiSanService = loaiTaiSanService;
            this._nguonGocTaiSanService = nguonGocTaiSanService;
            this._donViRepository = donViRepository;
            this._donViService = donViService;
        }

        #endregion

        #region Methods
        public virtual IList<QuyetDinhTichThu> GetAllQuyetDinhTichThus()
        {
            var query = _itemRepository.Table.Where(c => c.TRANG_THAI_ID == (int)enumTRANGTHAI_QUYETDINH_TSTD.DUYET);
            return query.ToList();
        }
        public virtual IList<QuyetDinhTichThu> GetAllQuyetDinhTichThusChuaDongBo()
        {
            var query = _itemRepository.Table.Where(c => c.TRANG_THAI_ID == (int)enumTRANGTHAI_QUYETDINH_TSTD.DUYET && (c.DB_ID == null || c.DB_ID == "0"));
            return query.ToList();
        }
        public virtual IList<QuyetDinhTichThu> GetQuyetDinhTichThus()
        {
            var query = _itemRepository.Table.Where(c => c.TRANG_THAI_ID == (int)enumTRANGTHAI_QUYETDINH_TSTD.DUYET && c.DON_VI_ID == _workContext.CurrentDonVi.ID);
            return query.ToList();
        }
        public virtual IPagedList<QuyetDinhTichThu> SearchQuyetDinhTichThus(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, int LoaiXuLy = 0, string SoQuyetDinh = null, DateTime? NgayQuyetDinhTu = null, DateTime? NgayQuyetDinhDen = null, int LoaiTaiSan = 0, int NguonGocTaiSan = 0, int TrangThaiId = (int)enumTRANGTHAI_QUYETDINH_TSTD.DUYET, Decimal LoaiHinhTaiSanId = 0, bool? isTichThu = null)
        {
            var donviTREE_NODE = _donViService.GetDonViById(_workContext.CurrentDonVi.ID)?.TREE_NODE;
            var query = from q in _itemRepository.Table
                        join dmdv in _donViRepository.Table on q.DON_VI_ID equals dmdv.ID
                        where dmdv.TREE_NODE.StartsWith(donviTREE_NODE)
                        select q;
            //var query = _itemRepository.Table.Where(c => c.DON_VI_ID == _workContext.CurrentDonVi.ID);
            if (TrangThaiId!= (int)enumTRANGTHAI_QUYETDINH_TSTD.DUYET && TrangThaiId >0)
            {
                query = query.Where(c => c.TRANG_THAI_ID == TrangThaiId);
            }
            else
            {
                query = query.Where(c => c.TRANG_THAI_ID == (int)enumTRANGTHAI_QUYETDINH_TSTD.DUYET);
            }
            if (SoQuyetDinh != null && SoQuyetDinh.Trim() != "")
            {
                query = query.Where(c => c.QUYET_DINH_SO.ToLower().Contains(SoQuyetDinh.ToLower()));
            }
            if (LoaiHinhTaiSanId >0)
            {
                var listqd = _taiSanTdService.GetTaiSanTds(LoaiHinhTaiSan: LoaiHinhTaiSanId).Select(c=>c.quyetdinh.ID).ToList();
                query = query.Where(c => listqd.Contains(c.ID));
            }
            if (NgayQuyetDinhTu != null)
            {
                query = query.Where(c => c.QUYET_DINH_NGAY >= NgayQuyetDinhTu);
            }
            if (NgayQuyetDinhDen != null)
            {
                query = query.Where(c => c.QUYET_DINH_NGAY <= NgayQuyetDinhDen);
            }
            if (LoaiTaiSan > 0)
            {
                var loaiTS = _loaiTaiSanService.GetLoaiTaiSanById(LoaiTaiSan);
                var listidTS = _loaiTaiSanService.GetLoaiTaiSans(TreeNode:loaiTS.TREE_NODE).Select(c=>(decimal?)c.ID).ToList();
                var listqd = (from ts in _taiSanTdService.GetAllTaiSanTds() where listidTS.Contains(ts.LOAI_TAI_SAN_ID) select ts.QUYET_DINH_TICH_THU_ID).ToList();
                query = query.Where(c => listqd.Contains(c.ID));
            }
            if (NguonGocTaiSan > 0)
            {
                var nguonGoc = _nguonGocTaiSanService.GetNguonGocTaiSanById(NguonGocTaiSan);
                var listIdNG = _nguonGocTaiSanService.GetNguonGocTaiSans(TreeNode:nguonGoc.TREE_NODE).Select(c => (decimal?)c.ID).ToList();               
                query = query.Where(c => listIdNG.Contains(c.NGUON_GOC_ID));
            }
            if(isTichThu != null)
            {
                var nguonGoc = _nguonGocTaiSanService.GetNguonGocTaiSanByMa(enumMaLoaiQuyetDinh.TichThu);
                var listIdNG = _nguonGocTaiSanService.GetNguonGocTaiSans(TreeNode: nguonGoc.TREE_NODE).Select(c => (decimal?)c.ID).ToList();
                if (isTichThu== true)
                {
                    query = query.Where(c => listIdNG.Contains(c.NGUON_GOC_ID));
                }
                else
                {
                    query = query.Where(c => !listIdNG.Contains(c.NGUON_GOC_ID));
                }
            }
            return new PagedList<QuyetDinhTichThu>(query.OrderByDescending(c => c.QUYET_DINH_NGAY), pageIndex, pageSize); ;
        }

        public virtual QuyetDinhTichThu GetQuyetDinhTichThuById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }
        public virtual QuyetDinhTichThu GetQuyetDinhTichThuByDB_ID(string DB_Id)
        {
            if (string.IsNullOrEmpty(DB_Id))
                return null;
            decimal?[] listInclu = new decimal?[] { (int)enumTRANGTHAI_QUYETDINH_TSTD.DUYET, (int)enumTRANGTHAI_QUYETDINH_TSTD.CHO_DUYET, (int)enumTRANGTHAI_QUYETDINH_TSTD.TU_CHOI };
            return _itemRepository.Table.Where(c=>c.DB_ID == DB_Id && listInclu.Contains(c.TRANG_THAI_ID)).FirstOrDefault();
        }
        public virtual QuyetDinhTichThu GetQuyetDinhTichThuByGuid(Guid guid)
        {

            return _itemRepository.Table.Where(c => c.GUID == guid).FirstOrDefault();
        }
        public virtual IList<QuyetDinhTichThu> GetQuyetDinhTichThuByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertQuyetDinhTichThu(QuyetDinhTichThu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            //entity.TRANG_THAI_ID = (int)enumTRANGTHAITSTD.DUYET;
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateQuyetDinhTichThu(QuyetDinhTichThu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            // tài sản nhật ký
            //_taiSanNhatKyService.UpdateQuyetDinhTichThuNhatKY(entity.ID);
            //var taiSanNhatKys = _taiSanNhatKyService.GetTaiSanNhatKys(isTaiSanToanDan: true, QuyetDinhID: (int)entity.ID);
            //if (taiSanNhatKys.Count() > 0)
            //{
            //    foreach (var taiSanNhatKy in taiSanNhatKys)
            //    {
            //        if (taiSanNhatKy != null)
            //        {
            //            taiSanNhatKy.NGAY_DONG_BO = DateTime.Now;
            //            if (taiSanNhatKy.TRANG_THAI_ID == (int)enumTrangThaiTaiSanNhatKy.DA_DONG_BO)
            //            {
            //                taiSanNhatKy.TRANG_THAI_ID = (int)enumTrangThaiTaiSanNhatKy.CO_THAY_DOI;
            //            }
            //            _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
            //        }
            //    }
            //}
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteQuyetDinhTichThu(QuyetDinhTichThu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            // tài sản nhật ký
            var taiSanNhatKys = _taiSanNhatKyService.GetTaiSanNhatKys(isTaiSanToanDan: true, QuyetDinhID: (int)entity.ID);
            if (taiSanNhatKys.Count() > 0)
            {
                foreach (var taiSanNhatKy in taiSanNhatKys)
                {
                    if (taiSanNhatKy != null)
                    {
                        taiSanNhatKy.NGAY_DONG_BO = DateTime.Now;
                        if (taiSanNhatKy.TRANG_THAI_ID == (int)enumTrangThaiTaiSanNhatKy.DA_DONG_BO)
                        {
                            taiSanNhatKy.TRANG_THAI_ID = (int)enumTrangThaiTaiSanNhatKy.CO_THAY_DOI;
                        }
                        _taiSanNhatKyService.UpdateTaiSanNhatKy(taiSanNhatKy);
                    }
                }
            }
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public virtual QuyetDinhTichThu GetQuyetDinhTichThu(string SoQuyetDinh = null, DateTime? NgayQuyetDinh = null, string MaDonVi = null)
        {
            if (string.IsNullOrEmpty(SoQuyetDinh) || NgayQuyetDinh == null)
                return null;
            var query = _itemRepository.Table;
            return query.Where(m => m.QUYET_DINH_SO == SoQuyetDinh && m.QUYET_DINH_NGAY.Value.Date == NgayQuyetDinh.Value.Date && m.DonVi.MA == MaDonVi).FirstOrDefault();
        }
        #endregion
    }
}

