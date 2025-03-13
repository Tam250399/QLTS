
using System;

namespace GS.Web.Framework.Models
{
    /// <summary>
    /// Represents base nopCommerce entity model
    /// </summary>
    public partial class BaseGSEntityModel : BaseGSModel
    {
        /// <summary>
        /// Gets or sets model identifier
        /// </summary>
        public virtual decimal ID { get; set; }
    }
    public partial class BaseGSApiModel 
    {
        /// <summary>
        /// Gets or sets model identifier
        /// </summary>
        public virtual Int64 ID { get; set; }
    }
}