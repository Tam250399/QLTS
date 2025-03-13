using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.HeThong;
using GS.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.HeThong
{
    public partial class VaiTroNguoiDungService : IVaiTroNguoiDungService
    {
        #region Fields
        private readonly IRepository<VaiTroNguoiDungMapping> _itemRepository;
        private readonly IStaticCacheManager _staticCacheManager;
        #endregion

        #region Ctor
        public VaiTroNguoiDungService(IRepository<VaiTroNguoiDungMapping> itemRepository,
            IStaticCacheManager staticCacheManager)
        {
            this._itemRepository = itemRepository;
            this._staticCacheManager = staticCacheManager;
        }
        #endregion

        public virtual void InsertVaiTroNguoiDung(VaiTroNguoiDungMapping entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            _staticCacheManager.RemoveByPattern(GSSecurityDefaults.PermissionsPatternCacheKey);
        }
        public virtual void DeleteVaiTroNguoiDung(VaiTroNguoiDungMapping entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            _staticCacheManager.RemoveByPattern(GSSecurityDefaults.PermissionsPatternCacheKey);
        }
        public virtual void DeleteVaiTroNguoiDung(List<VaiTroNguoiDungMapping> entity)
        {
            if (entity == null || (entity != null && entity.Count() == 0))
                throw new ArgumentNullException(nameof(VaiTroNguoiDungMapping));
            _itemRepository.Delete(entity);
            _staticCacheManager.RemoveByPattern(GSSecurityDefaults.PermissionsPatternCacheKey);
        }
        public virtual void DeleteVaiTroNguoiDung(decimal vaiTroId, decimal nguoiDungId)
        {
            var query = _itemRepository.Table.Where(c => c.VAI_TRO_ID == vaiTroId && c.NGUOI_DUNG_ID == nguoiDungId);

            if (query == null)
                throw new ArgumentNullException(nameof(query));
            _itemRepository.Delete(query);
            _staticCacheManager.RemoveByPattern(GSSecurityDefaults.PermissionsPatternCacheKey);
        }
        public virtual IList<VaiTroNguoiDungMapping> GetMapByNguoiDungId(decimal nguoiDungId)
        {
            var query = _itemRepository.Table.Where(c => c.NGUOI_DUNG_ID == nguoiDungId);
            return query.ToList();
        }
        public virtual IList<VaiTroNguoiDungMapping> GetMapByVaiTroId(decimal VaiTroId)
        {
            var query = _itemRepository.Table.Where(c => c.VAI_TRO_ID == VaiTroId);
            return query.ToList();
        }
        public virtual IList<VaiTroNguoiDungMapping> GetMapByMaVaiTro(string MaVaiTro)
        {
            var query = _itemRepository.Table.Where(c => c.vaiTro.MA == MaVaiTro);
            return query.ToList();
        }
        public virtual bool KiemTraDaChon(decimal VaiTroId, decimal NguoiDungId)
        {
            return _itemRepository.Table.Any(c => c.VAI_TRO_ID == VaiTroId && c.NGUOI_DUNG_ID == NguoiDungId);
        }

    }
}
