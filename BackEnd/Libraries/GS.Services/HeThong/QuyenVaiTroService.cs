using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.HeThong;
using GS.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.HeThong
{
    public partial class QuyenVaiTroService : IQuyenVaiTroService
    {
        #region Fields
        private readonly IRepository<QuyenVaiTroMapping> _itemRepository;
        private readonly IRepository<Quyen> _quyenRepository;
        private readonly IStaticCacheManager _staticCacheManager;
        #endregion

        #region Ctor
        public QuyenVaiTroService(IRepository<QuyenVaiTroMapping> itemRepository, IStaticCacheManager staticCacheManager, IRepository<Quyen> quyenRepository)
        {
            this._itemRepository = itemRepository;
            this._quyenRepository = quyenRepository;
            this._staticCacheManager = staticCacheManager;
        }
        #endregion

        #region Methods
        public virtual void DeleteQuyenVaiTro(QuyenVaiTroMapping entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
			_staticCacheManager.RemoveByPattern(GSSecurityDefaults.PermissionsPatternCacheKey);
		}

        public virtual IList<QuyenVaiTroMapping> GetMapByVaiTroId(decimal vaiTroId)
        {
            var query = _itemRepository.Table.Where(c => c.VAI_TRO_ID == vaiTroId);
            return query.ToList();
        }
        public virtual void DeleteQuyenVaiTroId(decimal vaiTroId, decimal quyenId)
        {
            var query = _itemRepository.Table.Where(c => c.VAI_TRO_ID == vaiTroId && c.QUYEN_ID == quyenId);

            if (query == null)
                throw new ArgumentNullException(nameof(query));
            var entity = query.FirstOrDefault();
            _itemRepository.Delete(query);
            //clear Cache
			_staticCacheManager.RemoveByPattern(GSSecurityDefaults.PermissionsPatternCacheKey);
		}
        public virtual void InsertQuyenVaiTro(QuyenVaiTroMapping entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
			_staticCacheManager.RemoveByPattern(GSSecurityDefaults.PermissionsPatternCacheKey);
		}
        #endregion
    }
}
