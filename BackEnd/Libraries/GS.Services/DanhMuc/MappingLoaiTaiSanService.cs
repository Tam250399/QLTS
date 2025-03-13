using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GS.Core.Data;
using GS.Core.Domain.DanhMuc;

namespace GS.Services.DanhMuc
{
    public partial class MappingLoaiTaiSanService : IMappingLoaiTaiSanService
    {
        #region Fields
        private readonly IRepository<MappingLoaiTaiSan> _itemRepository;
        #endregion

        #region Ctor

        public MappingLoaiTaiSanService(
            IRepository<MappingLoaiTaiSan> itemRepository
            )
        {
            this._itemRepository = itemRepository;
        }

        #endregion
       public virtual MappingLoaiTaiSan GetMappingLoaiTaiSanById(decimal OldLoaiTaiSanId=0, decimal NewLoaiTaiSanId = 0)
        {
            if (OldLoaiTaiSanId == 0 && NewLoaiTaiSanId == 0)
                return null;
            var query = _itemRepository.Table;
            if(OldLoaiTaiSanId>0)
            {
                query = query.Where(m => m.OLD_LOAI_TAI_SAN_ID == OldLoaiTaiSanId);
            }
            if (NewLoaiTaiSanId > 0)
            {
                query = query.Where(m => m.OLD_LOAI_TAI_SAN_ID == NewLoaiTaiSanId);
            }
            return query.FirstOrDefault();
        }
    }
}
