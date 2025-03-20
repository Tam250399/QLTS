using GS.Core.Domain.TaiSans;
using GS.Services.TaiSans;

namespace GS.NewAPI.Factories
{
    public class TaiSanDatModelFactory : ITaiSanDatModelFactory
    {
        private readonly ITaiSanDatService _taiSanDatService;
        public TaiSanDatModelFactory(ITaiSanDatService taiSanDatService) 
        {
            _taiSanDatService = taiSanDatService;
        }
        public void InsertTaiSanDat(TaiSanDat entity)
        {
            _taiSanDatService.InsertTaiSanDat(entity);
        }
    }
}
