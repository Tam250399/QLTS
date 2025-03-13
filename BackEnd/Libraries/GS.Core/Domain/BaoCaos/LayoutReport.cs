using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos
{
    public enum TypeLastRow
    {
        None = 0,
        /// <summary>
        /// lấy tổng
        /// </summary>
        Sum = 1,
        /// <summary>
        /// lấy trung bình
        /// </summary>
        Avg = 2,
        /// <summary>
        /// lấy giá trị nhỏ nhất
        /// </summary>
        Min = 3,
        /// <summary>
        /// lấy giá trị lớn nhất
        /// </summary>
        Max = 4
    }
    public partial class LayoutReport : BaseEntity
    {
        public LayoutReport()
        {
            ListColumeHeader = new List<ThongTinBaoCaoColume>();
            ListvalueContent = new List<ThongTinBaoCaoValue>();
        }
        public bool hasColumnIndex { get; set; }
        public bool hasRowIndex { get; set; }
        public int MaxLevel { get; set; }
        public ThongTinBaoCaoHeader ThongTinHeader { get; set; }
        public List<ThongTinBaoCaoValue> ListvalueContent { get; set; }
        public List<ThongTinBaoCaoColume> ListColumeHeader { get; set; }
    }
    public partial class ThongTinBaoCaoHeader
    {
        public string HeaderLeft { get; set; }
        public string HeaderCenter { get; set; }
        public string HeaderRight { get; set; }
    }
    public partial class ThongTinBaoCaoColume : BaseEntity
    {
        public ThongTinBaoCaoColume()
        {
            ListSubLayout = new List<ThongTinBaoCaoColume>();
        }
        public int TypeLastRowId { get; set; }
        public string ColumeHeaderName { get; set; }
        public int ColumeHeaderColspan { get; set; }
        public int ColumeHeaderRowspan { get; set; }
        public int ColumeHeaderIndex { get; set; }
        public int ColumeHeaderLevel { get; set; }
        public List<ThongTinBaoCaoColume> ListSubLayout { get; set; }
        public double ColumeHeaderWidthValue { get; set; }
    }
    public partial class ThongTinBaoCaoValue : BaseEntity
    {
        public ThongTinBaoCaoValue()
        {
            ValueContent = new List<string>();
        }
        public List<String> ValueContent { get; set; }
    }
    public partial class ThongTinHeightColume
    {
        public int RowIndex { get; set; }
        public Double RowHeight { get; set; }
    }
}
