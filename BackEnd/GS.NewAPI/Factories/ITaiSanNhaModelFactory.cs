using GS.Core.Domain.TaiSans;
using System.Collections.Generic;

namespace GS.NewAPI.Factories
{
    public interface ITaiSanNhaModelFactory
    {
         IList<TaiSanNha> GetTaiSanNhasByDatId(decimal taiSanDatId);

    }
}
