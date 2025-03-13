using GS.Core.Domain.HeThong;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Data.Mapping.Configuration
{
    /// <summary>
    /// Represents a setting mapping configuration
    /// </summary>
    public partial class CauHinhMap : GSEntityTypeConfiguration<CauHinh>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<CauHinh> builder)
        {
            builder.ToTable("QL_CAU_HINH");
            builder.HasKey(setting => setting.ID);
            builder.Property(c => c.GIA_TRI).HasColumnType("CLOB");
            base.Configure(builder);
        }

        #endregion
    }
}