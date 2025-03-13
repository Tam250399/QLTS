using GS.Core.Domain.BaoCaos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GS.Services.ExportImport
{
    public partial interface IExportManager
    {
        void ExportXlsxReportSoTL_AHS_PT(Stream stream, LayoutReport layoutReport, string workSheetName);
        void ExportXlsxEntity(Stream stream, LayoutReport layoutReport, string workSheetName, bool addSTT);
    }
}
