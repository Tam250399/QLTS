//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/3/2020
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
using GS.Core.Domain.DanhMuc;

namespace GS.Services.DanhMuc
{
    public partial class DiaBanTestService : IDiaBanTestService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<DiaBanTest> _itemRepository;
        #endregion

        #region Ctor

        public DiaBanTestService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<DiaBanTest> itemRepository
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
        public virtual IList<DiaBanTest> GetAllDiaBanTests() {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<DiaBanTest> SearchDiaBanTests(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? PARENTID = null)
        {
            var queryResults = _itemRepository.Table;
            Console.WriteLine("ParentId: " + PARENTID);
            if (!String.IsNullOrEmpty(Keysearch))
            {
                queryResults = _itemRepository.Table.Where(x => x.MA.ToLower().Contains(Keysearch.ToLower()) || x.TEN.ToLower().Contains(Keysearch.ToLower()) || x.QuocGia.TEN.ToLower().Contains(Keysearch.ToLower()));
            }
            else if(PARENTID != 0)
            {
                queryResults = queryResults.Where(x => x.PARENT_ID == PARENTID);
            }
            else
            {
                queryResults = queryResults.Where(x => x.PARENT_ID == null);

            }
            return new PagedList<DiaBanTest>(queryResults, pageIndex, pageSize);
        }
        
        public virtual IList<DiaBanTest> GetDiaBanForInputSearch(string name)
        {
            var query = _itemRepository.Table;
            if (!String.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.TEN.ToLower().Contains(name.ToLower())).Take(10);
            }
            if (String.IsNullOrEmpty(name))
            {
                query = query.Take(10);
            }
            return query.ToList();
        }

        public virtual DiaBanTest GetDiaBanTestByMa(string Ma)
        {
            if (String.IsNullOrEmpty(Ma))
            {
                return null;
            }
            else
            {
                return _itemRepository.Table.Where(x => x.MA == Ma).FirstOrDefault();
            }
        }
        
        public virtual DiaBanTest GetDiaBanTestById(decimal Id){
              if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
         }
         
        public virtual IList<DiaBanTest> GetDiaBanTestByIds(decimal[] Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }            
        
        public virtual void InsertDiaBanTest(DiaBanTest entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            UpdateTreeNode(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateDiaBanTest(DiaBanTest entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            UpdateTreeNode(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteDiaBanTest(DiaBanTest entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }

        public void UpdateTreeNode(DiaBanTest diaBanTest)
        {
            var node = diaBanTest.ID.toCode(8);
            diaBanTest.TREE_NODE = node;
            if (diaBanTest.PARENT_ID > 0)
            {
                var parentDiaBanTest = GetDiaBanTestById((decimal)diaBanTest.PARENT_ID);
                diaBanTest.TREE_NODE = parentDiaBanTest.TREE_NODE + "-" + diaBanTest.TREE_NODE;
                diaBanTest.TREE_LEVEL = (parentDiaBanTest.TREE_LEVEL.ToNumberInt32() + 1).ToString();
            }
            else
            {
                diaBanTest.TREE_LEVEL = "0";
            }
        }
        
        #endregion	
     }
}		

