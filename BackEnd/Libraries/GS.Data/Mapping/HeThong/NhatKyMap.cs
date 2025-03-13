//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.HeThong;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Data.Mapping.HeThong
{
    public partial class NhatKyMap : GSEntityTypeConfiguration<NhatKy>
    {
        public override void Configure(EntityTypeBuilder<NhatKy> builder)
        {
            builder.ToTable("QL_NHAT_KY");
            builder.HasKey(c => c.ID);
            //builder.Property(c => c.ID).UseOracleIdentityColumn();
            builder.Property(c => c.DU_LIEU).HasColumnType("CLOB");
            base.Configure(builder);
        }
    }
}
