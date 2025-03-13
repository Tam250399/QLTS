using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DB;
using GS.Core.Domain.KT;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Data;
using GS.Services.DanhMuc;
using GS.Services.DB;
using GS.Services.HeThong;
using GS.Services.NghiepVu;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Services.TaiSans
{
    public partial class TaiSanImportService : ITaiSanImportService
    {
        #region
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<ImportExcelTaiSan> _itemRepository;
        private readonly IWorkContext _workContext;
        private readonly IYeuCauService _yeuCauService;
        private readonly IDonViService _donViService;
        private readonly ICauHinhService _cauHinhService;
        private readonly ITaiSanNhatKyService _taiSanNhatKyService;
        #endregion Fields

        #region Ctor

        public TaiSanImportService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<ImportExcelTaiSan> itemRepository,
            IWorkContext workContext,
            IYeuCauService yeuCauService,
            IDonViService donViService,
            ICauHinhService cauHinhService,
            ITaiSanNhatKyService taiSanNhatKyService
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._workContext = workContext;
            this._yeuCauService = yeuCauService;
            this._donViService = donViService;
            this._cauHinhService = cauHinhService;
            this._taiSanNhatKyService = taiSanNhatKyService;
        }

        #endregion Ctor

        #region Method
        public virtual void InsertTaiSan(ImportExcelTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
        }

        public virtual void UpdateTaiSan(ImportExcelTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
        }

        public virtual void DeleteTaiSan(ImportExcelTaiSan entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
        }

        public virtual IList<ImportExcelTaiSan> GetAllTaiSans(int LoaiTaiSan = 0, string BoPhanSuDung = null, string donViMa = null)
        {
            var query = _itemRepository.Table;
            
            if (!String.IsNullOrEmpty(BoPhanSuDung))
            {
                query = query.Where(c => c.BO_PHAN_SU_DUNG == BoPhanSuDung);
            }
            if (!String.IsNullOrEmpty(donViMa))
            {
                query = query.Where(c => c.DON_VI_MA == donViMa);
            }
            return query.ToList();
        }

        public virtual ImportExcelTaiSan GetTaiSanById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }
        public virtual IList<ImportExcelTaiSan> GetTaiSanByDonViMa(string donViMa)
        {
            if (String.IsNullOrEmpty(donViMa))
                return null;
            var query = _itemRepository.Table;
            query = query.Where(c => c.DON_VI_MA == donViMa);
            return query.ToList();
        }
        #endregion
    }
}
