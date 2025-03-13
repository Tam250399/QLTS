using System.ComponentModel.DataAnnotations.Schema;

namespace GS.Core
{
    /// <summary>
    /// Base class for entities
    /// </summary>
    public abstract partial class BaseEntity
    {

        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public decimal ID { get; set; }
    }
    /// <summary>
    /// Cac entity base tren ViewEntity se ko can mapping trong csdl
    /// </summary>
    public abstract partial class BaseViewEntity
    {

    }
    public abstract partial class BaseCacheEntity
    {
        public decimal ID { get; set; }
    }
}
