namespace GS.Core.Domain.Common
{
    /// <summary>
    /// Represents a wrapper class for decimal values that used as a query type
    /// </summary>
    public partial class DecimalQueryType
    {
        /// <summary>
        /// Gets or sets a value
        /// </summary>
        public decimal? Value { get; set; }
    }
    public partial class DecimalViewQueryType: BaseViewEntity
    {
        /// <summary>
        /// Gets or sets a value
        /// </summary>
        public decimal? Value { get; set; }
    }
}