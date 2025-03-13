
namespace GS.Core.Domain.HeThong
{
    /// <summary>
    /// Represents a setting
    /// </summary>
    public partial class CauHinh : BaseEntity
    {
        public CauHinh()
        {
        }


        public CauHinh(string ten, string giatri)
        {
            this.TEN = ten;
            this.GIA_TRI = giatri;
        }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string TEN { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public string GIA_TRI { get; set; }

        public decimal DON_VI_ID { get; set; }

        /// <summary>
        /// To string
        /// </summary>
        /// <returns>Result</returns>
        public override string ToString()
        {
            return TEN;
        }
    }
}