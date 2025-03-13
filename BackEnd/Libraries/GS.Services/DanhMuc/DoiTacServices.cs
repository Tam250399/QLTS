//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Data;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GS.Services.DanhMuc
{
    public partial class DoiTacService : IDoiTacService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<DoiTac> _itemRepository;
        #endregion

        #region Ctor

        public DoiTacService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<DoiTac> itemRepository
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
        }

        #endregion

        #region Methods
        public virtual IList<DoiTac> GetAllDoiTacs()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IList<DoiTac> GetDoiTacs(Decimal? DonViId = 0, Decimal LoaiDoiTac = 0, string keySearch = null, bool isComboBox = false)
        {
            var query = _itemRepository.Table;
            if (DonViId > 0)
                query = query.Where(c => c.DON_VI_ID == DonViId);
            if (LoaiDoiTac > 0)
                query = query.Where(c => c.LOAI_DOI_TAC_ID == LoaiDoiTac);
            if (!String.IsNullOrEmpty(keySearch))
            {
                query = query.Where(c => c.TEN.Contains(keySearch) || c.MA.Contains(keySearch));
            }
            if (isComboBox)
            {
                query = query.Take(10);
            }
            return query.ToList();
        }

        public virtual IPagedList<DoiTac> SearchDoiTacs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, Decimal? donViId = 0)
        {
            var query = _itemRepository.Table;
            if (Keysearch != null)
            {
                query = query.Where(c => c.MA.ToUpper().Contains(Keysearch.ToUpper()) || c.TEN.ToUpper().Contains(Keysearch.ToUpper()));
            }
            if (donViId > 0)
            {
                query = query.Where(c => c.DON_VI_ID == donViId);
            }
            query = query.OrderBy(c => c.MA);
            return new PagedList<DoiTac>(query, pageIndex, pageSize); ;
        }

        public virtual DoiTac GetDoiTacById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<DoiTac> GetDoiTacByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertDoiTac(DoiTac entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateDoiTac(DoiTac entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteDoiTac(DoiTac entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public virtual DoiTac GetDoiTacByMa(string Ma)
        {
            var query = _itemRepository.Table.Where(c => c.MA == Ma);
            return query.FirstOrDefault();
        }
        public virtual DoiTac GetDoiTac(string Ma = null, string Ten = null, decimal? donViBoPhanId = null,decimal? loaiDoiTac = null)
        {
            var query = _itemRepository.Table;
            if (!string.IsNullOrEmpty(Ma))
                query = query.Where(c => c.MA == Ma);
            if (!string.IsNullOrEmpty(Ten))
                query = query.Where(c => c.TEN == Ten);
            if (donViBoPhanId.GetValueOrDefault(0) > 0)
                query = _itemRepository.Table.Where(c => c.DON_VI_BO_PHAN_ID == donViBoPhanId);
            if (loaiDoiTac.GetValueOrDefault(0) > 0)
                query = _itemRepository.Table.Where(c => c.LOAI_DOI_TAC_ID == loaiDoiTac);
            return query.FirstOrDefault();
        }
        #endregion
        #region Read only
        public virtual DoiTac GetReadOnlyDoiTac(string Ma = null, string Ten = null, decimal? donViBoPhanId = null, decimal? loaiDoiTac = null)
        {
            var query = _itemRepository.TableNoTracking;
            if (!string.IsNullOrEmpty(Ma))
                query = query.Where(c => c.MA == Ma);
            if (!string.IsNullOrEmpty(Ten))
                query = query.Where(c => c.TEN == Ten);
            if (donViBoPhanId.GetValueOrDefault(0) > 0)
                query = _itemRepository.Table.Where(c => c.DON_VI_BO_PHAN_ID == donViBoPhanId);
            if (loaiDoiTac.GetValueOrDefault(0) > 0)
                query = _itemRepository.Table.Where(c => c.LOAI_DOI_TAC_ID == loaiDoiTac);
            return query.FirstOrDefault();
        }
        #endregion

    }
}

