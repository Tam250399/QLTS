using System;
using System.Collections.Generic;
using System.Text;
using GS.Core;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.TaiSans;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace GS.Services.TaiSans
{
    public partial interface ITaiSanImportService
    {
        void InsertTaiSan(ImportExcelTaiSan entity);
        void UpdateTaiSan(ImportExcelTaiSan entity);
        void DeleteTaiSan(ImportExcelTaiSan entity);
        IList<ImportExcelTaiSan> GetAllTaiSans(int LoaiTaiSan = 0, string BoPhanSuDung = null, string donViMa = null);
        ImportExcelTaiSan GetTaiSanById(decimal Id);
        IList<ImportExcelTaiSan> GetTaiSanByDonViMa(string donViMa);
    }
}
