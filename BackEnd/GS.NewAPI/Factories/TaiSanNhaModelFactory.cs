using GS.Core.Domain.TaiSans;
using GS.Services.TaiSans;
using System.Collections.Generic;

namespace GS.NewAPI.Factories
{
    public class TaiSanNhaModelFactory : ITaiSanNhaModelFactory
    {
        private readonly ITaiSanNhaService _taiSanNhaService;
        public TaiSanNhaModelFactory(ITaiSanNhaService taiSanNhaService)
        {
            _taiSanNhaService = taiSanNhaService;
        }
        public IList<TaiSanNha> GetTaiSanNhasByDatId(decimal taiSanDatId)
        {
            return _taiSanNhaService.GetTaiSanNhaByDatId(taiSanDatId);
        }
    }
}
