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
    public partial class FileCongViecMap : GSEntityTypeConfiguration<FileCongViec>
    {
        public override void Configure(EntityTypeBuilder<FileCongViec> builder)
        {
            builder.ToTable("QL_FILE");
            builder.HasKey(c => c.ID);
            builder.Property(c => c.NOI_DUNG_FILE).HasColumnType("blob");
            base.Configure(builder);
        }
    }
}
