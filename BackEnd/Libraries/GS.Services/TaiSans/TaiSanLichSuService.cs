using GS.Core.Data;
using GS.Core.Domain.TaiSans;
using System;

namespace GS.Services.TaiSans
{
    public class TaiSanLichSuService : ITaiSanLichSuService
    {
        private readonly IRepository<TaiSanLichSu> _itemRepository;
        public TaiSanLichSuService(IRepository<TaiSanLichSu> itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public void InsertTaiSanLichSu(TaiSanLichSu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
        }
    }
}
