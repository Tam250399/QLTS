//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 20/5/2020
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
using GS.Services.HeThong;
using System.Collections.Immutable;

namespace GS.Services.HeThong
{
    public partial class WidgetService:IWidgetService
	{				
         #region Fields
    		private readonly CauHinhChung _cauhinhChung;
            private readonly ICacheManager _cacheManager;
            private readonly IDataProvider _dataProvider;
            private readonly IDbContext _dbContext;           
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IRepository<Widget> _itemRepository;
            private readonly IRepository<NguoiDung> _nguoiDungRepository;
            private readonly IRepository<VaiTroWidget> _vaiTroWidgetRepository;

         #endregion

        #region Ctor

        public WidgetService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,            
            IStaticCacheManager staticCacheManager,            
            IRepository<Widget> itemRepository,
            IRepository<NguoiDung> nguoiDungRepository,
            IRepository<VaiTroWidget> vaiTroWidgetRepository
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;    
            this._itemRepository = itemRepository;
            this._nguoiDungRepository = nguoiDungRepository;
            this._vaiTroWidgetRepository = vaiTroWidgetRepository;
        }

        #endregion
        
        #region Methods
        public virtual IList<Widget> GetAllWidgets(){
            var query = _itemRepository.Table;
             return query.ToList();
        }

        public bool AuthorizeWidget(decimal nguoiDungId, string partialViewname)
        {
            var qNguoiDung = _nguoiDungRepository.GetById(nguoiDungId);
            var listVaiTroNguoiDung = qNguoiDung.VaiTro.Select(c => c.ID).ToList();

            if (string.IsNullOrEmpty(partialViewname)) {
                return false;
            }
            else
            {
                var widget = _itemRepository.Table.Where(c => c.PARTIAL_VIEW_NAME == partialViewname).FirstOrDefault();
                var listVaiTroWidget = _vaiTroWidgetRepository.Table.Where(c => c.WIDGET_ID == widget.ID).Select(c => c.VAI_TRO_ID).ToList();
                foreach(var vaitro in listVaiTroNguoiDung)
                {
                    if (listVaiTroWidget.Contains(vaitro))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public virtual IPagedList <Widget> SearchWidgets(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null){
             var query = _itemRepository.Table;
             return new PagedList<Widget>(query, pageIndex, pageSize);
         }    
        
        public virtual Widget GetWidgetById(decimal Id){
              if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
         }

        public virtual Widget GetWidgetByPartialViewName(string partialViewName)
        {
            return _itemRepository.Table.Where(c => c.PARTIAL_VIEW_NAME == partialViewName).FirstOrDefault();
        }
         
        public virtual IList<Widget> GetWidgetByIds(decimal[] Ids){
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }            
        
        public virtual void InsertWidget(Widget entity){
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);
            
        }
        public virtual void UpdateWidget(Widget entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteWidget(Widget entity){
             if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        } 


        #endregion	
     }
}		

