using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.ConsumingApi.KhaiThacTaiSan
{

    public class Kho_KhaiThacTaiSan
    {
        public Kho_KhaiThacTaiSan()
        {
            this.assets = new List<assets>();
            this.decision = new decision();
        }
        public decision decision { get; set; }
        public List<assets> assets { get; set; }
    }
    public class decision
    {
        /// <summary>
        /// ID đơn vị nhập liệu
        /// </summary>
        public long? unitId { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        /// <summary>
        /// Người quyết định
        /// </summary>
        public string approver { get; set; }
        /// <summary>
        /// Hợp đồng liên quan
        /// </summary>
        public string contractNumber { get; set; }
        public string contractDate { get; set; }
        public long? contractValue { get; set; }
        public string contractContent { get; set; }
        /// <summary>
        /// Đối tác, đơn vị  thuê
        /// </summary>
        public string partner { get; set; }
        public string notes { get; set; }
        public string decisionNumber { get; set; }
        public string decisionDate { get; set; }
        /// <summary>
        /// Hình thức khai thác. 1: Cho thuê. 2: Tham gia kinh doanh. 3: Mang liên doanh, liên kết
        /// </summary>
        public string syncSourceId { get; set; }

    }
    public class assets
    {
        public string assetCode { get; set; }
        public Double? exploitingArea { get; set; }
        public long? expectedValue { get; set; }
    }
    public enum enumKho_HinhThucKhaiThac
    {
       ChoThue=1,
       KinhDoanh =2,
       LdLk = 3
    }
}
