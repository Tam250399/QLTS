using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.BaoCaos
{
    public partial class ThongTinBaoCaoModel : BaseGSEntityModel
    {
        public ThongTinBaoCaoModel()
        {
            ListColumeHeader = new List<ThongTinBaoCaoColumeModel>();
            ListValue = new List<ThongTinBaoCaoValueModel>();
        }
        public bool hasColumnIndex { get; set; }
        public bool hasRowIndex { get; set; }
        public ThongTinBaoCaoHeaderModel ThongTinHeader { get; set; }
        public List<ThongTinBaoCaoColumeModel> ListColumeHeader { get; set; }
        public List<ThongTinBaoCaoValueModel> ListValue { get; set; }
    }
    public partial class ThongTinBaoCaoHeaderModel
    {
        public string HeaderLeft { get; set; }
        public string HeaderCenter { get; set; }
        public string HeaderRight { get; set; }
    }
    public partial class InputHeaderReportModel
    {
        public InputHeaderReportModel()
        {
            ListSubModel = new List<InputHeaderReportModel>();
        }
        public int TypeLastRowId { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public List<InputHeaderReportModel> ListSubModel { get; set; }
    }
    public partial class ThongTinBaoCaoColumeModel
    {
        public string ColumeHeaderName { get; set; }
        public int ColumeHeaderColspan { get; set; }
        public int ColumeHeaderRowspan { get; set; }
        public int ColumeHeaderIndex { get; set; }
        public int ColumeHeaderLevel { get; set; }
    }
    public partial class ThongTinBaoCaoValueModel : BaseGSEntityModel
    {
        public ThongTinBaoCaoValueModel()
        {
            ValueContent = new List<string>();
        }
        public List<String> ValueContent { get; set; }
    }
    public partial class LayoutReportModel : BaseGSEntityModel
    {
        public LayoutReportModel()
        {
            ListSubLayout = new List<LayoutReportModel>();
        }
        public int TypeLastRowId { get; set; }
        public string ColumeHeaderName { get; set; }
        public int ColumeHeaderColspan { get; set; }
        public int ColumeHeaderRowspan { get; set; }
        public int ColumeHeaderIndex { get; set; }
        public int ColumeHeaderLevel { get; set; }
        public List<LayoutReportModel> ListSubLayout { get; set; }
    }
}
