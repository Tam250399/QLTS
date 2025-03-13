//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 11/2/2020
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
using GS.Core.Domain.CCDC;
using GS.Core.Domain.Common;
using Oracle.ManagedDataAccess.Client;

namespace GS.Services.CCDC
{
    public partial class KiemKeService : IKiemKeService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<KiemKe> _itemRepository;
        private readonly IWorkContext _workContext;
        private readonly IRepository<KiemKeHoiDong> _kiemKeHoiDongRepository;
        private readonly IRepository<KiemKeCongCu> _kiemKeCongCuRepository;
        private readonly IKiemKeCongCuService _kiemKeCongCuService;
        private readonly IKiemKeHoiDongService _kiemKeHoiDongService;
        #endregion

        #region Ctor

        public KiemKeService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<KiemKe> itemRepository,
            IWorkContext workContext,
            IRepository<KiemKeHoiDong> kiemKeHoiDongRepository,
            IRepository<KiemKeCongCu> kiemKeCongCuRepository,
            IKiemKeCongCuService kiemKeCongCuService,
            IKiemKeHoiDongService kiemKeHoiDongService
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._workContext = workContext;
            this._kiemKeHoiDongRepository = kiemKeHoiDongRepository;
            this._kiemKeCongCuRepository = kiemKeCongCuRepository;
            this._kiemKeCongCuService = kiemKeCongCuService;
            this._kiemKeHoiDongService = kiemKeHoiDongService;
        }

        #endregion

        #region Methods
        public virtual IList<KiemKe> GetAllKiemKes()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<KiemKe> SearchKiemKes(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, Decimal DonViId = 0, DateTime? TuNgay = null, DateTime? DenNgay = null, Decimal BoPhanId = 0)
        {
            var query = _itemRepository.Table.Where(c => c.DON_VI_ID == DonViId);
            if (Keysearch != null)
            {
                query = query.Where(c => c.SO_KIEM_KE.Contains(Keysearch));
            }
            if (TuNgay != null)
            {
                query = query.Where(c => c.NGAY_KIEM_KE >= TuNgay);
            }
            if (DenNgay != null)
            {
                query = query.Where(c => c.NGAY_KIEM_KE <= DenNgay);
            }
            if (BoPhanId > 0)
            {
                query = query.Where(c => c.DON_VI_BO_PHAN_ID == BoPhanId);
            }
            return new PagedList<KiemKe>(query.OrderByDescending(c => c.NGAY_KIEM_KE).ThenByDescending(c => c.SO_KIEM_KE), pageIndex, pageSize); ;
        }

        public virtual KiemKe GetKiemKeById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<KiemKe> GetKiemKeByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual KiemKe GetKiemKeIdMax(decimal DonViId)
        {
            //var query = _itemRepository.Table.OrderByDescending(c => c.ID).Select(c => c.ID);
            //return query.FirstOrDefault();
            return _itemRepository.Table.Where(c=>c.DON_VI_ID == DonViId).OrderByDescending(c => c.ID).FirstOrDefault();
        }

        public virtual void InsertKiemKe(KiemKe entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.NGAY_TAO = DateTime.Now;
            entity.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
            _itemRepository.Insert(entity);
        }

        public virtual void UpdateKiemKe(KiemKe entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteKiemKe(KiemKe entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            // delete hoidong
            var listHoiDong = _kiemKeHoiDongRepository.Table.Where(c => c.KIEM_KE_ID == entity.ID);
            foreach (var hoidong in listHoiDong)
            {
                _kiemKeHoiDongService.DeleteKiemKeHoiDong(hoidong);
            }
            // delete congcu
            var listCongCu = _kiemKeCongCuRepository.Table.Where(c => c.KIEM_KE_ID == entity.ID);
            foreach (var congcu in listCongCu)
            {
                _kiemKeCongCuService.DeleteKiemKeCongCu(congcu);
            }
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        //
        public MessageReturn DelelteKiemKeCCDCByStore(decimal KIEMKE_ID)
        {
            try
            {
                OracleParameter P_KIEMKE_ID = new OracleParameter("P_KIEMKE_ID", OracleDbType.Decimal, KIEMKE_ID, ParameterDirection.Input);
                var result = _dbContext.ExecuteSqlCommand("BEGIN DELETE_KIEMKE_CCDC( :P_KIEMKE_ID); END;", false, null, P_KIEMKE_ID);
                return MessageReturn.CreateSuccessMessage("Success done");

            }
            catch (Exception ex)
            {
                //DelelteTaiSanDB(ma_db);
                return MessageReturn.CreateErrorMessage(ex.Message);
            }

        }
        #endregion
    }
}

