//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
//
using GS.Core.Domain.HeThong;
using GS.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.HeThong
{
    public partial class NhanHienThiService : INhanHienThiService
    {
        const string NhanHienThiCaheKeyItem = "qltsc.nhanhienthi.{0}";
        const string NhanHienThiCacheKey = "gs.lsr.{0}";
        const string NhanHienThiPatternCacheKey = "gs.lsr.";
        const string EnumNhanHienThiPrefix = "Enums.";
        #region Fields
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        //private readonly IEventPublisher _eventPublisher;    
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<NhanHienThi> _itemRepository;
        #endregion

        #region Ctor

        public NhanHienThiService(
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            //IEventPublisher eventPublisher,
            IStaticCacheManager staticCacheManager,
            IRepository<NhanHienThi> itemRepository
            )
        {
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            //this._eventPublisher = eventPublisher;   
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
        }

        #endregion

        #region Methods
        public virtual IList<NhanHienThi> GetAllNhanHienThis()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<NhanHienThi> SearchNhanHienThis(int pageIndex = 0, int pageSize = int.MaxValue, string Ma = null, string giaTri = null)
        {
            var query = _itemRepository.Table;
            if (!string.IsNullOrEmpty(Ma))
                query = query.Where(c => c.MA.Contains(Ma.ToLower()));
            if (!string.IsNullOrEmpty(giaTri))
                query = query.Where(c => c.GIA_TRI.ToUpper().Contains(giaTri.ToUpper()));
            return new PagedList<NhanHienThi>(query, pageIndex, pageSize); ;
        }

        public virtual NhanHienThi GetNhanHienThiById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _staticCacheManager.Get(String.Format(NhanHienThiCaheKeyItem, Id), () =>
            {
                return _itemRepository.GetById(Id);
            });
        }

        public virtual NhanHienThi GetNhanHienThiByMa(string Ma)
        {
            Ma = Ma.ToLower();

            return _staticCacheManager.Get(String.Format(NhanHienThiCaheKeyItem, Ma), () =>
            {
                return _itemRepository.Table.Where(c => c.MA.Equals(Ma)).FirstOrDefault();
            });
        }
        public virtual string GetGiaTri(string Ma)
        {
            Ma = Ma.ToLower();
            var key = string.Format(NhanHienThiCacheKey, Ma);
            var lsr = _cacheManager.Get(key, () =>
            {
                var query = from l in _itemRepository.Table
                            where l.MA == Ma
                            select l.GIA_TRI;
                return query.FirstOrDefault();
            });

            if (lsr != null)
                return lsr;
            return Ma;
        }
        public virtual string GetGiaTriEnum<TEnum>(TEnum enumValue) where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            //localized value
            var resourceName = $"{EnumNhanHienThiPrefix}{typeof(TEnum)}.{enumValue}";
            var result = GetGiaTri(resourceName);

            //set default value if required
            if (string.IsNullOrEmpty(result))
                result = CommonHelper.ConvertEnum(enumValue.ToString());

            return result;
        }
        public virtual string GetListGiaTriEnum1<TEnum>(List<TEnum> enumValue) where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("T must be an enumerated type");
            string result = "";
            if (enumValue.Count > 0) {
                foreach (var a in enumValue)
                {
                    var resourceNameEnum = $"{EnumNhanHienThiPrefix}{typeof(TEnum)}.{a}";
                    if(result == "") {
                        result = GetGiaTri(resourceNameEnum);
                    }else
                    result = result  + "; "+ GetGiaTri(resourceNameEnum);
                }
            }

            //localized value
            //var resourceName = $"{EnumNhanHienThiPrefix}{typeof(TEnum)}.{enumValue}";
            //var result = GetGiaTri(resourceName);

            //set default value if required
            if (string.IsNullOrEmpty(result))
                result = CommonHelper.ConvertEnum(enumValue.ToString());

            return result;
        }
        public virtual void InsertNhanHienThi(NhanHienThi entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            _cacheManager.RemoveByPattern(NhanHienThiPatternCacheKey);


        }
        public virtual void UpdateNhanHienThi(NhanHienThi entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            _cacheManager.RemoveByPattern(NhanHienThiPatternCacheKey);

        }
        public virtual void DeleteNhanHienThi(NhanHienThi entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            _cacheManager.RemoveByPattern(NhanHienThiPatternCacheKey);

        }

        #endregion
    }
}

