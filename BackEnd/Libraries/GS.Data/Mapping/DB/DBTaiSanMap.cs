//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 15/5/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.DB;

namespace GS.Data.Mapping.DB
{
    public partial class DBTaiSanMap: GSEntityTypeConfiguration<DBTaiSan>
	{
     public override void Configure(EntityTypeBuilder<DBTaiSan> builder)
        { 
            builder.ToTable("DB_TAI_SAN");
            builder.HasKey(c => c.ID);
            builder.Property(p => p.DATA_JSON).HasColumnType("CLOB");
            builder.Property(p => p.RESPONSE).HasColumnType("CLOB");
            base.Configure(builder);
        }	
	}
}
