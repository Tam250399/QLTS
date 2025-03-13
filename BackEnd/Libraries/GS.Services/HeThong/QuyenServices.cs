//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.CauHinh;
//
using GS.Core.Domain.HeThong;
using GS.Data;
using GS.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.HeThong
{
    public partial class QuyenService : IQuyenService
    {
        #region Fields
        private readonly CauHinhNguoiDung _customerSettings;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<Quyen> _itemRepository;
        private readonly IWorkContext _workContext;
        private readonly IRepository<QuyenVaiTroMapping> _quyenVaiTroMappingRepository;
        private readonly IRepository<VaiTroNguoiDungMapping> _vaiTroNguoiDungMappingRepository;
        #endregion

        #region Ctor

        public QuyenService(CauHinhNguoiDung customerSettings,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            //IEventPublisher eventPublisher,
            IStaticCacheManager staticCacheManager,
            IRepository<Quyen> itemRepository,
            IWorkContext workContext,
            IRepository<QuyenVaiTroMapping> quyenVaiTroService,
            IRepository<VaiTroNguoiDungMapping> vaiTroNguoiDungMappingRepository
            )
        {
            this._vaiTroNguoiDungMappingRepository = vaiTroNguoiDungMappingRepository;
            this._quyenVaiTroMappingRepository = quyenVaiTroService;
            this._workContext = workContext;
            this._customerSettings = customerSettings;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            //this._eventPublisher = eventPublisher;   
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
        }

        #endregion

        public virtual IList<Quyen> GetQuyenByVaiTroId(decimal vaiTro)
        {
            var key = string.Format(GSSecurityDefaults.PermissionsAllByCustomerRoleIdCacheKey, vaiTro);
            return _cacheManager.Get(key, () =>
            {
                var query = from q in _itemRepository.Table
                            join qvt in _quyenVaiTroMappingRepository.Table on q.ID equals qvt.QUYEN_ID
                            where qvt.VAI_TRO_ID == vaiTro
                            orderby q.ID
                            select q;
                return query.ToList();
            });

        }

        #region Methods
        public virtual IList<Quyen> GetAllQuyens()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<Quyen> SearchQuyens(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, IList<int> ListQuyenDaChon = null, decimal idVaiTro = 0)
        {
            var query = _itemRepository.Table;
            if (!string.IsNullOrEmpty(Keysearch))
            {
                query = query.Where(c => c.TEN.ToUpper().Contains(Keysearch.ToUpper()) 
                                        || c.MA.ToUpper().Contains(Keysearch.ToUpper()) 
                                        || c.PHAN_HE.ToUpper().Contains(Keysearch.ToUpper()));
            }
            if (ListQuyenDaChon.Count > 0)
            {
                query = query.Where(m => !ListQuyenDaChon.Contains(Convert.ToInt32(m.ID)));
            }

            return new PagedList<Quyen>(query, pageIndex, pageSize); ;
        }

        public virtual Quyen GetQuyenById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }


        public virtual Quyen GetQuyenByTen(string ten)
        {
            if (string.IsNullOrWhiteSpace(ten))
                return null;

            var query = from pr in _itemRepository.Table
                        where pr.TEN == ten
                        orderby pr.ID
                        select pr;

            var permissionRecord = query.FirstOrDefault();
            return permissionRecord;
        }
        public virtual void InsertQuyen(Quyen entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateQuyen(Quyen entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteQuyen(Quyen entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public virtual bool Authorize(string tenQuyen, NguoiDung nguoiDung)
        {
            if (string.IsNullOrEmpty(tenQuyen))
                return false;

            var customerRoles = _vaiTroNguoiDungMappingRepository.Table.Where(c => c.NGUOI_DUNG_ID == nguoiDung.ID).Select(c => c.VAI_TRO_ID);
            foreach (var role in customerRoles)
                if (Authorize(tenQuyen, role))
                    //yes, we have such permission
                    return true;

            //no permission found
            return false;
        }
        public virtual bool Authorize(Quyen quyen)
      {
            if (_workContext.CurrentCustomer == null)
            {
                return false;
            }
            if (_workContext.CurrentCustomer.IS_QUAN_TRI)
                return true;
            else
                return Authorize(quyen, _workContext.CurrentCustomer);
        }
        public virtual bool Authorize(Quyen quyen, NguoiDung nguoiDung)
        {
            if (quyen == null)
                return false;

            if (nguoiDung == null)
                return false;

            return Authorize(quyen.MA, nguoiDung);
        }
        public virtual bool Authorize(string permissionRecordSystemName)
        {
            return Authorize(permissionRecordSystemName, _workContext.CurrentCustomer);
        }
        protected virtual bool Authorize(string tenQuyen, decimal vaiTroId)
        {
            if (string.IsNullOrEmpty(tenQuyen))
                return false;
            var key = string.Format(GSSecurityDefaults.PermissionsAllowedCacheKey, vaiTroId, tenQuyen);
            return _staticCacheManager.Get(key, () =>
            {
                var permissions = GetQuyenByVaiTroId(vaiTroId);
                foreach (var permission1 in permissions)
                    if (permission1.MA.Equals(tenQuyen, StringComparison.InvariantCultureIgnoreCase))
                        return true;

                return false;
            });
        }
        public bool KiemTraTrungMa(string Ma, decimal Id)
        {
            return _itemRepository.Table.Any(c => c.MA == Ma && c.ID != Id);
        }
        #endregion
    }
}

